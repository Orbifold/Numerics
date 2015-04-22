namespace Orbifold.Numerics
{
	/// <summary>
	/// Data bucket similar to a <see cref="TreeNode{TNode}"/> but based on a link rather than a parent property.
	/// </summary>
	/// <typeparam name="TNode">The type of the node.</typeparam>
	/// <typeparam name="TLink">The type of the link.</typeparam>
	public class Twig<TNode, TLink>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Twig&lt;TNode, TLink&gt;"/> class.
		/// </summary>
		public Twig()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Twig&lt;TNode, TLink&gt;"/> class.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="link">The link.</param>
		public Twig(TNode node, TLink link)
		{
			this.Node = node;
            this.Edge = link;
		}

		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>
		/// The node.
		/// </value>
		public TNode Node { get; set; }

		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		public TLink Edge { get; set; }
	}
}