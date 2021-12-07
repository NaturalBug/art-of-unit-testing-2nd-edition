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

            Assert.Equal("someone@somewhere.com", mockEmail.To);
            Assert.Equal("fake exception", mockEmail.Body);
            Assert.Equal("can't log", mockEmail.Subject);
        }
    }

    internal class FakeEmailService : IEmailService
    {
        public string To;
        public string Body;
        public string Subject;

        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
