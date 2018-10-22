using FluentNHibernate.Mapping;
namespace Programmerare.ShortestPaths.Example.Roadrouting.Database {
    public class CityMap : ClassMap<City> {
        public CityMap() {
            Id(x => x.CityKey);
            Map(x => x.CityName);
        }
    }
}