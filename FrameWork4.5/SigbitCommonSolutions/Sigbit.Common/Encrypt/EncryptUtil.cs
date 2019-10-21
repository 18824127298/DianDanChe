using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace Sigbit.Common.Encrypt
{
    /// <summary>
    /// ������ز���
    /// </summary>
    public class EncryptUtil
    {

        #region CRC ���뺯��

        #region CRC32
        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="sInput">���봮</param>
        /// <returns>CRC32ֵ</returns>
        public static long CRC32(string sInput)
        {
            CRC32 crc = new CRC32();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="buffer">�����ֽ�����</param>
        /// <returns>CRC32ֵ</returns>
        public static long CRC32(byte[] buffer)
        {
            CRC32 crc = new CRC32();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC32
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>CRC32ֵ</returns>
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
        /// <param name="sInput">���봮</param>
        /// <returns>CRC16ֵ</returns>
        public static long CRC16(string sInput)
        {
            CRC16 crc = new CRC16();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC16
        /// </summary>
        /// <param name="buffer">�����ֽ�����</param>
        /// <returns>CRC16ֵ</returns>
        public static long CRC16(byte[] buffer)
        {
            CRC16 crc = new CRC16();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC16�ļ�
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>CRC16ֵ</returns>
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
        /// <param name="sInput">���봮</param>
        /// <returns>CRC8ֵ</returns>
        public static long CRC8(string sInput)
        {
            CRC8 crc = new CRC8();
            crc.Crc(sInput);
            return crc.Value;
        }

        /// <summary>
        /// CRC8
        /// </summary>
        /// <param name="buffer">�����ֽ�����</param>
        /// <returns>CRC8ֵ</returns>
        public static long CRC8(byte[] buffer)
        {
            CRC8 crc = new CRC8();
            crc.Crc(buffer);
            return crc.Value;
        }

        /// <summary>
        /// CRC8�ļ�
        /// </summary>
        /// <param name="filename">�����ļ�</param>
        /// <returns>CRC8ֵ</returns>
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

        #region Base64 ���뺯��

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="sInputString">���봮</param>
        /// <returns>BASE64���봮</returns>
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
        /// Base64����
        /// </summary>
        /// <param name="sInputString">���봮</param>
        /// <returns>������</returns>
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
        /// Base64�ļ�����
        /// </summary>
        /// <param name="sFileSrc">Դ�ļ���</param>
        /// <param name="sFileDest">Ŀ���ļ���</param>
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
        /// Base64�ļ�����
        /// </summary>
        /// <param name="sFileSrc">Դ�ļ���</param>
        /// <param name="sFileDest">Ŀ���ļ���</param>
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

        #region �ַ���������
        /// <summary>
        /// ���ַ���ת��Ϊ����
        /// </summary>
        /// <param name="strKey">�ַ���</param>
        /// <returns>�ֽ�����</returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            byte[] tmpByte = Encoding.Default.GetBytes(strKey);
            return tmpByte;
        }

        /// <summary>
        ///  ������ת��Ϊ16�����ַ���
        /// </summary>
        /// <param name="Byte">�ֽ�����</param>
        /// <returns>�ַ���</returns>
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

        #endregion �ַ���������

        #region Des����

        const string des_key_postfix = "!@#$%^&*";
        /// <summary>
        /// DES���ܴ�
        /// </summary>
        /// <param name="originalValue">Դֵ</param>
        /// <param name="password">����</param>
        /// <returns>�����</returns>
        public static string DesEncodeString(string originalValue, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.EncryptKey = password + des_key_postfix;
            des.InputString = originalValue;
            des.DesEncrypt();
            return des.OutString;
        }

        /// <summary>
        /// DES���ܴ�
        /// </summary>
        /// <param name="originalValue">Դֵ</param>
        /// <param name="password">����</param>
        /// <returns>�����</returns>
        public static string DesDecodeString(string originalValue, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.DecryptKey = password + des_key_postfix;
            des.InputString = originalValue;
            des.DesDecrypt();
            return des.OutString;
        }

        /// <summary>
        /// DES�����ļ�
        /// </summary>
        /// <param name="sFileSrc">Դ�ļ�</param>
        /// <param name="sFileDest">Ŀ���ļ�</param>
        /// <param name="password">����</param>
        public static void DesEncodeFile(string sFileSrc, string sFileDest, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.InputFilePath = sFileSrc;
            des.OutFilePath = sFileDest;
            des.EncryptKey = password + des_key_postfix;
            des.FileDesEncrypt();
        }

        /// <summary>
        /// DES�����ļ�
        /// </summary>
        /// <param name="sFileSrc">Դ�ļ�</param>
        /// <param name="sFileDest">Ŀ���ļ�</param>
        /// <param name="password">����</param>
        public static void DesDecodeFile(string sFileSrc, string sFileDest, string password)
        {
            DESEncryptor des = new DESEncryptor();
            des.InputFilePath = sFileSrc;
            des.OutFilePath = sFileDest;
            des.DecryptKey = password + des_key_postfix;
            des.FileDesDecrypt();
        }

        #endregion Des����

        #region ��������뺯��
        /// <summary>
        /// 32λMD5����
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
        /// 16λMD5,ȡ32λ���ܴ���8��25λ
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string MD5String16(string sInput)
        {
            string sMD532 = MD5String(sInput);
            return sMD532.Substring(8, 16);
        }


        /// <summary>
        /// MD5�ļ�
        /// </summary>
        /// <param name="sFilename">�ļ���</param>
        /// <returns>MD5���</returns>
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
        /// SHA1����
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
        /// SHA1�ļ�
        /// </summary>
        /// <param name="sFilename">�ļ���</param>
        /// <returns>�����</returns>
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
        /// SHA256����
        /// </summary>
        /// <param name="sInput">���봮</param>
        /// <returns>������</returns>
        public static string SHA256String(string sInput)
        {
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(GetKeyByteArray(sInput));
            sha256.Clear();
            return GetStringValue(tmpByte);
        }

        /// <summary>
        /// SH256�ļ�
        /// </summary>
        /// <param name="sFilename">�ļ���</param>
        /// <returns>������</returns>
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
        ///  SHA512����
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
        /// SHA512�ļ�
        /// </summary>
        /// <param name="sFilename">�ļ���</param>
        /// <returns>���</returns>
        public static string SHA512File(string sFilename)
        {
            byte[] tmpByte;
            byte[] binaryData = GetFileBytes(sFilename);
            SHA512 sha512 = new SHA512Managed();
            tmpByte = sha512.ComputeHash(binaryData);
            sha512.Clear();
            return GetStringValue(tmpByte);
        }

        #endregion ��������뺯��
    }
}