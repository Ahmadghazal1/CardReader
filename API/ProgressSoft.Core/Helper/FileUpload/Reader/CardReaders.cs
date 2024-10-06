using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProgressSoft.Core.Helper.FileUpload.Reader
{
    [XmlRoot("CardReaders")]
    public class CardReaders
    {
        [XmlElement("CardReader")]
        public List<CardReaderFile> Readers { get; set; }
    }
}
