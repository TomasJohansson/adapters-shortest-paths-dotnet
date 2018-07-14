using FluentNHibernate.Mapping;
namespace roadrouting.database {
    public class RoadMap : ClassMap<Road> {
        public RoadMap() {
            Id(x => x.RoadKey);
            Map(x => x.CityFromId);
            Map(x => x.CityToId);
            Map(x => x.RoadLength);
            Map(x => x.RoadName);
            Map(x => x.RoadQuality); // TEXT is the default table column type for an enum with SQLite
        }
    }
}