using System.Linq;
using NHibernate;

namespace roadrouting.database {
    /**
     * @author Tomas Johansson
     */
    public sealed class CityDataMapper : BaseDataMapper<City, int>  {

        public CityDataMapper(ISessionFactory sf) : base(sf) {}
	
	    public City GetByCityName(string cityName) {
            using (ISession session = GetSessionFactory().OpenSession()) {
                var result =
                    from q in session.Query<City>()
                    where q.CityName == cityName
                    //orderby q.CityName ascending
                    select q;
                return result.FirstOrDefault();
            }
	    }	
    }
}