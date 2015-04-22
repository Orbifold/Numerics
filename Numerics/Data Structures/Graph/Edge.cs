using System;
using System.Collections.Generic;
using System.Windows;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Generic implementation of the <see cref="IEdge{TNode,TLink}"/> interface.
	/// </summary>
	/// <seealso cref="Node{TNodeData,TLinkData}"/>
	public class Edge<TNodeData, TLinkData> : IEdge<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>>
		where TNodeData : new()
		where TLinkData : new()
	{
		/// <summary>
		/// The <see cref="IsReversed"/> field.
		/// </summary>
		private bool reversed;

		/// <summary>
		/// Initializes a new instance of the <see cref="Edge{TNodeData,TLinkData}"/> class.
		/// </summary>
		public Edge()
		{
			this.SegmentIndex = -1;
			this.Weight = 1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Edge&lt;TNodeData, TLinkData&gt;"/> class.
		/// </summary>
        /// <param name="associatedObject">The connection on which this link is based.</param>
        public Edge(object associatedObject)
			: this()
		{
            if (associatedObject == null) throw new ArgumentNullException("associatedObject");
            this.AssociatedObject = associatedObject;
		}

		/// <summary>
		/// Gets or sets the number of virtual nodes which have been inserted during the process of breaking layer-crossing links.
		/// </summary>
		public int NumberOfVirtualNodes { get; set; }

		/// <summary>
		/// Gets wheter link has been reversed (using the <see cref="Reverse"/> method).
		/// </summary>
		public bool IsReversed
		{
			get
			{
				return this.reversed;
			}
		}

		/// <summary>
		/// Gets the associated object.
		/// This is usually the visual element, if any, from which this proxy is created.
		/// </summary>
		public object AssociatedObject { get; private set; }

        /// <summary>
        /// Gets or sets the payload of this edge.
        /// </summary>
        public TLinkData Data { get; set; }
		/// <summary>
		/// Gets or sets the points defining the link.
		/// </summary>
		/// <remarks>This also defines eventually the <see cref="IConnection"/> visual if it's a polyline or some other multi-point visual.</remarks>
		public IList<Point> Points { get; set; }

		/// <summary>
		/// Gets or sets the dictionary of runtime/layout properties.
		/// </summary>
		public Dictionary<object, object> PropertyBag { get; set; }

		/// <summary>
		/// Gets or sets index of the segment.
		/// </summary>
		public int SegmentIndex { get; set; }

		/// <summary>
		/// Gets the weight of this link.
		/// </summary>
		public double Weight { get; set; }

		/// <summary>
		/// Gets or sets identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets the destination (target, end) node of this link.
		/// </summary>
		public Node<TNodeData, TLinkData> Sink { get; set; }

		/// <summary>
		/// Gets the source (origin, start) node of this link.
		/// </summary>
		public Node<TNodeData, TLinkData> Source { get; set; }

		/// <summary>
		/// Gets or sets whether this is an outgoing link.
		/// </summary>
		internal bool OutgoingLink { get; set; }

		/// <summary>
		/// Returns a (shallow) clone of this link.
		/// </summary>
		/// <remarks>
		/// The following properties are being cloned: 
		/// <list type="bullet">
		/// <item>
		/// <description>The identifier (<see cref="Id">Id</see>)</description></item>
		/// <item>
		/// <description>The weight (<see cref="Weight">Weight</see>)</description></item>
		/// <item>
		/// <description>The segment index (<see
		/// cref="SegmentIndex">SegmentIndex</see>)</description></item>
		/// <item>
		/// <description>The points (<see
		/// cref="Points">Points</see>).</description></item></list>
		/// </remarks>
		/// <returns>
		/// A clone of this link.
		/// </returns>
		public Edge<TNodeData, TLinkData> Clone()
		{
			return new Edge<TNodeData, TLinkData>
				{
					Id = this.Id,
					Weight = this.Weight,
					SegmentIndex = this.SegmentIndex,
					Points = this.Points
				};
		}

		/// <summary>
		/// Returns the opposite or complementary node of the given one.
		/// </summary>
		/// <param name="node">
		/// The node whose complement is looked for.
		/// </param>
		/// <returns>
		/// <c>null</c> if the node is not part of this link, otherwise the opposite or complementary node with respect to this link.
		/// </returns>
		public Node<TNodeData, TLinkData> GetComplementaryNode(INode<Node<TNodeData, TLinkData>, Edge<TNodeData, TLinkData>> node)
		{
			if (this.Source != node && this.Sink != node) return null;
			return this.Source == node ? this.Sink : this.Source;
		}

		/// <summary>
		/// Gets the bounding rectangle of this entity.
		/// </summary>
		/// <returns>
		/// </returns>
		public Rect GetBounds()
		{
			var pt1 = this.Points[0];
			var pt2 = this.Points[this.Points.Count - 1];

			var l = System.Math.Min(pt1.X, pt2.X);
			var r = System.Math.Max(pt1.X, pt2.X);
			var t = System.Math.Min(pt1.Y, pt2.Y);
			var b = System.Math.Max(pt1.Y, pt2.Y);

			// calculate the bounds of the bounding rect
			for (var i = 1; i < this.Points.Count - 1; ++i)
			{
				l = System.Math.Min(l, this.Points[i].X);
				t = System.Math.Min(t, this.Points[i].Y);
				r = System.Math.Max(r, this.Points[i].X);
				b = System.Math.Max(b, this.Points[i].Y);
			}

			return new Rect(l, t, r - l, b - t);
		}

		/// <summary>
		/// Returns the node at the opposite end of the link.
		/// </summary>
		/// <param name="node">
		/// The a Node.
		/// </param>
		public Node<TNodeData, TLinkData> GetOppositeNode(Node<TNodeData, TLinkData> node)
		{
			return node == this.Sink ? this.Source : this.Sink;
		}

		/// <summary>
		/// Sets the control points (<see cref="Points"/>) of this link.
		/// </summary>
		/// <param name="points">
		/// The points defining this link.
		/// </param>
		public void SetPoints(IEnumerable<Point> points)
		{
			if (this.AssociatedObject == null) return;
			this.Points = new List<Point>(points);
		}

		/// <summary>
		/// Reverses the direction of this link.
		/// </summary>
		public void Reverse()
		{
			this.reversed = !this.reversed;

			// correct the adjacency structure.
			this.Sink.RemoveIncomingEdge(this);
			this.Sink.AddOutgoingEdge(this);
			this.Source.RemoveOutgoingEdge(this);
			this.Source.AddIncomingEdge(this);

			// swap source and sink
			var temp = this.Source;
			this.Source = this.Sink;
			this.Sink = temp;
		}
		public override string ToString()
		{
			return string.Format("{0}->{1}", Source.Id, Sink.Id);
		}
	}
}