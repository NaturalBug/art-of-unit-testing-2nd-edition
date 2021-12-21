namespace LogAn
{
    public class ExtensionManagerFactory
    {
        private static IExtensionManager customManager = null;

        internal static IExtensionManager Create()
        {
            if (customManager != null)
            {
                return customManager;
            }
            return new FileExtensionManager();
        }

        public static void SetManager(IExtensionManager mgr)
        {
            customManager = mgr;
        }
    }
}