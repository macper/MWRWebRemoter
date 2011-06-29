using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class Machine : BusinessObject
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
    }
}
