#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System.Linq;

namespace Orbifold.Numerics
{
    public class GiniImpurity : Impurity
    {
        public override double Calculate(Vector x)
        {
            return 1 - x.ToArray().GroupBy(i => i).Map(input => input.Count() / (double)x.Dimension).Sum(d => d * d);
        }
    }
}