using Application.OCR;
using OcrSpace;
using System;
using System.Text.Json;

namespace OcrTest {
    class Program {
        static void Main(string[] args) {
            byte[] fileContent = System.IO.File.ReadAllBytes(@"D:\sample.pdf");
            File file = new File() {
                Name = "sample.pdf",
                Content = fileContent
            };

            var ocr = new SpaceOcr(new System.Net.Http.HttpClient() , "94af3b059788957");
            var res = ocr.Recognize(file).Result;

        }
    }
}
