using System;

namespace LogAn
{
    public class LogAnalyzer3
    {
        private readonly ILogger logger;
        private readonly IWebService webService;

        public LogAnalyzer3(ILogger logger, IWebService webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        public int MinNameLength { get; set; }

        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
            {
                try
                {
                    logger.LogError(string.Format("Filename too short: {0}", filename));
                }
                catch (Exception e)
                {
                    webService.Write(new ErrorInfo(1000, e.Message));
                }
            }
        }
    }
}