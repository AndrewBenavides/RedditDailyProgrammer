using System;

namespace C273_E.Exceptions {
    public class DuplicateKeyException : ArgumentException {
        public DuplicateKeyException(string message) : base(message) {
        }
    }
}
