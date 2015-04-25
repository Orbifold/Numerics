using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Functional
{
	using System.Diagnostics;

	/// <summary>
	/// Summary description for FunctionalTests
	/// </summary>
	[TestFixture]
	public class FunctionalTests
	{
		private static Random Rand = new Random(Environment.TickCount);

		public FunctionalTests()
		{
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get {
				return testContextInstance;
			}
			set {
				testContextInstance = value;
			}
		}

		#region Additional test attributes

		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//

		#endregion

		[Test]
		[Category("Functional")]
		public void Curry1Test()
		{
			Func<int, int, int, int> func = (a, b, c) => a + b * c;
			var func2 = func.Curry();
			var product = func2(0);
			Assert.AreEqual(6, product(2)(3), "Should be a product now.");

			Func<int, int, int, int, double> func3 = (a, b, c, d) => a + b * c + System.Math.Cos(d);
			var func4 = func3.Curry();
			var cos = func4(0)(0)(0);
			Assert.AreEqual(System.Math.Cos(7), cos(7), "Currying failed here.");
		}

		[Test]
		[Category("Functional")]
		public void PartialTest()
		{
			Func<int, int, int, double> func = (a, b, c) => a + b * System.Math.Cosh(c);
			var func2 = PartialFunction.Partial(func, 0);
			Assert.AreEqual(45 * System.Math.Cosh(77), func2(45, 77), "Partial application failed.");
		}

		[Test]
		[Category("Functional")]
		public void MapTest()
		{
			var list = Range.Create(1, 10).ToFunctionalList();
			var result = list.Map(x => 2 * x);
			var shouldbe = new Range<int>(2, 20, x => x + 2);
			Assert.IsTrue(result.AreSameLists(shouldbe), "The mapping failed.");
		}

		[Test]
		[Category("Functional")]
		public void FoldTest()
		{
			var list = Range.Create(1, 100);
			var sum = list.Fold((a, v) => a + v, 0);
			Assert.AreEqual(5050, sum, "The sum is incorrect.");
		}

		[Test]
		[Category("Functional")]
		public void FoldBackTest()
		{
			var list = Range.Create(0, 10);
			var res = list.FoldList((a, i) => a + i, 5);
			Assert.AreEqual(11, res.Count(), "Not the full list.");
			var shouldbe = new[] { 5, 6, 8, 11, 15, 20, 26, 33, 41, 50, 60 };
			Console.WriteLine(res.ToPrettyFormat());
			Assert.IsTrue(res.AreSameLists(shouldbe));
		}

		[Test]
		[Category("Functional")]
		public void FilterTest()
		{
			var list = Range.Create(1, 100);
			var filtered = list.Filter(i => i <= 50);
			var shouldbe = Range.Create(1, 50);
			Assert.IsTrue(filtered.AreSameLists(shouldbe), "Filtering failed.");
		}

		[Test]
		[Category("Functional")]
		public void RangeCharTest()
		{
			var range = Range.Create('a', 'z');
			Assert.AreEqual(26, range.Count(), "Not all letter are in the range.");
			var inverseRange = Range.Create('Z', 'A');
			Assert.AreEqual(26, inverseRange.Count(), "The range doesnt go back.");
		}

		[Test]
		[Category("Functional")]
		public void RangeDateTest()
		{
			//let's do it a few times over a random date range
			for(var i = 0; i < Rand.Next(10, 20); i++) {
				var month = Rand.Next(1, 12);
				var year = Rand.Next(1900, 2020);
				var from = new DateTime(year, month, 1);
				var to = new DateTime(year, month, DateTime.DaysInMonth(year, month));
				var range = Range.Create(from, to);
				Assert.AreEqual((to - from).TotalDays + 1, range.Count(), "Not the correct amount of days in the range.");
			}
		}

		[Test]
		[Category("Functional")]
		public void RangeToArrayTest()
		{
			var range = Range.Create(0, 100, 2);
			Assert.AreEqual(51, range.Count());
			var arr = range.ToArray();
			Assert.AreEqual(51, arr.Length, "Array is not OK");
			for(int i = 0; i < 51; i++) {
				Assert.AreEqual(range.ElementAt(i), arr[i], "Array at position " + i + " is wrong.");
			}
		}

		[Test]
		[Category("Functional")]
		public void RangeFromVectorTest()
		{
			var v = new Vector(new[]{ 2.1, 3.4, 5.2, 9.7 });
			var range = Range.Create(v);
			Assert.AreEqual(v.Dimension, range.Count(), "Range has incorrect length.");
			for(int i = 0; i < v.Dimension; i++) {
				Assert.AreEqual(v[i], range.ElementAt(i), "Wrong element at position " + i + ".");
			}
		}

		[Test]
		[Category("Functional")]
		public void RangeStepTest()
		{
			var range = Range.Create(0, 100, 2);
			Assert.AreEqual(51, range.Count());
			range = Range.Create(3, 16, 3);
			Assert.AreEqual(5, range.Count());
			range = Range.Create(10, 1);
			Assert.AreEqual(10, range.Count());
			range = Range.Create(2, 1);
			Assert.AreEqual(2, range.Count());
			range = Range.Create(-2, -2);
			Assert.AreEqual(1, range.Count());
			range = Range.Create(-2, -12);
			Assert.AreEqual(11, range.Count());
			var ranged = Range.Create(2d, 12d);
			Assert.AreEqual(11, ranged.Count());
			var rangedate = Range.Create(new DateTime(2012, 1, 1), new DateTime(2012, 1, 28));
			Assert.AreEqual(28, rangedate.Count());
			rangedate = Range.Create(new DateTime(2012, 1, 19), new DateTime(2012, 1, 2));
			Assert.AreEqual(18, rangedate.Count());
		}

		[Test]
		[Category("Functional")]
		public void DropTest()
		{
			var seq = Range.Create(0, 8);
			var s = seq.Drop(-9);
			Assert.IsTrue(s.AreSameLists(seq.RemoveAt(0)));
		}

		[Test]
		[Category("Functional")]
		public void SumSeqsTest()
		{
			var r1 = Range.Create(0, 100) as IEnumerable<int>;
			var r2 = Range.Create(451, 551);
			var r3 = Range.Create(741, 841);
			var res = Sequence.Plus(r1, r2, r3);
			Assert.AreEqual(135542, res.Sum());
			var shouldbe = new[] {
				1192,
				1195,
				1198,
				1201,
				1204,
				1207,
				1210,
				1213,
				1216,
				1219,
				1222,
				1225,
				1228,
				1231,
				1234,
				1237,
				1240,
				1243,
				1246,
				1249,
				1252,
				1255, 
				1258,
				1261,
				1264,
				1267,
				1270,
				1273,
				1276,
				1279,
				1282,
				1285,
				1288,
				1291,
				1294,
				1297,
				1300,
				1303,
				1306,
				1309,
				1312,
				1315,
				1318,
				1321,
				1324,
				1327,
				1330,
				1333, 
				1336,
				1339,
				1342,
				1345,
				1348,
				1351,
				1354,
				1357,
				1360,
				1363,
				1366,
				1369,
				1372,
				1375,
				1378,
				1381,
				1384,
				1387,
				1390,
				1393,
				1396,
				1399,
				1402,
				1405,
				1408,
				1411, 
				1414,
				1417,
				1420,
				1423,
				1426,
				1429,
				1432,
				1435,
				1438,
				1441,
				1444,
				1447,
				1450,
				1453,
				1456,
				1459,
				1462,
				1465,
				1468,
				1471,
				1474,
				1477,
				1480,
				1483,
				1486,
				1489,
				1492
			};
			Assert.IsTrue(res.AreSameLists(shouldbe));
		}

		[Test]
		[Category("Functional")]
		public void RadomDatesSequenceTest()
		{
			var countr = Rand.Next(1, 148);
			var seq = Sequence.CreateRandomDates(countr);
			Assert.AreEqual(countr, seq.Count(), "Not the required amount of elements.");
			Assert.IsTrue(seq.AreAllDifferent(), "Some doubles present.");
		}

		[Test]
		[Category("Functional")]
		public void AreAllDifferentTest()
		{
			var seq = new[] { 5, 6, 99, 9, 4, 15, 47 };
			Assert.IsTrue(seq.AreAllDifferent(), "They are actually all different.");
			seq = new int[] { 1, 1, 54, 85, 93, 236 };
			Assert.IsFalse(seq.AreAllDifferent(), "They are not all different.");
		}

		[Test]
		[Category("Functional")]
		public void AreSameListsTest()
		{
			var list = Range.Create(1, 10).ToFunctionalList();
			Assert.IsTrue(list.AreSameLists(list), "The same list should be, well, the same.");
			var otherList = new List<string> { "a", "de", "swa" };
			var shouldbeList = new List<string> { "a", "de", "swa" };
			var shouldnotbeList = new List<string> { "a2", "de", "swa" };
			Assert.IsTrue(otherList.AreSameLists(shouldbeList), "The same content but different name fails.");
			Assert.IsFalse(otherList.AreSameLists(shouldnotbeList), "The same length but other content fails.");
		}

		[Test]
		[Category("Functional")]
		public void HaveSameContentTest()
		{
			var otherList = new List<string> { "a", "de", "swa" };
			var shouldbeList = new List<string> { "de", "a", "swa" };
			var shouldnotbeList = new List<string> { "a2", "de", "swa" };
			Assert.IsTrue(otherList.HaveSameContent(shouldbeList), "The same content but different order fails.");
			Assert.IsFalse(otherList.HaveSameContent(shouldnotbeList), "The same length but other content fails.");
		}

		[Test]
		[Category("Functional")]
		public void SequenceTest()
		{
			var seq = Sequence.Create(i => i + 5, 0, i => i >= 100);
			Assert.AreEqual(21, seq.Count(), "Wrong count.");
			seq.ForEach(i => Debug.WriteLine(string.Format("{0} ", i)));
		}

		[Test]
		[Category("Functional")]
		public void ComposeTest()
		{
			Func<double, double> f = x => x * x;
			Func<double, double> g = x => x + 2;
			var comp = f.Compose(g); // in F# this would be f |> g
			for(var i = 0; i < 15; i++)
				Assert.AreEqual(i * i + 2, comp(i), string.Format("Wrong value at {0}", i));
		}

		[Test]
		[Category("Functional")]
		public void CollectTest()
		{
			var range = Range.Create(1, 150);
			var c = range.Collect(i =>
                new[] { i, i + 200 });
			Assert.AreEqual(300, c.Count(), "The first collect operation failed.");
			var b = range.Collect(i => i <= 10 ? new[] { 5 * i } : null);
			Assert.AreEqual(10, b.Count(), "Second recollection failed.");
			Assert.AreEqual(5, b.First(), "Not the correct value.");
		}

		[Test]
		[Category("Functional")]
		public void Flist1Test()
		{
			var list = new FunctionalList<int>(0);
			Range.Create(1, 25).ForEach(i => list = list.Cons(i));

			Assert.AreEqual(25, list.Head, "The Cons failed.");
			Assert.AreEqual(25, list.Tail.Count(), "Tail count is wrong.");
		}

		[Test]
		[Category("Functional")]
		public void FQueue1Test()
		{
			var q = new FunctionalQueue<int>(-3);
			Range.Create(1, 25).ForEach(i => q = q.Snoc(i));
			Assert.AreEqual(26, q.Count, "Not the correct length.");
			Assert.AreEqual(-3, q.Head, "The Snoc failed.");

		}


	}
}
