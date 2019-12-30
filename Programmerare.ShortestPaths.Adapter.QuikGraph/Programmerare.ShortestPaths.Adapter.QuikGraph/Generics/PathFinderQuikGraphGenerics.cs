/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using QuikGraph;
using QuikGraph.Algorithms.ShortestPath;

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Impl.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;
using Programmerare.ShortestPaths.Core.Api.Generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Programmerare.ShortestPaths.Adapter.QuikGraph.Generics
{
    /// <summary>
    /// "Adapter" implementation of the "Target" interface 
    /// https://en.wikipedia.org/wiki/Adapter_pattern
    /// </summary>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public class PathFinderQuikGraphGenerics<P, E, V, W>
        : PathFinderBase<P, E, V, W>
        , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
        private readonly AdjacencyGraph<string, EquatableTaggedEdge<string, double>> graphAdaptee = new AdjacencyGraph<string, EquatableTaggedEdge<string, double>>(false);

        public PathFinderQuikGraphGenerics(
            GraphGenerics<E, V, W> graph
        ) : this(
                graph,
                null
            )
        {
        }
        protected PathFinderQuikGraphGenerics(
            GraphGenerics<E, V, W> graph,
            PathFactory<P, E, V, W> pathFactory
        ) : base(graph, pathFactory)
        {
            PopulateGraphAdapteeWithEdges();
        }

        private void PopulateGraphAdapteeWithEdges()
        {
            IList<E> edges = this.GetGraph().Edges;
            foreach (E edge in edges)
            {
                this.graphAdaptee.AddVerticesAndEdge(
                    new EquatableTaggedEdge<string, double>(edge.StartVertex.VertexId, edge.EndVertex.VertexId, edge.EdgeWeight.WeightValue)
                );
            }
        }

        protected override IList<P> FindShortestPathHook(
            V startVertex,
            V endVertex,
            int maxNumberOfPaths
        )
        {
            var yenAdaptee = new YenShortestPathsAlgorithm<string>(graphAdaptee, startVertex.VertexId, endVertex.VertexId, maxNumberOfPaths);
            IList<P> paths = new System.Collections.Generic.List<P>();
            IEnumerable<YenShortestPathsAlgorithm<string>.SortedPath> adapteePaths = yenAdaptee.Execute();
            foreach (YenShortestPathsAlgorithm<string>.SortedPath adapteePath in adapteePaths)
            {
                IEnumerable<EquatableTaggedEdge<string, double>> adapteeEdges = adapteePath.AsEnumerable();
                IList<E> edges = new System.Collections.Generic.List<E>();
                double wei = 0;
                foreach (EquatableTaggedEdge<string, double> adapteeEdge in adapteeEdges)
                {
                    wei += adapteeEdge.Tag;
                    E edge = this.GetOriginalEdgeInstance(adapteeEdge);
                    edges.Add(
                        edge
                    );
                }
                W totalWeight = base.CreateInstanceWithTotalWeight(wei, edges);
                paths.Add(base.CreatePath(totalWeight, edges));
            }
            return new ReadOnlyCollection<P>(paths);
        }

        private E GetOriginalEdgeInstance(EquatableTaggedEdge<string, double> edgeAdaptee)
        {
            return base.GetOriginalEdgeInstance(edgeAdaptee.Source, edgeAdaptee.Target);
        }
    }
}