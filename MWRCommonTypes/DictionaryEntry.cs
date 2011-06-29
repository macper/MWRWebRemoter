using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class DictionaryEntry : BusinessObject
    {
        public string Name { get; set; }
        public Enum.ObjectType Type { get; set; }
        public bool Active { get; set; }
        public string Guid { get; set; }
        public string Assembly { get; set; }
        public string TypeOf { get; set; }
        public string Config { get; set; }
    }
}
