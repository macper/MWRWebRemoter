using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace MWRCommonTypes.Tasks
{
	public class FileManageTask : Task
	{
		public OperationType CurrentOperation { get; set; }
		public string RequestedObjectPath { get; set; }
		public string DestinationObjectPath { get; set; }
		public MWRFileBase ProcessedObject { get; set; }
		public string FileContents { get; set; }
		protected int MaxSubLevel { get; set; }
		protected long MaxFileSizeToGet { get; set; }
		protected FtpSettings FTPSettings { get; set; }

		public static string UniqueID
		{
			get { return "8F913E9A-91CF-4383-9E41-5205DFC79CDF"; }
		}

		public FileManageTask()
			: base(FileManageTask.UniqueID)
		{
		}

		public override ProcessResult Process()
		{
			switch (CurrentOperation)
			{
				case OperationType.GetFile:
					return GetFile();
					break;

				case OperationType.GetDirectory:
					return GetDirectory();
					break;

				case OperationType.PutFile:
					return PutFile();
					break;

				case OperationType.CopyFileToFTP:
					return CopyFileToFTP();
					break;

				case OperationType.CopyFileFromFTP:
					return CopyFileFromFTP();
					break;

				case OperationType.DeleteFile:
					return DeleteFile();
					break;

				default:
					ProcessResult pr = new ProcessResult();
					pr.ErrorCode = 3;
					pr.ErrorDetails = "Nieprawidłowy kod operacji!";
					return pr;
			}
			return new ProcessResult();
		}

		public override void LoadConfig(string configString)
		{
			Dictionary<string, string> config = BusinessObject.GetConfigValues(configString);
			MaxSubLevel = int.Parse(config["MaxSubLevel"]);
			MaxFileSizeToGet = long.Parse(config["MaxFileSize"]);
			FTPSettings = new FtpSettings();
			FTPSettings.Directory = config["FTPDirectory"];
			FTPSettings.Password = config["FTPPassword"];
			FTPSettings.ServerAddress = config["FTPServer"];
			FTPSettings.User = config["FTPUser"];
		}

		protected override void InitFromXML(string xmlRequest, string xmlResponse)
		{
			string tmpStr;
			Dictionary<string, string> reqData = BusinessObject.GetConfigValues(xmlRequest);
			CurrentOperation = (OperationType)(int.Parse(reqData["OperationType"]));
			if (reqData.TryGetValue("RequestedObjectPath", out tmpStr))
			{
				RequestedObjectPath = tmpStr;
			}
			if (reqData.TryGetValue("DestinationObjectPath", out tmpStr))
			{
				DestinationObjectPath = tmpStr;
			}

			if (xmlResponse != null)
			{
				Dictionary<string, string> respData = BusinessObject.GetConfigValues(xmlResponse);
				if (respData.TryGetValue("ProcessedObject", out tmpStr))
				{
					ProcessedObject = MWRFileBase.Deserialize(tmpStr);
				}
				if (respData.TryGetValue("FileContents", out tmpStr))
				{
					FileContents = tmpStr;
				}
			}
		}

		protected override string GetRequestXML()
		{
			Dictionary<string, string> request = new Dictionary<string, string>();
			request["OperationType"] = ((int)CurrentOperation).ToString();
			request["RequestedObjectPath"] = RequestedObjectPath;
			request["DestinationObjectPath"] = DestinationObjectPath;
			return GetRequestResponse(true, request);
		}

		protected override string GetResponseXML()
		{
			Dictionary<string, string> response = new Dictionary<string, string>();
			if (ProcessedObject != null)
			{
				response["ProcessedObject"] = ProcessedObject.Serialize();
			}
			if (FileContents != null)
			{
				response["FileContents"] = FileContents;
			}
			return GetRequestResponse(false, response);
		}

		private ProcessResult GetFile()
		{
			ProcessResult res = new ProcessResult();
			try
			{
				if (RequestedObjectPath == null || RequestedObjectPath == string.Empty)
				{
					res.ErrorCode = 3;
					res.ErrorDetails = "Nie podano odpowiednich parametrów!";
					return res;
				}

				if (!File.Exists(RequestedObjectPath))
				{
					res.ErrorCode = 3;
					res.ErrorDetails = "Plik " + RequestedObjectPath + " nie istnieje.";
					return res;
				}

				FileInfo fi = new FileInfo(RequestedObjectPath);
				if (fi.Length > MaxFileSizeToGet)
				{
					res.ErrorCode = 3;
					res.ErrorDetails = "Plik jest za duży. Wielkość pliku: " + fi.Length.ToString() + " a max. dozwolona to " + MaxFileSizeToGet.ToString();
					return res;
				}

				string fileContents = File.ReadAllText(RequestedObjectPath);
				FileContents = fileContents;
				res.ErrorCode = 0;
				return res;
			}
			catch (Exception exc)
			{
				res.ErrorCode = 3;
				res.ErrorDetails = exc.ToString();
			}
			return res;
		}

		private ProcessResult GetDirectory()
		{
			ProcessResult pr = new ProcessResult();
			pr.ErrorCode = 3;
			try
			{
				if (!Directory.Exists(RequestedObjectPath))
				{
					pr.ErrorDetails = "Katalog " + RequestedObjectPath + " nie istnieje";
					return pr;
				}

				DirectoryInfo di = new DirectoryInfo(RequestedObjectPath);
				MWRFileDirectory root = new MWRFileDirectory(di.Name);
				GetObject(di, root);
				ProcessedObject = root;
				pr.ErrorCode = 0;
			}
			catch (Exception exc)
			{
				pr.ErrorDetails = exc.ToString();
			}
			return pr;
		}

		private ProcessResult PutFile()
		{
			ProcessResult pr = new ProcessResult();
			pr.ErrorCode = 3;
			try
			{
				StreamWriter sw = File.CreateText(RequestedObjectPath);
				sw.Write(FileContents);
				sw.Close();
				pr.ErrorCode = 0;
			}
			catch (Exception exc)
			{
				pr.ErrorDetails = exc.ToString();
			}
			return pr;
		}

		private ProcessResult CopyFileToFTP()
		{
			ProcessResult pr = new ProcessResult();
			FtpClient ftpClient = null;
			pr.ErrorCode = 3;
			try
			{
				if (!File.Exists(RequestedObjectPath))
				{
					pr.ErrorDetails = "Plik " + RequestedObjectPath + " nie istnieje";
					return pr;
				}
				ftpClient = new FtpClient(FTPSettings.ServerAddress, FTPSettings.User, FTPSettings.Password);
				ftpClient.ChangeDir(FTPSettings.Directory);
				ftpClient.Upload(RequestedObjectPath);
				pr.ErrorCode = 0;
			}
			catch (Exception exc)
			{
				pr.ErrorDetails = exc.ToString();
			}
			finally
			{
				try
				{
					if (ftpClient != null)
					{
						ftpClient.Close();
					}
				}
				catch { }
			}
			return pr;
		}

		private ProcessResult CopyFileFromFTP()
		{
			ProcessResult pr = new ProcessResult();
			FtpClient ftpClient = null;
			pr.ErrorCode = 3;
			try
			{
				ftpClient = new FtpClient(FTPSettings.ServerAddress, FTPSettings.User, FTPSettings.Password);
				ftpClient.ChangeDir(FTPSettings.Directory);
				ftpClient.Download(RequestedObjectPath, DestinationObjectPath);
				pr.ErrorCode = 0;
			}
			catch (Exception exc)
			{
				pr.ErrorDetails = exc.ToString();
			}
			finally
			{
				try
				{
					if (ftpClient != null)
					{
						ftpClient.Close();
					}
				}
				catch { }
			}
			return pr;
		}

		private ProcessResult DeleteFile()
		{
			ProcessResult pr = new ProcessResult();
			pr.ErrorCode = 3;
			try
			{
				if (!File.Exists(RequestedObjectPath))
				{
					pr.ErrorDetails = "Plik " + RequestedObjectPath + " nie istnieje";
					return pr;
				}
				File.Delete(RequestedObjectPath);
				pr.ErrorCode = 0;
			}
			catch (Exception exc)
			{
				pr.ErrorDetails = exc.ToString();
			}
			return pr;
		}

		private void GetObject(DirectoryInfo di, MWRFileDirectory root)
		{

			DirectoryInfo [] dSubs = di.GetDirectories();
			foreach (DirectoryInfo dsubInfo in dSubs)
			{
				MWRFileDirectory fDir = new MWRFileDirectory(dsubInfo.Name);
				GetObject(dsubInfo, fDir);
				root.SubFiles.Add(fDir);
			}
			FileInfo [] fInfos = di.GetFiles();
			foreach (FileInfo f in fInfos)
			{
				MWRFileInfo fFile = new MWRFileInfo(f.Name, f.Length);
				root.SubFiles.Add(fFile);
			}
		}
	}

	public enum OperationType
	{
		GetDirectory = 0,
		GetFile = 1,
		PutFile = 2,
		CopyFile = 3,
		CopyFileToFTP = 4,
		CopyFileFromFTP = 5,
		DeleteFile = 6
	}

	public class MWRFileInfo : MWRFileBase
	{
		public long FileSize { get; set; }

		public MWRFileInfo(string name, long fileSize) : base(name, false)
		{
			FileSize = fileSize;
		}
	}

	public abstract class MWRFileBase
	{
		public string Name { get; protected set; }
		public bool IsDirectory { get; protected set;}

		public MWRFileBase(string name, bool IsDir)
		{
			Name = name;
			IsDirectory = IsDir;
		}

		public string Serialize()
		{
			StringBuilder strBld = new StringBuilder();
			strBld.AppendFormat("<{1} Name=\"{0}\"", Name, IsDirectory ? "Directory" : "File");
			if (!IsDirectory)
			{
				strBld.AppendFormat(" Size=\"{0}\" />", ((MWRFileInfo)this).FileSize.ToString());
			}
			else
			{
				strBld.Append(">");
				MWRFileDirectory dir = this as MWRFileDirectory;
				foreach (MWRFileBase bs in dir.SubFiles)
				{
					strBld.Append(bs.Serialize());
				}
				strBld.AppendFormat("</Directory>");
			}
			return strBld.ToString();
		}

		public static MWRFileBase Deserialize(string input)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<root>" + input + "</root>");
			XmlNode rootNode = xmlDoc.FirstChild.ChildNodes[0];
			MWRFileBase Root;
			if (rootNode.Name == "File")
			{
				Root = new MWRFileInfo(rootNode.Attributes["Name"].Value, int.Parse(rootNode.Attributes["Size"].Value));
			}
			else
			{
				Root = new MWRFileDirectory(rootNode.Attributes["Name"].Value);
				FillChilds(rootNode.ChildNodes, Root as MWRFileDirectory);
			}
			return Root;
		}

		private static void FillChilds(XmlNodeList childs, MWRFileDirectory mwrDir)
		{
			if (childs.Count > 0)
			{
				foreach (XmlNode node in childs)
				{
					if (node.Name == "File")
					{
						mwrDir.SubFiles.Add(new MWRFileInfo(node.Attributes["Name"].Value, int.Parse(node.Attributes["Size"].Value)));
					}
					else
					{
						MWRFileDirectory mwrSub = new MWRFileDirectory(node.Attributes["Name"].Value);
						FillChilds(node.ChildNodes, mwrSub);
						mwrDir.SubFiles.Add(mwrSub);
					}
				}
			}
		}
	}

	public class MWRFileDirectory : MWRFileBase
	{
		public List<MWRFileBase> SubFiles { get; set; }

		public MWRFileDirectory(string name) : base(name, true)
		{
			SubFiles = new List<MWRFileBase>();
		}
	}
}
