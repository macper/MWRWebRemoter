using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MWRCommonTypes;

namespace Test
{
	/// <summary>
	/// Summary description for FileManagerTest
	/// </summary>
	[TestClass]
	public class FileManagerTest
	{
		public FileManagerTest()
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
		public void InitTest1()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.GetFile;
			ft.RequestedObjectPath = "d:\test\test.txt";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();
		}

		[TestMethod]
		public void GetFileTest()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.GetFile;
			ft.RequestedObjectPath = "d:\\test\\test.txt";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();

			DictionaryEntry entry = new DictionaryEntry();
			entry.Active = true;
			entry.Config = "<config><MaxSubLevel>5</MaxSubLevel><MaxFileSize>100000</MaxFileSize></config>";
			entry.Guid = MWRCommonTypes.Tasks.FileManageTask.UniqueID;
			entry.Type = MWRCommonTypes.Enum.ObjectType.Task;
			entry.TypeOf = "MWRCommonTypes.Tasks.FileManageTask";

			MWRCommonTypes.Tasks.FileManageTask ftNew = new MWRCommonTypes.Tasks.FileManageTask();
			ftNew.Init(ts);
			ftNew.LoadConfig(entry.Config);

			ProcessResult pr = ftNew.Process();

			TaskStruct toUpdate = ftNew.ToTaskStruct();
			Task toClient = new MWRCommonTypes.Tasks.FileManageTask();
			toClient.Init(toUpdate);
		}

		[TestMethod]
		public void GetDirectoryTest()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.GetDirectory;
			ft.RequestedObjectPath = "E:\\Konfiguracja";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();

			DictionaryEntry entry = new DictionaryEntry();
			entry.Active = true;
			entry.Config = "<config><MaxSubLevel>5</MaxSubLevel><MaxFileSize>100000</MaxFileSize></config>";
			entry.Guid = MWRCommonTypes.Tasks.FileManageTask.UniqueID;
			entry.Type = MWRCommonTypes.Enum.ObjectType.Task;
			entry.TypeOf = "MWRCommonTypes.Tasks.FileManageTask";

			MWRCommonTypes.Tasks.FileManageTask ftNew = new MWRCommonTypes.Tasks.FileManageTask();
			ftNew.Init(ts);
			ftNew.LoadConfig(entry.Config);

			ProcessResult pr = ftNew.Process();

			TaskStruct toUpdate = ftNew.ToTaskStruct();
			Task toClient = new MWRCommonTypes.Tasks.FileManageTask();
			toClient.Init(toUpdate);
		}

		[TestMethod]
		public void PutFileTest()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.PutFile;
			ft.RequestedObjectPath = "d:\\test\\test2.txt";
			ft.FileContents = "test2";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();

			MWRCommonTypes.Tasks.FileManageTask ftNew = new MWRCommonTypes.Tasks.FileManageTask();
			ftNew.Init(ts);

			ProcessResult pr = ftNew.Process();

			TaskStruct toUpdate = ftNew.ToTaskStruct();
			Task toClient = new MWRCommonTypes.Tasks.FileManageTask();
			toClient.Init(toUpdate);
		}

		[TestMethod]
		public void FtpPutTest()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.CopyFileToFTP;
			ft.RequestedObjectPath = "d:\\test\\test.txt";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();

			MWRCommonTypes.Tasks.FileManageTask ftNew = new MWRCommonTypes.Tasks.FileManageTask();
			ftNew.Init(ts);
			ftNew.LoadConfig("<config><MaxSubLevel>5</MaxSubLevel><MaxFileSize>100000</MaxFileSize><FTPServer>localhost</FTPServer><FTPUser>maciek</FTPUser><FTPPassword>mac21</FTPPassword><FTPDirectory>pliki</FTPDirectory></config>");

			ProcessResult pr = ftNew.Process();

			TaskStruct toUpdate = ftNew.ToTaskStruct();
			Task toClient = new MWRCommonTypes.Tasks.FileManageTask();
			toClient.Init(toUpdate);
		}

		[TestMethod]
		public void FtpGetTest()
		{
			MWRCommonTypes.Tasks.FileManageTask ft = new MWRCommonTypes.Tasks.FileManageTask();
			ft.CurrentOperation = MWRCommonTypes.Tasks.OperationType.CopyFileFromFTP;
			ft.RequestedObjectPath = "test.txt";
			ft.DestinationObjectPath = "E:\\test.txt";
			FillData(ft);
			TaskStruct ts = ft.ToTaskStruct();

			MWRCommonTypes.Tasks.FileManageTask ftNew = new MWRCommonTypes.Tasks.FileManageTask();
			ftNew.Init(ts);
			ftNew.LoadConfig("<config><MaxSubLevel>5</MaxSubLevel><MaxFileSize>100000</MaxFileSize><FTPServer>localhost</FTPServer><FTPUser>maciek</FTPUser><FTPPassword>mac21</FTPPassword><FTPDirectory>pliki</FTPDirectory></config>");

			ProcessResult pr = ftNew.Process();

			TaskStruct toUpdate = ftNew.ToTaskStruct();
			Task toClient = new MWRCommonTypes.Tasks.FileManageTask();
			toClient.Init(toUpdate);
		}


		private void FillData(Task tsk)
		{
			Machine m = new Machine();
			User u = new User();
			tsk.Machine = m;
			tsk.User = u;
		}
	}
}
