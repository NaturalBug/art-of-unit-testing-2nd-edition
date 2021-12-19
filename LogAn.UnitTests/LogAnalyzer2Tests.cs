using FluentAssertions;
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
            FakeWebService mockWebService = new FakeWebService();
            var analyzer2 = new LogAnalyzer2(new FakeLogger2
            {
                WillThrow = new Exception("fake exception")
            }, mockWebService)
            {
                MinNameLength = 8
            };
            string tooShortFileName = "abc.ext";

            analyzer2.Analyze(tooShortFileName);

            Assert.Contains("fake exception", mockWebService.MessageToWebService);
        }
    }

    internal class FakeLogger2 : ILogger
    {
        public Exception WillThrow = null;

        public string LoggerGetMessage;

        public void LogError(string message)
        {
            LoggerGetMessage = message;
            if (WillThrow != null)
            {
                throw WillThrow;
            }
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
