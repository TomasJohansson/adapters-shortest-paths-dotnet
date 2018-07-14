package roadrouting;

import java.util.Arrays;
import java.util.List;

/**
 * @author Tomas Johansson
 */
public final class CityRoadServiceHardcoded implements CityRoadService {

	private List<City> allCities;
	private List<Road> allRoads;
	private City startCity = null;
	private City endCity = null;
	
	public CityRoadServiceHardcoded() {
		setHardcodedFieldsWithCitiesAndRoads();
	}

	private void setHardcodedFieldsWithCitiesAndRoads() {
		startCity = createCity(NAME_OF_START_CITY); // "A"
		final City cityB = createCity("B");
		final City cityC = createCity("C");
		final City cityD = createCity("D");
		final City cityE = createCity("E");
		endCity = createCity(NAME_OF_END_CITY); // ("F"
		
		allCities = Arrays.asList(startCity, cityB, cityC, cityD, cityE, endCity);

		final Road roadAB = createRoad(startCity, cityB, 100, RoadQuality.GOOD, "A to B");
		final Road roadAC = createRoad(startCity, cityC, 200, RoadQuality.BAD, "A to C");
		final Road roadAD = createRoad(startCity, cityD, 300, RoadQuality.GOOD, "A to D");
		final Road roadBC = createRoad(cityB, cityC, 150, RoadQuality.BAD, "B to C");
		final Road roadBD = createRoad(cityB, cityD, 250, RoadQuality.GOOD, "B to D");
		final Road roadBE = createRoad(cityB, cityE, 350, RoadQuality.BAD, "B to E");
		final Road roadCD = createRoad(cityC, cityD, 220, RoadQuality.GOOD, "C to D");
		final Road roadCE = createRoad(cityC, cityE, 340, RoadQuality.BAD, "C to E");
		final Road roadDE = createRoad(cityD, cityE, 130, RoadQuality.GOOD, "D to E");
		final Road roadDF = createRoad(cityD, endCity, 140, RoadQuality.GOOD, "D to F");			
		final Road roadEF = createRoad(cityE, endCity, 150, RoadQuality.GOOD, "E to F");
		allRoads = Arrays.asList(roadAB, roadAC, roadAD, roadBC, roadBD, roadBE, roadCD, roadCE, roadDE, roadDF, roadEF);
		
	}

	private int cityKey = 1; // increased for each new City created 
	
	private City createCity(final String cityName) {
		return new City(cityKey++, cityName);
	}

	private int roadKey = 1; // increased for each new Road created
	
	private Road createRoad(
		final City cityFrom, 
		final City cityTo, 
		final int roadLength, 
		final RoadQuality roadQuality, 
		final String roadName
	) {
		return new Road(roadKey++, cityFrom, cityTo, roadLength, roadQuality, roadName);
	}

	public List<Road> getAllRoads() {
		return allRoads;
	}
	
	public List<City> getAllCities() {
		return allCities;
	}

	public City getStartCity() {
		return startCity;
	}
	public City getEndCity() {
		return endCity;
	}

	public void releaseResourcesIfAny() {
		// Nothing to do here in this class
	}
}