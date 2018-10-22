using NHibernate;
using Programmerare.ShortestPaths.Example.Roadrouting;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Example.Roadrouting.Database {
    /**
     * @author Tomas Johansson
     */
    public sealed class RoadDataMapper : BaseDataMapper<Road, int> {
	
	    private readonly CityDataMapper cityDataMapper; //  TODO: real ORM (Object-Relational Mapping) without usage of this class from here  
	
	    public RoadDataMapper(ISessionFactory sf): base(sf) {
		    cityDataMapper = new CityDataMapper(sf);
	    }

	    public override IList<Road> GetAll() {
		    IList<Road> roads= base.GetAll();
		    foreach (Road road in roads) {
			    PopulateWithCities(road); //  TODO: real ORM instead
		    }
		    return roads;
	    }
	
	    public override Road GetByPrimaryKey(int primaryKey) {
		    Road road = base.GetByPrimaryKey(primaryKey);
		    PopulateWithCities(road); //  TODO: real ORM instead
		    return road;
	    }

	    //  TODO: real ORM instead of this method
	    private void PopulateWithCities(Road road) {
		    City cityFrom = cityDataMapper.GetByPrimaryKey(road.CityFromId);
		    City cityTo = cityDataMapper.GetByPrimaryKey(road.CityToId);
		    road.SetCityFrom(cityFrom);
		    road.SetCityTo(cityTo);
	    }	
    }
}