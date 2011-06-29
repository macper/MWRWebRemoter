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
    /// Summary description for DBMachineTest
    /// </summary>
    [TestClass]
    public class DBMachineTest
    {
        private string connString;
    

        public DBMachineTest()
        {
            connString = Helper.GetConnectionString();
            connection = new MySqlConnection(connString);
            connection.Open();
        }

        private DBMachine GetMachine()
        {
            DBMachine machine = new DBMachine();
            machine.Connection = connection;
            return machine;
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
        public void DBMachineInsertTest()
        {
            DBMachine dbMachine = new DBMachine();
            dbMachine.Connection = connection;
            Machine machine = new Machine();
            machine.Guid = "8558DFA1-34CF-43e0-9BDF-7F9B45017357";
            machine.Name = "Test";
            machine.Description = "Opis";
            machine.IP = "127.0.0.1";
            Assert.IsTrue(dbMachine.Save(machine));
        }

        [TestMethod]
        public void DBMachineDeleteTest1()
        {
            DBMachine dbMachine = GetMachine();
            Assert.IsTrue(dbMachine.Delete("8558DFA1-34CF-43e0-9BDF-7F9B45017357"));
        }

        [TestMethod]
        public void DBMachineLoadTest()
        {
            DBMachineInsertTest();
            DBMachine dbMachine = new DBMachine();
            dbMachine.Connection = connection;
            dbMachine.Load("8558DFA1-34CF-43e0-9BDF-7F9B45017357");
            Machine machine = dbMachine.BusinessObject;
            Assert.IsNotNull(machine);
            Assert.IsTrue(machine.Guid == "8558DFA1-34CF-43e0-9BDF-7F9B45017357");
            Assert.IsTrue(machine.Name == "Test");
            Assert.IsTrue(machine.Description == "Opis");
            Assert.IsTrue(machine.IP == "127.0.0.1");
        }

        [TestMethod]
        public void DBMachineDeleteTest2()
        {
            DBMachine dbMachine = GetMachine();
            dbMachine.Load("8558DFA1-34CF-43e0-9BDF-7F9B45017357");
            dbMachine.Delete();
            Assert.IsNull(dbMachine.BusinessObject);
            dbMachine.Load("8558DFA1-34CF-43e0-9BDF-7F9B45017357");
            Assert.IsNull(dbMachine.BusinessObject);
        }

        [TestMethod]
        public void AddPrivillegesTest()
        {
            DBMachine dbMachine = GetMachine();
            DBMachineInsertTest();
            dbMachine.Load("8558DFA1-34CF-43e0-9BDF-7F9B45017357");
            Assert.IsTrue(dbMachine.AddPrivillges(new int[] { 1, 2, 3, 4 }, 1));
            int[] privilleges = dbMachine.GetPrivilleges("8558DFA1-34CF-43e0-9BDF-7F9B45017357", 1);
            Assert.IsTrue(privilleges.Length == 4);
            dbMachine.ClearPrivilleges(1);
            privilleges = dbMachine.GetPrivilleges("8558DFA1-34CF-43e0-9BDF-7F9B45017357", 1);
            Assert.IsNull(privilleges);
            dbMachine.Delete();
        }
    }
}
