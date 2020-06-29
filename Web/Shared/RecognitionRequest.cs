using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Shared {
    public class RecognitionRequest {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
