using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace Sigbit.Common.Encrypt
{
    /// <summary>
    /// 加密相关操作
    /// </summary>
    public class EncryptUtil
    {

        #region CRC 编码函数

        #region CRC32
        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="sInput">输入串</param>
        /// <returns>CRC32值</returns>
        public static long CRC32(string sInput)
        {
            CRC32 crc = new CRC32();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="buffer">输入字节数组</param>
        /// <returns>CRC32值</returns>
        public static long CRC32(byte[] buffer)
        {
            CRC32 crc = new CRC32();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>CRC32值</returns>
        public static long CRC32File(string filename)
        {
            FileStream inFile = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bInput = new byte[inFile.Length];
            inFile.Read(bInput, 0, bInput.Length);
            inFile.Close();
            return CRC32(bInput);
        }
        #endregion CRC32

        #region CRC16
        /// <summary>
        /// CRC16
        /// </summary>
        /// <param name="sInput">输入串</param>
        /// <returns>CRC16值</returns>
        public static long CRC16(string sInput)
        {
            CRC16 crc = new CRC16();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC16
        /// </summary>
        /// <param name="buffer">输入字节数组</param>
        /// <returns>CRC16值</returns>
        public static long CRC16(byte[] buffer)
        {
            CRC16 crc = new CRC16();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC16文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>CRC16值</returns>
        public static long CRC16File(string filename)
        {
            FileStream inFile = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bInput = new byte[inFile.Length];
            inFile.Read(bInput, 0, bInput.Length);
            inFile.Close();
            return CRC16(bInput);
        }
        #endregion CRC16

        #region CRC8
        /// <summary>
        /// CRC8
        /// </summary>
        /// <param name="sInput">输入串</param>
        /// <returns>CRC8值</returns>
        public static long CRC8(string sInput)
        {
            CRC8 crc = new CRC8();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC8
        /// </summary>
        /// <param name="buffer">输入字节数组</param>
        /// <returns>CRC8值</returns>
        public static long CRC8(byte[] buffer)
        {
            CRC8 crc = new CRC8();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC8文件
        /// </summary>
        /// <param name="filename">输入文件</param>
        /// <returns>CRC8值</returns>
        public static long CRC8File(string filename)
        {
            FileStream inFile = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bInput = new byte[inFile.Length];
            inFile.Read(bInput, 0, bInput.Length);
            inFile.Close();
            return CRC8(bInput);
        }
        #endregion CRC8

        #endregion CRC

        #region Base64 编码函数

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="sInputString">输入串</param>
        /// <returns>BASE64编码串</returns>
        public static string Base64EncodeString(string sInputString)
        {
            return Base64EncodeString(sInputString, Encoding.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInputString"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Base64EncodeString(string sInputString, Encoding enc)
        {
            byte[] bInput = enc.GetBytes(sInputString);
            try
            {
                return System.Convert.ToBase64String(bInput, 0, bInput.Length);
            }
            catch (System.ArgumentNullException)
            {
                return "";
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return "";
            }
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="sInputString">输入串</param>
        /// <returns>解码结果</returns>
        public static string Base64DecodeString(string sInputString)
        {
            return Base64DecodeString(sInputString, Encoding.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInputString"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string Base64DecodeString(string sInputString, Encoding enc)
        {
            char[] sInput = sInputString.ToCharArray();
            try
            {
                byte[] bOutput = System.Convert.FromBase64String(sInputString);
                return enc.GetString(bOutput);
            }
            catch (System.ArgumentNullException)
            {
                return "";
            }
            catch (System.FormatException)
            {
                return "";
            }
        }

        /// <summary>
        /// Base64文件解码
        /// </summary>
        /// <param name="sFileSrc">源文件名</param>
        /// <param name="sFileDest">目标文件名</param>
        public static void Base64DecodeFile(string sFileSrc, string sFileDest)
        {

            byte[] base64ByteArray = GetFileBytes(sFileSrc);
            char[] base64CharArray = Encoding.ASCII.GetChars(base64ByteArray);


            base64ByteArray =
                  Convert.FromBase64CharArray(base64CharArray,
                  0, base64CharArray.Length);

            string base64Str = System.Text.Encoding.GetEncoding("gb2312").GetString(base64ByteArray);
            StreamWriter writer;
            writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileDest),
                         System.Text.Encoding.Default);
            writer.Write(base64Str);
            writer.Close();
        }

        /// <summary>
        /// Base64文件编码
        /// </summary>
        /// <param name="sFileSrc">源文件名</param>
        /// <param name="sFileDest">目标文件名</param>
        public static void Base64EncodeFile(string sFileSrc, string sFileDest)
        {
            byte[] base64ByteArray = GetFileBytes(sFileSrc);

            string base64Str = System.Convert.ToBase64String(base64ByteArray, 0, base64ByteArray.Length);

            StreamWriter writer;
            writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileDest),
                         System.Text.Encoding.Default);
            writer.Write(base64Str);
            writer.Close();
        }

        #endregion Base64

        #region 字符串处理函数
        /// <summary>
        /// 把字符串转换为数组
        /// </summary>
        /// <param name="strKey">字符串</param>
        /// <returns>字节数组</returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            byte[] tmpByte = Encoding.Default.GetBytes(strKey);
            return tmpByte;
        }

        /// <summary>
        ///  把数组转换为16进制字符串
        /// </summary>
        /// <param name="Byte">字节数组</param>
        /// <returns>字符串</returns>
        private static string GetStringValue(byte[] Byte)
        {
            return BitConverter.ToString(Byte).Replace("-", "");
        }

        private static byte[] GetFileBytes(string filename)
        {
            System.IO.FileStream inFile; ;
            try
            {
                byte[] binaryData;
                inFile = new System.IO.FileStream(filename,
                  System.IO.FileMode.Open,
                  System.IO.FileAccess.Read);
                binaryData = new Byte[inFile.Length];
                long bytesRead = inFile.Read(binaryData, 0,
                  (int)inFile.Length);
                inFile.Close();
                return binaryData;
            }
            catch
            {
                return null;
            }
        }

        #endregion 字符串处理函数

        #region Des加密

        const string des_key_postfix = "!@#$%^&*";
        /// <summary>
        /// DES加密串
        /// </summary>
        /// <param name="originalValue">源值</param>
        /// <param name="password">密码</param>
        /// <returns>结果串</returns>
        public static string DesEncodeString(string originalValue, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.EncryptKey = password + des_key_postfix;
            des.InputString = originalValue;
            des.DesEncrypt();
            return des.OutString;
        }

        /// <summary>
        /// DES解密串
        /// </summary>
        /// <param name="originalValue">源值</param>
        /// <param name="password">密码</param>
        /// <returns>结果串</returns>
        public static string DesDecodeString(string originalValue, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.DecryptKey = password + des_key_postfix;
            des.InputString = originalValue;
            des.DesDecrypt();
            return des.OutString;
        }

        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="sFileSrc">源文件</param>
        /// <param name="sFileDest">目标文件</param>
        /// <param name="password">密码</param>
        public static void DesEncodeFile(string sFileSrc, string sFileDest, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.InputFilePath = sFileSrc;
            des.OutFilePath = sFileDest;
            des.EncryptKey = password + des_key_postfix;
            des.FileDesEncrypt();
        }

        /// <summary>
        /// DES解密文件
        /// </summary>
        /// <param name="sFileSrc">源文件</param>
        /// <param name="sFileDest">目标文件</param>
        /// <param name="password">密码</param>
        public static void DesDecodeFile(string sFileSrc, string sFileDest, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.InputFilePath = sFileSrc;
            des.OutFilePath = sFileDest;
            des.DecryptKey = password + des_key_postfix;
            des.FileDesDecrypt();
        }

        #endregion Des加密

        #region 不可逆编码函数
        /// <summary>
        /// 32位MD5编码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string MD5String(string sInput)
        {
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(sInput));
            md5.Clear();
            return GetStringValue(tmpByte);
        }

        /// <summary>
        /// 16位MD5,取32位加密串的8～25位
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string MD5String16(string sInput)
        {
            string sMD532 = MD5String(sInput);
            return sMD532.Substring(8, 16);
        }


        /// <summary>
        /// MD5文件
        /// </summary>
        /// <param name="sFilename">文件名</param>
        /// <returns>MD5结果</returns>
        public static string MD5File(string sFilename)
        {
            byte[] binaryData;
            byte[] tmpByte;

            try
            {
                binaryData = GetFileBytes(sFilename);
                MD5 md5 = new MD5CryptoServiceProvider();
                tmpByte = md5.ComputeHash(binaryData);
                md5.Clear();
                return GetStringValue(tmpByte);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// SHA1编码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string SHA1String(string sInput)
        {
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(sInput));
            sha1.Clear();

            return GetStringValue(tmpByte);
        }

        /// <summary>
        /// SHA1文件
        /// </summary>
        /// <param name="sFilename">文件名</param>
        /// <returns>结果串</returns>
        public static string SHA1File(string sFilename)
        {
            byte[] tmpByte;
            byte[] binaryData = GetFileBytes(sFilename);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            tmpByte = sha1.ComputeHash(binaryData);
            sha1.Clear();
            return GetStringValue(tmpByte);
        }

        /// <summary>
        /// SHA256编码
        /// </summary>
        /// <param name="sInput">输入串</param>
        /// <returns>编码结果</returns>
        public static string SHA256String(string sInput)
        {
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(GetKeyByteArray(sInput));
            sha256.Clear();
            return GetStringValue(tmpByte);
        }

        /// <summary>
        /// SH256文件
        /// </summary>
        /// <param name="sFilename">文件名</param>
        /// <returns>编码结果</returns>
        public static string SHA256File(string sFilename)
        {
            byte[] tmpByte;
            byte[] binaryData = GetFileBytes(sFilename);
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(binaryData);
            sha256.Clear();
            return GetStringValue(tmpByte);
        }

        /// <summary>
        ///  SHA512编码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string SHA512String(string sInput)
        {
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(sInput));
            sha512.Clear();

            return GetStringValue(tmpByte);

        }

        /// <summary>
        /// SHA512文件
        /// </summary>
        /// <param name="sFilename">文件名</param>
        /// <returns>结果</returns>
        public static string SHA512File(string sFilename)
        {
            byte[] tmpByte;
            byte[] binaryData = GetFileBytes(sFilename);
            SHA512 sha512 = new SHA512Managed();
            tmpByte = sha512.ComputeHash(binaryData);
            sha512.Clear();
            return GetStringValue(tmpByte);
        }

        #endregion 不可逆编码函数
    }
}