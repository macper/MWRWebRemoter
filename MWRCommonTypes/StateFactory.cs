using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using MWRCommonTypes.Configuration;
using System.Xml;
using System.Collections;

namespace MWRCommonTypes
{
    public class StateFactory : BaseFactory<StateConfigEntry>
    {
        public StateFactory(DictionaryEntry [] stateEntries, Machine host) : base(host)
        {
            loadedObjects = new Dictionary<string, StateConfigEntry>();
            foreach (DictionaryEntry entry in stateEntries)
            {
                StateConfigEntry stateEntry = new StateConfigEntry();
                stateEntry.Active = entry.Active;
                stateEntry.Assembly = entry.Assembly;
                stateEntry.Config = entry.Config;
                stateEntry.Guid = entry.Guid;
                stateEntry.Name = entry.Name;
                stateEntry.Type = entry.Type;
                stateEntry.TypeOf = entry.TypeOf;
                if (stateEntry.Type != MWRCommonTypes.Enum.ObjectType.State)
                {
                    throw new ArgumentException("Nieprawidłowy typ dictionary - ma być State!");
                }
                Hashtable stateConfigTable = GetConfigTable(stateEntry.Config);
                stateEntry.Interval = int.Parse((string)stateConfigTable["Interval"]);
                loadedObjects.Add(stateEntry.Guid, stateEntry);
            }
        }

        public override IMWRObject Load(IDictObject objStruct)
        {
            StateStruct stateStruct = objStruct as StateStruct;
            if (!loadedObjects.ContainsKey(stateStruct.Guid))
            {
                throw new ArgumentException("Nie załadowano stanu o guid " + stateStruct.Guid);
            }
            State newState = (State)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(loadedObjects[stateStruct.Guid].Assembly, loadedObjects[stateStruct.Guid].TypeOf);
            newState.FromStateStruct(stateStruct);
            newState.Init(stateStruct.XmlInfo);
            newState.LoadConfig(loadedObjects[stateStruct.Guid].Config);
            return newState;
        }

        public State Load(string guid)
        {
            State newState = (State)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(loadedObjects[guid].Assembly, loadedObjects[guid].TypeOf);
            newState.Name = loadedObjects[guid].Name;
            newState.Machine = machine;
            newState.LoadConfig(loadedObjects[guid].Config);
            return newState;
        }


    }
}
