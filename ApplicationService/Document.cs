using Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService {
    public class Document {
        public Identifier Id { get; set; }
        public string Name { get; set; }
        public List<Page> Pages { get; set; }
        public byte[] Image { get; set; }
    }
}
