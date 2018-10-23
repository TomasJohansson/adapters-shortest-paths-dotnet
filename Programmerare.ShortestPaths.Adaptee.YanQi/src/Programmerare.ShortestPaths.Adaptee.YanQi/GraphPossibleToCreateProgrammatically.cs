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

using edu.asu.emit.algorithm.graph; // Graph
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Adaptee.YanQi
{
    /**
     * The purpose of creating this class was to make it possible to create a Graph programmatically 
     * as an alternative to creating it from textfile data input.
     * The base class 'Graph' has a method 'importFromFile' and I did not want modify more of the existing code than needed. 
     * (for two reasons: to minimize risk of introducing bugs and to make it more likely that a future pull request will be accepted)
     *
     * When looking in that method, I could see it does three things:
     * 	(well, at least kind of, since it depends on how you count 'things' but I think the code can at least be divided into three parts as below) 
     * 1. invoke the method 'clear'
     * 2. set the number of vertices (but also some more things at the same time, i.e. not just setting an instance variable)
     * 3. add the edges from string lines with three items in each row, the two vertex names and the weight
     * 
     * The method 'clear' was already public so it could have been used without subclassing.
     * However, the method used in step 3 above is named 'addEdge' and is protected.
     * To be able to invoke it (without changing its access level to public) I created this subclass.
     * There were a few lines of code involved in step 2 above, and instead of copying those
     * rows into this subclass, I extracted those lines of code into a new protected method 'setNumberOfVertices' used below. 
     * 
     * @author Tomas Johansson
     */
    public sealed class GraphPossibleToCreateProgrammatically : Graph {
	 
	    /**
	     * There is a requirement for the input graph. 
	     * The ids of vertices must be consecutive.
	     * The previous two sentences was copied from method {@link #importFromFile(String)} in the base class
	     * which does basically the same thing as this constructor but it has dependency to input existing in a file. 
	     * 
	     * @param numberOfVertices self descriptive name but note that they must be consecutive as mentioned above. 
	     * @param linesWithEdgeNamesAndWeight a list of strings, and each such string must have three parts separated with a space, 
	     * 		the id for the start and end vertex for the edge, and the weight for the edge 
	     */
	    public GraphPossibleToCreateProgrammatically(int numberOfVertices, IList<EdgeYanQi> edges) {
		    Clear();
		    SetNumberOfVertices(numberOfVertices);
		    foreach (EdgeYanQi edge in edges) {
			    AddEdge(edge.GetStartVertexId(), edge.GetEndVertexId(), edge.GetWeight());
		    }		
	    }	
    }
}