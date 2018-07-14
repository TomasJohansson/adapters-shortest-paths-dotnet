package roadrouting;

import roadrouting.database.CityRoadServiceDatabase;

public class CityRoadServiceFactory {

	public static CityRoadService createCityRoadService(final boolean useDatabase) {
		final CityRoadService cityRoadService = new CityRoadServiceHardcoded();

		if(useDatabase) {
			// the hardcoded values sent as parameter will be used for initial populatiion of the database
			return new CityRoadServiceDatabase(cityRoadService);
		}
		else {
			return cityRoadService;	
		}
	}
}
