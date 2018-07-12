using System;

namespace edu.ufl.cise.bsmock.graph {
    /**
     * The WeightedEdge class implements standard properties and methods for a weighted edge in a directed graph.
     *
     * Created by Brandon Smock on 6/6/15.
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
