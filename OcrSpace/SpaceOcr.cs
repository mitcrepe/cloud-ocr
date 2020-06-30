using Application.OCR;
using ApplicationService;
using Microsoft.AspNetCore.StaticFiles;
using OcrSpace.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OcrSpace {
    public class SpaceOcr : IOcr {
        private readonly string apiKey;
        private readonly HttpClient client;

        public SpaceOcr(HttpClient client, string apiKey) {
            this.apiKey = apiKey;
            this.client = client;
        }

        ~SpaceOcr() {
            this.client.Dispose();
        }

        public async Task<RecognitionResult> Recognize(File file) {
            HttpContent content = PrepareRequestContent(file);
            WebResultModel webResult = await SendRequest(content);
            return ParseWebResult(webResult);
        }


        private HttpContent PrepareRequestContent(File file) {
            bool parseSuccessful = new FileExtensionContentTypeProvider().TryGetContentType(file.Name, out string contentType);
            if (!parseSuccessful) {
                throw new ArgumentException("Cannot get mime type from file.");
            }

            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(this.apiKey), "apikey");
            content.Add(new ByteArrayContent(file.Content), "image", file.Name);
            content.Add(new StringContent("True"), "isOverlayRequired");

            return content;
        }

        private async Task<WebResultModel> SendRequest(HttpContent content) {
            HttpResponseMessage response = await this.client.PostAsync("https://api.ocr.space/parse/image", content);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            WebResultModel webResult = JsonSerializer.Deserialize<WebResultModel>(jsonResponse);
            return webResult;
        }

        private RecognitionResult ParseWebResult(WebResultModel webResult) {
            WebResultParser resultParser = new WebResultParser(webResult);
            return resultParser.Parse();
        } 
    }
}
