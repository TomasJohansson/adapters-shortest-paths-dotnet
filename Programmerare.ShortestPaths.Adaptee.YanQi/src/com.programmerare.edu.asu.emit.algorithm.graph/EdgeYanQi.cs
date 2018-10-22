/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace com.programmerare.edu.asu.emit.algorithm.graph {
    /**
     * The purpose of the class suffix "YanQi" is just to very clearly indicate (without having to look at the full name including the package name) 
     * that it originates from the project https://github.com/yan-qi/k-shortest-paths-java-version
     * The class is intended to be used among other "Edge" interfaces/types from the project https://github.com/TomasJohansson/adapters-shortest-paths/tree/master/adapters-shortest-paths-impl-yanqi  
     * @author Tomas Johansson
     */
    public sealed class EdgeYanQi {
	    private readonly int startVertexId;
	    private readonly int endVertexId;
	    private readonly double weight;

	    public EdgeYanQi(int startVertexId, int endVertexId, double weight) {
		    this.startVertexId = startVertexId;
		    this.endVertexId = endVertexId;
		    this.weight = weight;
	    }

	    public int GetStartVertexId() {
		    return startVertexId;
	    }

	    public int GetEndVertexId() {
		    return endVertexId;
	    }

	    public double GetWeight() {
		    return weight;
	    }
    }
}