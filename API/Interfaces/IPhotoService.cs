using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        //ImageUploadResult comes from Cloudinary
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        //DeletionResult comes from Cloudinary 
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}