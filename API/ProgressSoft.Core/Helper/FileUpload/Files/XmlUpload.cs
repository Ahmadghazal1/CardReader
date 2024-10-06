using Microsoft.AspNetCore.Http;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper.FileUpload.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProgressSoft.Core.Helper.FileUpload.Files
{
    public class XmlUpload : IFormUpload
    {
        public UploadResult ProcessUpload(IFormFile file)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var serializer = new XmlSerializer(typeof(CardReaders)); // Change to CardReaders
                    var cardReaders = (CardReaders)serializer.Deserialize(stream);
                    return new UploadResult
                    {
                        Success = true,
                        Data = cardReaders.Readers,
                        Length = cardReaders.Readers.Count()
                    };
                }
            }
            catch (Exception ex)
            {
                return new UploadResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}

