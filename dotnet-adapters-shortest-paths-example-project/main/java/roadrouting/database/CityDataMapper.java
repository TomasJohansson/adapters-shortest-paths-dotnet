package roadrouting.database;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

import roadrouting.City;
import roadrouting.City_;

/**
 * @author Tomas Johansson
 */
public final class CityDataMapper extends BaseDataMapper<City, Integer>  {

	public CityDataMapper(final EntityManager em) {
		super(em, City.class);
	}
	
	public City getByCityName(final String cityName) {
		final EntityManager em = getEntityManager();
		final CriteriaBuilder cb = em.getCriteriaBuilder();
		final CriteriaQuery<City> cq = cb.createQuery(City.class);
		
		final Root<City> root = cq.from(City.class);
		
		// Note that the below used class "City_ is code generated and part of the JPA meta model
		// You do not always want to add genereated source files to the source control system 
		// but in this case the below "City_.java" (and "Road_.java") are added to git 
		// since otherwise you might get a compiling error because it is missing which  
		// would be unnecessary since the goal here is not to teach JPA configuration but rather 
		// to provide an executable example using JPA.
		// The two generated files should be located at "/adapters-shortest-paths-example-project-jpa-entities/src/java-generated/roadrouting/"
		// according to the configuration in the pom file  ( <outputDirectoryForGeneratedJpaMetamodel>${project.basedir}/src/java-generated</outputDirectoryForGeneratedJpaMetamodel> )
		cq.where(cb.like(root.get(City_.cityName), cityName));
		
		final List<City> resultList = em.createQuery(cq).getResultList();
		if(resultList.size() == 0) {
			return null;
		}
		if(resultList.size() > 1) {
			throw new RuntimeException("There should not be more than one city with the same name " + cityName);
		}		
		return resultList.get(0); 
	}	
}