#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System;
using System.Linq;

namespace Orbifold.Numerics
{
    public class ShannonImpurity : Impurity
    {
        public override double Calculate(Vector x)
        {
            var ar = x.ToArray();
            var px = from i in ar.Distinct() let q = (from j in ar where i.IsEqualTo(j) select j).Count() select q / (double)x.Dimension;
            var e = (from p in px select -1 * p * Math.Log(p, 2)).Sum();
            return e;
        }
    }
}