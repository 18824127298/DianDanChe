using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Specialized;

namespace Sigbit.Net.WebTinyServer
{
	/// <summary>
	/// the various levels of logging possible
	/// </summary>
	public enum WebTinyServerLogKind {Informational, Warning, Error, None};

	/// <summary>
	/// TinyServer
	/// Copyright (c) 2004 Stephan Meyn
	/// 
	/// A tiny web server that an application can host (e.g. a news aggregator)
	/// This tiny web server only understands HTTP GET commands, nothing else - 
	/// no PUT of forms.
	/// However it is ideal for building your own specialised web server. 
	/// You can subclass TinyServer and override the function doGet() to 
	/// implement special services.
	/// </summary>
	public class WebTinyServer
	{
		#region 常量定义
		private const string serverID = "TinyServer V0.1";

		#endregion

		#region 私有属性 
		/// <summary>
		/// ip address of client
		/// </summary>
		private IPAddress ia;
		/// <summary>
		/// parameters provided in last call
		/// </summary>
		protected StringDictionary parameters;
		private Socket serverSock;
		private int serverPort = 0;
		private NetworkStream ns;    //for reading & writing data to the client
		private TextReader reader;
		Thread runningThread = null;
		//private bool secure;   // is this connection secure - i.e. from localhost or nonsecure version?
		/// <summary>
		/// hostname for current connection
		/// </summary>
		private string hname;
		private bool ready = false;
		/// <summary>
		/// configured item where all templates are stored
		/// </summary>
		private string templatePath = string.Empty;

		private string defaultPageName = "index.html";

		/// <summary>
		/// path to the web directory
		/// </summary>
		private string webRootPath = string.Empty;

		private string logFile = string.Empty;
		private WebTinyServerLogKind logLevel = WebTinyServerLogKind.None;
		#endregion

		#region 保护属性
		/// <summary>
		/// name of host from last call
		/// </summary>
		protected string HostName { get { return hname;} }

		/// <summary>
		/// ip address for current call
		/// </summary>
		protected IPAddress CurrentAddress { get { return ia;} }
		#endregion

		#region 公共属性
		/// <summary>
		/// the port the webserver will run on.
		/// </summary>
		public int Port { get { return serverPort;} set {serverPort = value;}}
		/// <summary>
		/// path to the web pages
		/// </summary>
		public string WebRootPath { get { return webRootPath;} set { webRootPath = value;}}
		/// <summary>
		/// default web page name
		/// </summary>
		public string DefaultPage { get { return defaultPageName;} set { defaultPageName = value;}}
		/// <summary>
		/// path to templates used by the server
		/// </summary>
		public string Templates { get { return templatePath;} set { templatePath = value;}}
		/// <summary>
		/// logfile name. set to empty string to log to console
		/// </summary>
		public string LogFile { get { return logFile;} set { logFile = value;}}

		/// <summary>
		/// level of logging. set to None to stop logging
		/// </summary>
		public WebTinyServerLogKind LogLevel { get { return logLevel;} set { logLevel = value;}}

		/// <summary>
		/// informational, is the web server running
		/// </summary>
		public bool Running { get { return ready; }}
		#endregion

		#region 构造函数
		public WebTinyServer()
		{
			parameters = new StringDictionary();
		}
		#endregion

		#region 服务器处理逻辑
		/// <summary>
		/// check if we can actually run. If not throw an exception
		/// </summary>
		protected virtual void checkCanRun()
		{
			if (templatePath.Length == 0)
				throw new TinyServerException("配置错误: 临时文件路经不能文空");
			if (defaultPageName.Length == 0)
                throw new TinyServerException("配置错误: 默认页面没有设置");
			if (webRootPath.Length == 0)
                throw new TinyServerException("配置错误: 根目录没有设置");
			if (serverPort == 0)
                throw new TinyServerException("配置错误: 访问端口没有设置");
		}
		/// <summary>
		/// listen to port and get HTTP calls
		/// </summary>
		public void Run()
		{
			if (runningThread != null)
			{
				runningThread.Abort();
			}
			runningThread = new Thread(new ThreadStart(runThread));
			runningThread.IsBackground = true;
			runningThread.Start();
		}
		/// <summary>
		/// stop web server
		/// </summary>
		public void Stop()
		{
			if (runningThread != null)
				try
				{
					runningThread.Abort();
				}
				finally
				{
					runningThread = null;
				}
		}
		/// <summary>
		/// the threadstart of the web server. called by Run()
		/// </summary>
		private void runThread()
		{
			checkCanRun();
			try
			{
				serverSock = new  Socket(AddressFamily.InterNetwork, 
					SocketType.Stream,ProtocolType.Tcp);

				IPEndPoint ipe = new IPEndPoint(IPAddress.Any, serverPort);
				serverSock.Bind(ipe);
				log(WebTinyServerLogKind.Informational, "Web Server 启动时间:{0}",serverPort);
				ready = true;
				loop();                                          
			}
            catch (Exception e) 
            {
                log(WebTinyServerLogKind.Error, "运行错误时间:{0}",DateTime.Now.ToString());
                log(WebTinyServerLogKind.Error, e.Message);
                log(WebTinyServerLogKind.Error, e.StackTrace);
                sendOk();
            }
            finally
            {
                //ns.Close();
                //serverSock.Close(); // do we need this
                //ready = false;
                log(WebTinyServerLogKind.Informational, "Web Server 结束时间: {0}", DateTime.Now);
            }
		}
		///<summary>
		/// loop listening to commands. 
		/// </summary>
		private void loop()
		{
			for (;;) 
			{
				log(WebTinyServerLogKind.Informational,"循环开始");
				Socket s = acceptConnection();
				String command = getCommand();
				command = System.Web.HttpUtility.UrlDecode(command);
                if (command.StartsWith("GET"))
                {
                    doGet(CommandArg(command));
                }
                else
                {
                    doUnknownCommand(command);
                }
                log(WebTinyServerLogKind.Informational, "循环结束");
				ns.Flush();
				ns.Close();
			}
		}
		#endregion

