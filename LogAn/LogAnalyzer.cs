using System;
using System.IO;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("LogAn.nUnitTests")]
[assembly: InternalsVisibleTo("LogAn.UnitTests")]

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager manager;
        private readonly IWebService service;
        private readonly ILogger logger;

        public bool WasLastFileNameValid { get; set; }
        public int MinNameLength { get; set; }

        public LogAnalyzer()
        {
            manager = ExtensionManagerFactory.Create();
        }
        
        internal LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
        }

        public LogAnalyzer(IWebService service)
        {
            this.service = service;
        }

        public LogAnalyzer(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsVaildLogFileName(string fileName)
        {
            if (manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length >= 5)
            {
                WasLastFileNameValid = true;
                return true;
            }

            WasLastFileNameValid = false;
            return false;
        }
    
        public void Analyze(string fileName)
        {
            if (MinNameLength != 0 && fileName.Length < MinNameLength)
            {
                logger.LogError($"Filename too short: {fileName}");
            }
            else if (fileName.Length < 8)
            {
                service.LogError("Filename too short:" + fileName);
                LoggingFacility.Log("Filename too short:" + fileName);
            }
            else
            {
                throw new Exception();
            }
        }
    }

    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return IsValid(fileName);
        }

        protected virtual bool IsValid(string fileName)
        {
            FileExtensionManager mgr = new FileExtensionManager();
            return mgr.IsValid(fileName);
        }
    }
}
