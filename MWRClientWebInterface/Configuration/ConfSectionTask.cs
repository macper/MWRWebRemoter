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

namespace MWRClientWebInterface.Configuration
{
   public class ConfSectionTask : ConfigurationSection
   {
	  [ConfigurationProperty("Tasks")]
	  public ConfigTaskCollection Tasks
	  {
		 get { return (ConfigTaskCollection)this["Tasks"]; }
		 set { this["Tasks"] = value; }
	  }
   }

   	public class ConfigTaskCollection : ConfigurationElementCollection
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
}
