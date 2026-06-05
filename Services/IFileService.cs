using System.Threading.Tasks;

namespace MarkdownEditorApp.Services
{
    public interface IFileService
    {
        string? CurrentFilePath { get; set; }
        string CurrentFileName { get; }
        Task<string?> OpenFileAsync();
        Task<bool> SaveFileAsync(string content);
        Task<bool> SaveFileAsAsync(string content);
    }
}
