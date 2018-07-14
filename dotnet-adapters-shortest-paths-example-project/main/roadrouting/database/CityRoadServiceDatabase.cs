using NHibernate;
using System;
using System.Collections.Generic;
using static roadrouting.CityRoadServiceHardcoded; // constants NAME_OF_START_CITY and NAME_OF_END_CITY

namespace roadrouting.database {
    /**
     * @author Tomas Johansson
     */
    public sealed class CityRoadServiceDatabase : CityRoadService {

        private IList<City> allCities;
        private IList<Road> allRoads;
        private City startCity;
        private City endCity;
      
        private readonly CityDataMapper cityDataMapper;
        private readonly RoadDataMapper roadDataMapper;
        private readonly CityRoadService cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty;
        
        private readonly ISessionFactory sessionFactory;

        public CityRoadServiceDatabase(
            CityRoadService cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty
        ) {
            sessionFactory = PersistenceSessionFactory.CreateSessionFactory();
            cityDataMapper = new CityDataMapper(sessionFactory);
            roadDataMapper = new RoadDataMapper(sessionFactory);
            this.cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty = cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty;
        }

        public City GetStartCity() {
            SetFieldsIfNeeded();
            return startCity;
        }
        
        public City GetEndCity() {
            SetFieldsIfNeeded();
            return endCity;
        }

        public IList<City> GetAllCities() {
            SetFieldsIfNeeded();
            return allCities;
        }

        public IList<Road> GetAllRoads() {
            SetFieldsIfNeeded();
            return allRoads;
        }

        private void SetFieldsIfNeeded() {
            if (!IsAllFieldsLoadedFromDatabase()) { // at first invocation for one of the four getters 
                PopulateDatabaseWithHardcodedCityAndRoads();
                ThrowExceptionIfCouldNotBeRetrievedFromDatabase();
            }
        }

        private bool isAllFieldsLoadedFromDatabase = false;  // lazy loading field

        // TODO: refactor this method to instead using Command-Query-Separation
        private bool IsAllFieldsLoadedFromDatabase() {
            if (isAllFieldsLoadedFromDatabase) return true; // lazy loading field

            // try to query all needed fields from the database
            LoadFieldsFromDatabase();

            // if everything above worked then that is good,
            // but if nothing works, it is also a reasonable expected/normal scenario
            // when not yet populated, but if some failed and some succeeded then we have an unknown problem
            // so then throw an exception
            int successCount = 0;
            if (allCities != null && allCities.Count > 0) successCount++;
            if (allRoads != null && allRoads.Count > 0) successCount++;
            if (startCity != null) successCount++;
            if (endCity != null) successCount++;

            int numberOfQueries = 4;

            if (successCount == numberOfQueries) {
                return true;
            }
            else if (successCount == 0) {
                isAllFieldsLoadedFromDatabase = true;
                return false;
            } else {
                throw new Exception("Only " + successCount + " of " + numberOfQueries + " database queries succeeded");
            }
        }

        private void PopulateDatabaseWithHardcodedCityAndRoads() {
            IList<City> citiesNotYetPersisted = this.cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty.GetAllCities();
            IList<Road> roadsNotYetPersisted = this.cityRoadServiceProvidingDataForPopulatingDatabaseIfEmpty.GetAllRoads();

            PersistCitiesToDatabase(citiesNotYetPersisted);
            PersistRoadsToDatabase(roadsNotYetPersisted);

            LoadFieldsFromDatabase();
        }

        private void LoadFieldsFromDatabase() {
            allCities = cityDataMapper.GetAll();
            allRoads = roadDataMapper.GetAll();
            startCity = cityDataMapper.GetByCityName(NAME_OF_START_CITY);
            endCity = cityDataMapper.GetByCityName(NAME_OF_END_CITY);
        }

        private void PersistCitiesToDatabase(IList<City> cities) {
            cityDataMapper.Save(cities);
        }

        private void PersistRoadsToDatabase(IList<Road> roads) {
            roadDataMapper.Save(roads);
        }

        private void ThrowExceptionIfCouldNotBeRetrievedFromDatabase(
            string messageSuffixForException,
            bool shouldNowThrowException
        ) {
            if (shouldNowThrowException) {
                throw new Exception("Problem with loading data from database: " + messageSuffixForException);
            }
        }

        private void ThrowExceptionIfCouldNotBeRetrievedFromDatabase() {
            ThrowExceptionIfCouldNotBeRetrievedFromDatabase("all Cities", allCities == null || allCities.Count == 0);
            ThrowExceptionIfCouldNotBeRetrievedFromDatabase("all Roads", allRoads == null || allRoads.Count == 0);
            ThrowExceptionIfCouldNotBeRetrievedFromDatabase("start city with name " + NAME_OF_START_CITY, startCity == null);
            ThrowExceptionIfCouldNotBeRetrievedFromDatabase("end city with name " + NAME_OF_END_CITY, endCity == null);
        }

	    ~CityRoadServiceDatabase() {
		    ReleaseResourcesIfAny();
	    }

        public void ReleaseResourcesIfAny() {
            CloseEntityManagerAndEntityManagerFactoryIfStillOpen();
        }

        private void CloseEntityManagerAndEntityManagerFactoryIfStillOpen() {
            if (!sessionFactory.IsClosed) {
                sessionFactory.Close();
            }
        }
    }
}