using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CheDaiBaoCommonService.Expansion
{
    public static class Expansions
    {
        #region  枚举转Mvc下拉框
        public static SelectList ToSelectList<TEnum>(
            this TEnum enumObj,
            bool markCurrentAsSelected = true,
            string Value = "Id",
            string Name = "Name")
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("An Enumeration type is required.", "enumObj");

            Func<object, string> GetDisplayName = o =>
            {
                var result = null as string;
                var display = o.GetType().
                    GetMember(o.ToString()).
                    First().GetCustomAttributes(false).
                    OfType<DisplayAttribute>().
                    LastOrDefault();
                if (display != null)
                {
                    result = display.GetName();
                }
                return result ?? o.ToString();
            };

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         select new { ID = Convert.ToInt32(enumValue), Name = GetDisplayName(enumValue) };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, Value, Name, selectedValue);
        }
        #endregion

        #region Json
        static JavaScriptSerializer js = new JavaScriptSerializer();
        /// <summary>
        /// 对象转Json格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJson(this object obj)
        {
            string jsonString = js.Serialize(obj);
            string sRet = new Regex(@"\\/Date\((?<aa>\d+)\)\\/").Replace(jsonString,
                 new MatchEvaluator((Match m) =>
                 {
                     return new DateTime(1970, 1, 1)
                           .AddMilliseconds(long.Parse(m.Groups[1].Value))
                           .ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                 }));

            sRet = sRet.Replace("\\u0026", "&");

            return sRet;
        }

        /// <summary>
        /// Json字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObj<T>(this string json)
        {
            return js.Deserialize<T>(json);
        }
        #endregion

        #region 压缩字符串
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CompressionString(this string str)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(str);
            MemoryStream ms = new MemoryStream();

            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(buffer, 0, buffer.Length);
            zip.Close();
            ms.Position = 0;
            byte[] zipBuffer = new byte[ms.Length];
            ms.Read(zipBuffer, 0, zipBuffer.Length);
            ms.Close();
            string zipstr = Convert.ToBase64String(zipBuffer);
            return zipstr;
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeCompressString(this string str)
        {
            byte[] buffer = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
            byte[] zipBuffer = new byte[1024];
            MemoryStream ms2 = new MemoryStream();
            while (true)
            {
                int bytesRead = zip.Read(zipBuffer, 0, zipBuffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                ms2.Write(zipBuffer, 0, bytesRead);
            }
            zip.Close();

            string zipstr = Encoding.Unicode.GetString(ms2.ToArray(), 0, (int)ms2.Length);
            return zipstr;
        }
        #endregion

        #region 加密解密
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string Encode(this string encryptString, string encryptKey = "wangdai")
        {
            encryptKey = encryptKey.Length > 8 ? encryptKey.Substring(0, 8) : encryptKey;
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            string base64String = Convert.ToBase64String(mStream.ToArray());
            return base64String.Replace("/", "@").Replace("=", "!").Replace("+", "["); ;
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string Decode(this string decryptString, string decryptKey = "wangdai")
        {
            try
            {
                decryptString = decryptString.Replace("@", "/").Replace("!", "=").Replace("[", "+");
                decryptKey = decryptKey.Length > 8 ? decryptKey.Substring(0, 8) : decryptKey;
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception)
            {
                throw new Exception("解密失败");
            }
        }
        #endregion

        #region 部分加密
        /// <summary>
        /// 加密部分手机号码 13912345678=》139****5678
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string EncruptionSectionMobile(this string phone)
        {
            phone = phone + "";
            if (phone.Length != 11)
                return phone;
            return phone.Substring(0, 3) + "******" + phone.Substring(9, 2);
        }


        /// <summary>
        /// 加密部分姓名 孙思远=> **远
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string EncruptionSectionFullName(this string fullName)
        {
            fullName = fullName + "";
            if (fullName.Length > 0)
            {
                //fullName = string.Join("", Enumerable.Repeat("*", fullName.Length - 1)) + fullName.Substring(fullName.Length - 1, 1);
                fullName = fullName.Substring(0, 1) + string.Join("", Enumerable.Repeat("*", fullName.Length - 1));
            }
            return fullName;
        }

        /// <summary>
        /// 加密部分银行卡号 6225750008742145=> "************"2145
        /// </summary>
        /// <param name="bankCardNumber"></param>
        /// <returns></returns>
        public static string EncruptionSectionBankCardNumber(this string bankCardNumber)
        {
            bankCardNumber = bankCardNumber + "";
            if (bankCardNumber.Length < 5)
            {
                return "";
            }
            bankCardNumber = "************" + bankCardNumber.Substring(bankCardNumber.Length - 4);

            return bankCardNumber;
        }

        /// <summary>
        /// 加密部分用户名  abcdefg  abc***
        /// </summary>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public static string EncruptionSectionAliases(this string aliases)
        {
            aliases = aliases + "";
            if (aliases.Length >= 6)
            {
                aliases = aliases.Substring(0, aliases.Length * 2 / 3) + "***"; ;
            }
            return aliases;
        }

        /// <summary>
        /// 加密部分身份证号码  abcdefg  abc***
        /// </summary>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public static string EncruptionSectionIDNumber(this string IDNumber)
        {
            IDNumber = IDNumber + "";
            if (IDNumber.Length == 18)
                IDNumber = string.Format("{0}************{1}", IDNumber.Substring(0, 3), IDNumber.Substring(IDNumber.Length - 3, 3));
            return IDNumber;
        }

        /// <summary>
        /// 加密用户名/昵称
        /// </summary>
        /// <param name="alias">用户名/昵称</param>
        /// <returns></returns>
        public static string EncryptAlias(this string alias)
        {
            string result = "";
            alias += "";

            if (alias.Length <= 1)
            {
                result = "***";
            }
            else if (alias.Length <= 5)
            {
                result = alias.First() + new string('*', alias.Length - 1);
            }
            else
            {
                int snowCharCount = alias.Length / 3;
                int leftLength = Convert.ToInt32(Math.Ceiling(alias.Length / 2 - snowCharCount / 2.0));
                int rightStartIndex = Convert.ToInt32(Math.Ceiling(alias.Length / 2 + snowCharCount / 2.0));
                result = string.Format("{0}{1}{2}", alias.Substring(0, leftLength),
                    new string('*', snowCharCount), alias.Substring(rightStartIndex));
            }

            return result;
        }
        /// <summary>
        /// 邮箱地址部分加密
        /// <para>By Jonney  2015-01-23</para>
        /// <para>例 do@qq.com加密为 d*****@qq.com</para>
        /// </summary>
        /// <param name="email">需要加密的邮箱地址</param>
        /// <returns></returns>
        public static string EncruptionSectionEmail(this string email)
        {
            Match match = Regex.Match(email, ".(.*)@.*");
            if (match.Success)
            {
                string mail = match.Groups[1].Value;
                email = email.Replace(mail, "".PadLeft(mail.Length, '*'));
            }
            return email;
        }

        #endregion

        #region 数字金额小写转大写

        /// <summary>
        /// 把小数转换成大写金额
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToChinese(this decimal number)
        {
            if (number == 0)
                return "零";
            string s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", m => "负圆空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 把小数转换成大写金额
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToChinese(this int number)
        {
            if (number == 0)
                return "零";
            string s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        public static bool IsAllChinese(this string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] <= 127)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAllNumber(this string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int ascii = (int)str[i];
                if (ascii < 48 || ascii > 57)
                    return false;
            }
            return true;
        }

        public static bool IsMobile(this string str)
        {
            return Regex.IsMatch(str, @"^1\d{10}$");
        }

        /// <summary>切割字符串
        /// 切割字符串
        /// 创建者：陈伟   时间：2011-4-26
        /// 修改者：         时间： 
        /// </summary>
        /// <param name="s">需要切割的字符串</param>
        /// <param name="i">需要切割的字符串长度</param>
        /// <param name="smore">切割后字符串后面添加的字符串</param>
        /// <returns></returns>
        public static string SubStr(string s, int i, string smore)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return "";
            }
            int intResult = 0;
            int j = 0;
            string s1 = s;
            if (GetStrLen(s) > i)
            {
                foreach (char Char in s)
                {
                    if (intResult < i)
                    {
                        j++;
                        if ((int)Char > 127)
                            intResult = intResult + 2;
                        else
                            intResult++;
                    }
                    else
                        break;
                }
                s1 = s.Substring(0, j);
            }
            else
            {
                return s1;
            }
            return s1 + smore;
        }

        /// <summary>得到字符串的长度
        /// 得到字符串的长度
        /// 创建者：陈伟   时间：20011-4-26
        /// 修改者：         时间： 
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static int GetStrLen(string strData)
        {
            System.Text.Encoding encoder5 = System.Text.Encoding.GetEncoding("GB2312");
            return encoder5.GetByteCount(strData);
        }

        /// <summary>  
        /// 字符串转为UniCode码字符串  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("\\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        #region 获取当前时间戳
        public static int GetCreatetime()
        {
            DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);

            return Convert.ToInt32((DateTime.Now.AddDays(7) - DateStart).TotalSeconds);
        }
        #endregion

        #region 获取Json字符串某节点的值
        /// <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'', '}' }); //过滤引号或空格
                }
            }
            return result;
        }
        #endregion


        #region AES加解密
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        //public static string AesEncrypt(string str, string key)
        //{
        //    if (string.IsNullOrEmpty(str)) return null;
        //    Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

        //    RijndaelManaged rm = new RijndaelManaged
        //    {
        //        Key = Encoding.UTF8.GetBytes(key),
        //        Mode = CipherMode.ECB,
        //        Padding = PaddingMode.PKCS7
        //    };

        //    ICryptoTransform cTransform = rm.CreateEncryptor();
        //    Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}

        public static string AesEncrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
            if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
            if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
            if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Encoding.UTF8.GetBytes(value);
            using (var aes = new RijndaelManaged())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var cryptoTransform = aes.CreateEncryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion
    }
}
