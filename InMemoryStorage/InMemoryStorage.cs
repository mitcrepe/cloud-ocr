using Application;
using Application.Storage;
using ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryStorage {
    public class InMemoryStorage : IStorage {
        readonly List<Document> documents = new List<Document>();

        public Task<Document> GetDocument(Identifier id) {
            return Task.FromResult(documents.First(document => document.Id == id));
        }

        public Task<List<DocumentInfo>> ListDocuments() {
            return Task.FromResult(new List<DocumentInfo>(documents.Select(document => new DocumentInfo() { Id = document.Id, Name = document.Name })));
        }

        public Identifier NextIdentifier() {
            return new Identifier(Guid.NewGuid());
        }

        public Task SaveDocument(Document document) {
            if (documents.Exists(doc => doc.Id == document.Id)) {
                throw new StorageException("Cannot save document, because document already exists.");
            }
            documents.Add(document);

            return Task.CompletedTask;
        }
    }
}
