using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application;
using ApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Web.Shared;

namespace Web.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase {
        const int FileSizeLimit = 10000000; //10MB 

        private readonly IApplicationService service;

        public DocumentsController(IApplicationService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DocumentInfoModel>> ListDocuments() {
            var documentsList = await this.service.ListRecognizedDocuments();
            var response = documentsList.Select(doc => new DocumentInfoModel() { Id = doc.Id.ToString(), Name = doc.Name });
            return response;
        }

        [HttpGet("{id}")]
        public async Task<DocumentModel> GetDocument(string id) {
            var document = await this.service.GetDocument(new Identifier(id));
            List<PageModel> documentPages = new List<PageModel>();
            foreach (Page page in document.Pages) {
                PageModel pageModel = new PageModel();
                pageModel.Number = page.Number;
                List<WordModel> pageWords = new List<WordModel>();
                foreach (Word word in page.Words) {
                    pageWords.Add(new WordModel() { Rectangle = word.Rectangle, Text = word.Text });
                }

                pageModel.Words = pageWords.ToArray();
                documentPages.Add(pageModel);
            }

            return new DocumentModel { Id = document.Id.ToString(), Name = document.Name, Image = document.Image, Pages = documentPages.ToArray() };
            
        }

        [HttpPost]
        public async Task<DocumentModel> UploadDocument() {
            if (HttpContext.Request.Form.Files.Any()) {
                IFormFile requestFile = HttpContext.Request.Form.Files[0];
                // Check if there are multiple files, and return error?
                using MemoryStream memoryStream = new MemoryStream();
                await requestFile.CopyToAsync(memoryStream);
                // check if seek is needed
                byte[] fileContent = memoryStream.ToArray();

                var filee = new Application.OCR.File() {
                    Name = requestFile.FileName,
                    Content = fileContent
                };

                Document document = await this.service.RecognizeDocument(filee);

                return new DocumentModel() {
                    Id = document.Id.ToString(),
                    Name = document.Name,
                    Image = document.Image
                };
            } else {
                return null; // refactor this
            }
        }
    }
}
