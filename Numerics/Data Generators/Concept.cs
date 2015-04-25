
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Basic name-description data bucket.
	/// </summary>
    public class Concept
    {
		/// <summary>
		/// Gets or sets the name of the concept.
		/// </summary>
		/// <value>The name.</value>
        public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the concept.
		/// </summary>
		/// <value>The description.</value>
        public string Description { get; set; }
    }
	
}