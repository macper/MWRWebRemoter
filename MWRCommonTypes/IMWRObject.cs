using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public interface IMWRObject
    {
        Enum.ObjectType Type { get; }
        string Guid { get;  }
        bool Active { get; set; }
        string Name { get; set; }
        ProcessResult Process();
        void LoadConfig(string configString);
    }
}
