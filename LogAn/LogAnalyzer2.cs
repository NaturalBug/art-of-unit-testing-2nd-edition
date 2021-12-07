using System;

namespace LogAn
{
    public class LogAnalyzer2
    {
        public IWebService Service { get; set; }
        public IEmailService Email { get; set; }

        public LogAnalyzer2(IWebService service, IEmailService email)
        {
            Service = service;
            Email = email;
        }

        internal void Analyze(string fileName)
        {
            try
            {
                if (fileName.Length < 8)
                {
                    Service.LogError("Filename too short:" + fileName);
                }
            }
            catch (Exception e)
            {
                Email.SendEmail("someone@somewhere.com", "can't log", e.Message);
            }
        }
    }
}