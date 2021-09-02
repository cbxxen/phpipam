//Photo Service uses Cloudinary storage

using System.Threading.Tasks;
using API.Helper;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config){
            //Adding Cloudinary Account
            var acc = new Account(
                config.Value.CLoudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            //Save account to instanc variable
            _cloudinary = new Cloudinary(acc);
        }

        //ImageUploadResult comes from Cloudinary
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            //define variable for result
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0){
                //get file as stream of data
                using var stream = file.OpenReadStream();
                //Preparing upload parameters for Cloudinary
                var uploadParams = new ImageUploadParams {
                    File = new FileDescription(file.FileName, stream),
                    //Transform image to square and grop to face
                    Type = "private",
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    
                };
                //uploading image to cloudinary
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
        //DeletionResult comes from Cloudinary 
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            //prepare deletion Var
            var deleteParams = new DeletionParams(publicId);
            //delete file
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}