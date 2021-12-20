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
            Presenter p = new(mockView.Object, new Mock<ILogger>().Object);

            mockView.Raise(x => x.Loaded += null);

            mockView.Verify(x => x.Render(It.Is<string>(s => s.Contains("Hello World"))));
        }

        [Fact]
        public void Ctor_WhenViewHaError_CallLogger()
        {
            var stubView = new Mock<IView>();
            var mockLogger = new Mock<ILogger>();
            Presenter p = new(stubView.Object, mockLogger.Object);

            stubView.Raise(x => x.ErrorOccured += null, "fake error");

            mockLogger.Verify(x => x.LogError(It.Is<string>(s => s.Contains("fake error"))));
        }
    }
}
