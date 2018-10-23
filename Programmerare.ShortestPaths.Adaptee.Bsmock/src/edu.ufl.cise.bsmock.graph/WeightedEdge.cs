/*
* The code in this project is based on the following Java project created by Brandon Smock:
* https://github.com/bsmock/k-shortest-paths/
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths/
* Tomas Johansson later translated the above Java code to C#.NET .
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.Bsmock
* 
* Regarding the latest license, Brandon Smock has released (13th of November 2017) the code with Apache License 2.0
* https://github.com/bsmock/k-shortest-paths/commit/b0af3f4a66ab5e4e741a5c9faffeb88def752882
* https://github.com/bsmock/k-shortest-paths/pull/4
* https://github.com/bsmock/k-shortest-paths/blob/master/LICENSE
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
*/

using System;

namespace edu.ufl.cise.bsmock.graph {
    /**
     * The WeightedEdge class implements standard properties and methods for a weighted edge in a directed graph.
     *
     * Created by Brandon Smock on 6/6/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class WeightedEdge { // : Comparable<WeightedEdge> {
        private String sourceLabel;
        private String targetLabel;
        private double edgeWeight = 0.0;

        public WeightedEdge(String targetLabel, double edgeWeight) {
            this.targetLabel = targetLabel;
            this.edgeWeight = edgeWeight;
        }

        public WeightedEdge(String sourceLabel, String targetLabel, double edgeWeight) {
            this.sourceLabel = sourceLabel;
            this.targetLabel = targetLabel;
            this.edgeWeight = edgeWeight;
        }

        public String GetSourceLabel() {
            return sourceLabel;
        }

        public void SetSourceLabel(String sourceLabel) {
            this.sourceLabel = sourceLabel;
        }

        public String GetTargetLabel() {
            return targetLabel;
        }

        public void SetTargetLabel(String targetLabel) {
            this.targetLabel = targetLabel;
        }

        public double GetEdgeWeight() {
            return edgeWeight;
        }

        public void SetEdgeWeight(double edgeWeight) {
            this.edgeWeight = edgeWeight;
        }

        public int CompareTo(WeightedEdge comparedObject) {
            double weight1 = this.GetEdgeWeight();
            double weight2 = comparedObject.GetEdgeWeight();

            if (weight1 == weight2)
                return 0;
            if (weight1 > weight2)
                return 1;
            return -1;
        }
    }
}
