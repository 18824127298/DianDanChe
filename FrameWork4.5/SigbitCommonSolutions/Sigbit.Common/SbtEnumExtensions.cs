using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Collections;

//namespace System.Runtime.CompilerServices
//{
//    /// <summary>
//    /// 下面的实现使用了.net framework 3.5的新机制：扩展方法，而编译器会将这些方法带上ExtensionAttribute属性。
//    /// 因为这个属性只存在于.net framework 3.5中，因此在2.0环境中，人为地补一个，否则编译不通过。
//    /// </summary>
//    public class ExtensionAttribute : Attribute { }
//}

namespace Sigbit.Common
{
    /// <summary>
    /// 枚举项描述，继承Attribute
    /// </summary>
    public class SbtEnumDescString : Attribute
    {
        private string _text;
        /// <summary>
        /// 描述内容
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public SbtEnumDescString(string text)
        {
            _text = text;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SbtEnumExtensions
    {
        /// <summary>
        /// 我们定义了一个扩大函数ToDescription， 就像所有的扩大函数一样，它的参数是类似（this …），
        /// </summary>
        public static string ToDescString(this Enum enumeration)    
        {
            //========= 1. GetType 获得了当前列举的类型 ===========
            Type type = enumeration.GetType();

            //======= 2. 借助 GetMember遵守元素的名字（值）， 获得这个特定的元素 =========
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());        
            if (null != memInfo && memInfo.Length > 0)        
            {
                //====== 3. 用GetCustomAttributes 获得描述的内容 ===============
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(SbtEnumDescString), false);            
                if (null != attrs && attrs.Length > 0)
                    return ((SbtEnumDescString)attrs[0]).Text;        
            }        
            return enumeration.ToString();    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string ToCodeString(this Enum enumeration)
        {
            return ConvertUtil.EnumToString(enumeration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="sCode"></param>
        /// <returns></returns>
        public static string CodeToDesc(this Enum enumeration, string sCode)
        {
            //=========== 1. 得到每一个值 ==============
            Type type = enumeration.GetType();
            
            Array arrValues = Enum.GetValues(type);
            int nLowerBound = arrValues.GetLowerBound(0);
            int nUpperBound = arrValues.GetUpperBound(0);

            //=========== 2. 遍历每一个值 =================
            for (int i = nLowerBound; i <= nUpperBound; i++)
            {
                object oneValue = arrValues.GetValue(i);
                string sOneCode = ConvertUtil.EnumToString(oneValue);

                //========== 3. 定位到，就取出描述信息 ===========
                if (sOneCode == sCode)
                {
                    Enum oneEnum = (Enum)oneValue;
                    return oneEnum.ToDescString();
                }
            }

            return sCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enueration"></param>
        /// <param name="sCode"></param>
        /// <returns></returns>
        public static object CodeToEnum(this Enum enueration, string sCode)
        {
            try
            {
                return ConvertUtil.StringToEnum(sCode, enueration);
            }
            catch
            {
                return enueration;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static CodeTableBase ToCodeTable(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            string sEnumTypeFullName = type.FullName;

            //========== 1. 在池中定位CodeTable =============
            CodeTableBase codeTableRet = SbtEnumExtensions__CodeTablePool.Instance.GetCodeTable(sEnumTypeFullName);
            if (codeTableRet != null)
                return codeTableRet;

            //============ 2. 新建CodeTable ===============
            codeTableRet = new CodeTableBase();

            //=========== 3.1 得到每一个值 ==============
            Array arrValues = Enum.GetValues(type);
            int nLowerBound = arrValues.GetLowerBound(0);
            int nUpperBound = arrValues.GetUpperBound(0);

            //=========== 3.2 遍历每一个值 =================
            for (int i = nLowerBound; i <= nUpperBound; i++)
            {
                object oneValue = arrValues.GetValue(i);
                string sOneCode = ConvertUtil.EnumToString(oneValue);
                Enum oneEnum = (Enum)oneValue;
                string sOneDesc = oneEnum.ToDescString();

                if (sOneCode != "none")
                    codeTableRet.AddItem(sOneCode, sOneDesc);
            }

            //=========== 3. 加到池中 ===============
            SbtEnumExtensions__CodeTablePool.Instance.SetCodeTable(sEnumTypeFullName, codeTableRet);

            return codeTableRet;
        }

        //public static string ToDescList(this Enum enumeration)
        //{
        //    string sRet = "";

        //    //========= 1. GetType 获得了当前列举的类型 ===========
        //    Type type = enumeration.GetType();

        //    FieldInfo[] fieldinfo = type.GetFields();

        //    foreach (FieldInfo item in fieldinfo)
        //      {
        //          Object[] obj = item.GetCustomAttributes(typeof(SbtEnumDescString), false);
        //         if (obj != null&&obj.Length!=0)
        //         {
        //             sRet += ((SbtEnumDescString)obj[0]).Text;
        //             //DescriptionAttribute des = (DescriptionAttribute)obj[0];
        //             //Console.WriteLine(des.Description);
        //         }
        //     }


        //    return sRet;
        //}
    }

    class SbtEnumExtensions__CodeTablePool : Hashtable
    {
        private static SbtEnumExtensions__CodeTablePool _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static SbtEnumExtensions__CodeTablePool Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new SbtEnumExtensions__CodeTablePool();
                return _thisInstance;
            }
        }

        public void SetCodeTable(string sTypeFullName, CodeTableBase codeTable)
        {
            this[sTypeFullName] = codeTable;
        }

        public CodeTableBase GetCodeTable(string sTypeFullName)
        {
            return (CodeTableBase)this[sTypeFullName];
        }
    }
}
