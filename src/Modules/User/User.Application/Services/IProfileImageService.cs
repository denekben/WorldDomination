using Microsoft.AspNetCore.Http;

namespace User.Application.Services
{
    public interface IProfileImageService
    {
        public Task<string> UploadFileAsync(IFormFile formFile);
    }
}