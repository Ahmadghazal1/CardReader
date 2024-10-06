using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Text.Json;
using ZXing.Windows.Compatibility;

namespace ProgressSoft.Core.Helper.FileUpload
{
    public class QrCodeUpload : IFormUpload
    {
        public UploadResult ProcessUpload(IFormFile file)
        {
            var uploadResult = new UploadResult();
            uploadResult.Data = new List<CardReaderFile>(); 

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var bitmap = new Bitmap(stream)) 
                    {
                        var reader = new BarcodeReader();
                        var result = reader.Decode(bitmap);

                        if (result != null)
                        {
                            var cardReaderData = JsonSerializer.Deserialize<List<CardReaderFile>>(result.Text);

                            if (cardReaderData != null)
                            {

                                uploadResult.Data.AddRange(cardReaderData); 
                            }

                            uploadResult.Success = true;
                        }
                        else
                        {
                            uploadResult.Success = false;
                            uploadResult.ErrorMessage = "QR code could not be decoded.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                uploadResult.Success = false;
                uploadResult.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return uploadResult; 
        }
    }
}
