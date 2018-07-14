package roadrouting;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Transient;

import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;

/**
 * 
 * @author Tomas Johansson
 *
 */
@Entity
public class Road implements EdgeGenerics<City , WeightDeterminedByRoadLengthAndQuality> {
	
	@Id
	private int roadKey;
	
	private int cityFromId;
	private int cityToId;
	
	private int roadLength;
	private RoadQuality roadQuality;
	
	private String roadName;

	// Currently simple database mapping is used, and therefore three fields below are marked as Transient.
	// They are managed with explicit code, i.e. cyurrently not using real ORM (Object-Relational Mapping)..
	// The two City fields are persisted entities and must be populated in the class RoadDataMapper while the Weight field
	// is not  a persisted entity and it can be instantiated through its get method. 
	
	@Transient
	private City cityFrom;
	
	@Transient
	private City cityTo;
	
	@Transient
	private WeightDeterminedByRoadLengthAndQuality edgeWeight;


	protected Road()  {	}
	
	public Road(
		final int roadKey, 
		final City cityFrom, 
		final City cityTo, 
		final int roadLength, 
		final RoadQuality roadQuality, 
		final String roadName
	) {
		this.roadKey = roadKey;
		setCityFrom(cityFrom);
		setCityTo(cityTo);
		this.roadLength = roadLength;
		this.roadQuality = roadQuality;		
		this.roadName = roadName;
	}

	public int getRoadKey() {
		return roadKey;
	}
	
	public int getRoadLength() {
		return roadLength;
	}
	
	public RoadQuality getRoadQuality() {
		return roadQuality;
	}

	public String getRoadName() {
		return roadName;
	}

	
	public void setCityFrom(City cityFrom) {
		this.cityFrom = cityFrom;
		this.cityFromId = cityFrom.getCityKey();
	}
	public void setCityTo(City cityTo) {
		this.cityTo = cityTo;
		this.cityToId = cityTo.getCityKey();		
	}		

	public City getCityFrom() {
		return cityFrom;
	}

	public City getCityTo() {
		return cityTo;
	}
	
	public int getCityFromId() {
		return cityFromId;
	}

	public int getCityToId() {
		return cityToId;
	}

	@Override
	public String toString() {
		return "Road [roadKey=" + roadKey + ", cityFromId=" + cityFromId + ", cityToId=" + cityToId + ", roadLength="
				+ roadLength + ", roadQuality=" + roadQuality + ", roadName=" + roadName + ", cityFrom=" + cityFrom
				+ ", cityTo=" + cityTo + ", edgeWeight=" + edgeWeight + "]";
	}

	// ------------------------------------------------------------ 
	//  Below are the methods from the interface com.programmerare.shortestpaths.adapter.Edge
	public String getEdgeId() { // important as documented in the interface for Edge
		return getCityFrom().getVertexId() + "_" + getCityTo().getVertexId();
	}

	public City getStartVertex() {
		return getCityFrom();
	}

	public City getEndVertex() {
		return getCityTo();
	}	

	public WeightDeterminedByRoadLengthAndQuality getEdgeWeight() {
		if(edgeWeight == null) { // lazy loading, using the twop persisted fields
			edgeWeight = new WeightDeterminedByRoadLengthAndQuality(roadLength, roadQuality);
		}
		return edgeWeight;
	}
	
	// ------------------------------------------------------------
	
	public String renderToString() {
		return toString();
	}
	
	
	
}