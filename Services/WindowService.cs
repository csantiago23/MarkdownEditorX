using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MarkdownEditorApp.Services
{
    public class WindowService : IWindowService
    {
        public void Minimize()
        {
#if WINDOWS
            var window = Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                var presenter = appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                presenter?.Minimize();
            }
#endif
        }

        public void Maximize()
        {
#if WINDOWS
            var window = Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                var presenter = appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                if (presenter != null)
                {
                    if (presenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
                    {
                        presenter.Restore();
                    }
                    else
                    {
                        presenter.Maximize();
                    }
                }
            }
#endif
        }

        public void Close()
        {
            Application.Current?.Quit();
        }
    }
}
