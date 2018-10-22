using System.Linq;
using NHibernate;
using System;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Example.Roadrouting.Database {
    /**
     * @author Tomas Johansson
     */
    public abstract class BaseDataMapper<T, U>  {
        private readonly ISessionFactory sessionFactory;

        protected BaseDataMapper(ISessionFactory sessionFactory) {
		   this.sessionFactory = sessionFactory;
	    }

	    public void Save(IList<T> entities) {
            using (ISession session = GetSessionFactory().OpenSession()) {
                foreach (T entity in entities) {
                    session.Save(entity);
                }
            }
	    }
	
	    public virtual IList<T> GetAll() {
            using (ISession session = GetSessionFactory().OpenSession()) {
                var all = from one in session.Query<T>() select one;
                return all.ToList();
            }
	    }
	
	    public virtual T GetByPrimaryKey(U primaryKeyValue) {
            using (ISession session = GetSessionFactory().OpenSession()) {
                var persistedInstance = session.Get<T>(primaryKeyValue);
                return persistedInstance;
            }
	    }
	
	    public T GetByPrimaryKey(U primaryKeyValue, bool throwExceptionIfNotFound) {
		   T entity = GetByPrimaryKey(primaryKeyValue);
		   if(entity == null && throwExceptionIfNotFound) throw new Exception("Entity " + GetType() + " not found for primary key" + primaryKeyValue);
		   return entity;
	    }

	    protected ISessionFactory GetSessionFactory() {
            return sessionFactory;
	    }
    }
}