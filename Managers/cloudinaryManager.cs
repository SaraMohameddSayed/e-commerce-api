using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class cloudinaryManager
    {
        private readonly Cloudinary cloudinary;
        public cloudinaryManager(IConfiguration configuration) { 

            var cloudName = configuration["CloudinarySettings:CloudName"];
            var apiKey = configuration["CloudinarySettings:ApiKey"];
            var apiSecret = configuration["CloudinarySettings:ApiSecret"];
            var account = new Account(cloudName, apiKey, apiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file,string Folder="Images")
        {

            if(file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty", nameof(file));
            }
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = Folder
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);


            return uploadResult.SecureUrl.ToString();
        }


        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await cloudinary.DestroyAsync(deletionParams);
            if (deletionResult.Result == "ok")
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
