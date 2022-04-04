using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Taallama.Service.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> SaveFileAsync(Stream file, string fileName, IConfiguration config, IWebHostEnvironment env)
        {
            fileName = Guid.NewGuid().ToString("N") + '_' + fileName;

            string storagePath = config.GetSection("Storage:ImageUrl").Value;

            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            FileStream mainFile = File.Create(filePath);

            await file.CopyToAsync(mainFile);

            mainFile.Close();

            return fileName;
        }
    }
}