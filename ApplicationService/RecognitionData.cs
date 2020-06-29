using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ApplicationService {
    public class RecognitionResult {
        public List<string> Errors { get; set; }
        public bool HasErrors { get => Errors != null && Errors.Count > 0; }
        public List<Page> Pages { get; set; }
    }
}
