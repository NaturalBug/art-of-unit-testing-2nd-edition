using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;

namespace LogAn.nUnitTests
{
    [TestFixture]
    class LogAnalyzer2Tests
    {
        [Test]
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

        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });
            var analyzer = new LogAnalyzer2(stubLogger, mockWebService)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockWebService.Received().Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }
    }

    internal class FakeEmailService : IEmailService
    {
        public EmailInfo Email;

        public void SendEmail(EmailInfo emailInfo)
        {
            Email = emailInfo;
        }
    }
}
