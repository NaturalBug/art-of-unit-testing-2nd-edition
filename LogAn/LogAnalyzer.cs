namespace LogAn
{
    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; set; }


        public bool IsVaildLogFileName(string fileName)
        {
            FileExtensionManager mgr = new FileExtensionManager();
            if (mgr.IsValid(fileName))
            {
                WasLastFileNameValid = true;
                return true;
            }

            WasLastFileNameValid = false;
            return false;
        }
    }
}
