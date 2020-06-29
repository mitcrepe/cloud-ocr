using Application;
using Application.OCR;
using Application.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationService {
    public class ApplicationService : IApplicationService {
        private readonly IOcr ocr;
        private readonly IStorage storage;

        public ApplicationService(IOcr ocr, IStorage storage) {
            this.ocr = ocr;
            this.storage = storage;
        }

        public async Task<Document> RecognizeDocument(File file) {
            RecognitionResult recognitionData = await this.ocr.Recognize(file);
            //handle errors in recognitionData

            var document = new Document() {
                Id = storage.NextIdentifier(),
                Name = file.Name,
                Pages = recognitionData.Pages,
                Image = file.Content
            };

            await storage.SaveDocument(document);

            return document;
        }

        public async Task<List<DocumentInfo>> ListRecognizedDocuments() {
            return await storage.ListDocuments();
        }

        public async Task<Document> GetDocument(Identifier id) {
            return await storage.GetDocument(id);
        }
    }
}
