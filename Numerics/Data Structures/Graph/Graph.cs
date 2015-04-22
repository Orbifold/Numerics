using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Base graph class for the various incarnations in the graph analysis.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>The graph is directed by default (<code lang="C#">IsDirected =
	/// true</code>)</description></item>
	/// <item>
	/// <description>The adjacency structure is not centralized but resides in the
	/// Outgoing and Incoming collection attached to the
	/// Nodes.</description></item></list>
	/// </remarks>
	/// <typeparam name="TNode">The data type of the node which should be an
	/// implementation of the <see cref="INode{TNode,TLink}">INode{TNode,TLink}</see>
	/// interface and have a parameterless constructor.</typeparam>
	/// <typeparam name="TLink">The data type of the edge which should be an
	/// implementation of the <see cref="IEdge{TNode,TLink}">IEdge{TNode,TLink}</see>
	/// interface and have a parameterless constructor.</typeparam>
	public class Graph<TNode, TLink>
		where TNode : class, INode<TNode, TLink>, new()
		where TLink : class, IEdge<TNode, TLink>, new()
	{
		/// <summary>
		/// The <see cref="IsDirected"/> field.
		/// </summary>
		private bool isDirected;

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph{TNode,TLink}"/> class. 
		/// </summary>
		public Graph()
		{
			this.Nodes = new List<TNode>();
			this.Edges = new List<TLink>();
			this.isDirected = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph{TNode,TLink}"/> class.
		/// </summary>
		/// <param name="graph">The graph content to start with. Note that references will be added, not clones.</param>
		public Graph(Graph<TNode, TLink> graph)
			: this()
		{
			graph.Nodes.ForEach(n => this.Nodes.Add(n));
			graph.Edges.ForEach(l => this.Edges.Add(l));
		}

		/// <summary>
		/// Gets whether this graph is connected.
		/// See also this article;  http://en.wikipedia.org/wiki/Connected_graph. 
		/// </summary>
		/// <remarks>
		/// A graph is connected if every two vertices are connected by a path. A connected
		/// graph has only one component.
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public bool IsConnected
		{
			get
			{
				return this.GetConnectedComponents().Count() == 1;
			}
		}

		/// <summary>
		/// Gets whether the graph is acyclic.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>If there are no cycles in a graph it's acyclic. A cycle means a
		/// closed path or loop.</description></item>
		/// <item>
		/// <description>See also the article;
		/// http://en.wikipedia.org/wiki/Directed_acyclic_graph
		/// .</description></item></list>
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is acyclic; otherwise, <c>false</c>.
		/// </value>
		public bool IsAcyclic
		{
			get
			{
				return !this.FindCycles().Any();
			}
		}

		/// <summary>
		/// Gets whether the graph is hamiltonian.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>An Hamitonian cycle is a cycle which contains all nodes of the
		/// graph. If there is at least one such cycle the graph is called
		/// Hamiltonian.</description></item>
		/// <item>
		/// <description>See also the article;http://en.wikipedia.org/wiki/Hamiltonian_graph
		/// .</description></item></list>
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is acyclic; otherwise, <c>false</c>.
		/// </value>
		public bool IsHamiltonian
		{
			get
			{
				throw new NotImplementedException("This is work in progress, please hold your horses.");
			}
		}

		/// <summary>
		/// Gets or sets the links of this graph.
		/// </summary>
		/// <value>
		/// The links collection.
		/// </value>
		public List<TLink> Edges { get; protected set; }

		/// <summary>
		/// Gets whether this graph is directed.
		/// </summary>
		public bool IsDirected
		{
			get
			{
				return this.isDirected;
			}
			set
			{
				if (value == this.isDirected) return;

				/*Here's the thing; our adjacency structure doesn't reside in a global entity but in each node's Incoming/Outgoing collections.
				 So, if the graph becomes (un)direct the nodes need to know since they contain the linkage.
				 */
				this.Nodes.ForEach(n => n.IsDirected = value);
				this.isDirected = value;
			}
		}

		/// <summary>
		/// Gets or sets the nodes of this graph.
		/// </summary>
		/// <value>
		/// The nodes collection.
		/// </value>
		public List<TNode> Nodes { get; protected set; }

		/// <summary>
		/// Adds a edge to this graph.
		/// </summary>
		/// <param name="source">
		/// The source of the edge.
		/// </param>
		/// <param name="sink">
		/// The sink of the edge.
		/// </param>
		/// <returns>
		/// The added edge.
		/// </returns>
		public TLink AddEdge(TNode source, TNode sink)
		{
			return this.AddEdge(new TLink { Source = source, Sink = sink });
		}

		/// <summary>
		/// Adds the givven edge to the graph. It will add the sink and source nodes to the <see cref="Nodes"/> collection if they are not yet
		/// part of it.
		/// </summary>
		/// <param name="edge">
		/// The edge to add.
		/// </param>
		/// <returns>
		/// The added edge.
		/// </returns>
		public TLink AddEdge(TLink edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			/* Allowing multigraphs...
			 * if (this.AreConnected(edge.Source, edge.Sink, true)) throw new InvalidOperationException("The edge cannot be added; this structure does not support multigraphs and the given nodes are already connected.");*/
			this.Edges.Add(edge);
			edge.Source.AddOutgoingEdge(edge);
			edge.Sink.AddIncomingEdge(edge);
			if (!this.Nodes.Contains(edge.Source)) this.AddNode(edge.Source);
			if (!this.Nodes.Contains(edge.Sink)) this.AddNode(edge.Sink);
			return edge;
		}

		/// <summary>
		/// Adds the given node to the graph.
		/// </summary>
		/// <param name="node">The node to add.</param>
		public void AddNode(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			this.Nodes.Add(node);
			node.IsDirected = this.IsDirected;
		}

		/// <summary>
		/// Adds a series of nodes to the graph.
		/// </summary>
		/// <param name="nodes">
		/// The nodes.
		/// </param>
		public void AddNodes(params TNode[] nodes)
		{
			if (nodes == null) throw new ArgumentNullException("nodes");
			nodes.ToList().ForEach(n => this.Nodes.Add(n));
		}

		/// <summary>
		/// Returns whether the given nodes are connected in one direction or the other.
		/// </summary>
		/// <remarks>
		/// Because the structure allows multigraphs the connectedness means there is at
		/// least one edge between the given nodes.
		/// </remarks>
		/// <param name="n">A node.</param>
		/// <param name="m">Another node.</param>
		/// <param name="strict">If set to <c>true</c> the first node has to be the source of the edge and the second the sink..</param>
		/// <returns>
		/// <c>true</c> If there is a edge connecting the given nodes with the first one as source and the second as sink, <c>false</c> if both options have to be considered.
		/// </returns>
		public bool AreConnected(TNode n, TNode m, bool strict = false)
		{
			if (n == null) throw new ArgumentNullException("n");
			if (m == null) throw new ArgumentNullException("m");
			if (strict) return n.Outgoing.Any(l => l.GetComplementaryNode(n) == m);
			return n.Outgoing.Any(l => l.GetComplementaryNode(n) == m) || n.Incoming.Any(l => l.GetComplementaryNode(n) == m);
		}

		/// <summary>
		/// Returns whether the given nodes are connected in one direction or the other.
		/// </summary>
		/// <remarks>
		/// Because the structure allows multigraphs the connectedness means there is at
		/// least one edge between the given nodes.
		/// </remarks>
		/// <param name="i">The id of the first node.</param>
		/// <param name="j">The id of the second node.</param>
		/// <param name="strict">If set to <c>true</c> the first node has to be the source of the edge and the second the sink..</param>
		/// <returns>
		///   <c>true</c> If there is a edge connecting the given nodes with the first one as source and the second as sink, <c>false</c> if both options have to be considered.
		/// </returns>
		public bool AreConnected(int i, int j, bool strict = false)
		{
			var n = this.FindNode(i);
			var m = this.FindNode(j);
			if (n == null || m == null) return false;
			return this.AreConnected(n, m, strict);
		}

		/// <summary>
		/// Assigns to each edge and node an identifier based on their collection listIndex.
		/// </summary>
		public void AssignIdentifiers()
		{
			// guess some other way would work as well but let's keep it simple
			this.Nodes.ForEach(n => n.Id = this.Nodes.IndexOf(n));
			this.Edges.ForEach(l => l.Id = this.Edges.IndexOf(l));
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public Graph<TNode, TLink> Clone()
		{
			var clone = new Graph<TNode, TLink>();
			this.Nodes.ForEach(n => clone.AddNode(n.Clone()));
			foreach (var edge in this.Edges)
			{
				var n = this.FindNode(edge.Source.Id);
				var m = this.FindNode(edge.Sink.Id);
				clone.AddEdge(new TLink { Id = edge.Id, Sink = m, Source = n });
			}
			return clone;
		}

		/// <summary>
		/// Finds the edge with the specified identifiers.
		/// </summary>
		/// <param name="i">The id of the source.</param>
		/// <param name="j">The id of the sink.</param>
		/// <param name="strict">If set to <c>true</c> the found edge has to go from i to j.</param>
		/// <returns></returns>
		public TLink FindEdge(int i, int j, bool strict)
		{
			if (!this.AreConnected(i, j, strict)) return null;

			var n = this.FindNode(i);
			if (n == null) return null;
			var m = this.FindNode(j);
			if (m == null) return null;
			if (strict) return n.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(n) == m);

			var found = n.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(n) == m);
			return found ?? m.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(m) == n);
		}

		/// <summary>
		/// Finds the node with the specified identifier.
		/// </summary>
		/// <param name="id">The id to look for.</param>
		/// <returns></returns>
		public TNode FindNode(int id)
		{
			return this.Nodes.FirstOrDefault(n => n.Id == id);
		}

		/// <summary>
		/// Attempts to find a tree root by looking at the longest paths in the graph.
		/// </summary>
		/// <remarks>The algorithms looks for all shortest paths between all vertices, which means it will also function for disconnected graphs but will return the root
		/// of the tree with longest path.</remarks>
		/// <returns>A tree root or <c>null</c> is none was found.
		/// </returns>
		public TNode FindTreeRoot()
		{
			if (this.Nodes == null || !this.Nodes.Any()) return null;
			if (this.Nodes.Count == 1) return this.Nodes[0];
			var shortestPaths = this.ShortestPaths();
			TNode found = null;
			var max = 0;
			foreach (var node in this.Nodes)
			{
				var maxPathlengthStartingFromThisNode = this.Nodes.Select((otherNode, j) => shortestPaths[Tuple.Create(node, otherNode)]).Concat(new[] { int.MinValue }).Max();
				if (maxPathlengthStartingFromThisNode <= max) continue;
				max = maxPathlengthStartingFromThisNode;
				found = node;
			}
			return found;
		}

		/// <summary>
		/// Returns the connected components of this graph.
		/// </summary>
		/// <returns>
		/// The list of connected components.
		/// </returns>
		public IEnumerable<Graph<TNode, TLink>> GetConnectedComponents()
		{
			this.HaveUniqueIdentifiers();
			Dictionary<int, int> componentMap;
			var componentsCount = this.NumberOfComponents(out componentMap);

			// now convert it to a list of graphs
			var components = new List<Graph<TNode, TLink>>();
			for (var i = 0; i < componentsCount; ++i) components.Add(new Graph<TNode, TLink>());
			foreach (var nodeId in componentMap.Keys)
			{
				var graph = components[componentMap[nodeId]];
				var node = this.FindNode(nodeId);
				graph.Nodes.Add(node);
				foreach (var l in node.Outgoing) graph.Edges.Add(l);
			}
			return components;
		}

		/// <summary>
		/// Gets the next identifier of the nodes sequence.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public int GetNextIdInNodes(int id)
		{
			var found = this.FindNode(id);
			if (found == null) return -1;
			return this.Nodes[this.Nodes.IndexOf(found) + 1].Id;
		}

		/// <summary>
		/// Ensures the unique identifiers.
		/// </summary>
		public bool HaveUniqueIdentifiers()
		{
			var ids = new List<int>();
			foreach (var node in this.Nodes)
			{
				if (ids.Contains(node.Id)) return false;
				ids.Add(node.Id);
			}
			return true;
		}

		/// <summary>
		/// Ensures that the graph nodes all have a unique identifier assigned.
		/// </summary>
		/// <remarks>If the nodes do have unique identifiers nothing will be altered.</remarks>
		public void EnsureUniqueIdentifiers()
		{
			if (!this.HaveUniqueIdentifiers()) this.AssignIdentifiers();
		}

		/// <summary>
		/// Returns the number of (connected) components.
		/// </summary>
		/// <example>
		/// The following example create two components; 
		/// <para> </para>
		/// <code lang="C#">var g = new Graph&lt;Node, Edge&gt;();
		/// for (var i = 0; i &lt; 4; i++) g.AddNode(new Node(1, true));
		/// g.AddEdge(g.Nodes[0], g.Nodes[1]);
		/// g.AddEdge(g.Nodes[2], g.Nodes[3]);
		/// var count = g.NumberOfComponents();</code>
		/// .</example>
		public int NumberOfComponents()
		{
			Dictionary<int, int> componentMap;
			return this.NumberOfComponents(out componentMap);
		}

		/// <summary>
		/// Returns the number of connected components.
		/// </summary>
		/// <param name="componentMap">The component map as a dictionary where the key is the node identifier and the value is the number of the connected component to which the node belongs.</param>
		/// <returns></returns>
		public int NumberOfComponents(out Dictionary<int, int> componentMap)
		{
			this.EnsureUniqueIdentifiers();
			var componentIndex = 0;

			// the map tells to which component a node belongs
			componentMap = new Dictionary<int, int>(this.Nodes.Count);
			foreach (var t in this.Nodes) componentMap.Add(t.Id, -1);

			for (var i = 0; i < this.Nodes.Count; i++)
			{
				if (componentMap[this.Nodes[i].Id] != -1) continue; // means it already belongs to a component
				this.AssignConnectedComponent(componentMap, i, componentIndex);
				componentIndex++;
			}
			return componentIndex;
		}

		/// <summary>
		/// Detaches all Edges from from the given node and removes them from the graph structure.
		/// </summary>
		/// <param name="node">The node.</param>
		public void RemoveAllLinksFrom(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");

			// one cannot change the collection in the looped collection, hence this construction
			while (node.Incoming.Count > 0) this.RemoveLink(node.Incoming[0]);
			while (node.Outgoing.Count > 0) this.RemoveLink(node.Outgoing[0]);
		}

		/// <summary>
		/// Removes the edge from the graph.
		/// </summary>
		/// <param name="edge">
		/// The edge.
		/// </param>
		public void RemoveLink(TLink edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			if (!this.Edges.Contains(edge)) return;
			edge.Source.RemoveLink(edge);
			edge.Sink.RemoveLink(edge);
			this.Edges.Remove(edge);
		}

		/// <summary>
		/// Removes the given node from this graph.
		/// </summary>
		/// <param name="node">The node to remove.</param>
		public void RemoveNode(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			if (!this.Nodes.Contains(node)) return;
			this.RemoveAllLinksFrom(node);
			this.Nodes.Remove(node);
		}

		/// <summary>
		/// Assigns a new identifier to the nodes.
		/// </summary>
		/// <param name="startId">The number to start the numbering from.</param>
		public void RenumberNodes(int startId = 0)
		{
			for (var i = 0; i < this.Nodes.Count; i++) this.Nodes[i].Id = startId + i;
		}

		/// <summary>
		/// Returns a string representation of the incidence structure of this graph.
		/// </summary>
		/// <example>
		/// A diagram with Edges between identifier 1 and 2, 2 and 3, 3 and 4 will result in
		/// a string
		/// <para><code lang="C#">{&quot;1,2&quot;, &quot;2,3&quot;,
		/// &quot;3,4&quot;}</code></para>.
		/// </example>
		/// <seealso cref="ToLinksList">ToLinksList</seealso>
		public string ToLinkListString()
		{
			var sb = new StringBuilder();
			var list = this.ToLinksList();
			sb.Append("{");
			list.ForEach(s => sb.AppendFormat(",\"{0}\"", s));
			sb.Remove(0, 1);
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		/// Returns the Edges structure of this graph as a list of identifier tuples.
		/// </summary>
		/// <returns></returns>
		/// <seealso cref="ToLinkListString"/>
		public List<string> ToLinksList()
		{
			var list = new List<string>();
			this.Edges.ForEach(l => list.Add(string.Format("{0},{1}", l.Source.Id, l.Sink.Id)));
			return list;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			/*
			var sb = new StringBuilder();
			foreach (var v in this.Nodes)
			{
				sb.AppendFormat("{0} ", v.Id);
				v.AllLinks.ForEach(e => sb.AppendFormat("{0}-{1}", e.Source.Id, e.Sink.Id));
			}

			return sb.ToString();
			 */

			return string.Format("Nodes: {0}, Edges: {1}", this.Nodes.Count, this.Edges.Count);
		}

		/// <summary>
		/// Is a linear ordering of its vertices such that, for every edge uv, u comes
		/// before v in the ordering. See Wikipedia for example;
		/// http://en.wikipedia.org/wiki/Topological_sorting.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>The sorting is not unique.</description></item>
		/// <item>
		/// <description>The graph has to be acyclic in order to have a topological
		/// sort.</description></item>
		/// <item>
		/// <description>The sorting works on disconnected
		/// graphs.</description></item></list>
		/// </remarks>
		/// <param name="forceNewIdentifier"></param>
		/// <returns>
		/// The topologically sorted sequence of node identifiers or <c>null</c> is the graph has cycles.
		/// </returns>
		public List<int> TopologicalSort(bool forceNewIdentifier = false)
		{
			if (!this.HaveUniqueIdentifiers() || forceNewIdentifier) this.AssignIdentifiers();

			// this will store the id's of the nodes
			var result = new List<int>();
			var handledCounter = 0;

			// the value is -1 if unvisited
			var handledSequence = this.CreateDictionary(-1);
			var visitSequence = this.CreateDictionary(-1);

			// access to modified closure here, as usual
			var sequence = visitSequence;
			if ((from node in this.Nodes where sequence[node.Id] == -1 select this.TopologicalSort(node.Id, result, handledSequence, ref visitSequence, ref handledCounter)).Any(acyclic => !acyclic)) return null;
			result.Reverse();
			return result;
		}

		/// <summary>
		/// Gets the shortests path lengths between each two vertices.
		/// </summary>
		/// <returns>
		/// A dictionary keyed with the node id's and value equal to the path lengths.
		/// </returns>
		public Dictionary<Tuple<TNode, TNode>, int> ShortestPaths()
		{
			this.EnsureUniqueIdentifiers();
			var pathLengths = this.CreateBiDictionary(-1);
			foreach (var node in this.Nodes)
			{
				var queue = new Queue<TNode>();
				pathLengths[Tuple.Create(node, node)] = 0;

				// now, just a standard BFS and adding +1 at each next level being visited
				queue.Enqueue(node);
				while (queue.Count > 0)
				{
					var v = queue.Dequeue();
					var vertex1 = node;
					foreach (var w in v.Neighbors.Where(w => pathLengths[Tuple.Create(vertex1, w)] == -1))
					{
						pathLengths[Tuple.Create(node, w)] = pathLengths[Tuple.Create(node, v)] + 1;
						queue.Enqueue(w);
					}
				}
			}

			return pathLengths;
		}

		/// <summary>
		/// Returns whether the two nodes with specified ide's are the in same component.
		/// </summary>
		/// <param name="id1">The id1.</param>
		/// <param name="id2">The id2.</param>
		/// <returns></returns>
		public bool AreInSameComponent(int id1, int id2)
		{
			var comps = this.GetConnectedComponents().ToList();
			if (comps.Count() <= 1) return true;
			var one = this.FindNode(id1);
			var two = this.FindNode(id2);
			if (one == null)
				throw new Exception("The first identifier could not be found in the given graph.");
			if (two == null)
				throw new Exception("The second identifier could not be found in the given graph.");
			var indexOne = -1;
			var indexTwo = -1;
			foreach (var com in comps)
			{
				var foundone = com.FindNode(id1);
				var foundtwo = com.FindNode(id2);
				if (foundone != null) indexOne = comps.IndexOf(com);
				if (foundtwo != null) indexTwo = comps.IndexOf(com);
				if (indexOne > -1 && indexTwo > -1) return indexOne == indexTwo;
			}
			return false;
		}

		/// <summary>
		/// Returns the shortest path between two nodes using the Dijkstra algorithm.
		/// </summary>
		/// <param name="source">The node from where to start.</param>
		/// <param name="target">The node where the shortest path should end.</param>
		/// <returns>The shortest path, if any.</returns>
		public GraphPath<TNode, TLink> DijkstraShortestPath(TNode source, TNode target)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (target == null) throw new ArgumentNullException("target");

			return DijkstraShortestPath(source.Id, target.Id);
		}

		/// <summary>
		/// Returns the shortest path between two nodes using the Dijkstra algorithm.
		/// </summary>
		/// <param name="sourceId">The identifier of the node from where to start.</param>
		/// <param name="targetId">The identifier of the node where the shortest path should end.</param>
		/// <returns>The shortest path, if any.</returns>
		public GraphPath<TNode, TLink> DijkstraShortestPath(int sourceId, int targetId)
		{
			this.EnsureUniqueIdentifiers();

			// if they don't sit in the same component they certainly have no path from one to another
			if (!this.AreInSameComponent(sourceId, targetId)) return null;

			var source = this.FindNode(sourceId);
			var target = this.FindNode(targetId);

			if (source == null)
				throw new Exception("The given source id is not part of the graph.");
			if (target == null)
				throw new Exception("The given target id is is not part of the graph.");

			// note that we use the Ougoing, so it wont work with a setting which ignores the direction
			// Also need to add the target in case it doesnt have outgoing Edges
			var nodesWithOutgoingLinks = this.Nodes.Where(n => n.Outgoing.Any()).ToList();
			if (!nodesWithOutgoingLinks.Contains(target))
				nodesWithOutgoingLinks.Add(target);
			var twigs = new List<Twig<TNode, TLink>>();
			nodesWithOutgoingLinks.ForEach(n => twigs.Add(new Twig<TNode, TLink> { Node = n }));
			var distance = nodesWithOutgoingLinks.ToDictionary(node => node, node => double.PositiveInfinity);

			var targetTwig = twigs.Single(m => m.Node == target);
			distance[source] = 0;
			var localNodes = new List<TNode>(nodesWithOutgoingLinks);

			while (localNodes.Count > 0)
			{
				// Return and remove best vertex (that is, connection with minimum distance
				var minNode = localNodes.OrderBy(n => distance[n]).First();
				localNodes.Remove(minNode);

				// Loop all connected nodes
				foreach (var edge in minNode.Outgoing)
				{
					var neighbor = edge.Sink;

					// The positive distance between node and it's neighbor, added to the distance of the current node
					var dist = distance[minNode] + 1;
					if (dist >= distance[neighbor]) continue;
					distance[neighbor] = dist;
					twigs.Single(t => t.Node == neighbor).Edge = edge;
				}

				// If we're at the target node, break
				if (minNode == target) break;
			}

			// Construct a list containing the complete path. We'll start by looking at the previous node of the target and then making our way to the beginning.
			// We'll reverse it to get a source->target list instead of the other way around. The source node is manually added.
			var path = new GraphPath<TNode, TLink>();

			var runner = targetTwig;
			path.AddNode(target);
            while (runner.Edge != null)
			{
                path.AddNode(runner.Edge.Source);
                path.AddEdge(runner.Edge);
                runner = twigs.Single(m => m.Node == runner.Edge.Source);
			}

			// it's bottomup, so let's reverse
			path.Reverse();
			return path.PathLength > 1 ? path : null;
		}

		/// <summary>
		/// Finds the longest path in this (directed acyclic) graph.
		/// </summary>
		/// <returns>A list of identifiers corresponding to the path, or <c>null</c> if the graph has cycles.</returns>
		public GraphPath<TNode, TLink> FindLongestPath()
		{
			var toposort = this.TopologicalSort();

			// a graph with a cycle cannot be topologically sorted and, hence, no longest path exists
			if (toposort == null) return null;

			var dic = this.Nodes.ToDictionary(node => new Twig<TNode, TLink> { Node = node }, node => 0);
			var links = new List<TLink>();
			foreach (var index in toposort)
			{
				var source = this.FindNode(index);
				foreach (var edge in source.Outgoing)
				{
					var target = edge.Sink;
					var keySource = dic.Keys.SingleOrDefault(k => k.Node == source);
					var keyTarget = dic.Keys.SingleOrDefault(k => k.Node == target);
					if (keySource != null && keyTarget != null)
					{
						var lengthSource = dic[keySource];
						var lengthTarget = dic[keyTarget];

						// here the weight is +1 but one could generalize easily
						if (lengthTarget <= lengthSource + 1)
						{
							links.Add(edge);
							dic[keyTarget] = lengthSource + 1;
							keyTarget.Edge = edge;
						}
					}
				}
			}

			var maxlength = dic.Max(kv => kv.Value);
			var endnode = dic.First(kv => kv.Value == maxlength).Key;
			var path = new GraphPath<TNode, TLink>();
			path.AddNode(endnode.Node);
			var runner = endnode;
            while (runner.Edge != null)
			{
                path.AddNode(runner.Edge.Source);
                path.AddEdge(runner.Edge);
                runner = dic.Keys.Single(m => m.Node == runner.Edge.Source);
			}

			// it's bottomup, so let's reverse
			path.Reverse();

			return path;
		}

		/// <summary>
		/// Iteratively assigns a component listIndex to the connected nodes of the given node.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>Initially the Indices collection needs to
		/// be initialized with -1 to set the nodes which haven't been
		/// visited.</description>
		/// </item>
		/// <item>
		/// <description>The visiting process is really a DFT of the connected nodes
		/// starting from a given node and keeping track of the visited item via the
		/// assigned component map.</description>
		/// </item>
		/// </list>
		/// </remarks>
		/// <param name="componentMap">
		/// The indices is the list of component indices mapped to the node indices.
		/// </param>
		/// <param name="listIndex">
		/// The node id being assigned currently.
		/// </param>
		/// <param name="componentIndex">
		/// Index of the current component.
		/// </param>
		private void AssignConnectedComponent(IDictionary<int, int> componentMap, int listIndex, int componentIndex)
		{
			var node = this.Nodes[listIndex];
			componentMap[node.Id] = componentIndex;

			// take the neighbor nodes and iterate
			foreach (var unvisitedNode in node.AllLinks.Select(edge => edge.GetComplementaryNode(node)).Where(n => componentMap[n.Id] == -1)) this.AssignConnectedComponent(componentMap, this.Nodes.IndexOf(unvisitedNode), componentIndex);
		}

		/// <summary>
		/// Iterative function helping with the topological sort, see the public overload of TopologicalSort.
		/// </summary>
		/// <param name="nodeId">The current node id.</param>
		/// <param name="result">The result of the sorting (up to this point).</param>
		/// <param name="handledSequence">The handled sequence.</param>
		/// <param name="visitSequence">The visit sequence.</param>
		/// <param name="handledCounter">The handled counter.</param>
		/// <returns></returns>
		private bool TopologicalSort(int nodeId, ICollection<int> result, IDictionary<int, int> handledSequence, ref Dictionary<int, int> visitSequence, ref int handledCounter)
		{
			visitSequence[nodeId] = handledCounter++;
			var node = this.FindNode(nodeId);

			foreach (var sinkNodeId in node.Outgoing.Select(edge => edge.Sink).Select(sinkNode => sinkNode.Id))
			{
				if (visitSequence.ContainsKey(sinkNodeId) && visitSequence[sinkNodeId] == -1)
				{
					// not yet visited
					var hasnoloops = this.TopologicalSort(
						sinkNodeId,
						result,
						handledSequence,
						ref visitSequence,
						ref handledCounter);
					if (!hasnoloops) return false;
				}
				else if (handledSequence.ContainsKey(sinkNodeId) && handledSequence[sinkNodeId] == -1)
				{
					// ai, a loop
					return false;
				}
				else
				{
					// just a revisit but no loop
				}
			}

			result.Add(nodeId);
			handledSequence[nodeId] = handledCounter++;
			return true;
		}
	}
}