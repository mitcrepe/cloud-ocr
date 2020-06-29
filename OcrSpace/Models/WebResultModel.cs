using System;
using System.Collections.Generic;
using System.Text;

namespace OcrSpace.Models {
    public class WebResultModel {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string[] ErrorMessage { get; set; } //This is not present on successful result
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }

    public class Parsedresult {
        public Textoverlay TextOverlay { get; set; }
        public string TextOrientation { get; set; }
        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Textoverlay {
        public Line[] Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }

    public class Line {
        public string LineText { get; set; }
        public Word[] Words { get; set; }
        public float MaxHeight { get; set; }
        public float MinTop { get; set; }
    }

    public class Word {
        public string WordText { get; set; }
        public float Left { get; set; }
        public float Top { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }

    public class Rootobject {
        public object[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string[] ErrorMessage { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }
}
