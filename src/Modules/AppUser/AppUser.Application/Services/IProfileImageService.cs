using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AppUser.Application.Services
{
    public interface IProfileImageService
    {
        public Task<string> UploadFileAsync(IFormFile formFile);
    }
}
