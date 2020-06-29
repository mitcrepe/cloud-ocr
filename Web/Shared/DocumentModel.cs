using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Shared {
    public class DocumentModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public PageModel[] Pages { get; set; }

    }
}
