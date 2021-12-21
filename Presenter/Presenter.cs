namespace Presenter
{
    public class Presenter
    {
        private readonly IView view;
        private readonly ILogger logger;

        public Presenter(IView view, ILogger logger)
        {
            this.view = view;
            this.logger = logger;
            view.Loaded += OnLoaded;
            view.ErrorOccured += OnError;
        }

        private void OnLoaded()
        {
            view.Render("Hello World");
        }

        private void OnError(string text)
        {
            logger.LogError(text);
        }
    }
}
