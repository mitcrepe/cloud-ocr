using Application;
using ApplicationService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Storage {
    public interface IStorage {
        Task SaveDocument(Document document);
        Task<List<DocumentInfo>> ListDocuments();
        Task<Document> GetDocument(Identifier id);
        Identifier NextIdentifier();
    }
}