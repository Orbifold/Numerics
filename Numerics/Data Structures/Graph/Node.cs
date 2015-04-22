using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Default implementation of the <see cref="Node{TNodeData,TLinkData}"/> interface.
	/// </summary>
	/// <typeparam name="TNodeData">The type of the payload.</typeparam>
	/// <typeparam name="TLinkData">The type of the link data.</typeparam>
	/// <seealso cref="Edge{TNodeData,TLinkData}"/>
	public class Node<TNodeData, TLinkData> : INode<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>>
		where TNodeData : new()
		where TLinkData : new()
	{
		/// <summary>
		/// The <see cref="Incoming"/> field.
		/// </summary>
		private IList<Edge<TNodeData, TLinkData>> incoming;

		/// <summary>
		/// The <see cref="Outgoing"/> field.
		/// </summary>
		private IList<Edge<TNodeData, TLinkData>> outgoing;

		/// <summary>
		/// Initializes a new instance of the <see cref="Node{TNodeData,TLinkData}"/> class.
		/// </summary>
		public Node()
		{
			this.incoming = new List<Edge<TNodeData, TLinkData>>();
			this.outgoing = new List<Edge<TNodeData, TLinkData>>();
			this.AllLinks = new List<Edge<TNodeData, TLinkData>>();
			this.Data = new TNodeData();

			// This default corresponds to the IsDirected setting of the Graph.
			this.IsDirected = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Node{TNodeData,TLinkData}"/> class.
		/// </summary>
		/// <param name="shape">
		/// The shape this node is based on. Usually this a visual item and this node is a proxy used to
		/// perform graph analysis. If no element is associated the shape can be set to null.
		/// </param>
		public Node(object shape)
			: this()
		{
			this.AssociatedObject = shape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Node{TNodeData,TLinkData}"/> class.
		/// </summary>
		/// <param name="shape">The shape.</param>
		/// <param name="isDirected">If set to <c>true</c> [is directed].</param>
		public Node(object shape, bool isDirected)
			: this(shape)
		{
			this.IsDirected = isDirected;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Node{TNodeData,TLinkData}"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="isDirected">If set to <c>true</c> the links are considered as directed and <see cref="AllLinks"/> is the same as the <see cref="Outgoing"/> or <see cref="Incoming"/> collections.</param>
		public Node(int id, bool isDirected)
			: this()
		{
			this.Id = id;
			this.IsDirected = isDirected;
		}

		/// <summary>
		/// Gets or sets the data or payload carried by this node.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public TNodeData Data { get; set; }

		/// <summary>
		/// Get the shape associated with this layout shape.
		/// </summary>
        public object AssociatedObject { get; protected set; }

		/// <summary>
		/// Gets or sets the geometric center of this ControlShape.
		/// </summary>
		public Point Center { get; set; }

		/// <summary>
		/// Returns the total number of links attached.
		/// </summary>
		/// <seealso cref="AllLinks"/>
		public int Degree
		{
			get
			{
				return this.AllLinks.Count;
			}
		}

		/// <summary>
		/// Gets or sets whether this node is a tree-root.
		/// </summary>
		/// <remarks>This property only makes sense in a tree-context. Use a spanning tree algorithm to extract a tree from a generic graph if necessary.</remarks>
		/// <value>
		///   <c>true</c> if this instance is root; otherwise, <c>false</c>.
		/// </value>
		public bool IsRoot { get; set; }

		/// <summary>
		/// Gets all the links of this node.
		/// </summary>
		public IList<Edge<TNodeData, TLinkData>> AllLinks { get; private set; }

		/// <summary>
		/// Gets or sets the bounding rectangle of the shape.
		/// </summary>
		public Rect BoundingRectangle { get; set; }

		/// <summary>
		/// Gets or sets Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Get the node's incoming links.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>If the node is directed (i.e. <c>IsDirected = true</c>) then this collection is the same as the <see cref="AllLinks">AllLinks</see> collection.</description></item>
		/// <item>
		/// <description>Do not add links to this collection but use the <see cref="Graph{TNode,TLink}.AddEdge(TNode,TNode)"/> method in
		/// order to update the related properties (<see cref="AllLinks"/>,
		/// <see cref="Outgoing"/>...).</description></item></list>
		/// </remarks>
		/// <seealso cref="Graph{TNode,TLink}.AddEdge(TNode,TNode)"/>
		/// <seealso cref="Outgoing"/>
		public IList<Edge<TNodeData, TLinkData>> Incoming
		{
			get
			{
				return this.IsDirected ? this.incoming : this.AllLinks;
			}
			protected set
			{
				this.incoming = value;
			}
		}

		/// <summary>
		/// Get the node's outgoing links.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>If the node is directed (i.e. <c>IsDirected = true</c>) then this collection is the same as the <see cref="AllLinks">AllLinks</see> collection.</description></item>
		/// <item>
		/// <description>Do not add links to this collection but use the <see
		/// cref="Graph{TNode,TLink}.AddEdge(TNode,TNode)">Graph{TNode,TLink}.AddEdge(TNode,TNode)</see>
		/// method in order to update the related properties (<see
		/// cref="AllLinks">AllLinks</see>, <see
		/// cref="Outgoing">Outgoing</see>...).</description></item></list>
		/// </remarks>
		/// <seealso
		/// cref="Graph{TNode,TLink}.AddEdge(TNode,TNode)">AddEdge(TNode,TNode)</seealso>
		/// <seealso cref="Incoming">Incoming</seealso>
		public IList<Edge<TNodeData, TLinkData>> Outgoing
		{
			get
			{
				return this.IsDirected ? this.outgoing : this.AllLinks;
			}

			set
			{
				this.outgoing = value;
			}
		}

		/// <summary>
		/// Gets whether this node's links are directed.
		/// </summary>
		/// <seealso cref="Graph{TNode,TLink}.IsDirected"/>
		/// <value>
		/// 	<c>true</c> if this instance is directed; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirected { get; set; }

		/// <summary>
		/// Gets the children nodes attached to this node.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>A child is defined as the opposite node from an edge starting at this node.</description></item>
		/// <item>
		/// <description>If the graph is not directed this will return the same collection
		/// as the <see cref="Children"/> and the <see cref="Neighbors"/> property, i.e. all the nodes attached to the
		/// this node.</description></item></list>
		/// </remarks>
		/// <returns>The children collection.</returns>
		/// <seealso cref="Parents"/>
		/// <seealso cref="Neighbors"/>
		public IEnumerable<Node<TNodeData, TLinkData>> Children
		{
			get
			{
				return this.IsDirected ? this.Outgoing.Select(edge => edge.GetOppositeNode(this)) : this.Neighbors;
			}
		}

		/// <summary>
		/// Gets the parent nodes attached to this node.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>A parent is defined as the opposite node from an edge ending at
		/// this node.</description></item>
		/// <item>
		/// <description>If the graph is not directed this will return the same collection
		/// as the <see cref="Children"/> and the <see cref="Neighbors"/> property, i.e. all the nodes attached to the
		/// this node.</description></item></list>
		/// </remarks>
		/// <value>
		/// The parent collection.
		/// </value>
		/// <seealso cref="Neighbors">Neighbors</seealso>
		/// <seealso cref="Children">Children</seealso>
		public IEnumerable<Node<TNodeData, TLinkData>> Parents
		{
			get
			{
				return this.IsDirected ? this.Incoming.Select(edge => edge.GetOppositeNode(this)) : this.Neighbors;
			}
		}

		/// <summary>
		/// Gets the nodes adjacent to this node, i.e. both the <see cref="Parents"/> and <see cref="Children"/> nodes.
		/// </summary>
		/// <returns>
		/// All the neighbors nodes of this node.
		/// </returns>
		/// <seealso cref="Parents">Parents</seealso>
		/// <seealso cref="Children">Children</seealso>
		public IEnumerable<Node<TNodeData, TLinkData>> Neighbors
		{
			get
			{
				var collection = new List<Node<TNodeData, TLinkData>>();
				foreach (var node in this.Outgoing.Union(this.Incoming).Select(edge => edge.GetOppositeNode(this)).Where(node => !collection.Contains(node))) collection.Add(node);
				return collection;
			}
		}

		/// <summary>
		/// Returns a (shallow) copy of this node.
		/// </summary>
		/// <returns>
		/// Returns a copy of this node, including the references of incoming and outgoing edges.
		/// It does not however clone beyond these collections.
		/// </returns>
		public Node<TNodeData, TLinkData> Clone()
		{
			var copy = new Node<TNodeData, TLinkData>
				{
					Id = -1,
                    AssociatedObject = this.AssociatedObject,
					Center = this.Center,
					IsDirected = this.IsDirected
				};
			foreach (var item in this.Incoming) copy.AddIncomingEdge(item);
			foreach (var item in this.Outgoing) copy.AddOutgoingEdge(item);
			return copy;
		}

		/// <summary>
		/// Removes a link.
		/// </summary>
		/// <param name="link">
		/// The link.
		/// </param>
		public void RemoveLink(Edge<TNodeData, TLinkData> link)
		{
			if (link == null) throw new ArgumentNullException("link");

			// has to be one or the other and neither will raise an error if not part of the collection
			this.RemoveIncomingEdge(link);
			this.RemoveOutgoingEdge(link);
		}

		/// <summary>
		/// Adds an incoming link.
		/// </summary>
		/// <param name="edge">
		/// The link to add.
		/// </param>
		public void AddIncomingEdge(Edge<TNodeData, TLinkData> edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			this.incoming.Add(edge);
			this.AllLinks.Add(edge);
		}

		/// <summary>
		/// Adds an outgoing link.
		/// </summary>
		/// <param name="edge">
		/// The link to add.
		/// </param>
		public void AddOutgoingEdge(Edge<TNodeData, TLinkData> edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			this.outgoing.Add(edge);
			this.AllLinks.Add(edge);
		}

		/// <summary>
		/// Removes an incoming edge.
		/// </summary>
		/// <param name="edge">
		/// The edge to remove.
		/// </param>
		public void RemoveIncomingEdge(Edge<TNodeData, TLinkData> edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			if (this.incoming.Contains(edge))
			{
				this.incoming.Remove(edge);
				this.AllLinks.Remove(edge);
			}
		}

		/// <summary>
		/// Removes the given outgoing edge.
		/// </summary>
		/// <param name="edge">
		/// The edge to remove.
		/// </param>
		public void RemoveOutgoingEdge(Edge<TNodeData, TLinkData> edge)
		{
			if (this.outgoing.Contains(edge))
			{
				this.outgoing.Remove(edge);
				this.AllLinks.Remove(edge);
			}
		}
		public override string ToString()
		{
			return string.Format("#{0}", this.Id);
		}		
	}
}