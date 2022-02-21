using Microsoft.AspNetCore.Http;

namespace Application.Photos
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile photo);

        Task<string> DeletePhoto(string publicId);
    }
}