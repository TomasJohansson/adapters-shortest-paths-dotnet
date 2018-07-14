package roadrouting.database;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityTransaction;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;

/**
 * @author Tomas Johansson
 */
public abstract class BaseDataMapper<T, U> 
{
	private final EntityManager entityManager;
	private final Class<T> clazz;

	protected BaseDataMapper(final EntityManager em, final Class<T> clazz) {
		this.entityManager = em;
		this.clazz = clazz;
	}

	public void save(final List<T> entities) {
		final EntityTransaction tx = getEntityManager().getTransaction();
		tx.begin();
		for (T entity : entities) {
			getEntityManager().persist(entity);	
		}
		tx.commit();
	}
	
	public List<T> getAll() {
		final EntityManager em = getEntityManager();
		final CriteriaBuilder cb = em.getCriteriaBuilder();
		final CriteriaQuery<T> cq = cb.createQuery(clazz);
		cq.from(clazz);
		final TypedQuery<T> query = em.createQuery(cq);
		final List<T> results = query.getResultList();
		return results;
	}
	
	public T getByPrimaryKey(U primaryKey) {
		return getByPrimaryKey(primaryKey, true);
	}
	
	public T getByPrimaryKey(U primaryKey, boolean throwExceptionIfNotFound) {
		T entity = getEntityManager().find(clazz, primaryKey);
		if(entity == null && throwExceptionIfNotFound) throw new RuntimeException("Entity " + clazz + " not found for primary key" + primaryKey);
		return entity;
	}

	protected EntityManager getEntityManager() {
		return entityManager;
	}
}