using Microsoft.AspNetCore.Http;
using ProgressSoft.Core.Entites;

namespace ProgressSoft.Core.Helper.FileUpload
{
    public interface IFormUpload
    {
        UploadResult ProcessUpload(IFormFile file);
    }
}
