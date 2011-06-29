using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWRCommonTypes.Configuration;
using System.Collections;
using System.Xml;

namespace MWRCommonTypes
{
    public abstract class BaseFactory<T> where T:DictionaryEntry
    {
        protected Dictionary<string, T> loadedObjects;
        protected Machine machine;
        public Dictionary<string, T> LoadedObjects { get { return loadedObjects; } }

        public abstract IMWRObject Load(IDictObject dictStruct);

        protected Hashtable GetConfigTable(string xmlConfig)
        {
            Hashtable htRet = new Hashtable();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlConfig);
            foreach (XmlNode node in xmlDoc.ChildNodes[0].ChildNodes)
            {
                htRet[node.Name] = node.InnerText;
            }
            return htRet;
        }

        public BaseFactory(Machine host)
        {
            machine = host;
        }
    }
}
