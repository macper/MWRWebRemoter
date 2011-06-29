using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using MWRCommonTypes;
using ProxyServer.DBLayer;

namespace Test
{
    /// <summary>
    /// Summary description for DBDictionaryTest
    /// </summary>
    [TestClass]
    public class DBDictionaryTest
    {
        public DBDictionaryTest()
        {
            
        }

        private DbConnection GetConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(Helper.GetConnectionString());
            con.Open();
            return con;
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
        public void InsertTest()
        {
            DBDictionary dbDictionary = new DBDictionary();
            dbDictionary.Connection = GetConnection();
            DictionaryEntry entry = new DictionaryEntry();
            entry.Active = true;
            entry.Guid = "AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503";
            entry.Name = "Test";
            entry.Type = MWRCommonTypes.Enum.ObjectType.State;
            Assert.IsTrue(dbDictionary.Save(entry));
        }

        [TestMethod]
        public void LoadTest()
        {
            DBDictionary dbDictionary = new DBDictionary();
            dbDictionary.Connection = GetConnection();
            dbDictionary.Load("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
            Assert.IsNotNull(dbDictionary.BusinessObject);
            Assert.IsTrue(dbDictionary.BusinessObject.Active);
            Assert.IsTrue(dbDictionary.BusinessObject.Name == "Test");
            Assert.IsTrue(dbDictionary.BusinessObject.Type == MWRCommonTypes.Enum.ObjectType.State);
        }

        [TestMethod]
        public void UpdateTest()
        {
            DBDictionary dbDictionary = new DBDictionary();
            dbDictionary.Connection = GetConnection();
            dbDictionary.Load("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
            dbDictionary.BusinessObject.Active = false;
            dbDictionary.BusinessObject.Name = "Test2";
            dbDictionary.BusinessObject.Type = MWRCommonTypes.Enum.ObjectType.Task;
            dbDictionary.Save();
            dbDictionary.Load("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
            Assert.IsFalse(dbDictionary.BusinessObject.Active);
            Assert.IsTrue(dbDictionary.BusinessObject.Name == "Test2");
            Assert.IsTrue(dbDictionary.BusinessObject.Type == MWRCommonTypes.Enum.ObjectType.Task);
        }

        [TestMethod]
        public void DeleteTest()
        {
            DBDictionary dbDictionary = new DBDictionary();
            dbDictionary.Connection = GetConnection();
            dbDictionary.Delete("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
            dbDictionary.Load("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
            Assert.IsNull(dbDictionary.BusinessObject);
        }

        [TestMethod]
        public void CollectionTest()
        {
            InsertTest();
            DBDictionary dbDictionary = new DBDictionary();
            dbDictionary.Connection = GetConnection();
            DictionaryEntry entry = new DictionaryEntry();
            entry.Active = true;
            entry.Guid = "69E68ADF-A083-4889-BC11-8353B8CA5EAD";
            entry.Name = "Test";
            entry.Type = MWRCommonTypes.Enum.ObjectType.State;
            dbDictionary.Save(entry);
            DBDictionaryCollection collection = new DBDictionaryCollection(new SqlDataProvider());
            collection.Connection = GetConnection();
            collection.Load();
            Assert.IsTrue(collection.List.Count == 2);
            dbDictionary.Delete();
            dbDictionary.Delete("AEF29AAA-0F23-4ba3-AA10-BAE05B3E4503");
        }

    }
}
