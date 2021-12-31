namespace LogAn
{
    internal class ConfigurationManager
    {
        internal bool IsConfigured(string configName)
        {
            LoggingFacility.Log("checking" + configName);
            return true;
        }
    }
}