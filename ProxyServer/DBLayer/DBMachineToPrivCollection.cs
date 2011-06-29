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
using MWRCommonTypes;
using System.Data.Common;
using System.Collections.Generic;

namespace ProxyServer.DBLayer
{
    public class DBMachineToPrivCollection : DBBaseObject
    {
        public DBMachineToPrivCollection(DataProvider prov)
            : base(prov)
        {
        }

        public MachineWithPrivilleges[] Load(int groupID)
        {
            Dictionary<string, MachineWithPrivilleges> list = new Dictionary<string,MachineWithPrivilleges>();
            string sqlCmd = "SELECT * FROM machines LEFT JOIN privilleges ON guid = machine WHERE group_id = " + groupID.ToString();
            using (DbDataAdapter sqlAdapter = dataProvider.GetAdapter(dataProvider.GetCommand(sqlCmd, connection)))
            {
                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MachineWithPrivilleges mwPriv;
                        if (list.ContainsKey(dr["guid"].ToString()))
                        {
                            mwPriv = list[dr["guid"].ToString()];
                        }
                        else
                        {
                            mwPriv = new MachineWithPrivilleges();
                            mwPriv.Description = dr["description"] != DBNull.Value ? (string)dr["description"] : null;
                            mwPriv.Guid = dr["guid"].ToString();
                            mwPriv.IP = (string)dr["ip"];
                            mwPriv.Name = (string)dr["name"];
                            list.Add(mwPriv.Guid, mwPriv);
                        }
                        
                        if (mwPriv.Privilleges == null)
                        {
                            mwPriv.Privilleges = new int[1];
                            mwPriv.Privilleges[0] = (int)dr["permission"];
                        }
                        else
                        {
                            int [] temp = mwPriv.Privilleges;
                            mwPriv.Privilleges = new int[temp.Length+1];
                            temp.CopyTo(mwPriv.Privilleges, 0);
                            mwPriv.Privilleges[mwPriv.Privilleges.Length - 1] = (int)dr["permission"];
                        }
                        
                    }
                }
            }
            return list.Values.ToArray();
        }
    }
}
