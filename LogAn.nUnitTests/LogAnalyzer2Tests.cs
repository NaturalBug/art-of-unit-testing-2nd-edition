using FluentAssertions;
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
            FakeWebService mockWebService = new FakeWebService();
            var analyer2 = new LogAnalyzer2(new FakeLogger2
            {
                WillThrow = new Exception("fake exception")
            }, mockWebService)
            {
                MinNameLength = 8
            };
            string tooShortFileName = "abc.ext";

            analyer2.Analyze(tooShortFileName);

            StringAssert.Contains("fake exception", mockWebService.MessageToWebService);
        }
    }

    internal class FakeLogger2 : ILogger
    {
        public Exception WillThrow = null;

        public string LoggerGotMessage;

        public void LogError(string message)
        {
            LoggerGotMessage = message;
            if (WillThrow != null)
            {
                throw WillThrow;
            }
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
