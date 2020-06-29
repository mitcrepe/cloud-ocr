using Application;
using Application.OCR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationService {
    public interface IApplicationService {

        Task<Document> RecognizeDocument(File file);
        Task<List<DocumentInfo>> ListRecognizedDocuments();
        Task<Document> GetDocument(Identifier id);
    }
}
