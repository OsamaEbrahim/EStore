using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Services
{
    public class FilesManager
    {
        private readonly IWebHostEnvironment _webHostEnviornment;

        public string FilePath { get; set; }
        public FilesManager(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnviornment = hostEnvironment;
        }

        public async Task UploadFile(string path, IFormFile file)
        {
            string folderPath = Path.Combine(_webHostEnviornment.WebRootPath, path);
            Directory.CreateDirectory(folderPath);
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            FilePath = folderPath.Substring(folderPath.IndexOf("wwwroot") + 8);
            await file.CopyToAsync(new FileStream(folderPath, FileMode.Create));

        }

        public void DeleteFile(string path)
        {
            string filePath = Path.Combine(_webHostEnviornment.WebRootPath, path);
            File.Delete(filePath);
        }

        public void DeleteFolder(string path)
        {
            string folderPath = Path.Combine(_webHostEnviornment.WebRootPath, path);
            Directory.Delete(folderPath, true);
        }

    }
}
