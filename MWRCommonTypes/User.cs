using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class User : BusinessObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Group { get; set; }
    }
}
