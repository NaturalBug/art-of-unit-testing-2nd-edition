﻿using System.IO;

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager manager;

        public bool WasLastFileNameValid { get; set; }

        public LogAnalyzer()
        {
            manager = ExtensionManagerFactory.Create();
        }
        
        public LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
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
    }

    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName);
        }

        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }
    }
}
