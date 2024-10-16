using User.Application.Services;
using Microsoft.AspNetCore.Http;
using Uploadcare.Upload;
using Uploadcare;
using Microsoft.Extensions.Configuration;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Infrastructure.Services
{
    public class ProfileImageService : IProfileImageService
    {
        protected readonly FileUploader _fileUploader;
        public ProfileImageService(IConfiguration config)
        {
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

                return "https://ucarecdn.com/" + uploadedFile.Uuid + "/-/preview/500x500/-/quality/smart/-/format/auto/";
            }
            else
            {
                throw new BadRequestException("IFormFile is empty");
            }

        }
    }
}
