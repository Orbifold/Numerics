namespace Orbifold.Numerics
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// The AVL Tree data structure.
	/// </summary>
	/// <typeparam name="TData">
	/// </typeparam>
	/// <remarks>
	/// See http://en.wikipedia.org/wiki/AVL_tree .
	/// </remarks>
	public class AVLTree<TData> : BinarySearchTreeBase<TData>
		where TData : IComparable
	{
		public AVLTree()
		{

		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AVLTree&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="comparer">The comparer.</param>
		public AVLTree(IComparer<TData> comparer) : base(comparer)
		{
			
		}
		/// <summary>
		/// Returns the AVL Node of the tree.
		/// </summary>
		public BinaryTree<TData> Root
		{
			get
			{
				return base.Tree;
			}
		}

		public BinaryTree<TData> Find(TData value)
		{
			return this.FindNode(value);
		}
		protected override void AddItem(TData value)
		{
			var node = new BinaryTree<TData>(value);
			int result;

			var current = Root;
			BinaryTree<TData> parent = null;
			while (current != null)
			{
				result = Comparer.Compare(current.Data, value);
				if (result == 0)
				{
					parent = current;
					current = current.Left;
				}
				if (result > 0)
				{
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					parent = current;
					current = current.Right;
				}
			}

			 
			if (parent == null)
				Tree = node;
			else
			{
				result = Comparer.Compare(parent.Data, value);
				if (result > 0) parent.Left = node;
				else parent.Right = node;
			}

			 
			 

			// Balance every node going up, starting with the parent
			var parentNode = node.Parent;
			while (parentNode != null)
			{
				var balance = this.GetBalance(parentNode);
				/*-2 or 2 is unbalanced*/
				if (System.Math.Abs(balance) == 2) this.BalanceAt(parentNode, balance);
				parentNode = parentNode.Parent;
			}
		}

		protected override bool RemoveItem(TData item)
		{
			var valueNode = this.FindNode(item); 
			var parentNode = valueNode.Parent;
			while (parentNode != null)
			{
				var balance = this.GetBalance(parentNode);
				if (System.Math.Abs(balance) == 1) break;
				if (System.Math.Abs(balance) == 2) this.BalanceAt(parentNode, balance);
				parentNode = parentNode.Parent;
			}
			return true;
		}

		 

		/// <summary>
		/// Balances an AVL Tree node.
		/// </summary>
		/// <param name="node">
		/// The node.
		/// </param>
		/// <param name="balance">
		/// The balance.
		/// </param>
		protected virtual void BalanceAt(BinaryTree<TData> node, int balance)
		{
			switch (balance)
			{
				case 2:
					{
						var rightBalance = this.GetBalance(node.Right);
						switch (rightBalance)
						{
							case 0:
							case 1:
								this.RotateLeft(node);
								break;
							case -1:
								this.RotateRight(node.Right);
								this.RotateLeft(node);
								break;
						}
					}
					break;
				case -2:
					{
						var leftBalance = this.GetBalance(node.Left);
						if (leftBalance == 1)
						{
							this.RotateLeft(node.Left);
							this.RotateRight(node);
						}
						else if (leftBalance == -1 || leftBalance == 0)
						{
							this.RotateRight(node);
						}
					}
					break;
			}
		}

		/// <summary>
		/// Returns the right height minus the left height of the binary branch.
		/// </summary>
		/// <param name="root">
		/// The node for which the balance needs to be returned.
		/// </param>
		/// <returns>
		/// The balance at the given node.
		/// </returns>
		protected virtual int GetBalance(BinaryTree<TData> root)
		{
			// need to +1 the Height since it's zero-based; a leaf node has height zero but as a balance it counts one.
			return (root.Right == null ? 0 : root.Right.Height+1) - (root.Left == null ? 0 : root.Left.Height+1);
		}

		/// <summary>
		/// Rotates a node to the left within an AVL Tree.
		/// </summary>
		/// <remarks>The pivot node becomes the root if the given node was the root before.</remarks>
		/// <param name="node">
		/// The node.
		/// </param>
		protected virtual void RotateLeft(BinaryTree<TData> node)
		{
			if (node == null) return;
			var pivot = node.Right;
			if (pivot == null) return;
			var nodeParent = node.Parent;
			var parentNodeIsLeft = (nodeParent != null) && nodeParent.Left == node;

			// Rotate
			node.Right = pivot.Left;
			pivot.Left = node;

			// Update parents
			node.Parent = pivot;
			pivot.Parent = nodeParent;

			if (node.Right != null) node.Right.Parent = node;

			// Update the original parent's child node
			if (parentNodeIsLeft) nodeParent.Left = pivot;
			else if (nodeParent != null) nodeParent.Right = pivot;
			if (Tree == node) Tree = pivot;
		}

		/// <summary>
		/// Rotates a node to the right within an AVL Tree.
		/// </summary>
		/// <remarks>The pivot node becomes the root if the given node was the root before.</remarks>
		/// <param name="node">
		/// The node.
		/// </param>
		protected virtual void RotateRight(BinaryTree<TData> node)
		{
			if (node == null) return;
			var pivot = node.Left;
			if (pivot == null) return;
			var nodeParent = node.Parent;
			var nodeParentIsLeft = (nodeParent != null) && nodeParent.Left == node;

			// Rotate
			node.Left = pivot.Right;
			pivot.Right = node;

			// Update parents
			node.Parent = pivot;
			pivot.Parent = nodeParent;

			if (node.Left != null) node.Left.Parent = node;

			if (nodeParentIsLeft) nodeParent.Left = pivot;
			else if (nodeParent != null) nodeParent.Right = pivot;
			if (Tree == node) Tree = pivot;
		}
	}
}