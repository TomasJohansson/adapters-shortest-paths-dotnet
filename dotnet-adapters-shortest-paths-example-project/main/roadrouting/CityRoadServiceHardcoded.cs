using System.Collections.Generic;

namespace roadrouting {
    /**
     * @author Tomas Johansson
     */
    public sealed class CityRoadServiceHardcoded : CityRoadService {

	    private IList<City> allCities;
	    private IList<Road> allRoads;
	    private City startCity = null;
	    private City endCity = null;
	
	    public CityRoadServiceHardcoded() {
		    SetHardcodedFieldsWithCitiesAndRoads();
	    }

	    public const string NAME_OF_START_CITY 	= "A";
	    public const string  NAME_OF_END_CITY 	= "F";

	    private void SetHardcodedFieldsWithCitiesAndRoads() {
		    startCity = CreateCity(NAME_OF_START_CITY); // "A"
		    City cityB = CreateCity("B");
		    City cityC = CreateCity("C");
		    City cityD = CreateCity("D");
		    City cityE = CreateCity("E");
		    endCity = CreateCity(NAME_OF_END_CITY); // "F"
		    allCities = new List<City>{startCity, cityB, cityC, cityD, cityE, endCity};

		    Road roadAB = CreateRoad(startCity, cityB, 100, RoadQuality.GOOD, "A to B");
		    Road roadAC = CreateRoad(startCity, cityC, 200, RoadQuality.BAD, "A to C");
		    Road roadAD = CreateRoad(startCity, cityD, 300, RoadQuality.GOOD, "A to D");
		    Road roadBC = CreateRoad(cityB, cityC, 150, RoadQuality.BAD, "B to C");
		    Road roadBD = CreateRoad(cityB, cityD, 250, RoadQuality.GOOD, "B to D");
		    Road roadBE = CreateRoad(cityB, cityE, 350, RoadQuality.BAD, "B to E");
		    Road roadCD = CreateRoad(cityC, cityD, 220, RoadQuality.GOOD, "C to D");
		    Road roadCE = CreateRoad(cityC, cityE, 340, RoadQuality.BAD, "C to E");
		    Road roadDE = CreateRoad(cityD, cityE, 130, RoadQuality.GOOD, "D to E");
		    Road roadDF = CreateRoad(cityD, endCity, 140, RoadQuality.GOOD, "D to F");
		    Road roadEF = CreateRoad(cityE, endCity, 150, RoadQuality.GOOD, "E to F");
		    allRoads = new List<Road>{roadAB, roadAC, roadAD, roadBC, roadBD, roadBE, roadCD, roadCE, roadDE, roadDF, roadEF};
	    }

	    private int cityKey = 1; // increased for each new City created 
	
	    private City CreateCity(string cityName) {
		    return new City(cityKey++, cityName);
	    }

	    private int roadKey = 1; // increased for each new Road created
	
	    private Road CreateRoad(
		    City cityFrom, 
		    City cityTo, 
		    int roadLength, 
		    RoadQuality roadQuality, 
		    string roadName
	    ) {
		    return new Road(roadKey++, cityFrom, cityTo, roadLength, roadQuality, roadName);
	    }

	    public IList<Road> GetAllRoads() {
		    return allRoads;
	    }
	
	    public IList<City> GetAllCities() {
		    return allCities;
	    }

	    public City GetStartCity() {
		    return startCity;
	    }
	    public City GetEndCity() {
		    return endCity;
	    }

	    public void ReleaseResourcesIfAny() {
		    // Nothing to do here in this class
	    }
    }
}