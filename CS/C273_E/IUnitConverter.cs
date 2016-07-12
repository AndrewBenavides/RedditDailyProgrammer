using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E {
    public interface IUnitConverter {
        decimal To(IUnit unit);
        decimal From(IUnit unit);
    }
}
