using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Data.FoxDBF;

namespace Sigbit.Data.FoxUtils.HZUtf8PinYin
{
    /// <summary>
    /// 缓冲区项
    /// </summary>
    class FUXCacheItem
    {
        private string _UTF8Code = "";
        /// <summary>
        /// UTF8编码
        /// </summary>
        public string UTF8Code
        {
            get { return _UTF8Code; }
            set { _UTF8Code = value; }
        }

        private string _PinYin = "";
        /// <summary>
        /// 拼音
        /// </summary>
        public string PinYin
        {
            get { return _PinYin; }
            set { _PinYin = value; }
        }
    }

    public class FUXHanzPinYin
    {
        /// <summary>
        /// DBF文件的路径
        /// </summary>
        private const string HZ_UTF8_PY_DBF_FILE = "config/__fux_db/HZUTF8PY.DBF";
        //private const string HZ_UTF8_PY_DBF_FILE = "D:\\SIGBIT\\iwProject\\10086\\i10086ToolsWinTest\\bin\\Debug\\config\\__fux_db\\HZUTF8PY.DBF";

        /// <summary>
        /// 最大的缓冲区大小
        /// </summary>
        private const int MAX_CACHE_RECORD_COUNT = 500;

        /// <summary>
        /// 保存拼音的DBF
        /// </summary>
        private FoxDBFile _dbfHZPinYin = null;

        private Hashtable _htQuickCache = new Hashtable();

