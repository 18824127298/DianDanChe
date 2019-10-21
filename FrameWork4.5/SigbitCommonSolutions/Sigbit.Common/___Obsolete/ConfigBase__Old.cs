using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Xml;

namespace Sigbit.Common
{
    /// <summary>
    /// ���õĻ��ࡣ���û���XML�ļ��ĸ�ʽ������Թ̶��ĸ�ʽ��֯��
    /// �����Ҫ���һ���ض��ļ����д���ʱ��һ����Ҫ�̳и��ࡣ
    /// </summary>
    [Obsolete]
    public class ConfigBase__Old
    {
        Hashtable _htSections;

        private void LoadSectionNode(XmlNode sectionNode)
        {
            Hashtable htKeys = new Hashtable();
            
            foreach (XmlNode keyNode in sectionNode.ChildNodes)
            {
                if (keyNode.NodeType != XmlNodeType.Comment)
                {
                    foreach (XmlAttribute attr in keyNode.Attributes)
                    {
                        if (attr.Name == "value")
                            htKeys.Add(keyNode.Name, attr.Value);
                    }
                }
            }

            if (sectionNode.NodeType != XmlNodeType.Comment)
                _htSections.Add(sectionNode.Name, htKeys);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public ConfigBase__Old()
        {
            _htSections = new Hashtable();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sFileName">�����ļ���</param>
        public ConfigBase__Old(string sFileName)
        {
            _htSections = new Hashtable();
            LoadFromFile(sFileName);
        }

        /// <summary>
        /// ���ļ��м���������
        /// </summary>
        /// <param name="sFileName">�����ļ���</param>
        public void LoadFromFile(string sFileName)
        {
            //========== 1. ���ļ� ============
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sFileName);
            XmlNode nodeRoot = xmlDoc.DocumentElement;

            //============= 2. ����ÿһ���ӽڵ� ========
            foreach (XmlNode node in nodeRoot.ChildNodes)
                LoadSectionNode(node);
        }

        /// <summary>
        /// �õ������ַ���
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <returns>���õ��ַ���</returns>
        public string GetString(string sSectionName, string sKeyName)
        {
            return GetString(sSectionName, sKeyName, "");
        }

        /// <summary>
        /// �õ������ַ���
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <param name="sDefault">ȱʡֵ</param>
        /// <returns>���õ��ַ���</returns>
        public string GetString(string sSectionName, string sKeyName, string sDefault)
        {
            string sRet;
            try
            {
                sRet = (string)((Hashtable)_htSections[sSectionName])[sKeyName];
            }
            catch
            {
                return sDefault;
            }
            if (sRet == null)
                return sDefault;
            else
                return sRet;
        }

        /// <summary>
        /// �õ����õ�����
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <returns>���õ�����</returns>
        public int GetInt(string sSectionName, string sKeyName)
        {
            return GetInt(sSectionName, sKeyName, 0);                
        }

        /// <summary>
        /// �õ����õ�����
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <param name="nDefault">ȱʡֵ</param>
        /// <returns>���õ�����</returns>
        public int GetInt(string sSectionName, string sKeyName, int nDefault)
        {
            Object objRet;
            int nRet;
            try
            {
                objRet = ((Hashtable)_htSections[sSectionName])[sKeyName];
            }
            catch
            {
                return nDefault;
            }

            if (objRet == null)
                return nDefault;

            try
            {
                nRet = Convert.ToInt32(objRet);
                return nRet;
            }
            catch
            {
                return nDefault;
            }
        }

        /// <summary>
        /// �õ����õĲ���ֵ
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <returns>���õĲ���ֵ</returns>
        /// <remarks>ȱʡ����false</remarks>
        public bool GetBool(string sSectionName, string sKeyName)
        {
            return GetBool(sSectionName, sKeyName, false);
        }

        /// <summary>
        /// �õ����õĲ���ֵ
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <param name="bDefault">ȱʡֵ</param>
        /// <returns>���õĲ���ֵ</returns>
        /// <remarks>
        /// �ж�������ĵ�һ���ַ���Ϊ1,T,t,Y,y�򷵻��棬
        /// Ϊ0,F,f,N,n���ؼ١�����������true, false, yes,
        /// no�Ĵ���Ҳ�ܹ�һ���򾡡�
        /// </remarks>
        public bool GetBool(string sSectionName, string sKeyName, bool bDefault)
        {
            string sRet;
            sRet = GetString(sSectionName, sKeyName, "HlThoz");
            if (sRet == "HlThoz")
                return bDefault;

            char cFirstChar = sRet[0];
            switch (cFirstChar)
            {
                case '1':
                case 'T':
                case 't':
                case 'Y':
                case 'y':
                    return true;
                case '0':
                case 'F':
                case 'f':
                case 'N':
                case 'n':
                    return false;
                default:
                    return bDefault;
            }
        }

        /// <summary>
        /// �õ����õ���̬����ֵ
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <returns>���õ���̬����ֵ</returns>
        public Bool3State GetBool3State(string sSectionName, string sKeyName)
        {
            return GetBool3State(sSectionName, sKeyName, Bool3State.Undefine);
        }

        /// <summary>
        /// �õ����õ���̬����ֵ
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <param name="sKeyName">�ؼ�����</param>
        /// <param name="bDefault">ȱʡֵ</param>
        /// <returns>���õ���̬����ֵ</returns>
        public Bool3State GetBool3State(string sSectionName, string sKeyName, Bool3State bDefault)
        {
            string sRet;
            sRet = GetString(sSectionName, sKeyName, "HlThoz");
            if (sRet == "HlThoz")
                return bDefault;

            char cFirstChar = sRet[0];
            switch (cFirstChar)
            {
                case '1':
                case 'T':
                case 't':
                case 'Y':
                case 'y':
                    return Bool3State.True;
                case '0':
                case 'F':
                case 'f':
                case 'N':
                case 'n':
                    return Bool3State.False;
                default:
                    return bDefault;
            }
        }

        /// <summary>
        /// �õ�һ�����������Hash��
        /// </summary>
        /// <param name="sSectionName">����</param>
        /// <returns>�ε�����Hash��</returns>
        public Hashtable GetSection(string sSectionName)
        {
            Hashtable htRet;
            htRet = (Hashtable)_htSections[sSectionName];
            return htRet;
        }
    }
}
