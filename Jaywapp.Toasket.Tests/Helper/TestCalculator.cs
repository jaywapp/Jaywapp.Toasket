using Jaywapp.Toasket.Helper;
using NUnit.Framework;

namespace Jaywapp.Toasket.Tests.Helper
{
    [TestFixture]
    public class TestCalculator
    {
        [TestCase(new double[] { 1, 2, 3, 4, 5 }, 120)]
        [TestCase(new double[] { 123, 512, 123, 123, 0 }, 0)]
        public void TestMultiply(double[] values, double expect)
        {
            // when
            var actual = Calculator.Multiply(values, v => v);

            // then
            Assert.AreEqual(actual, expect);
        }
    }
}
