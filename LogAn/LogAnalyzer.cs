namespace LogAn
{
    public class LogAnalyzer
    {
        public bool IsVaildLogFileName(string fileName)
        {
            if (!fileName.EndsWith(".SLF", System.StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
