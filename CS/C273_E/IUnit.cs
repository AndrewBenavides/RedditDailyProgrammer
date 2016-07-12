using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E {
    public interface IUnit {
        char Code { get; }
        decimal Value { get; set; }

        IUnit ConvertTo(char code);
        T ConvertTo<T>() where T : IUnit;
    }
}
