using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.IO;

namespace Sigbit.Framework
{
    public class SbtTheme
    {
        private static string _staDefaultTheme = "";
        public static string DefaultTheme
        {
            get
            {
                if (_staDefaultTheme == "")
                    _staDefaultTheme = TbSysPreferenceSetting.GetDefaultPreference("theme");

                return _staDefaultTheme;
            }
        }

        private static Hashtable _htThemeTemplate = new Hashtable();
        /// <summary>
        /// 得到主题的模板内容
        /// </summary>
        /// <param name="sTheme">主题名</param>
        /// <param name="sTemplateName">模板名</param>
        /// <returns>模板的内容</returns>
        /// <remarks>
        /// 模板目前存在文件中，存放位置为：
        /// ~/App_Themes/[主题名]/template/[模板名].tpl;
        /// 如果找不到所需主题的模板文件，则定位缺省主题的模板文件
        /// </remarks>
        public static string GetThemeTemplate(string sTheme, string sTemplateName)
        {
            //======= 1. 如果哈希表中有值（以前得到过），就直接返回 ======
            string sKey = sTheme + "/" + sTemplateName;
            string sRet = (string)_htThemeTemplate[sKey];
            if (sRet != null)
                return sRet;

            //======= 2. 得到template文件名 =========
            string sTemplateFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            sTemplateFileName += "\\App_Themes\\" + sTheme + "\\template\\" 
                    + sTemplateName + ".txt";
            if (!File.Exists(sTemplateFileName))
            {
                string sDefaultFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                sDefaultFileName += "\\App_Themes\\" + DefaultTheme + "\\template\\"
                    + sTemplateName + ".txt";
                if (!File.Exists(sDefaultFileName))
                    throw new Exception("SbtTheme.GetThemeTemplate() error :"
                            + "主题模板找不到，无法定位以下两个文件：(1)主题模板文件"
                            + sTemplateFileName + "；(2)缺省模板文件" + sDefaultFileName);
                else
                    sTemplateFileName = sDefaultFileName;
            }

            //============ 3. 读取模板文件的内容 =========
             StreamReader reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sTemplateFileName),
                         System.Text.Encoding.GetEncoding("gb2312"));

            string sTemplateContent = reader.ReadToEnd();
            reader.Close();

            //========== 4. 设到Hash中，并返回 =========
            _htThemeTemplate[sKey] = sTemplateContent;
            return sTemplateContent;
        }
    }
}
