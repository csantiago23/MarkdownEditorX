using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices;

namespace MarkdownEditorApp.Services
{
    public class FileService : IFileService
    {
        public string? CurrentFilePath { get; set; }

        public string CurrentFileName => string.IsNullOrEmpty(CurrentFilePath) 
            ? "Untitled" 
            : Path.GetFileName(CurrentFilePath);

        public async Task<string?> OpenFileAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".md", ".txt" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Open Markdown or Text File",
                    FileTypes = customFileType
                };

                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    CurrentFilePath = result.FullPath;
                    return await File.ReadAllTextAsync(result.FullPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error opening file: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> SaveFileAsync(string content)
        {
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                return await SaveFileAsAsync(content);
            }

            try
            {
                await File.WriteAllTextAsync(CurrentFilePath, content);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving file: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveFileAsAsync(string content)
        {
            try
            {
#if WINDOWS
                var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                
                var window = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
                if (window == null)
                {
                    System.Diagnostics.Debug.WriteLine("Error: WinUI Window instance is null.");
                    return false;
                }
                
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);

                savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Markdown", new System.Collections.Generic.List<string>() { ".md" });
                savePicker.FileTypeChoices.Add("Text", new System.Collections.Generic.List<string>() { ".txt" });
                savePicker.SuggestedFileName = string.IsNullOrEmpty(CurrentFilePath) ? "Untitled" : Path.GetFileNameWithoutExtension(CurrentFilePath);

                var file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    await File.WriteAllTextAsync(file.Path, content);
                    CurrentFilePath = file.Path;
                    return true;
                }
#else
                await Task.CompletedTask;
#endif
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving file as: {ex.Message}");
            }
            return false;
        }
    }
}
