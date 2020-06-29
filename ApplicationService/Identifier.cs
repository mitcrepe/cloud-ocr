using System;
using System.Collections.Generic;
using System.Text;

namespace Application {
    public class Identifier : ValueObject {
        private Guid value;

        public Identifier(Guid value) {
            this.value = value;
        }

        public Identifier(string value) {
            this.value = Guid.Parse(value);
        }

        protected override IEnumerable<object> GetAtomicValues() {
            yield return value;
        }

        public override string ToString() {
            return value.ToString();
        }
    }
}
