using NSubstitute;
using NUnit.Framework;
using System;

namespace Presenter.nUnitTests
{
    public class PresenterTests
    {
        [Test]
        public void Ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();
            Presenter p = new(mockView, Substitute.For<ILogger>());

            mockView.Loaded += Raise.Event<Action>();

            mockView.Received().Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }

        [Test]
        public void Ctor_WhenViewHaError_CallLogger()
        {
            var stubView = Substitute.For<IView>();
            var mockLogger = Substitute.For<ILogger>();
            Presenter p = new(stubView, mockLogger);

            stubView.ErrorOccured += Raise.Event<Action<string>>("fake error");

            mockLogger.Received().LogError(Arg.Is<string>(s => s.Contains("fake error")));
        }
    }
}