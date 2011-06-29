using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MWRCommonTypes;
using ProxyServer.DBLayer;

namespace Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DBUserTest
    {
        string connString = "server=localhost;user id=proxy;database=proxydb; password=mac21";
        public DBUserTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void LoadUserTest()
        {
            DBUser user = new DBUser();
            User bUser = new User();
            user.Connection = new MySqlConnection(connString);
            user.Connection.Open();
            user.Load(1);
            Assert.IsNotNull(user.BusinessObject);
            Assert.IsTrue(user.BusinessObject.Name == "Tester");
        }

        [TestMethod]
        public void LoadUser2Test()
        {
            using (DBUser dbUser = new DBUser())
            {
                User user = new User();
                dbUser.Connection = new MySqlConnection(connString);
                dbUser.Connection.Open();
                dbUser.Load("Tester", "test");
                Assert.IsNotNull(dbUser.BusinessObject);
            }
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            using (DBUser dbUser = new DBUser())
            {
                User user = new User();
                dbUser.Connection = new MySqlConnection(connString);
                dbUser.Connection.Open();
                user.Name = "TestUpdate";
                user.Password = "test";
                user.Group = 1;
                Assert.IsTrue(dbUser.Save(user));
                int lastID = (int)dbUser.LastID;
                dbUser.Load(lastID);
                Assert.IsNotNull(dbUser.BusinessObject);
                dbUser.BusinessObject.Group = 2;
                dbUser.Save();
                dbUser.Load(lastID);
                Assert.IsTrue(dbUser.BusinessObject.Name == "TestUpdate");
                Assert.IsTrue(dbUser.BusinessObject.Group == 2);
                dbUser.Delete();
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            using (DBUser dbUser = new DBUser())
            {
                User user = new User();
                dbUser.Connection = new MySqlConnection(connString);
                dbUser.Connection.Open();
                user.Name = "TestDelete";
                user.Password = "test";
                user.Group = 1;
                Assert.IsTrue(dbUser.Save(user));
                int lastID = (int)dbUser.LastID;
                dbUser.Load(lastID);
                Assert.IsNotNull(dbUser.BusinessObject);
                dbUser.Delete();
                dbUser.Load(lastID);
                Assert.IsNull(dbUser.BusinessObject);
                Assert.IsTrue(dbUser.Save(user));
                lastID = (int)dbUser.LastID;
                dbUser.Delete(lastID);
                dbUser.Load(lastID);
                Assert.IsNull(dbUser.BusinessObject);
            }
        }

        [TestMethod]
        public void GetUsers()
        {
            using (DBUserCollection users = new DBUserCollection(new SqlDataProvider()))
            {
                users.Connection = new MySqlConnection(connString);
                users.Connection.Open();
                users.Load();
                Assert.IsTrue(users.List.Count > 0);
            }
        }
    }
}
