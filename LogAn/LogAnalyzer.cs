namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager manager;

        public bool WasLastFileNameValid { get; set; }

        public LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
        }


        public bool IsVaildLogFileName(string fileName)
        {
            if (manager.IsValid(fileName))
            {
                WasLastFileNameValid = true;
                return true;
            }

            WasLastFileNameValid = false;
            return false;
        }
    }
}
