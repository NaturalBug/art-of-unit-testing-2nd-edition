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

            StringAssert.Contains("someone@somewhere.com", mockEmail.To);
            StringAssert.Contains("fake exception", mockEmail.Body);
            StringAssert.Contains("can't log", mockEmail.Subject);
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
