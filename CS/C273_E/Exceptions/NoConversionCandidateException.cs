using System;

namespace C273_E.Exceptions {
    public class NoConversionCandidateException : Exception {
        public NoConversionCandidateException(string message) : base(message) {
        }
    }
}
