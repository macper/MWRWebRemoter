using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWRCommonTypes;
using MWRCommonTypes.Configuration;
using ProxyServer;
using ProxyServer.BusinessLayer;

namespace MWRServerLib
{
    public class StateProcesser : BaseProcesser<StateConfigEntry>
    {
        public StateProcesser(IServerInterface webServ, string machineGuid, string authToken)
            : base(webServ, machineGuid, authToken)
        {
            DictionaryResponse response = webServ.GetActiveStates(authorizationData);
            if (response.ErrorCode != 0)
            {
                throw new Exception(string.Format("Błąd podczas synchronizacji - kod błędu {0} - {1}", response.ErrorCode, response.ErrorDescription));
            }
            this.objectFactory = new StateFactory(response.DictionaryTable, machine);
        }

        public override MWRCommonTypes.ProcessResult Update(MWRCommonTypes.IMWRObject obj, ProcessResult result)
        {
            ProcessResult ProcResult = new ProcessResult();
            if (result.ErrorCode != 0)
            {
                return result;
            }
            try
            {
                State state = (State)obj;
                StateStruct stateStruct = state.ToStateStruct();
                WebResponse webResponse = webServer.UpdateStateRequest(authorizationData, stateStruct.Guid, stateStruct.XmlInfo);
                ProcResult.ErrorCode = webResponse.ErrorCode;
                ProcResult.ErrorDetails = webResponse.ErrorDescription;
            }
            catch (Exception exc)
            {
                ProcResult.ErrorCode = (int)ResponseCode.ServerError;
                ProcResult.ErrorDetails = exc.ToString();
            }
            return ProcResult;
        }

        public void Process(long counter)
        {
            foreach (StateConfigEntry entry in ((StateFactory)objectFactory).LoadedObjects.Values)
            {
                if (counter % entry.Interval == 0)
                {
                    Process(((StateFactory)objectFactory).Load(entry.Guid));
                }
            }
        }
    }
}
