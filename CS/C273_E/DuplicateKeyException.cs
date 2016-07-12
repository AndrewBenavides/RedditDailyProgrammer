using System;

namespace C273_E {
    public class DuplicateKeyException : ArgumentException {
        public DuplicateKeyException() : base() {
        }

        public DuplicateKeyException(string message) : base(message) {
        }

        public DuplicateKeyException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
