using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public abstract class State : BusinessObject, IMWRObject
    {

        #region IMWRObject Members

        public MWRCommonTypes.Enum.ObjectType Type
        {
            get { return MWRCommonTypes.Enum.ObjectType.State; }
        }

        protected string guid;
        public string Guid { get { return guid; } }

        public Machine Machine { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public State(string guid)
        {
            this.guid = guid;
        }

        #endregion

        public StateStruct ToStateStruct()
        {
            StateStruct retStruct = new StateStruct();
            retStruct.Active = Active;
            retStruct.Guid = Guid;
            retStruct.Machine = Machine.Guid;
            retStruct.RegisteredDate = RegisteredDate;
            retStruct.Name = Name;
            retStruct.XmlInfo = GetInformationXML();
            return retStruct;
        }

        public void FromStateStruct(StateStruct stateStruct)
        {
            Active = stateStruct.Active;
            Machine = new Machine();
            Machine.Guid = stateStruct.Guid;
            RegisteredDate = stateStruct.RegisteredDate;
            Name = stateStruct.Name;
        }
        protected abstract string GetInformationXML();
        public abstract void Init(string xmlInformation);
        public abstract ProcessResult Process();
        public abstract void LoadConfig(string configString);
    }
}
