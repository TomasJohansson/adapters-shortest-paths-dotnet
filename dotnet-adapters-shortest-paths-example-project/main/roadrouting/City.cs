package roadrouting;
 
import javax.persistence.Entity;
import javax.persistence.Id;

import com.programmerare.shortestpaths.core.api.Vertex;

/**
 * @author Tomas Johansson
 */
@Entity
public class City implements Vertex {

	@Id
	private int cityKey;

	private String cityName;
	
	protected City() { } // Requirements for Entity Classes: The class must have a public or protected, no-argument constructor. The class may have other constructors. , https://docs.oracle.com/javaee/6/tutorial/doc/bnbqa.html
	
	public City(final int cityKey, final String cityName) {
		this.cityKey = cityKey;
		this.cityName = cityName;
	}

	public int getCityKey() {  
		return cityKey;
	}	
	
	public String getCityName() {
		return cityName;
	}

	@Override
	public String toString() {
		return "City [cityKey=" + cityKey + ", cityName=" + cityName + "]";
	}


	// ------------------------------------------------------------ 
	/**
	 *  The method from the interface com.programmerare.shortestpaths.adapter.Vertex
	 */
	public String getVertexId() {
		return Integer.toString(cityKey);
	}
	// ------------------------------------------------------------

	public String renderToString() {
		return toString();
	}
} 