using BlazorBookApp.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorBookApp.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageProcesing> UploadImage(IBrowserFile file);
    }
}
