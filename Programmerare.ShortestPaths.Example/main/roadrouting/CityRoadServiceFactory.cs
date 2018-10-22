using Programmerare.ShortestPaths.Example.Roadrouting.Database;

namespace Programmerare.ShortestPaths.Example.Roadrouting {
    public sealed class CityRoadServiceFactory {

	    public static CityRoadService CreateCityRoadService(bool useDatabase) {
		    CityRoadService cityRoadService = new CityRoadServiceHardcoded();

		    if(useDatabase) {
			    // the hardcoded values sent as parameter will be used for initial populatiion of the database
			    return new CityRoadServiceDatabase(cityRoadService);
		    }
		    else {
			    return cityRoadService;	
		    }
	    }
    }
}