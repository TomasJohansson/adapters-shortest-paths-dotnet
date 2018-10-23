/*	 
* The code in this project is based on the following Java project created by Yan Qi (https://github.com/yan-qi) :
* https://github.com/yan-qi/k-shortest-paths-java-version
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths-java-version
* Tomas Johansson later translated the above Java code to C#.NET
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.YanQi
* 
* Regarding the latest license, Yan Qi has released (19th of Januari 2017) the code with Apache License 2.0
* https://github.com/yan-qi/k-shortest-paths-java-version/commit/d028fd873ff34efc1e851421be380d2382dcb104
* https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/License.txt
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
* 
*/

namespace com.programmerare.edu.asu.emit.algorithm.graph {
    /**
     * The purpose of the class suffix "YanQi" is just to very clearly indicate (without having to look at the full name including the package name) 
     * that it originates from the project https://github.com/yan-qi/k-shortest-paths-java-version
     * The class is intended to be used among other "Edge" types from the 
     * project e.g. the interface "Edge" in the project currently named "Programmerare.ShortestPaths.Core"
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