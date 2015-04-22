namespace Orbifold.Numerics
{
	/// <summary>
	/// Graph analysis link description.
	/// </summary>
	/// <typeparam name="TNode">
	/// The type of the node.
	/// </typeparam>
	/// <typeparam name="TLink">
	/// The type of the link.
	/// </typeparam>
	/// <seealso cref="Edge{TNodeData,TLinkData}"/>
	/// <seealso cref="Node{TNodeData,TLinkData}"/>
	public interface IEdge<TNode, TLink>
		where TLink : IEdge<TNode, TLink>
		where TNode : INode<TNode, TLink>
	{
		/// <summary>
		/// Gets or sets the identifier of this link.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		int Id { get; set; }

		/// <summary>
		/// Gets or sets the sink of this link, i.e. the node at the end of the link.
		/// </summary>
		/// <value>
		/// The sink node.
		/// </value>
		TNode Sink { get; set; }

		/// <summary>
		/// Gets or sets the source of this link, i.e. the node at the beginning of the link.
		/// </summary>
		/// <value>
		/// The source.
		/// </value>
		TNode Source { get; set; }

		/// <summary>
		/// Gets the weight of this link.
		/// </summary>
		double Weight { get; set; }

		/// <summary>
		/// Returns a shallow copy of this link.
		/// </summary>
		/// <returns>
		/// </returns>
		TLink Clone();

		/// <summary>
		/// Gets the other node of this link.
		/// </summary>
		/// <param name="node">
		/// The complementary node which defines this link.
		/// </param>
		/// <returns>
		/// </returns>
		TNode GetComplementaryNode(INode<TNode, TLink> node);

		/// <summary>
		/// Inverts this link by changing its direction.
		/// </summary>
		void Reverse();
	}
}