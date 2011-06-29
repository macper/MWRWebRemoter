using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Configuration;

namespace MWRClientWebInterface.Configuration
{
	public class ConfSectionState : ConfigurationSection
	{
		[ConfigurationProperty("States")]
		public ConfigStateCollection States
		{
			get { return (ConfigStateCollection)this["States"]; }
			set { this["States"] = value; }
		}
	}

	public class ConfigStateCollection : ConfigurationElementCollection
	{

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConfigSectionElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigSectionElement)element).UniqueID;
		}

		public ConfigSectionElement this[string guid]
		{
			get
			{
				foreach (ConfigSectionElement el in this)
				{
					if (el.UniqueID == guid)
					{
						return el;
					}
				}
				return null;
			}
		}

		public override ConfigurationElementCollectionType CollectionType
		{
		   get
		   {
			  return ConfigurationElementCollectionType.AddRemoveClearMap;
		   }
		}
	}

	public class ConfigSectionElement : ConfigurationElement
	{
		[ConfigurationProperty("Name")]
		public string Name
		{
			get { return (string)this["Name"]; }
			set { this["Name"] = value; }
		}

		[ConfigurationProperty("VirtualPath")]
		public string VirtualPath
		{
			get { return (string)this["VirtualPath"]; }
			set { this["VirtualPath"] = value; }
		}

		[ConfigurationProperty("UniqueID")]
		public string UniqueID
		{
			get { return (string)this["UniqueID"]; }
			set { this["UniqueID"] = value; }
		}
	}
}
