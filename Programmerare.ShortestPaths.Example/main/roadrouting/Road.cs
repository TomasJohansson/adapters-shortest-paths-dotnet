using Programmerare.ShortestPaths.Core.Api.Generics;

namespace Programmerare.ShortestPaths.Example.Roadrouting {
    /**
     * 
     * @author Tomas Johansson
     *
     */
    public class Road : EdgeGenerics<City , WeightDeterminedByRoadLengthAndQuality> {
	
        public virtual int RoadKey { get; set; }
        public virtual int CityFromId { get; set; }
        public virtual int CityToId { get; set; }
        public virtual int RoadLength { get; set; }
        public virtual string RoadName{ get; set; }
        public virtual RoadQuality RoadQuality { get; set; }

        // Java comment below (TODO: adjust to C# after having translated)
	    // Currently simple database mapping is used, and therefore three fields below are marked as Transient.
	    // They are managed with explicit code, i.e. cyurrently not using real ORM (Object-Relational Mapping)..
	    // The two City fields are persisted entities and must be populated in the class RoadDataMapper while the Weight field
	    // is not  a persisted entity and it can be instantiated through its get method. 
	
	    // Three "transient" (not persisted) fields:
	    private City cityFrom;
	    private City cityTo;
	    private WeightDeterminedByRoadLengthAndQuality edgeWeight;

        protected Road()  {	}

        public Road(
		    int roadKey, 
		    City cityFrom, 
		    City cityTo, 
		    int roadLength, 
		    RoadQuality roadQuality, 
		    string roadName
	    ) {
		    RoadKey = roadKey;
		    SetCityFrom(cityFrom);
		    SetCityTo(cityTo);
		    RoadLength = roadLength;
		    RoadQuality = roadQuality;		
		    RoadName = roadName;
	    }

	    public virtual void SetCityFrom(City cityFrom) {
		    this.cityFrom = cityFrom;
		    CityFromId = cityFrom.CityKey;
	    }

	    public virtual void SetCityTo(City cityTo) {
		    this.cityTo = cityTo;
		    CityToId = cityTo.CityKey;
	    }		

        public virtual WeightDeterminedByRoadLengthAndQuality EdgeWeight { 
            get {
                if (edgeWeight == null) { // lazy loading, using the twop persisted fields
                    edgeWeight = new WeightDeterminedByRoadLengthAndQuality(RoadLength, RoadQuality);
                }
                return edgeWeight;
        }}

	    public virtual City GetCityFrom() {
		    return cityFrom;
	    }

	    public virtual City GetCityTo() {
		    return cityTo;
	    }
	
	    public override string ToString() {
		    return "Road [roadKey=" + RoadKey + ", cityFromId=" + CityFromId + ", cityToId=" + CityToId + ", roadLength="
				    + RoadLength + ", roadQuality=" + RoadQuality + ", roadName=" + RoadName + ", cityFrom=" + cityFrom
				    + ", cityTo=" + cityTo + ", edgeWeight=" + edgeWeight + "]";
	    }

	    // ------------------------------------------------------------ 
	    //  Below are the methods from the interface com.programmerare.shortestpaths.adapter.Edge
        public virtual string EdgeId => GetCityFrom().VertexId + "_" + GetCityTo().VertexId;

        public virtual City StartVertex => GetCityFrom();

        public virtual City EndVertex => GetCityTo();

	    public virtual WeightDeterminedByRoadLengthAndQuality GetEdgeWeight() {
		    if(edgeWeight == null) { // lazy loading, using the twop persisted fields
			    edgeWeight = new WeightDeterminedByRoadLengthAndQuality(RoadLength, RoadQuality);
		    }
		    return edgeWeight;
	    }
	
	    // ------------------------------------------------------------
        public virtual string RenderToString() {
            return ToString();
        }
    }
}