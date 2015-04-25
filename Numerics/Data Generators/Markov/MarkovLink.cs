using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// An element of a Markov chain.
	/// </summary>
	/// <typeparam name="T">link type</typeparam>
	internal class MarkovLink<T>
	{
		readonly T data;
		int count;
		// following links
		readonly Dictionary<T, MarkovLink<T>> links;

		/// <summary>
		/// create a new link
		/// </summary>
		/// <param name="data">value of the item in sequence</param>
		internal MarkovLink(T data)
		{
			this.data = data;
			this.count = 0;

			this.links = new Dictionary<T, MarkovLink<T>>();
		}

		/// <summary>
		/// process the input in window sized chunks
		/// </summary>
		/// <param name="input">the sample set</param>
		/// <param name="length">size of sequence window</param>
		public void Process(IEnumerable<T> input, int length)
		{
			// holds the current window
			var window = new Queue<T>(length);

			// process the input, a window at a time (overlapping)
			foreach (var part in input)
			{
				if (window.Count == length)
					window.Dequeue();
				window.Enqueue(part);

				this.ProcessWindow(window);
			}
		}

		/// <summary>
		/// process the window to construct the chain
		/// </summary>
		/// <param name="window"></param>
		private void ProcessWindow(IEnumerable<T> window)
		{
			var link = this;
			link = window.Aggregate(link, (current, part) => current.Process(part));
		}

		/// <summary>
		/// process an item following us
		/// keep track of how many times
		/// we are followed by each item
		/// </summary>
		/// <param name="part"></param>
		/// <returns></returns>
		internal MarkovLink<T> Process(T part)
		{
			var link = this.Find(part);

			// not been followed by this
			// item before
			if (link == null)
			{
				link = new MarkovLink<T>(part);
				this.links.Add(part, link);
			}

			link.Seen();

			return link;
		}

		private void Seen()
		{
			this.count++;
		}

		public T Data
		{
			get
			{
				return this.data;
			}
		}

		public int Occurances
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>
		/// Total number of incidences after this link
		/// </summary>
		public int ChildOccurances
		{
			get
			{
				// sum all followers occurances
				var result = this.links.Sum(link => link.Value.Occurances);

				return result;
			}
		}

		public override string ToString()
		{
			return String.Format("{0} ({1})", this.data, this.count);
		}

		/// <summary>
		/// find a follower of this link
		/// </summary>
		/// <param name="follower">The follower.</param>
		/// <returns></returns>
		private MarkovLink<T> Find(T follower)
		{
			MarkovLink<T> markovLink = null;

			if (this.links.ContainsKey(follower))
				markovLink = this.links[follower];

			return markovLink;
		}

		static readonly Random rand = new Random();
		/// <summary>
		/// select a random follower weighted
		/// towards followers that followed us
		/// more often in the sample set
		/// </summary>
		/// <returns></returns>
		public MarkovLink<T> SelectRandomLink()
		{
			MarkovLink<T> markovLink = null;

			var universe = this.ChildOccurances;

			// select a random probability
			var rnd = rand.Next(1, universe + 1);

			// match the probability by treating
			// the followers as bands of probability
			var total = 0;
			foreach (var child in this.links.Values)
			{
				total += child.Occurances;

				if (total >= rnd)
				{
					markovLink = child;
					break;
				}
			}

			return markovLink;
		}

		/// <summary>
		/// find a window of followers that
		/// are after this link, returns where
		/// the last link if found, or null if
		/// this window never occured after this link
		/// </summary>
		/// <param name="window">the sequence to look for</param>
		/// <returns></returns>
		private MarkovLink<T> Find(IEnumerable<T> window)
		{
			var link = this;

			foreach (var part in window)
			{
				link = link.Find(part);

				if (link == null)
					break;
			}

			return link;
		}
		
		/// <summary>
		/// a generated set of followers based
		/// on the likelyhood of sequence steps
		/// seen in the sample data
		/// </summary>
		/// <param name="start">a seed value to start the sequence with</param>
		/// <param name="length">how bug a window to use for sequence steps</param>
		/// <param name="max">maximum size of the set produced</param>
		/// <returns></returns>
		internal IEnumerable<MarkovLink<T>> Generate(T start, int length, int max)
		{
			var window = new Queue<T>(length);

			window.Enqueue(start);

			for (var link = this.Find(window); link != null && max != 0; link = this.Find(window), max--)
			{
				var next = link.SelectRandomLink();

				yield return link;

				if (window.Count == length - 1)
					window.Dequeue();
				if (next != null)
					window.Enqueue(next.Data);
			}
		}

	}
}