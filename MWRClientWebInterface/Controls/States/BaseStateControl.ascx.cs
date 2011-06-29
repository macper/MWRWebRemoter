﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MWRClientWebInterface.Controls.States
{
	public partial class BaseStateControl : System.Web.UI.UserControl
	{
		public virtual void InitControl(MWRCommonTypes.State state)
		{
		}
	}
}