		#region 命令处理
		/// <summary>
		/// handle a get command. Override this method for your own implementation
		/// </summary>
		/// <param name="argument">the argument of the get command</param>
		protected virtual void doGet(string argument)
		{
			string url = getUrl(argument);
			if (url.StartsWith("/"))
				url = url.Substring(1);
			if (url.Length == 0)
				url = defaultPageName;
			
			string path = Path.Combine(webRootPath, url);
			if (File.Exists(path))
			{
				sendOk();
				sendfile(path);
			}
			else
				sendError(404, "File Not Found");
		}
		/// <summary>
		/// handle http commands other than GET.
		/// Override this function to implement extra commands.
		/// This demonstrates how to use templates
		/// </summary>
		/// <param name="command"></param>
		protected virtual void doUnknownCommand(string command)
		{
			log(WebTinyServerLogKind.Warning, "unknown request:{0}", command);
			sendTemplate("header.html");
			sendTemplate("HOME.html");
			sendTemplate("trailer.html");
		}
		/// <summary>
		/// take an argument starting with a URL 
		/// and retrieve the URL in a form usable for System.IO
		/// </summary>
		/// <param name="argument"></param>
		/// <returns></returns>
		protected string getUrl(string argument)
		{
			StringBuilder sb = new StringBuilder();
			CharEnumerator argChar = argument.GetEnumerator();
			while (argChar.MoveNext())
			{
				char c = argChar.Current;
				if (c == '?')
					break;
				if (Char.IsWhiteSpace(c))
					break;
				sb.Append(c);
			}
			return sb.ToString();
		}
		/// <summary>
		/// determine the arguments in a command
		/// </summary>
		/// <param name="command"></param>
		/// <returns>array of arguments</returns>
		protected string [] urlArgs(string command)
		{
			int qMarkIdx = command.IndexOf("?");
			string argsString = command.Substring(qMarkIdx+1);
			string [] args = argsString.Split('&');
			return args;
		}			
		#endregion

		#region 访问请求处理
		/// <summary>
		/// accept a connection and return a TCPClient
		/// </summary>
		/// <returns></returns>
		private Socket acceptConnection() 
		{
			serverSock.Listen(1);
			Socket s =	serverSock.Accept();
			ns = new NetworkStream(s, System.IO.FileAccess.ReadWrite, true); // so when to close this one?
			reader = new StreamReader(ns);
			IPEndPoint ep =(IPEndPoint) s.RemoteEndPoint;
			ia = ep.Address;
			//hname = Dns.GetHostByAddress(ia).HostName;
            hname = Dns.GetHostEntry(ia).HostName;
			
			log(WebTinyServerLogKind.Informational, "接受连接终端:{0}", hname);

			return s;
		}

		/// <summary>
		/// get a command from a connection, return the first line
		/// and store the rest as parameters
		/// </summary>
		/// <returns></returns>
		private String getCommand() 
		{
			parameters.Clear();
			String buf = "";
			String command = "";
			bool first = true;
			try 
			{
				log(WebTinyServerLogKind.Informational, "---- 开始接收命令 ----");
				while ((buf = reader.ReadLine()) != null && buf.Length > 0) 
				{
					log(WebTinyServerLogKind.Informational, "接收内容:'{0}'", buf);
					//string[] args = buf.Split(' ');
					if (first)
					{
						command = buf;
						first = false;
					}
					else
					{
						int colonIdx = buf.IndexOf(":");
						if (colonIdx > 0)
							parameters.Add(buf.Substring(0, colonIdx).Trim(), buf.Substring(colonIdx+1).Trim());
					}
				}
				log(WebTinyServerLogKind.Informational, "---- 结束接收命令 ----");
			} 
			catch (SocketException e) 
			{
				log(WebTinyServerLogKind.Warning, "发生Socket例外:"+e);
			} 
			catch (IOException ioe) 
			{
				log (WebTinyServerLogKind.Warning, "发送IO例外:"+ioe);
			}
			log(WebTinyServerLogKind.Informational, "接收完成");
			return command;
		}

