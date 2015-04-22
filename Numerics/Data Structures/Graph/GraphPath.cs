using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A path consists of a series of adjacent links.
	/// </summary>
	public class GraphPath<TNode, TLink>
		where TNode : new()
		where TLink : new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GraphPath&lt;TNode, TLink&gt;"/> class.
		/// </summary>
		public GraphPath()
		{
			this.Nodes = new List<TNode>();
			this.Links = new List<TLink>();
			this.Elements = new List<object>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphPath&lt;TNode, TLink&gt;"/> class.
		/// </summary>
		/// <param name="path">The path from which a clone will be taken as a starting point for this path.</param>
		public GraphPath(GraphPath<TNode, TLink> path)
		{
			this.Nodes = GraphExtensions.Clone(path.Nodes).ToList();
			this.Links = GraphExtensions.Clone(path.Links);
			this.Elements = GraphExtensions.Clone(path.Elements).ToList();
		}

		/// <summary>
		/// Gets or sets the nodes of this path.
		/// </summary>
		/// <value>
		/// The nodes this path is made of.
		/// </value>
		public List<TNode> Nodes { get; private set; }

		/// <summary>
		/// Gets length of this path (i.e. the amount of links).
		/// </summary>
		public int PathLength
		{
			get
			{
				return this.Links.Count;
			}
		}

		/// <summary>
		/// Gets or sets the collection of nodes and links this path is made of.
		/// </summary>
		public List<object> Elements { get; set; }

		/// <summary>
		/// Gets or sets the links of this path.
		/// </summary>
		/// <value>
		/// The links this path is made of.
		/// </value>
		public IList<TLink> Links { get; set; }

		/// <summary>
		/// Adds a links and a node to this path.
		/// </summary>
		/// <param name="edge">
		/// The layout edge.
		/// </param>
		/// <param name="node">
		/// The node.
		/// </param>
		public void Add(TLink edge, TNode node)
		{
			this.Links.Add(edge);
			this.Elements.Add(edge);
			this.Nodes.Add(node);
			this.Elements.Add(node);
		}

		/// <summary>
		/// Adds a edge to this path.
		/// </summary>
		/// <param name="edge">The edge.</param>
		public void AddEdge(TLink edge)
		{
			this.Links.Add(edge);
			this.Elements.Add(edge);
		}

		/// <summary>
		/// Adds a node to this path.
		/// </summary>
		/// <param name="node">
		/// The node.
		/// </param>
		public void AddNode(TNode node)
		{
			this.Nodes.Add(node);
			this.Elements.Add(node);
		}

		/// <summary>
		/// Reverses the nodes sequence.
		/// </summary>
		public void Reverse()
		{
			this.Nodes.Reverse();
		}
	}
}