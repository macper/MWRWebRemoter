using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public interface IDictObject
    {
        string Guid { get; set; }
        bool Active { get; set; }
        string Name { get; set; }
    }
}
