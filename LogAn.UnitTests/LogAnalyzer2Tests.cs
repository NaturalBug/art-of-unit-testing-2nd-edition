using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzer2Tests
    {
        [Fact]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            FakeEmailService mockEmail = new FakeEmailService();
            LogAnalyzer2 log = new LogAnalyzer2(new FakeWebService
            {
                ToThrow = new Exception("fake exception")
            }, mockEmail);
            string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            mockEmail.Email.Should().BeEquivalentTo(new EmailInfo
            {
                Body = "fake exception",
                To = "someone@somewhere.com",
                Subject = "can't log"
            });
        }

        [Fact]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockWebService = new Mock<IWebService>();
            var stubLogger = new Mock<ILogger>();
            stubLogger.Setup(logger => logger.LogError(It.IsAny<string>())).Throws(new Exception("fake exception"));
            var analyzer = new LogAnalyzer2(stubLogger.Object, mockWebService.Object)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockWebService.Verify(x => x.Write(It.Is<string>(message => message.Contains("fake exception"))));
        }
    }

    internal class FakeEmailService : IEmailService
    {
        public EmailInfo Email = null;

        public void SendEmail(EmailInfo emailInfo)
        {
            Email = emailInfo;
        }
    }
}
