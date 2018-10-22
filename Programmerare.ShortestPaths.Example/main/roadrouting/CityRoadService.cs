using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Example.Roadrouting {
    public interface CityRoadService {
	    IList<City> GetAllCities();
	    IList<Road> GetAllRoads();
	    City GetStartCity();
	    City GetEndCity();
	
	    /**
	     * Database implementation should close its resources
	     */
	    void ReleaseResourcesIfAny();
    }
}