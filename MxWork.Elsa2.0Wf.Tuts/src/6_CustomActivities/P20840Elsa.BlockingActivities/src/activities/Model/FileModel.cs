using System;
using System.Collections.Generic;
using System.Text;

namespace Elsa.CustomActivityLibrary.Model
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
