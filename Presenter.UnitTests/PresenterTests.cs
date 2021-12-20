using Moq;
using System;
using Xunit;

namespace Presenter.UnitTests
{
    public class PresenterTests
    {
        [Fact]
        public void Ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = new Mock<IView>();
            Presenter p = new(mockView.Object);

            mockView.Raise(x => x.Loaded += null);

            mockView.Verify(x => x.Render(It.Is<string>(s => s.Contains("Hello World"))));
        }
    }
}
