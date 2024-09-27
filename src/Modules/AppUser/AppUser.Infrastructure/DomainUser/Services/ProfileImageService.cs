using AppUser.Application.Services;
using Microsoft.AspNetCore.Http;
using Uploadcare.Upload;
using Uploadcare;
using Microsoft.Extensions.Configuration;
using AppUser.Application.Exceptions;

namespace AppUser.Infrastructure.DomainUser.Services
{
    public class ProfileImageService : IProfileImageService
    {
        protected readonly FileUploader _fileUploader;
        private readonly IConfiguration _config;
        public ProfileImageService(IConfiguration config)
        {
            _config = config;
            var client = new UploadcareClient(
                config["UploadcareSettings:PublicKey"],
                config["UploadcareSettings:PrivateKey"]
                );
            _fileUploader = new FileUploader(client);
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                // Create a unique temporary file path
                var tempFilePath = Path.GetTempFileName();

                // Create a new file stream to write the IFormFile content to the temporary file
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                var fileInfo = new FileInfo(tempFilePath);
                var uploadedFile = await _fileUploader.Upload(fileInfo);

                return uploadedFile.Uuid;
            }
            else
            {
                throw new BadRequestException("IFormFile is empty");
            }

        }
    }
}
