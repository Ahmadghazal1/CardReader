using CsvHelper;
using Microsoft.AspNetCore.Http;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper.FileUpload.Reader;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.Helper.FileUpload.Files
{
    public class CsvUpload : IFormUpload
    {
        public UploadResult ProcessUpload(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CardReaderFile>().ToList();
                    // Return the records in a successful UploadResult
                    return new UploadResult
                    {
                        Success = true,
                        Data = records.ToList(),
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

