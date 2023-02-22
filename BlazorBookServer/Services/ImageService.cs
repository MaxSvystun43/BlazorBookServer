using BlazorBookApp.Data;
using BlazorBookApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System.Drawing;
using System.IO;

namespace BlazorBookApp.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileService _fileService;
        public ImageService(IFileService fileService) 
        {
            _fileService = fileService;
        }
        public async Task<ImageProcesing> UploadImage(IBrowserFile file)
        {
            var imageProcesing = new ImageProcesing();
            try
            {
                var fileUpload = await ResizeImage(file, 400, 600);
                imageProcesing.Name = await _fileService.UploadFile(fileUpload);
                imageProcesing.ImageState = $"{imageProcesing.Name} file(s) uploaded on server";
                return imageProcesing;
            }
            catch (Exception ex)
            {
                imageProcesing.ImageState = ex.Message;
                return imageProcesing;
            }
        }

        private async Task<UploadedFile> ResizeImage(IBrowserFile file, int width, int heigth)
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream ms = new MemoryStream();
            var maxSize = 1024 * 1024 * 15;
            await file.OpenReadStream(maxSize).CopyToAsync(stream);
            Bitmap sourceImage = new Bitmap(stream);
            using (Bitmap objBitmap = new Bitmap(width, heigth))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, width, heigth);

                    objBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            UploadedFile uploadedFile = new UploadedFile();
            uploadedFile.FileName = file.Name.Replace(" ", "-");
            uploadedFile.FileContent = ms.ToArray();

            return uploadedFile;
        }
    }
}
