using FluentNHibernate.Mapping;
namespace roadrouting.database {
    public class CityMap : ClassMap<City> {
        public CityMap() {
            Id(x => x.CityKey);
            Map(x => x.CityName);
        }
    }
}