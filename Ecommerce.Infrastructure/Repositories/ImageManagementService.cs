﻿using Ecommerce.core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageasync(IFormFileCollection files, string src)
        {
            var saveImageSrc = new List<string>();
            var imageDirectory = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);
            foreach (var file in files)
            {
                if(file.Length > 0)
                {
                    var imageName = file.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";
                    var root = Path.Combine(imageDirectory, imageName);
                    using (FileStream stream=new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    saveImageSrc.Add(imageSrc);
                }
            }
            return saveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
