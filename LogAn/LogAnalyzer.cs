namespace LogAn
{
    public class LogAnalyzer
    {
        public bool IsVaildLogFileName(string fileName)
        {
            if (fileName.EndsWith(".SLF"))
            {
                return false;
            }
            return true;
        }
    }
}
