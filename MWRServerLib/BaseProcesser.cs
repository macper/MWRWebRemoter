using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWRCommonTypes;
using System.Threading;
using MWRCommonTypes.Configuration;
using ProxyServer;
using ProxyServer.BusinessLayer;

namespace MWRServerLib
{
    public abstract class BaseProcesser<T> where T:DictionaryEntry
    {
        protected BaseFactory<T> objectFactory;
        public event Notifier ProcessStarted;
        public event Notifier ProcessCompleted;
        public event Notifier UpdateCompleted;
        public event TextNotifier Message;

        protected IServerInterface webServer;
        protected ServerAuthStruct authorizationData;

        protected Machine machine;
        public int LoadedObjectCount
        {
            get { return objectFactory.LoadedObjects.Count; }
        }

        public BaseProcesser(IServerInterface webServ, string machineGuid, string authToken)
        {
            ThreadPool.SetMaxThreads(100, 200);
            webServer = webServ;
            authorizationData = new ServerAuthStruct();
            authorizationData.MachineGuid = machineGuid;
            authorizationData.AuthToken = authToken;
            machine = new Machine();
            machine.Guid = machineGuid;
            machine.Name = authToken;
        }

        public abstract ProcessResult Update(IMWRObject obj, ProcessResult processRes);

        public virtual void Process(IDictObject obj)
        {
            IMWRObject loadedObj = objectFactory.Load(obj);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Process), loadedObj);
        }

        protected void Process(object obj)
        {
            IMWRObject mwrObject = (IMWRObject)obj;
            ProcessStarted(mwrObject, null);
            ProcessResult res = mwrObject.Process();
            ProcessCompleted(mwrObject, res);
            OnProcessCompleted(mwrObject, res);
        }

        protected virtual void OnProcessCompleted(IMWRObject obj, ProcessResult res)
        {
            ProcessResult result = Update(obj,res);
            UpdateCompleted(obj, result);
        }

        protected void MessageHit(string message, System.Diagnostics.TraceEventType type)
        {
            Message(message, type, null);
        }
    }
}
