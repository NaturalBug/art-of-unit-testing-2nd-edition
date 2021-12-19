namespace LogAn
{
    public class ErrorInfo
    {
        private readonly int severity;
        private readonly string message;

        public ErrorInfo(int severity, string message)
        {
            this.severity = severity;
            this.message = message;
        }

        public int Severity
        {
            get { return severity; }
        }

        public string Message
        {
            get { return message; }
        }
    }
}