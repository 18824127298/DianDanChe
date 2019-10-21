using System;
using System.IO;
//=========== 调用System.Runtime.InteropServices ==========
// 因为我们需要调用API函数，所以必须创建System.Runtime.InteropServices
// 命名空间以提供可用于访问.NET 中的COM对象和本机API的类的集合。  
using System.Runtime.InteropServices;
////=======================================================
using System.Text;

namespace Sigbit.Common
{
    /// <summary>
    /// 传统Windows格式配置文件的读写类
    /// </summary>
    public class IniFile
    {
        private string _path;
        /// <summary>
        /// INI文件名
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// 写入配置项
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="val">具体的配置值</param>
        /// <param name="filePath">文件名</param>
        /// <returns>是否成功</returns>
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string
        section, string key, string val, string filePath);

        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="def">缺省值</param>
        /// <param name="retVal">返回值</param>
        /// <param name="size">返回值大小</param>
        /// <param name="filePath">文件名</param>
        /// <returns>是否成功</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key,
        string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 构造函数，传入指定的ini文件名
        /// </summary>
        /// <param name="iniPath">ini文件名</param>
        public IniFile(string iniPath)
        {
            _path = iniPath;
        }

        /// <summary>
        /// 写入ini文件的键值
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="value">键值</param>
        public void IniWriteValue(string section, string key, string value)
        {
            bool result = WritePrivateProfileString(section, key, value, this._path);
            if (!result)
            {
                throw new Exception("IniFile.IniWriteValue() Error : cannot write to the file.");
            }
        }

        /// <summary>
        /// 读取配置文件的键值
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <returns>读取到的键值</returns>
        /// <remarks>缺省值为""</remarks>
        public string IniReadValue(string section, string key)
        {
            return IniReadValue(section, key, "");
        }

        /// <summary>
        /// 读取配置文件的键值
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="defaultString">缺省值</param>
        /// <returns>读取到的键值</returns>
        public string IniReadValue(string section, string key, string defaultString)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, defaultString, temp, 255, this._path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取文件的配置项
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <returns>读取到的配置项</returns>
        /// <remarks>如果读取不到，缺省值为""</remarks>
        public static string ProfileString(string fileName, string section, string key)
        {
            return ProfileString(fileName, section, key, "");
        }

        /// <summary>
        /// 读取文件的配置项
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>读取到的配置项</returns>
        public static string ProfileString(string fileName, string section, string key, string defaultValue)
        {
            IniFile iniFile = new IniFile(fileName);
            return iniFile.IniReadValue(section, key, defaultValue);
        }

        /// <summary>
        /// 写入文件的配置项
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="section">段名</param>
        /// <param name="key">关键字名</param>
        /// <param name="value">缺省值</param>
        public static void SetProfileString(string fileName, string section, string key, string value)
        {
            IniFile iniFile = new IniFile(fileName);
            iniFile.IniWriteValue(section, key, value);
        }
    }
}