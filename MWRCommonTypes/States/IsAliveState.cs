using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes.States
{
    class IsAliveState : State
    {
        public IsAliveState() : base("2A55B578-132D-461d-AA8B-881A16B42A4E")
        {
        }

        protected override string GetInformationXML()
        {
            return string.Empty;
        }

        public override void Init(string xmlInformation)
        {
        }

        public override ProcessResult Process()
        {
            ProcessResult result = new ProcessResult();
            result.ErrorCode = 0;
            return result;
        }

        public override void LoadConfig(string configString)
        {
        }
    }
}
