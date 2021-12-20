using System;

namespace Presenter
{
    public interface IView
    {
        event Action Loaded;

        void Render(string text);
    }
}