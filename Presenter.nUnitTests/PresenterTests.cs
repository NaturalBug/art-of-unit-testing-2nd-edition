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
            Presenter p = new(mockView);

            mockView.Loaded += Raise.Event<Action>();

            mockView.Received().Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }
    }
}