using System;

namespace LogAn
{
    public class LogAnalyzer2
    {
        public IWebService Service { get; set; }
        public IEmailService Email { get; set; }
        public int MinNameLength { get; set; }

        private readonly ILogger logger;
        private readonly IWebService webService;

        public LogAnalyzer2(IWebService service, IEmailService email)
        {
            Service = service;
            Email = email;
        }

        public LogAnalyzer2(ILogger logger, IWebService webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                try
                {
                    logger.LogError(string.Format("filename too short {0}", fileName));
                }
                catch (Exception e)
                {
                    webService.Write("Error From Logger: " + e);
                }
            }
            else
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
                    Email.SendEmail(new EmailInfo
                    {
                        Body = e.Message,
                        To = "someone@somewhere.com",
                        Subject = "can't log"
                    });
                }
            }
        }
    }
}