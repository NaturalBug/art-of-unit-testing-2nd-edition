namespace Presenter
{
    public class Presenter
    {
        private readonly IView view;

        public Presenter(IView view)
        {
            this.view = view;
            view.Loaded += OnLoaded;
        }

        private void OnLoaded()
        {
            view.Render("Hello World");
        }
    }
}
