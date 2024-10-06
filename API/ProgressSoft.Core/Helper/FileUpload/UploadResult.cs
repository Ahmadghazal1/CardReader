using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.Helper.FileUpload
{
    public class UploadResult
    {
        public bool Success { get; set; }
        public List<CardReaderFile> Data { get; set; }
        public string ErrorMessage { get; set; }
        public int Length { get; set; }
    }
    
}
