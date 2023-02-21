using BlazorBookApp.Data;

namespace BlazorBookApp.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(UploadedFile uploadedFile);
        string GetBlob(string fileName);
    }
}
