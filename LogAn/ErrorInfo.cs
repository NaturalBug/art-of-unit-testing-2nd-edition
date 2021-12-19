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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ErrorInfo)obj);
        }

        protected bool Equals(ErrorInfo other)
        {
            return other.severity == severity && string.Equals(other.message, message);
        }
    }
}