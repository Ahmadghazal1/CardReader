using Microsoft.AspNetCore.Http;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper.FileUpload.Reader;

namespace ProgressSoft.Core.Helper.FileUpload
{
    public interface IFormUpload
    {
        UploadResult ProcessUpload(IFormFile file);
    }
}
