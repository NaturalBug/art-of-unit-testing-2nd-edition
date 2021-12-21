using Moq;
using System;
using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzer3Tests
    {
        [Fact]
        public void Analyze_LoggerThrows_CallsWebServiceWithMoqObject()
        {
            var mockWebService = new Mock<IWebService>();
            var stubLogger = new Mock<ILogger>();
            stubLogger.Setup(x => x.LogError(It.IsAny<string>())).Throws(new Exception("fake exception"));
            var analyzer = new LogAnalyzer3(stubLogger.Object, mockWebService.Object)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockWebService.Verify(x => x.Write(It.Is<ErrorInfo>(info => info.Severity == 1000 & info.Message.Contains("fake exception"))));
        }

        [Fact]
        public void Analyze_LoggerThrows_CallsWebServiceWithMoqObjectCompare()
        {
            var mockWebService = new Mock<IWebService>();
            var stubLogger = new Mock<ILogger>();
            stubLogger.Setup(x => x.LogError(It.IsAny<string>())).Throws(new Exception("fake exception"));
            var analyzer = new LogAnalyzer3(stubLogger.Object, mockWebService.Object)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockWebService.Verify(x => x.Write(new ErrorInfo(1000, "fake exception")));
        }
    }
}
