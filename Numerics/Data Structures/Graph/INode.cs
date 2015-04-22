using System.Collections.Generic;
using System.Windows;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Graph analysis node description.
	/// </summary>
	/// <typeparam name="TNode">
	/// The type of the node.
	/// </typeparam>
	/// <typeparam name="TLink">
	/// The type of the link.
	/// </typeparam>
	/// <seealso cref="Edge{TNode,TLink}"/>
	/// <seealso cref="Node{TNode,TLink}"/>
	public interface INode<TNode, TLink>
		where TLink : IEdge<TNode, TLink>
		where TNode : INode<TNode, TLink>
	{
		/// <summary>
		/// Gets all links bounds to this node.
		/// </summary>
		/// <value>
		/// All links.
		/// </value>
		IList<TLink> AllLinks { get; }

		/// <summary>
		/// Gets or sets the bounding rectangle.
		/// </summary>
		/// <remarks>
		/// This defines the location as well as the size of the shape as a result of a layout process.
		/// </remarks>
		/// <value>
		/// The bounding rectangle.
		/// </value>
		Rect BoundingRectangle { get; set; }

		/// <summary>
		/// Gets or sets the (supposed unique) identifier.
		/// </summary>
		/// <value>
		/// The identifier of this node.
		/// </value>
		int Id { get; set; }

		/// <summary>
		/// Returns the total number of links attached.
		/// </summary>
		/// <seealso cref="AllLinks"/>
		int Degree { get; }

		/// <summary>
		/// Gets the incoming links, i.e. the links towards this node.
		/// </summary>
		/// <value>
		/// The incoming links.
		/// </value>
		IList<TLink> Incoming { get; }

		/// <summary>
		/// Gets or sets whether this node is the root of a tree.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is root; otherwise, <c>false</c>.
		/// </value>
		bool IsRoot { get; set; }

		/// <summary>
		/// Gets the outgoing links, i.e. the links leaving this node.
		/// </summary>
		/// <value>
		/// The outgoing.
		/// </value>
		IList<TLink> Outgoing { get; }

		/// <summary>
		/// Gets or sets a value indicating whether this node is part of directed graph.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is directed; otherwise, <c>false</c>.
		/// </value>
		bool IsDirected { get; set; }

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
		IEnumerable<TNode> Children { get; }

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
		IEnumerable<TNode> Parents { get; }

		/// <summary>
		/// Gets the nodes adjacent to this node, i.e. both the <see cref="Parents"/> and <see cref="Children"/> nodes.
		/// </summary>
		/// <returns>
		/// All the neighbors nodes of this node.
		/// </returns>
		/// <seealso cref="Parents">Parents</seealso>
		/// <seealso cref="Children">Children</seealso>
		IEnumerable<TNode> Neighbors { get; }

		/// <summary>
		/// Returns a shallow copy of this node.
		/// </summary>
		/// <returns>
		/// </returns>
		TNode Clone();

		/// <summary>
		/// Removes a link from this node.
		/// </summary>
		/// <param name="link">
		/// The link.
		/// </param>
		void RemoveLink(TLink link);

		/// <summary>
		/// Adds an incoming link.
		/// </summary>
		/// <param name="edge">
		/// The link to add.
		/// </param>
		void AddIncomingEdge(TLink edge);

		/// <summary>
		/// Adds an outgoing link.
		/// </summary>
		/// <param name="edge">
		/// The link to add.
		/// </param>
		void AddOutgoingEdge(TLink edge);

		/// <summary>
		/// Removes an incoming edge.
		/// </summary>
		/// <param name="edge">
		/// The edge to remove.
		/// </param>
		void RemoveIncomingEdge(TLink edge);

		/// <summary>
		/// Removes the given outgoing edge.
		/// </summary>
		/// <param name="edge">
		/// The edge to remove.
		/// </param>
		void RemoveOutgoingEdge(TLink edge);
	}
}