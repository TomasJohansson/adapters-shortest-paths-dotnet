using System.Collections.Generic;

namespace roadrouting {
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