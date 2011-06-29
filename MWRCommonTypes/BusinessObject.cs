using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MWRCommonTypes
{
    public class BusinessObject
    {
        public static Dictionary<string, string> GetConfigValues(string xmlString)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            foreach (XmlNode xmlNode in xmlDoc.ChildNodes[0].ChildNodes)
            {
					if (xmlNode.InnerXml != string.Empty)
					{
						dic.Add(xmlNode.Name, xmlNode.InnerXml);
					}
					else
					{
						dic.Add(xmlNode.Name, xmlNode.InnerText);
					}
            }
            return dic;
        }
    }
}
