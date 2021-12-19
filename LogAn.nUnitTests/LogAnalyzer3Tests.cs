using NSubstitute;
using NUnit.Framework;
using System;

namespace LogAn.nUnitTests
{
    class LogAnalyzer3Tests
    {
        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });
            var analyzer = new LogAnalyzer3(stubLogger, mockWebService)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockWebService.Received().Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000
                && info.Message.Contains("fake exception")));
        }
    }
}
