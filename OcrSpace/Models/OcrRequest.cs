using System;
using System.Collections.Generic;
using System.Text;

namespace OcrSpace.Models {
    public class OcrRequest {
        public string base64Image { get; set; }
        public string filetype { get; set; }
        public bool isOverlayRequired { get; set; }
    }
}
