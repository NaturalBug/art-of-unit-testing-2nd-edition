using NUnit.Framework;

namespace MemCalculator.nUnitTests
{
    public class MemCalculatorTests
    {

        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            MemCalculator calc = new();

            int lastSum = calc.Sum();

            Assert.AreEqual(0, lastSum);
        }
    }
}