using System;

namespace C273_E.Exceptions {
    public class UnitTypeNotImplementedException : NotImplementedException {
        public UnitTypeNotImplementedException(string message) : base(message) {
        }
    }
}