		#endregion

        #region 处理结果返回
        /// <summary>
		///	method sendfile(s, fname) sends the contents of file fname through 
		/// the connection on socket s.
		/// </summary>
		/// <param name="path">full path to file</param>
		protected void sendfile(String path) 
		{
			if (!File.Exists(path))
				throw new TinyServerException("丢失文件: 发送文件({0})", path);
			log(WebTinyServerLogKind.Informational, "发送文件: '{0}'",path);
			byte []buff = new byte[2048];
			try 
			{
				int d;
				FileStream f =  File.OpenRead(path);
				while ((d = f.Read(buff,0,2048)) > 0) 
				{
					ns.Write(buff, 0, d);
				}
				f.Close();
			} 
			catch (Exception e) { log(WebTinyServerLogKind.Error, "发送文件例外: "+e);          }
		} // end of method sendfile 
		
		/// <summary>
		/// send a template to the connection. This may contain html fragments
		/// </summary>
		/// <param name="name">filename of template</param>
		protected void sendTemplate(string name)
		{
			sendfile(template(name));
		}
		/// <summary>
		/// sends the contents of String name back to the browser 
		/// </summary>
		/// <param name="msg">content of the message</param>
		protected void sendString(String msg) 
		{
			byte []buff ;
			buff = System.Text.Encoding.ASCII.GetBytes(msg);
			
			try 
			{
				ns.Write(buff, 0, buff.Length);
			} 
			catch (Exception e) 
			{
				log(WebTinyServerLogKind.Error, "发送字符串例外:{0} ", e);
			}
		} // end of method sendString

        /// <summary>
        /// sends the contents of String name back to the browser 
        /// </summary>
        /// <param name="msg">content of the message</param>
        protected void sendHTTPString(String msg)
        {
            sendOk();
            sendString(msg);
        }

		/// <summary>
		/// return an error to the web browser
		/// </summary>
		/// <param name="errno"></param>
		/// <param name="errString"></param>
		protected void sendError(int errno, string errString)
		{
			sendString(string.Format("HTTP/1.1 {0} {1}\r\n", errno, errString));
			sendString(string.Format("Date:{0}\r\n", DateTime.Now));
			sendString(string.Format("Server:{0}\r\n", serverID));
			sendString("Content-Type: text/html; charset=utf-8\r\n");
			sendString("Connection: close\r\n");
		}

		protected void sendOk()
		{
			sendString("HTTP/1.1 200 OK\r\n");
			sendString(string.Format("Date:{0}\r\n", DateTime.Now));
			sendString(string.Format("Server:{0}\r\n", serverID));
			sendString("Content-Type: text/html; charset=utf-8\r\n\r\n");
		}
		#endregion

		#region 日志

		
		/// <summary>
		/// log a message
		/// </summary>
		/// <param name="s"></param>
		protected void log (WebTinyServerLogKind level,  string s)
		{
            if (!WebTinyServerConfig.Instance.WillSaveLog)
            {
                return;
            }

			if (level >= logLevel)
			{
				if (logFile.Length > 0) 
					using (StreamWriter sw = File.AppendText(logFile))
						sw.WriteLine(s);
				else
					System.Console.WriteLine(s);
			}
		}
		protected void log (WebTinyServerLogKind level, string fmt, params object[] args)
		{
			log(level, string.Format(fmt, args));
		}
		#endregion

		#region utility
		/// <summary>
		/// get the path for the required template
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		protected string template(string name)
		{
			return Path.Combine(templatePath, name);
		}

		/// <summary>
		/// return the command argument part of an HTTP command. That is the bit after the GET. That include a URL 
		/// and optional arguments
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected virtual string CommandArg(string command)
		{
			int idx=0;
			//skip blanks before command
			while ((idx < command.Length) && (Char.IsWhiteSpace(command, idx))) idx++;
			if (idx >= command.Length) return string.Empty;

			//skip command
			while ((idx < command.Length) && (!Char.IsWhiteSpace(command, idx))) idx++;
			if (idx >= command.Length) return string.Empty;
		
			return command.Substring(idx).Trim();				
		}

		#endregion
	}
	/// <summary>
	/// Exceptions the MicroServer can throw
	/// </summary>
	public class TinyServerException: ApplicationException
	{
		public TinyServerException(string msg): base(msg) {}
		public TinyServerException(string fmt, params object[] args): base(string.Format(fmt, args)) {}
		public TinyServerException(string msg, Exception innerException): base(msg, innerException){}
	}
}
