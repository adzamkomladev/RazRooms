using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RazorPagesRoomReservations.Services
{
    public class UploadFileService
    {
        private readonly IWebHostEnvironment _environment;

        public UploadFileService(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(
            IFormFile file,
            string categoryDirectory,
            string fileDirectory)
        {
            var directoryPath = Path.Combine(
                _environment.WebRootPath,
                "Uploads",
                categoryDirectory,
                fileDirectory);

            // Create directory if it doesn't exist
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            //Getting file Extension
            var fileExtension = Path.GetExtension(Path.GetFileName(file.FileName));

            // concatenating  FileName + FileExtension
            var fileName = $"{System.Guid.NewGuid()}{fileExtension}";

            // Generate file path
            var fullPath = Path.Combine(directoryPath, fileName);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
            }

            return Path.GetRelativePath(_environment.WebRootPath, fullPath);
        }

        public void DeleteFile(string filePath)
        {
            var absoluteFilePath = Path.Combine(_environment.WebRootPath, filePath);

            if (File.Exists(absoluteFilePath))
            {
                File.Delete(absoluteFilePath);
            }
        }
    }
}