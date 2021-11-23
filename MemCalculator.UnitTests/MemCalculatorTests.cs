using Xunit;

namespace MemCalculator.UnitTests
{
    public class MemCalculatorTests
    {
        [Fact]
        public void Sum_ByDefault_ReturnsZero()
        {
            MemCalculator calc = new();

            int lastSum = calc.Sum();

            Assert.Equal(0, lastSum);
        }
    }
}
