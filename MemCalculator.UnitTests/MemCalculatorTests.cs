using Xunit;

namespace MemCalculator.UnitTests
{
    public class MemCalculatorTests
    {
        private readonly MemCalculator calc = new();

        [Fact]
        public void Sum_ByDefault_ReturnsZero()
        {
            int lastSum = calc.Sum();

            Assert.Equal(0, lastSum);
        }

        [Fact]
        public void Add_WenCalled_ChangesSum()
        {
            calc.Add(1);
            int sum = calc.Sum();

            Assert.Equal(1, sum);
        }
    }
}
