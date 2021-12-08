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