        private static FUXHanzPinYin _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static FUXHanzPinYin Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new FUXHanzPinYin();
                return _thisInstance;
            }
        }

        /// <summary>
        /// 退出处理
        /// </summary>
        /// <remarks>
        /// 如果不调用退出处理，则文件一直打开着。
        /// 可以在应用退出时，或着用不着拼音的场合调用该函数。
        /// </remarks>
        public void TerminateProcess()
        {
            if (_dbfHZPinYin != null)
            {
                _dbfHZPinYin.CloseDBF();
                _dbfHZPinYin = null;
            }
        }

        /// <summary>
        /// 得到汉字字符的UTF8编码
        /// </summary>
        /// <param name="cHZChar">汉字字符</param>
        /// <returns>UTF8编码</returns>
        private string GetUTF8HexString(char cHZChar)
        {
            string sHZString = "";
            sHZString += cHZChar;

            byte[] bsPacket = Encoding.UTF8.GetBytes(sHZString);
            string sHexString = GetUTF8HexString__HexStringOfBytes(bsPacket);
            return sHexString;
        }

        /// <summary>
        /// 得到十六进制表示的字符串
        /// </summary>
        /// <param name="bsPacket">字节数组</param>
        /// <returns>十六进制字符串</returns>
        private string GetUTF8HexString__HexStringOfBytes(byte[] bsPacket)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte by in bsPacket)
                sb.Append(String.Format("{0:X2}", by));

            string sRet = sb.ToString();
            return sRet;
        }

        /// <summary>
        /// 得到DBF文件相应记录号的数据
        /// </summary>
        /// <param name="nRecNo">记录号</param>
        /// <returns>该记录的数据</returns>
        /// <remarks>
        /// 如果缓冲区中有数据，则读取缓冲区中的数据，否则，读数据库中的数据
        /// </remarks>
        private FUXCacheItem GetCacheItemOfDBFRecNo(int nRecNo)
        {
            //========== 1. 定位缓冲区，如果定位到，就直接返回 ===========
            FUXCacheItem item = (FUXCacheItem)_htQuickCache[nRecNo];
            if (item != null)
                return item;

            //========== 2. 从数据库中读取 ==============
            item = new FUXCacheItem();

            _dbfHZPinYin.Go(nRecNo);
            item.UTF8Code = _dbfHZPinYin.GetRecordString("UTF8_CODE");
            item.PinYin = _dbfHZPinYin.GetRecordString("PIN_YIN");

            //========= 3. 如果缓冲区还没满，就填进缓冲区 ============
            if (_htQuickCache.Count < MAX_CACHE_RECORD_COUNT)
                _htQuickCache[nRecNo] = item;

            return item;
        }

        /// <summary>
        /// 得到一个汉字的拼音
        /// </summary>
        /// <param name="cHZChar">一个汉字</param>
        /// <returns>拼音</returns>
        /// <remarks>
        /// 如果找不到拼音（如标点符号、特怪汉字、英文数字），就返回空串。
        /// </remarks>
        public string GetPinYinOfHZChar(char cHZChar)
        {
            //======== 1. 得到汉字字符的UTF8Code表示 =============
            string sHZUTF8HexCode = GetUTF8HexString(cHZChar);

            //====== 2. 打开DBF文件 ===============
            if (_dbfHZPinYin == null)
            {
                _dbfHZPinYin = new FoxDBFile();
                //_dbfHZPinYin.AttachFile(HZ_UTF8_PY_DBF_FILE);
                string sDBFFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                        + "\\" + HZ_UTF8_PY_DBF_FILE;
                _dbfHZPinYin.AttachFile(sDBFFileName);
            }

            //======== 3. 二分法定位到相应的记录 =============
            int nLow = 1;
            int nHigh = _dbfHZPinYin.RecCount;

            while (nLow <= nHigh)
            {
                //======= 3.1 二分之一的记录 ========
                int nMid = (nLow + nHigh) / 2;

                //========= 3.2 定位到这条记录，取出数据 =========
                FUXCacheItem cacheItem = GetCacheItemOfDBFRecNo(nMid);
                string sHexCodeOfMid = cacheItem.UTF8Code;

                //========= 3.3 比较待定位的值和取出的二分之一处的数据 =======
                int nCompareResult = sHZUTF8HexCode.CompareTo(sHexCodeOfMid);

                //========== 3.4 如果相同，就返回结果 ==============
                if (nCompareResult == 0)
                {
                    string sRet = cacheItem.PinYin;

                    return sRet;
                }

                //========= 3.5 否则，调整Low、High的值 ==========
                if (nCompareResult > 0)
                    nLow = nMid + 1;
                else
                    nHigh = nMid - 1;
            }

            //========= 4. 找不到，就返回空串 =============
            return "";
        }

        /// <summary>
        /// 得到汉字串的拼音
        /// </summary>
        /// <param name="sHZString">汉字串</param>
        /// <returns>拼音</returns>
        /// <remarks>
        /// 所有汉字拼音用空格分开，得不到拼音的汉字、数字字母或标点符号，用
        /// "[]"括起来。主要是从排序的角色考虑。
        /// </remarks>
        public string GetPinYinOfHZString(string sHZString)
        {
            StringBuilder sbRet = new StringBuilder();

            string sRawStringTmp = "";

            //======== 1. 处理每一个汉字 ===========
            for (int i = 0; i < sHZString.Length; i++)
            {
                //========= 2. 得到汉字的拼音 ============
                char cHZ = sHZString[i];
                string sPinYinOfChar = GetPinYinOfHZChar(cHZ);

                //========= 3. 得到拼音的处理 =============
                if (sPinYinOfChar != "")
                {
                    //========== 3.1 之前非汉字的处理 =======
                    if (sRawStringTmp != "")
                    {
                        sRawStringTmp = "[" + sRawStringTmp + "]";

                        if (sbRet.Length == 0)
                            sbRet.Append(sRawStringTmp);
                        else
                        {
                            sbRet.Append(" ");
                            sbRet.Append(sRawStringTmp);
                        }

                        sRawStringTmp = "";
                    }

                    //======== 3.2 加到返回串中 ===========
                    if (sbRet.Length == 0)
                        sbRet.Append(sPinYinOfChar);
                    else
                    {
                        sbRet.Append(" ");
                        sbRet.Append(sPinYinOfChar);
                    }
                }
                //============ 4. 未得到拼音的处理 ===========
                else
                    sRawStringTmp += cHZ;
            }

            //============ 5. 非汉字的处理 ==========
            if (sRawStringTmp != "")
            {
                sRawStringTmp = "[" + sRawStringTmp + "]";

                if (sbRet.Length == 0)
                    sbRet.Append(sRawStringTmp);
                else
                {
                    sbRet.Append(" ");
                    sbRet.Append(sRawStringTmp);
                }

                sRawStringTmp = "";
            }

            return sbRet.ToString();
        }

        /// <summary>
        /// 得到字符串的简拼
        /// </summary>
        /// <param name="sHZString">字符串</param>
        /// <returns>简拼</returns>
        /// <remarks>如果是非汉字，则取第一个字母做为简拼</remarks>
        public string GetJianPinOfHZString(string sHZString)
        {
            StringBuilder sbRet = new StringBuilder();
            string sRawStringTmp = "";

            for (int i = 0; i < sHZString.Length; i++)
            {
                char cHZ = sHZString[i];
                string sPinYinOfChar = GetPinYinOfHZChar(cHZ);

                if (sPinYinOfChar != "")
                {
                    if (sRawStringTmp != "")
                    {
                        string sFirstLetter = GetJianPinOfHZString__FirstLetter(sRawStringTmp);

                        if (sFirstLetter != "")
                            sbRet.Append(sFirstLetter);

                        sRawStringTmp = "";
                    }

                    char cJianPin = sPinYinOfChar[0];
                    sbRet.Append(cJianPin);
                }
                else
                    sRawStringTmp += cHZ;
            }

            if (sRawStringTmp != "")
            {
                string sFirstLetter = GetJianPinOfHZString__FirstLetter(sRawStringTmp);

                if (sFirstLetter != "")
                    sbRet.Append(sFirstLetter);

                sRawStringTmp = "";
            }

            return sbRet.ToString();
        }

        private string GetJianPinOfHZString__FirstLetter(string sLetterString)
        {
            string sRet = "";

            for (int i = 0; i < sLetterString.Length; i++)
            {
                char cLetter = sLetterString[i];
                if (cLetter >= 'A' && cLetter <= 'Z')
                {
                    sRet += cLetter;
                    break;
                }
                if (cLetter >= 'a' && cLetter <= 'z')
                {
                    sRet += cLetter;
                    break;
                }
            }

            return sRet.ToLower();
        }
    }
}
