using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Storage {

    [Serializable]
    public class StorageException : Exception {
        public StorageException() { }
        public StorageException(string message) : base(message) { }
        public StorageException(string message, Exception inner) : base(message, inner) { }
        protected StorageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
