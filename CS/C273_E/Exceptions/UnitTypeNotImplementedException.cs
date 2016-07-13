using System;

namespace C273_E.Exceptions {
    public class UnitTypeNotImplementedException : NotImplementedException {
        public UnitTypeNotImplementedException(char code, string message) : base(message) {
        }
    }
}
