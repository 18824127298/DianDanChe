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
        /// �õ������ģ������
        /// </summary>
        /// <param name="sTheme">������</param>
        /// <param name="sTemplateName">ģ����</param>
        /// <returns>ģ�������</returns>
        /// <remarks>
        /// ģ��Ŀǰ�����ļ��У����λ��Ϊ��
        /// ~/App_Themes/[������]/template/[ģ����].tpl;
        /// ����Ҳ������������ģ���ļ�����λȱʡ�����ģ���ļ�
        /// </remarks>
        public static string GetThemeTemplate(string sTheme, string sTemplateName)
        {
            //======= 1. �����ϣ������ֵ����ǰ�õ���������ֱ�ӷ��� ======
            string sKey = sTheme + "/" + sTemplateName;
            string sRet = (string)_htThemeTemplate[sKey];
            if (sRet != null)
                return sRet;

            //======= 2. �õ�template�ļ��� =========
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
                            + "����ģ���Ҳ������޷���λ���������ļ���(1)����ģ���ļ�"
                            + sTemplateFileName + "��(2)ȱʡģ���ļ�" + sDefaultFileName);
                else
                    sTemplateFileName = sDefaultFileName;
            }

            //============ 3. ��ȡģ���ļ������� =========
             StreamReader reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sTemplateFileName),
                         System.Text.Encoding.GetEncoding("gb2312"));

            string sTemplateContent = reader.ReadToEnd();
            reader.Close();

            //========== 4. �赽Hash�У������� =========
            _htThemeTemplate[sKey] = sTemplateContent;
            return sTemplateContent;
        }
    }
}
