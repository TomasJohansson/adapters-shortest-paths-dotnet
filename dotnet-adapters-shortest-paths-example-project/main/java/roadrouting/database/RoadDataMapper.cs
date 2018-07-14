package roadrouting.database;

import java.util.List;

import javax.persistence.EntityManager;

import roadrouting.City;
import roadrouting.Road;

/**
 * @author Tomas Johansson
 */
public final class RoadDataMapper extends BaseDataMapper<Road, Integer> {
	
	private final CityDataMapper cityDataMapper; //  TODO: real ORM (Object-Relational Mapping) without usage of this class from here  
	
	public RoadDataMapper(final EntityManager em) {
		super(em, Road.class);
		cityDataMapper = new CityDataMapper(em);
	}

	public List<Road> getAll() {
		final List<Road> roads= super.getAll();
		for (Road road : roads) {
			populateWithCities(road); //  TODO: real ORM instead
		}
		return roads;
	}
	
	public Road getByPrimaryKey(final int primaryKey) {
		final Road road = super.getByPrimaryKey(primaryKey);
		populateWithCities(road); //  TODO: real ORM instead
		return road;
	}

	//  TODO: real ORM instead of this method
	private void populateWithCities(Road road) {
		final City cityFrom = cityDataMapper.getByPrimaryKey(road.getCityFromId());
		final City cityTo = cityDataMapper.getByPrimaryKey(road.getCityToId());
		road.setCityFrom(cityFrom);
		road.setCityTo(cityTo);
	}	
}