namespace LogAn
{
    public interface ILogger
    {
        void LogError(string message);
        void Log(string text);
    }
}