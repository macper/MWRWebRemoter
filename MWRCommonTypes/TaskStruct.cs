using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class TaskStruct : BusinessObject, IDictObject
    {
        public string Guid { get; set; }
        public int ID { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateSended { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateChecked { get; set; }
        public DateTime DateToExecute { get; set; }
        public int User { get; set; }
        public string Machine { get; set; }
        public int State { get; set; } 
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
		  public string ErrorDetails { get; set; }
    }
}
