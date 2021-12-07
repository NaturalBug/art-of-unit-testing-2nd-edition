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

        public bool WasLastFileNameValid { get; set; }

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
            if (fileName.Length < 8)
            {
                service.LogError("Filename too short:" + fileName);
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
