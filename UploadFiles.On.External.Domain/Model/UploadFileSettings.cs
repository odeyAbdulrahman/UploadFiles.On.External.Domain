using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFiles.On.External.Domain.Model
{
    public class UploadFileSettings
    {
        public string Domain { get; set; }
        public string Root { get; set; }
        public string Separator { get; set; }
        public int MaximumSize { get; set; }
    }
}
