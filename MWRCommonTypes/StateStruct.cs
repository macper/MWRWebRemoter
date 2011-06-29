using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class StateStruct : BusinessObject, IDictObject
    {
        public string Guid { get; set; }
        public string XmlInfo { get; set; }
        public int ID { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Machine { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
