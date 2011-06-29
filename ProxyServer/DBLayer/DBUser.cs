using System;
using System.Data;
using System.Data.Common;
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

namespace ProxyServer.DBLayer
{
    public class DBUser : DatabaseObject<User, int>
    {
        public DBUser()
        {
        }

        public DBUser(DataProvider dp)
            : base(dp)
        {
        }

        public void Load(int id)
        {
            string sqlCommand = "SELECT * FROM users WHERE id = " + id.ToString();
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(sqlCommand, Connection)))
            {
                DataTable res = new DataTable();
                adapter.Fill(res);

                if (res.Rows.Count != 1)
                {
                    businessObject = null;
                    return;
                }

                businessObject = new User();
                businessObject.Name = (string)res.Rows[0]["name"];
                businessObject.Group = (int)res.Rows[0]["group_id"];
                businessObject.Password = (string)res.Rows[0]["password"];
                businessObject.RegisterDate = (DateTime)res.Rows[0]["registered"];
                this.id = id;
                businessObject.ID = id;
            }
        }

        public void Load(string username, string password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(password);
            string hashedPass = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-","").ToLower();
            string sqlCommand = string.Format("SELECT * FROM users WHERE name = '{0}' AND password = '{1}'", username, hashedPass);
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(sqlCommand, Connection)))
            {
                DataTable result = new DataTable();
                adapter.Fill(result);
                if (result.Rows.Count == 0)
                {
                    businessObject = null;
                    return;
                }
                if (result.Rows.Count > 1)
                {
                    throw new DataException("Tabela users zawiera więcej niż jednego usera o tej samej nazwie i haśle");
                }
                businessObject = new User();
                businessObject.Name = result.Rows[0]["name"] as string;
                businessObject.Password = result.Rows[0]["password"] as string;
                businessObject.Group = (int)result.Rows[0]["group_id"];
                businessObject.RegisterDate = (DateTime)result.Rows[0]["registered"];
                id = (int)result.Rows[0]["id"];
                businessObject.ID = id;
            }
        }

        public override bool Save(User objectToAdd)
        {
            string sqlCommand = string.Format("INSERT INTO users(name, password, group_id, registered) VALUES ('{0}',md5('{1}'),{2},now())",
                objectToAdd.Name, objectToAdd.Password, objectToAdd.Group);
            return ExecuteNonQuery(sqlCommand);
        }

        public override bool Save()
        {
            if (businessObject == null)
            {
                throw new ArgumentException("Business Object jest nullem!");
            }
            string sqlCom = string.Format("UPDATE users SET name = '{0}', password = '{1}', group_id = {2}, registered = '{3}' WHERE id = {4}",
                businessObject.Name, businessObject.Password, businessObject.Group, businessObject.RegisterDate, ID);

            return ExecuteNonQuery(sqlCom);
        }

        public override bool Delete()
        {
            if (businessObject == null)
            {
                throw new ArgumentException("Business Object jest nullem!");
            }
            string strDeleteSQL = "DELETE FROM users WHERE id = " + (int)ID;
            businessObject = null;
            return ExecuteNonQuery(strDeleteSQL);
        }

        public bool Delete(int id)
        {
            string strCommand = "DELETE FROM users WHERE id = " + id;
            return ExecuteNonQuery(strCommand);
        }

    }
}
