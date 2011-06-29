using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public class TaskFactory : BaseFactory<DictionaryEntry>
    {
        public override IMWRObject Load(IDictObject dictStruct)
        {
            TaskStruct taskStruct = dictStruct as TaskStruct;
            if (!loadedObjects.ContainsKey(taskStruct.Guid))
            {
                throw new ArgumentException("Nie załadowano taska - " + taskStruct.Guid);
            }
            Task newTask = (Task)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(loadedObjects[taskStruct.Guid].Assembly, loadedObjects[taskStruct.Guid].TypeOf);
            newTask.Init(taskStruct);
            newTask.Name = loadedObjects[taskStruct.Guid].Name;
            newTask.LoadConfig(loadedObjects[taskStruct.Guid].Config);
            return newTask;
        }

        public TaskFactory(DictionaryEntry[] dictionary, Machine host)
            : base(host)
        {
            loadedObjects = new Dictionary<string, DictionaryEntry>();
            foreach (DictionaryEntry entry in dictionary)
            {
                loadedObjects.Add(entry.Guid, entry);
            }
        }

        public Task GetEmptyTask(string guid)
        {
            if (!loadedObjects.ContainsKey(guid))
            {
                throw new ArgumentException("Nie załadowano taska - " + guid);
            }
            Task newTask = (Task)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(loadedObjects[guid].Assembly, loadedObjects[guid].TypeOf);
            return newTask;
        }
    }
}
