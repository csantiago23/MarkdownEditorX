using Microsoft.Extensions.Logging;
#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
#endif

namespace MarkdownEditorApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if WINDOWS
		var userDataFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "MarkdownEditorX");
		System.Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", userDataFolder);
#endif

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

#if WINDOWS
		builder.ConfigureLifecycleEvents(events =>
		{
			events.AddWindows(windows => windows.OnWindowCreated(window =>
			{
				var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
				var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
				var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
				
				// Set window and taskbar icon
				var iconPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "appicon.ico");
				if (System.IO.File.Exists(iconPath))
				{
					appWindow.SetIcon(iconPath);
				}
				
				var width = 1200;
				var height = 800;
				
				var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(id, Microsoft.UI.Windowing.DisplayAreaFallback.Primary);
				if (displayArea != null)
				{
					var centeredX = (displayArea.WorkArea.Width - width) / 2;
					var centeredY = (displayArea.WorkArea.Height - height) / 2;
					appWindow.MoveAndResize(new Windows.Graphics.RectInt32(centeredX, centeredY, width, height));
				}
				else
				{
					appWindow.Resize(new Windows.Graphics.SizeInt32(width, height));
				}
			}));
		});
#endif

		builder.Services.AddSingleton<MarkdownEditorApp.Services.IFileService, MarkdownEditorApp.Services.FileService>();
		builder.Services.AddSingleton<MarkdownEditorApp.Services.IWindowService, MarkdownEditorApp.Services.WindowService>();
		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
