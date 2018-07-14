using com.programmerare.shortestpaths.core.api;
namespace roadrouting {
    /**
     * @author Tomas Johansson
     */
    public class City : Vertex {

	    public virtual int CityKey { get; protected set; }
        public virtual string CityName { get; protected set; }

        protected City()  {	}

        public City(int cityKey, string cityName) {
		    CityKey = cityKey;
		    CityName = cityName;
	    }

	    public override string ToString() {
		    return "City [cityKey=" + CityKey + ", cityName=" + CityName + "]";
	    }

	    // ------------------------------------------------------------ 
	    /**
	     *  The property from the interface com.programmerare.shortestpaths.core.api.Vertex
	     */
        public virtual string VertexId => CityKey.ToString();
	    // ------------------------------------------------------------

        // Method from interface StringRenderable (subinterface to Vertex)
        public virtual string RenderToString() {
            return ToString();
        }
    } 
}