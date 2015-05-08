using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Edge = Orbifold.Numerics.Edge<object, object>;
using Graph = Orbifold.Numerics.Graph<Orbifold.Numerics.Node<object, object>, Orbifold.Numerics.Edge<object, object>>;
using Node = Orbifold.Numerics.Node<object, object>;
namespace 
    Orbifold.Numerics
{

    /// <summary>
    /// The static graph-analysis related extensions.
    /// </summary>
    public static class GraphExtensions
    {
        /// <summary>
        /// The rand.
        /// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

        /// <summary>
        /// Performs a breadth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="g">
        /// The graph to traverse.
        /// </param>
        /// <param name="visitor">
        /// The visitor traversing the graph.
        /// </param>
        /// <param name="startNode">
        /// The start node.
        /// </param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Breadth-first_search.
        /// </remarks>
        public static void BreadthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IVisitor<TNode> visitor, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            if (startNode == null) throw new ArgumentNullException("startNode");

            var queue = new Queue<TNode>();
            var visited = new Dictionary<TNode, bool>();
            g.Nodes.ForEach(n => visited.Add(n, false));
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                if (visitor.HasCompleted) break;
                var node = queue.Dequeue();
                visitor.Visit(node);
                visited[node] = true;
                var children = node.Children;
                foreach (var child in children.Where(child => child != null && !visited[child] && !queue.Contains(child)))
                {
                    queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// Enforces a correct edge direction on the given tree-graph so that children of a node are the sink of the edge connecting the parent to the child. If the given graph is not a a tree an exception
        /// will be thrown.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="root">The root of the flow. If none specified an arbitrary node will be taken.</param>
        /// <returns></returns>
        public static Graph<TNode, TLink> TreeFlow<TNode, TLink>(this Graph<TNode, TLink> graph, TNode root = null)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (graph.Nodes.Count <= 2) return graph;
            if (!graph.IsAcyclic) throw new Exception("The given graph contains cycles and cannot be forced into a tree flow.");
            var rootId = 0;
            if (root != null)
            {
                if (!graph.Nodes.Contains(root)) throw new Exception("The given root does not belong to the graph.");
                rootId = root.Id;
            }
            else
            {
                root = graph.Nodes[0];
                rootId = graph.Nodes[0].Id;
            }

            var result = new Graph<TNode, TLink> { IsDirected = true };

            // make a shallow copy of the nodes
            var map = new Dictionary<TNode, TNode>();
            foreach (TNode node in graph.Nodes)
            {
                var copy = new TNode { Id = node.Id };
                map.Add(node, copy);
                result.AddNode(copy);
            }

            //var rootcopy = result.FindNode(rootId);
            var visitor = new ParentActionVisitor<TNode>((node, parent) => { if (parent != null) result.AddEdge(map[parent], map[node]); });
            graph.DepthFirstTraversal(visitor, root);
            return result;
        }

        /// <summary>
        /// Performs a breadth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="g">
        /// The graph to traverse.
        /// </param>
        /// <param name="action">
        /// The action acting a the visited node.
        /// </param>
        /// <param name="startNode">
        /// The start node.
        /// </param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Breadth-first_search.
        /// </remarks>
        public static void BreadthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, Action<TNode> action, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (action == null) throw new ArgumentNullException("action");
            if (startNode == null) throw new ArgumentNullException("startNode");

            var visitor = new ActionVisitor<TNode>(action);
            BreadthFirstTraversal(g, visitor, startNode);
        }

        /// <summary>
        /// Returns a shallow clone from the given collection.
        /// </summary>
        /// <param name="list">
        /// The collection to clone.
        /// </param>
        /// <returns>
        /// </returns>
        public static IList<object> Clone(this IEnumerable<object> list)
        {
            var clone = new List<object>();
            clone.AddRange(list);
            return clone;
        }

        public static IList<T> Clone<T>(this IEnumerable<T> list)
        {
            var clone = new List<T>();
            clone.AddRange(list);
            return clone;
        }

        /// <summary>
        /// Returns a shallow clone from the given collection.
        /// </summary>
        /// <typeparam name="TNodeData">
        /// The node data type.
        /// </typeparam>
        /// <typeparam name="TLinkData">
        /// The edge data type.
        /// </typeparam>
        /// <param name="list">
        /// The collection to clone.
        /// </param>
        /// <returns>
        /// </returns>
        public static IList<Edge<TNodeData, TLinkData>> Clone<TNodeData, TLinkData>(
            this IEnumerable<Edge<TNodeData, TLinkData>> list)
            where TNodeData : new()
            where TLinkData : new()
        {
            var clone = new List<Edge<TNodeData, TLinkData>>();
            clone.AddRange(list);
            return clone;
        }

        /// <summary>
        /// Returns an array of the specified size.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="value">
        /// The Graph value of the elements in the array.
        /// </param>
        /// <returns>
        /// </returns>
        public static int[] CreateArray(int size, int value)
        {
            var array = new int[size];
            for (var i = 0; i < size; ++i)
            {
                array[i] = value;
            }

            return array;
        }

        /// <summary>
        /// Returns an array of the specified size.
        /// </summary>
        /// <param name="dim1">
        /// The first dimension.
        /// </param>
        /// <param name="dim2">
        /// The second dimension.
        /// </param>
        /// <param name="value">
        /// The Graph value of the elements.
        /// </param>
        /// <returns>
        /// </returns>
        public static int[,] CreateArray(int dim1, int dim2, int value)
        {
            var array = new int[dim1, dim2];
            for (var i = 0; i < dim1; ++i)
            {
                for (var j = 0; j < dim2; ++j)
                {
                    array[i, j] = value;
                }
            }

            return array;
        }

        /// <summary>
        /// Creates a bidictionary with keys equal to the (supposedly unique) identifiers and value equal to the provided initial value.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// </returns>
        public static Dictionary<Tuple<TNode, TNode>, int> CreateBiDictionary<TNode, TLink>(
            this Graph<TNode, TLink> graph, int value)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            var dic = new Dictionary<Tuple<TNode, TNode>, int>();
            foreach (var m in graph.Nodes)
            {
                foreach (var n in graph.Nodes)
                {
                    dic.Add(Tuple.Create(m, n), value);
                }
            }

            return dic;
        }

        /// <summary>
        /// Creates a random graph with a specified amounts of components.
        /// </summary>
        /// <param name="numberOfComponent">
        /// The number of component.
        /// </param>
        /// <returns>
        /// </returns>
        public static Graph CreateComponents(int numberOfComponent)
        {
            var graph = new Graph();
            if (numberOfComponent <= 0)
            {
                return graph;
            }

            for (var i = 0; i < numberOfComponent; i++)
            {
                var component = CreateRandomConnectedGraph(Rand.Next(2, 20));

                // renumber vertices otherwise graphs will overlap in the merge
                component.RenumberNodes(graph.Nodes.Count);

                // Debug.WriteLine("New component: " + component.ToLinkListString());
                graph = graph.Merge(component);
            }

            return graph;
        }

        /// <summary>
        /// Creates a dictionary with keys equal to the (supposedly unique) identifiers and value equal to the provided initial value.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// </returns>
        public static Dictionary<int, int> CreateDictionary<TNode, TLink>(this Graph<TNode, TLink> graph, int value)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            return graph.Nodes.ToDictionary(node => node.Id, node => value);
        }

        /// <summary>
        /// Creates a random connected graph.
        /// </summary>
        /// <param name="nodesCount">The nodes count.</param>
        /// <param name="maxIncidence">The max incidence.</param>
        /// <param name="tree">If set to <c>true</c> the random graph will be effectively a tree.</param>
        /// <returns></returns>
        public static Graph CreateRandomConnectedGraph(int nodesCount, int maxIncidence = 4, bool tree = false)
        {
            var graph = new Graph();
            if (nodesCount <= 0) return graph;
            graph.AddNode(new Node(0, true));
            if (nodesCount >= 1)
            {
                // this creates a random tree
                for (var i = 1; i < nodesCount; i++)
                {
                    var poolNode = graph.TakeRandomNode(null, maxIncidence);

                    // failed to find one so the graph will have less nodes than specified
                    if (poolNode == null) break;
                    var newNode = new Node(i, true);
                    graph.AddNode(newNode);
                    graph.AddEdge(newNode, poolNode);
                }

                if (!tree && nodesCount > 1)
                {
                    var randomAdditions = Rand.Next(1, nodesCount);
                    for (var i = 0; i < randomAdditions; i++)
                    {
                        var n1 = graph.TakeRandomNode(null, maxIncidence);
                        var n2 = graph.TakeRandomNode(null, maxIncidence);
                        if (n1 != null && n2 != null && !graph.AreConnected(n1, n2)) graph.AddEdge(n1, n2);
                    }
                }
            }

            // graph.AddNodes(Range.Create(1, nodesCount).Map(i => new LayoutNode(i, true)).ToArray());
            return graph;
        }

        /// <summary>
        /// Creates a balanced tree.
        /// </summary>
        /// <param name="levels">The levels.</param>
        /// <param name="siblingsCount">The siblings count.</param>
        /// <returns></returns>
        public static Graph CreateBalancedTree(int levels = 3, int siblingsCount = 3)
        {
            var counter = -1;
            var graph = new Graph() { IsDirected = true };
            if (levels <= 0) return graph;
            var root = new Node(counter++, true);
            graph.AddNode(root);
            if (siblingsCount <= 0) return graph;
            var lastAdded = new List<Node>();
            lastAdded.Add(root);
            for (var i = 0; i < levels; i++)
            {
                var news = new List<Node>();
                foreach (var node in lastAdded)
                {
                    for (var j = 0; j < siblingsCount; j++)
                    {
                        var item = new Node(counter++, true);
                        graph.AddNode(item);
                        graph.AddEdge(node, item);
                        news.Add(item);
                    }
                }
                lastAdded = news;
            }

            return graph;
        }

        /// <summary>
        /// Creates a forest of balanced trees.
        /// </summary>
        /// <param name="levels">The levels.</param>
        /// <param name="siblingsCount">The siblings count.</param>
        /// <param name="treeCount">The tree count.</param>
        /// <returns></returns>
        public static Graph CreateBalancedForest(int levels = 4, int siblingsCount = 2, int treeCount = 5)
        {
            var counter = -1;
            var graph = new Graph() { IsDirected = true };
            if (levels <= 0) return graph;

            for (var k = 0; k < treeCount; k++)
            {
                var root = new Node(counter++, true);
                graph.AddNode(root);
                if (siblingsCount <= 0) return graph;
                var lastAdded = new List<Node> { root };
                for (var i = 0; i < levels; i++)
                {
                    var news = new List<Node>();
                    foreach (var node in lastAdded)
                    {
                        for (var j = 0; j < siblingsCount; j++)
                        {
                            var item = new Node(counter++, true);
                            graph.AddNode(item);
                            graph.AddEdge(node, item);
                            news.Add(item);
                        }
                    }
                    lastAdded = news;
                }
            }

            return graph;
        }

        /// <summary>
        /// Creates a random graph.
        /// </summary>
        /// <param name="nodesCount">The count.</param>
        /// <param name="maxIncidence">The maximum incidence of each node.</param>
        /// <param name="tree">If set to <c>true</c> the generated graph will be a tree.</param>
        /// <returns></returns>
        public static Graph CreateRandomGraph(int nodesCount = 150, int maxIncidence = 4, bool tree = false)
        {
            var graph = new Graph();
            for (var i = 0; i < nodesCount; i++)
            {
                var node = new Node(i, true);
                graph.Nodes.Add(node);
                var otherNode = FetchNode(graph, maxIncidence);
                if (otherNode != null) graph.AddEdge(node, otherNode);
            }
            if (!tree && nodesCount > 1)
            {
                var randomAdditions = Rand.Next(1, nodesCount);
                for (var i = 0; i < randomAdditions; i++)
                {
                    var n1 = graph.TakeRandomNode(null, maxIncidence);
                    var n2 = graph.TakeRandomNode(null, maxIncidence);
                    if (n1 != null && n2 != null && !graph.AreConnected(n1, n2)) graph.AddEdge(n1, n2);
                }
            }
            return graph;
        }

        /// <summary>
        /// Performs a depth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="g">
        /// The graph to traverse.
        /// </param>
        /// <param name="visitor">
        /// The visitor.
        /// </param>
        /// <param name="startNode">
        /// The start node.
        /// </param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Depth-first_search.
        /// </remarks>
        public static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IVisitor<TNode> visitor, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            if (startNode == null) throw new ArgumentNullException("startNode");

            var visited = new Dictionary<TNode, bool>();
            g.Nodes.ForEach(n => visited.Add(n, false));

            g.DepthFirstTraversal(visitor, startNode, visited, 0);
        }

        /// <summary>
        /// Performs a depth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="g">The g.</param>
        /// <param name="visitor">The visitor.</param>
        /// <param name="startNode">The start node.</param>
        public static void DepthFirstTraversal<TNode, TLink>(this Graph<TNode, TLink> g, IParentVisitor<TNode> visitor, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            var visited = new Dictionary<TNode, bool>();
            g.Nodes.ForEach(n => visited.Add(n, false));
            g.DepthFirstTraversal(visitor, startNode, visited, null);
        }

        /// <summary>
        /// Performs a depth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="g">The graph.</param>
        /// <param name="visitor">The visitor.</param>
        /// <param name="startNode">The start node.</param>
        public static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IDepthVisitor<TNode> visitor, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            if (startNode == null)
            {
                throw new ArgumentNullException("startNode");
            }

            var visited = new Dictionary<TNode, bool>();
            g.Nodes.ForEach(n => visited.Add(n, false));

            g.DepthFirstTraversal(visitor, startNode, visited, 0);
        }

        /// <summary>
        /// Performs a depth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="startNode">
        /// The start node.
        /// </param>
        public static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, Action<TNode> action, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (startNode == null)
            {
                throw new ArgumentNullException("startNode");
            }

            var visitor = new ActionVisitor<TNode>(action);
            DepthFirstTraversal(g, visitor, startNode);
        }

        /// <summary>
        /// Performs a depth-first traversal of the graph starting at the given node.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="g">The g.</param>
        /// <param name="action">The action.</param>
        /// <param name="startNode">The start node.</param>
        public static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, Action<TNode, int> action, TNode startNode)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (action == null) throw new ArgumentNullException("action");
            if (startNode == null) throw new ArgumentNullException("startNode");

            var visitor = new DepthActionVisitor<TNode>(action);
            DepthFirstTraversal(g, visitor, startNode);
        }

        /// <summary>
        /// Finds cycles in a graph using Tarjan's strongly connected components algorithm.
        /// See http://en.wikipedia.org/wiki/Tarjan's_strongly_connected_components_algorithm .
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="excludeSingleItems">
        /// If set to <c>true</c> nodes with no edges are excluded.
        /// </param>
        /// <returns>
        /// A list of of vertice arrays (paths) that form cycles in the graph.
        /// </returns>
        public static IList<TNode[]> FindCycles<TNode, TLink>(
            this Graph<TNode, TLink> graph, bool excludeSingleItems = true)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            var indices = new Dictionary<TNode, int>();
            var lowlinks = new Dictionary<TNode, int>();
            var connected = new List<TNode[]>();
            var stack = new Stack<TNode>();

            foreach (var vertex in graph.Nodes.Where(vertex => !indices.ContainsKey(vertex)))
            {
                TarjansStronglyConnectedComponentsAlgorithm<TNode, TLink>(excludeSingleItems, vertex, indices, lowlinks, connected, stack, 0);
            }

            return connected;
        }

        /// <summary>
        /// Compares the two graph and assert they are identical if
        /// <list type="bullet">
        /// <item>
        /// <description>They have the same amount of nodes and links.</description>
        /// </item>
        /// <item>
        /// <description>The set of node identifiers are the same.</description>
        /// </item>
        /// <item>
        /// <description>The links defined by couples of identifiers are the
        /// same.</description>
        /// </item>
        /// </list>
        /// .
        /// </summary>
        /// <param name="g">
        /// A graph structure.
        /// </param>
        /// <param name="h">
        /// Another graph structure.
        /// </param>
        /// <returns>
        /// The has identical structure with.
        /// </returns>
        public static bool HasIdenticalStructureWith(this Graph g, Graph h)
        {
            // node count must be the same
            if (g.Nodes.Count != h.Nodes.Count)
            {
                return false;
            }

            // edge count must be the same
            if (g.Edges.Count != h.Edges.Count)
            {
                return false;
            }

            // the id sets must match
            if (g.Nodes.Any(n => h.FindNode(n.Id) == null))
            {
                return false;
            }

            // we compare the edges one by one
            var visited = new List<Edge>(h.Edges);
            foreach (var edge in g.Edges)
            {
                if (!h.AreConnected(edge.Source.Id, edge.Sink.Id, true))
                {
                    return false;
                }
                else
                {
                    var found = visited.FirstOrDefault(l => l.Source.Id == edge.Source.Id && l.Sink.Id == edge.Sink.Id);
                    if (found == null)
                    {
                        return false;
                    }

                    visited.Remove(found);
                }
            }

            // none should remain in the compared list
            if (visited.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kruskalses the algorithm.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <description>On whether you should use Kruskal or Prim's see
        /// http://stackoverflow.com/questions/1195872/kruskal-vs-prim.</description>
        /// </item>
        /// </list>
        /// .
        /// </remarks>
        /// <param name="convertToTreeFlow">
        /// If set to <c>true</c> and the resulting spanning tree will be converted to a flow, see <see cref="TreeFlow{TNode,TLink}"/> for more on this.
        /// </param>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <seealso cref="PrimsSpanningTree{TNode,TLink}"/>
        public static Graph<TNode, TLink> KruskalsSpanningTree<TNode, TLink>(this Graph<TNode, TLink> graph, bool convertToTreeFlow = false)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            var parentMap = new Dictionary<TNode, TNode>();
            var mapping = new Dictionary<TNode, TNode>();
            var edgeQueue = new Heap<Muple<double, TLink>>(OrderType.Ascending, new MupleComparer<double, TLink>());

            // The spanning tree should contain one less than the numer of nodes
            var edgeCount = graph.Nodes.Count - 1;

            var result = new Graph<TNode, TLink> { IsDirected = graph.IsDirected };

            foreach (var node in graph.Nodes)
            {
                var copy = new TNode { Id = node.Id };
                mapping.Add(node, copy);
                result.AddNode(copy);

                // assume all nodes in different trees
                parentMap.Add(node, null);
            }

            // We need to move the edges into a priority queue
            // and use the weight in the association to sort them.
            foreach (var e in graph.Edges) edgeQueue.Add(new Muple<double, TLink>(e.Weight, e));

            while ((edgeQueue.Count > 0) && (edgeCount > 0))
            {
                var linkMuple = edgeQueue.RemoveRoot();

                var edge = linkMuple.Item2;

                var source = edge.Source;
                var sink = edge.Sink;

                // Find the head vertex of the forest the fromVertex is in
                while (parentMap[source] != null) source = parentMap[source];

                while (parentMap[sink] != null) sink = parentMap[sink];

                if (source != sink)
                {
                    // this one is so fiery tricky to understand;
                    parentMap[source] = edge.Sink;
                    edgeCount--;
                    result.AddEdge(mapping[edge.Source], mapping[edge.Sink]);
                }
            }
            if (convertToTreeFlow) result = result.TreeFlow();
            return result;
        }

        /// <summary>
        /// Merges the given graph into the current graph.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="otherGraph">
        /// The graph to merge into the current one.
        /// </param>
        /// <returns>
        /// </returns>
        public static Graph Merge(this Graph graph, Graph otherGraph)
        {
            var merged = new Graph(graph);

            // merge the nodes with different id
            var existingList = new List<Node>();
            foreach (var node in otherGraph.Nodes)
            {
                if (merged.Nodes.Count(n => n.Id == node.Id) == 0)
                {
                    merged.Nodes.Add(node);

                    // augment the Edges collection
                    foreach (var item in node.Outgoing)
                    {
                        merged.Edges.Add(item);
                    }

                    ////node.Outgoing.ForEach(merged.Edges.Add);
                }
                else
                {
                    existingList.Add(node);
                }
            }

            // handle the links from the nodes which were already present
            foreach (var node in existingList)
            {
                var existingNode = merged.FindNode(node.Id);
                foreach (var edge in node.Outgoing)
                {
                    var i = edge.Source.Id;
                    var j = edge.Sink.Id;

                    // do not use the 'AreConnected' here since it will rely on the Outgoing collection and will, hence, always return true
                    if (existingNode.AllLinks.Count(l => l.Source.Id == i && l.Sink.Id == j) == 0)
                    {
                        var n = merged.FindNode(i);
                        var m = merged.FindNode(j);
                        merged.AddEdge(n, m);
                    }
                }
            }

            return merged;
        }



        /// <summary>
        /// Offsets the given rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="x">
        /// The horizontal offset.
        /// </param>
        /// <param name="y">
        /// The vertical offset.
        /// </param>
        /// <returns>
        /// </returns>
        public static Rect Offset(this Rect rect, double x, double y)
        {
            rect.X += x;
            rect.Y += y;
            return rect;
        }

        /// <summary>
        /// Parses the specified list representing the incidence structure of a graph.
        /// </summary>
        /// <param name="list">
        /// The list of edge couples.
        /// </param>
        /// <returns>
        /// The graph corresponding to the incidence structure given.
        /// </returns>
        public static Graph Parse(IEnumerable<string> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            var graph = new Graph();

            foreach (var tuple in list.Select(s => s.Split(',')).Where(tuple => tuple.Length >= 2))
            {
                int j;
                int i;
                if (int.TryParse(tuple[0], out i) && int.TryParse(tuple[1], out j))
                {
                    var n = graph.FindNode(i);
                    var m = graph.FindNode(j);
                    if (n == null)
                    {
                        n = new Node(i, true);
                        graph.AddNode(n);
                    }

                    if (m == null)
                    {
                        m = new Node(j, true);
                        graph.AddNode(m);
                    }

                    /* We'll allow multigraphs... if (!graph.AreConnected(i, j, true)) */
                    graph.AddEdge(n, m);
                }
            }

            return graph;
        }

        /// <summary>
        /// Returns the position of the given rectangle.
        /// </summary>
        /// <param name="r">
        /// The rectangle.
        /// </param>
        /// <returns>
        /// </returns>
        public static Point Position(this Rect r)
        {
            return new Point(r.X, r.Y);
        }

        /// <summary>
        /// Prim's algorithm  finds a minimum-cost spanning tree of an edge-weighted, connected, undirected graph.
        /// </summary>
        /// <typeparam name="TNode">
        /// The type of the node.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The type of the edge.
        /// </typeparam>
        /// <param name="g">
        /// The graph structure.
        /// </param>
        /// <param name="fromNode">
        /// The node to start from.
        /// </param>
        /// <param name="reverseWrongEdges">
        /// If set to <c>true</c> and the graph is not directed then the edges which do not point in the correct tree flow direction 
        /// will be reversed.
        /// </param>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Prim%27s_algorithm .
        /// </remarks>
        public static Graph<TNode, TLink> PrimsSpanningTree<TNode, TLink>(
            this Graph<TNode, TLink> g, TNode fromNode, bool reverseWrongEdges = false)
            where TNode : class, INode<TNode, TLink> /*, IComparable*/, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (!g.Nodes.Contains(fromNode))
            {
                throw new ArgumentException("The given node is not in the graph.", "fromNode");
            }

            // note that we need to use a muple instead of a keyvalue because the key/value need to be writable
            var heap = new Heap<Muple<double, TNode>>(OrderType.Ascending, new MupleComparer<double, TNode>());

            // the state consists of: Item1=distance, Item2=edge, Item3=visited
            var state = g.Nodes.ToDictionary(
                vertex => vertex, vertex => Muple.Create<double, TLink, bool>(double.MaxValue, null, false));

            state[fromNode].Item1 = 0;

            heap.Add(new Muple<double, TNode>(0, fromNode));

            while (heap.Count > 0)
            {
                var item = heap.RemoveRoot();

                // if the graph is not directed then these Outgoing edges could contain parents
                var edges = item.Item2.Outgoing;

                state[item.Item2].Item3 = true;

                // because the collection cannot be changed while looping over it we'll reverse the links thereafter
                var switchlist = new List<TLink>();

                // Enumerate through all the edges emanating from this node					
                foreach (var edge in edges)
                {
                    // edge goes from item.Item2 -> otherNode
                    var otherNode = edge.GetComplementaryNode(item.Item2);
                    var otherNodeState = state[otherNode];
                    if (!otherNodeState.Item3)
                    {
                        // with weeight this would be '> edge.Weight'
                        if (otherNodeState.Item1 > 0)
                        {
                            otherNodeState.Item2 = edge;
                            if (reverseWrongEdges && edge.Source == otherNode) switchlist.Add(edge);

                            otherNodeState.Item1 = 0;
                            heap.Add(new Muple<double, TNode>(0, otherNode));
                        }
                    }
                }

                if (switchlist.Any())
                {
                    switchlist.ForEach(l => l.Reverse());
                }
            }

            // remove the edges not belonging to the spanning tree
            var tree = new Graph<TNode, TLink>(g) { IsDirected = g.IsDirected };
            var included = state.Where(kv => kv.Value.Item2 != null).Select(kv => kv.Value.Item2);

            // take away the edges which do not make up the tree
            g.Edges.ForEach(
                l =>
                {
                    if (!included.Contains(l))
                    {
                        tree.RemoveLink(l);
                    }
                });

            return tree;
        }

        /// <summary>
        /// Splits the given, not necessarily connected, graph into its connected components.
        /// </summary>
        /// <typeparam name="TNodeData">
        /// The node data type.
        /// </typeparam>
        /// <typeparam name="TLinkData">
        /// The edge data type.
        /// </typeparam>
        /// <param name="graph">
        /// The graph to be split.
        /// </param>
        public static IEnumerable<Graph> Split<TNodeData, TLinkData>(this Graph graph)
            where TNodeData : new()
            where TLinkData : new()
        {
            var subgraphs = new List<Graph>(SplitGraph(graph));
            subgraphs.Sort((a, b) => -a.Nodes.Count.CompareTo(b.Nodes.Count));
            return subgraphs.ToArray();
        }

        /// <summary>
        /// Takes a random node with incidence less than specified.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="node">
        /// The node which should not be returned; i.e. the random node should be in the complement of the given node.
        /// </param>
        /// <param name="incidenceLessThan">
        /// The incidence less than.
        /// </param>
        /// <returns>
        /// </returns>
        public static Node TakeRandomNode(this Graph graph, Node node = null, int incidenceLessThan = 4)
        {
            if (graph.Nodes.Count == 0)
            {
                return null;
            }

            if (graph.Nodes.Count == 1)
            {
                var singleton = graph.Nodes[0];
                return singleton == node ? null : singleton;
            }

            var pool = graph.Nodes.Where(n => n.AllLinks.Count < incidenceLessThan && n != node).ToList();
            if (!pool.Any())
            {
                return null;
            }

            return pool.ToList()[Rand.Next(pool.Count())];
        }

        /// <summary>
        /// Takes two random nodes from the given graph.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <returns>
        /// </returns>
        public static Tuple<Node, Node> TakeTwoRandomNodes(this Graph graph)
        {
            if (graph.Nodes.Count < 2)
            {
                throw new Exception("There are not enough nodes in the graph to take two.");
            }

            if (graph.Nodes.Count == 2)
            {
                return Tuple.Create(graph.Nodes[0], graph.Nodes[1]);
            }

            var node1 = graph.Nodes[Rand.Next(graph.Nodes.Count)];
            Node node2;
            do node2 = graph.Nodes[Rand.Next(graph.Nodes.Count)];
            while (node2 == node1);
            return Tuple.Create(node1, node2);
        }

        /// <summary>
        /// Returns the strongly connected components of the graph.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="excludeSingleItems">if set to <c>true</c> [exclude single items].</param>
        /// <returns></returns>
        public static object TarjanStronglyConnectedComponents<TNode, TLink>(this Graph<TNode, TLink> graph, bool excludeSingleItems = true)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            var indices = new Dictionary<TNode, int>();
            var lowlinks = new Dictionary<TNode, int>();
            var connected = new List<TNode[]>();
            var stack = new Stack<TNode>();

            foreach (var vertex in graph.Nodes.Where(vertex => !indices.ContainsKey(vertex)))
            {
                TarjansStronglyConnectedComponentsAlgorithm<TNode, TLink>(excludeSingleItems, vertex, indices, lowlinks, connected, stack, 0);
            }
            return connected;
        }

        /// <summary>
        /// Executes Tarjan's algorithm on the graph.
        /// </summary>
        /// <typeparam name="TNode">
        /// The node data type.
        /// </typeparam>
        /// <typeparam name="TLink">
        /// The edge data type.
        /// </typeparam>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Pseudoforest .
        /// </remarks>
        /// <param name="excludeSinlgeItems">
        /// If set to <c>true</c> sinlge items (singletons) will not be taken into account.
        /// </param>
        /// <param name="node">
        /// The node to start with.
        /// </param>
        /// <param name="indices">
        /// The current indices.
        /// </param>
        /// <param name="lowlinks">
        /// The current lowlinks.
        /// </param>
        /// <param name="connected">
        /// The connected components.
        /// </param>
        /// <param name="stack">
        /// The stack.
        /// </param>
        /// <param name="index">
        /// The current index.
        /// </param>
        public static void TarjansStronglyConnectedComponentsAlgorithm<TNode, TLink>(
            bool excludeSinlgeItems,
            TNode node,
            IDictionary<TNode, int> indices,
            IDictionary<TNode, int> lowlinks,
            ICollection<TNode[]> connected,
            Stack<TNode> stack,
            int index)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            indices[node] = index;
            lowlinks[node] = index;
            index++;

            stack.Push(node);

            foreach (var next in node.Outgoing.Select(edge => edge.Sink))
            {
                if (!indices.ContainsKey(next))
                {
                    TarjansStronglyConnectedComponentsAlgorithm<TNode, TLink>(excludeSinlgeItems, next, indices, lowlinks, connected, stack, index);
                    lowlinks[node] = System.Math.Min(lowlinks[node], lowlinks[next]);
                }
                else if (stack.Contains(next))
                {
                    lowlinks[node] = System.Math.Min(lowlinks[node], lowlinks[next]);
                }
            }

            if (lowlinks[node] == indices[node])
            {
                TNode next;
                var component = new List<TNode>();

                do
                {
                    next = stack.Pop();
                    component.Add(next);
                }
                while (next != node);

                if (!excludeSinlgeItems || (component.Count > 1))
                {
                    connected.Add(component.ToArray());
                }
            }
        }

        /*		/// <summary>
                /// Returns a volatile graph from the given <see cref="Graph"/>.
                /// </summary>
                /// <param name="Graph">
                /// The layout graph to convert.
                /// </param>
                /// <returns>
                /// </returns>
                public static Graph<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>> ToVolatileGraph<TNodeData, TLinkData>(this Graph<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>> Graph)
                {
                    var graph = new Graph<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>>();
                    foreach (var node in Graph.Nodes)
                    {
                        graph.Nodes.Add(new Node<TNodeData, TLinkData>(node));
                    }

                    foreach (var edge in Graph.Edges)
                    {
                        var origin = graph.Nodes.FirstOrDefault(g => g.Node.Equals(edge.Source));
                        var destination = graph.Nodes.FirstOrDefault(g => g.Node.Equals(edge.Sink));
                        graph.Edges.Add(new Edge<TNodeData, TLinkData>(edge, origin, destination));
                    }

                    graph.AssignIdentifiers();

                    return graph;
                }*/

        /// <summary>
        /// If the first supplied rectangle has width or height zero the second rectangle will be returned. Otherwise the
        /// standard union of two rectangles will be used.
        /// </summary>
        /// <param name="r1">
        /// A rectangle.
        /// </param>
        /// <param name="r2">
        /// Another rectangle.
        /// </param>
        /// <returns>
        /// </returns>
        public static Rect UnionEmptyRects(Rect r1, Rect r2)
        {
            if (System.Math.Abs(r1.Width - 0) < Constants.Epsilon || System.Math.Abs(r1.Height - 0) < Constants.Epsilon)
            {
                return r2;
            }

            if (System.Math.Abs(r2.Width - 0) < Constants.Epsilon || System.Math.Abs(r2.Height - 0) < Constants.Epsilon)
            {
                return r1;
            }

            return UnionRects(r1, r2);
        }

        /// <summary>
        /// Returns the smallest possible rectangle containing
        /// both of the specified rectangles.
        /// </summary>
        /// <param name="a">
        /// The first rectangle.
        /// </param>
        /// <param name="b">
        /// The second rectangle.
        /// </param>
        /// <returns>
        /// The union of the rectangles.
        /// </returns>
        public static Rect UnionRects(Rect a, Rect b)
        {
            var x = a;
            x.Union(b);
            return x;
        }

        /// <summary>
        /// Returns a shallow clone from the given collection.
        /// </summary>
        /// <typeparam name="TNodeData">
        /// The node data type.
        /// </typeparam>
        /// <typeparam name="TLinkData">
        /// The edge data type.
        /// </typeparam>
        /// <param name="list">
        /// The collection to clone.
        /// </param>
        /// <returns>
        /// </returns>
        internal static List<Node<TNodeData, TLinkData>> Clone<TNodeData, TLinkData>(
            this IEnumerable<Node<TNodeData, TLinkData>> list)
            where TNodeData : new()
            where TLinkData : new()
        {
            var clone = new List<Node<TNodeData, TLinkData>>();
            clone.AddRange(list);
            return clone;
        }

        /// <summary>
        /// A recursively called depth traversal helper method.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <typeparam name="TLink">The type of the edge.</typeparam>
        /// <param name="g">The graph to traverse.</param>
        /// <param name="visitor">The visitor.</param>
        /// <param name="startNode">The start node.</param>
        /// <param name="visited">The visited dictionary.</param>
        /// <param name="height">The height.</param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Depth-first_search .
        /// </remarks>
        private static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IVisitor<TNode> visitor, TNode startNode, Dictionary<TNode, bool> visited, int height)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("visitor");
            }

            if (startNode == null)
            {
                throw new ArgumentNullException("startNode");
            }
            visitor.Visit(startNode);
            visited[startNode] = true;
            var children = startNode.Children;
            foreach (var child in children.Where(child => child != null && !visited[child]))
            {
                g.DepthFirstTraversal(visitor, child, visited, height++);
            }
        }

        private static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IDepthVisitor<TNode> visitor, TNode startNode, IDictionary<TNode, bool> visited, int height)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            if (startNode == null) throw new ArgumentNullException("startNode");
            visitor.Visit(startNode, height);

            visited[startNode] = true;
            var children = startNode.Children;
            var nextLevel = height + 1;
            foreach (var child in children.Where(child => child != null && !visited[child])) g.DepthFirstTraversal(visitor, child, visited, nextLevel);
        }

        private static void DepthFirstTraversal<TNode, TLink>(
            this Graph<TNode, TLink> g, IParentVisitor<TNode> visitor, TNode startNode, IDictionary<TNode, bool> visited, TNode parent)
            where TNode : class, INode<TNode, TLink>, new()
            where TLink : class, IEdge<TNode, TLink>, new()
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            if (startNode == null) throw new ArgumentNullException("startNode");
            visitor.Visit(startNode, parent);

            visited[startNode] = true;
            var children = startNode.Children;
            foreach (var child in children.Where(child => child != null && !visited[child])) g.DepthFirstTraversal(visitor, child, visited, startNode);
        }

        /// <summary>
        /// The fetch node.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="incidence">
        /// The incidence.
        /// </param>
        /// <returns>
        /// </returns>
        private static Node FetchNode(this Graph graph, int incidence = 4)
        {
            // we don't try more than 50 times
            for (var i = 0; i < 50; i++)
            {
                var found = graph.Nodes[Rand.Next(graph.Nodes.Count)];

                // we keep the incidence below 4
                if ((found.Incoming.Count + found.Outgoing.Count) < incidence)
                {
                    return found;
                }
            }

            return null;
        }



        /// <summary>
        /// Separates the various graph components from the given graph.
        /// </summary>
        /// <param name="graph">
        /// The Graph which should be separated.
        /// </param>
        private static IEnumerable<Graph> SplitGraph(Graph graph)
        {
            // we'll systematically add each node and perform a traversal of the linked nodes
            var nodes = graph.Nodes.Clone();
            var subgraphs = new List<Graph>();

            while (nodes.Count > 0)
            {
                var node = nodes[0];
                nodes.RemoveAt(0);

                var subgraph = new Graph();
                subgraph.Nodes.Add(node);

                var i = 0;
                while (i < subgraph.Nodes.Count)
                {
                    node = subgraph.Nodes[i];

                    foreach (var edge in node.Incoming)
                    {
                        if (!subgraph.Nodes.Contains(edge.Source))
                        {
                            subgraph.Nodes.Add(edge.Source);
                            nodes.Remove(edge.Source);
                        }

                        if (!subgraph.Edges.Contains(edge))
                        {
                            subgraph.Edges.Add(edge);
                        }
                    }

                    foreach (var edge in node.Outgoing)
                    {
                        if (!subgraph.Nodes.Contains(edge.Sink))
                        {
                            subgraph.Nodes.Add(edge.Sink);
                            nodes.Remove(edge.Sink);
                        }

                        if (!subgraph.Edges.Contains(edge))
                        {
                            subgraph.Edges.Add(edge);
                        }
                    }

                    i++;
                }

                subgraphs.Add(subgraph);
            }

            return subgraphs.ToArray();
        }
    }

    /// <summary>
    /// Describes a DFT visitor to a data structure.
    /// </summary>
    /// <typeparam name="T">The type of objects to be visited.</typeparam>
    /// <seealso cref="GraphExtensions.DepthFirstTraversal{TNode,TLink}(Graph{TNode,TLink},IVisitor{T},TNode)"/>
    public interface IDepthVisitor<in T>
    {
        /// <summary>
        /// Gets wether this visitor has finished.
        /// </summary>
        /// <remarks>Assigning this value is important to break the traversals when searching.</remarks>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        bool HasCompleted { get; }

        /// <summary>
        /// Visits the specified object.
        /// </summary>
        /// <param name="obj">The object to visit.</param>
        /// <param name="height">The height in the DFT.</param>
        void Visit(T obj, int height);
    }

    public interface IParentVisitor<in T>
    {
        /// <summary>
        /// Gets wether this visitor has finished.
        /// </summary>
        /// <remarks>Assigning this value is important to break the traversals when searching.</remarks>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        bool HasCompleted { get; }

        /// <summary>
        /// Visits the specified object.
        /// </summary>
        /// <param name="obj">The object to visit.</param>
        /// <param name="parent">The parent of the visited node.</param>
        void Visit(T obj, T parent);
    }



    /// <summary>
    /// A visitor which encloses a standard action.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public sealed class DepthActionVisitor<T> : IDepthVisitor<T>
    {
        private readonly Action<T, int> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthActionVisitor&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DepthActionVisitor(Action<T, int> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.action = action;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public bool HasCompleted
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Visits the specified object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="height">The height.</param>
        public void Visit(T obj, int height)
        {
            this.action(obj, height);
        }
    }

    /// <summary>
    /// A visitor which encloses a standard action.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public sealed class ParentActionVisitor<T> : IParentVisitor<T>
    {
        private readonly Action<T, T> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthActionVisitor&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public ParentActionVisitor(Action<T, T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.action = action;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public bool HasCompleted
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Visits the specified object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="parent">The parent of the visited node (null if the root).</param>
        public void Visit(T obj, T parent)
        {
            this.action(obj, parent);
        }
    }
}