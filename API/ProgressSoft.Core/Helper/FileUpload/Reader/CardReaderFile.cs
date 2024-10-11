using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.Helper.FileUpload.Reader
{
    public class CardReaderFile
    {
        public string name { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string photo { get; set; } // Base64 encoded
        public string address { get; set; }
    }
}
