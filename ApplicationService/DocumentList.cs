using System;
using System.Collections.Generic;
using System.Text;

namespace Application {
    public class DocumentList : Dictionary<int, string> {
        public DocumentList() { }

        public DocumentList(Dictionary<int, string> documentList) : base(documentList) { }
    }
}
