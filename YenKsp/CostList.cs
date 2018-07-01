using System.Collections.Generic;

namespace YenKsp
{
    // this class was added to use instead of the 
    // Python variable "cost_list" (used from method dijkstraImpl) 
    // containing tuple values with a Node and a Path
    public class CostList
    {
        private List<(Node n_cost, Path p_cost)> cost_list = new List<(Node n_cost, Path p_cost)>();

        public void append((Node, Path) p) {
            cost_list.Add(p);
        }

        public IList<(Node n_cost, Path p_cost)> GetCostList() {
            return cost_list;
        }

        public void SetCostListTuple(int idx, (Node n_cost, Path candidatePath) p) {
            cost_list[idx] = p;
        }

        public (Node n_cost, Path candidatePath) pop(int idx) {
            var item = this.cost_list[idx];
            this.cost_list.RemoveAt(idx);
            return item;
        }

        public void SortByPathCost() {
            cost_list.Sort(new NodePathTupleComparer());
        }
    }

    public class NodePathTupleComparer : IComparer<(Node n_cost, Path p_cost)> {
        public int Compare((Node n_cost, Path p_cost) x, (Node n_cost, Path p_cost) y) {
            return (int)(x.p_cost.getPathCost() - y.p_cost.getPathCost());
        }
    }
}