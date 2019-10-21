using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sigbit.Web.MediaServer
{
    /// <summary>
    /// MediaServer��·��������
    /// </summary>
    /// <remarks>
    /// �����ĸ������д��κ�һ���������ã����ɻ�������ĸ����Ե�ֵ������Set��������ֵ��
    /// </remarks>
    public class MediaServerPath
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MediaServerPath()
        {
            this.FullPath = MediaServerConfig.Instance.RootPath;
        }


        private string _fullUrl = "";
        /// <summary>
        /// ����Url·��,ʾ��:http://localhost/MediaServer/Site/ChannelName/****.jpg;
        /// </summary>
        public string FullUrl
        {
            get { return _fullUrl; }
            set
            {
                _fullUrl = value;

                //=====================1.·��У��=====================
                string sRootUrl = MediaServerConfig.Instance.RootUrl.ToLower();

                if (_fullUrl.Substring(0, sRootUrl.Length).ToLower() != sRootUrl)
                {
                    throw new Exception("��Ч����Url:" + _fullUrl);
                }

                //=====================2.��ȡ���Url===================
                _relativeUrl = _fullUrl.Substring(sRootUrl.Length);

                //=====================3.��ȡ���·��==================
                _relativePath = _relativeUrl.Replace('/', '\\');

                //=====================4.��ȡ����·��==================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;


                //=====================5.��ʽ��·��====================
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);


            }
        }


        private string _fullPath = "";
        /// <summary>
        /// ��������·��,ʾ��:D:\Sigbit\Project\MediaServer\BaseStation\UploadFile\***.jpg;
        /// </summary>
        public string FullPath
        {
            get
            {
                string sRetPath = TrimPath(_fullPath);
                return sRetPath;
            }
            set
            {
                //=====================1.·��У��=====================
                _fullPath = value.Replace('/', '\\');

                string sRootPath = MediaServerConfig.Instance.RootPath.ToLower();
                if (_fullPath.Substring(0, sRootPath.Length).ToLower() != sRootPath)
                {
                    throw new Exception("��Ч��������·��:" + _fullPath);
                }

                //=====================2.��ȡ�������·��==================
                _relativePath = _fullPath.Substring(sRootPath.Length);

                //=====================3.��ȡ���Url===================
                _relativeUrl = _relativePath.Replace('\\', '/');

                //=====================4.��ȡ����Url===================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================5.��ʽ��·��====================
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);

            }
        }

        private string _relativeUrl = "";
        /// <summary>
        /// ���Url·��,����MediaServer�µ���·��,ʾ��:/SiteName/ChannelName/****.jpg;
        /// </summary>
        public string RelativeUrl
        {
            get { return _relativeUrl; }
            set
            {
                //=====================1.·��У��=====================
                _relativeUrl = value;
                //if (_relativeUrl.Contains("\\"))
                //{
                //    throw new Exception("��Ч���Url·��:" + _relativeUrl + "\r\n��ȷ·����ʽ:***/***/***");
                //}

                _relativeUrl = _relativeUrl.Replace('\\', '/');

                if (_relativeUrl.Contains(":"))
                {
                    throw new Exception("��Ч���Url·��:" + _relativeUrl);
                }

                _relativeUrl = "/" + _relativeUrl.TrimStart('/');

                //=====================2.��ȡ���·��=================
                _relativePath =  _relativeUrl.Replace('/', '\\');

                //=====================3.��ȡ����Url==================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================4.��ȡ����·��=================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;

                //=====================5.������ʽ��·��===============
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);
            }
        }


        private string _relativePath = "";
        /// <summary>
        /// �������·��,����MediaServer�µ���·��,ʾ��:\BaseStation\UploadFile\***.jpg;
        /// </summary>
        public string RelativePath
        {
            get
            {
                string sRetPath = TrimPath(_relativePath);
                return sRetPath;
            }
            set
            {
                //=====================1.·��У��=====================
                _relativePath = value.Replace('/', '\\');
                if (_relativePath.Contains("/"))
                {
                    throw new Exception("��Ч�������·��:" + _relativePath + "\r\n��ȷ·����ʽ:***\\***");
                }

                if (_relativePath.Contains(":"))
                {
                    throw new Exception("��Ч�������·��:" + _relativePath);
                }

                _relativePath = "\\" + _relativePath.TrimStart('\\');

                //=====================2.��ȡ���Url=================
                _relativeUrl = _relativePath.Replace('\\', '/');

                //=====================3.��ȡ����Url==================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================4.��ȡ����·��=================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;

                //=====================5.������ʽ��·��===============
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);
            }
        }


        private string _formatFullUrl = "";
        /// <summary>
        /// ��ʽ�����URL·��
        /// </summary>
        public string FormatFullUrl
        {
            get
            {
                return _formatFullUrl;
            }
            set
            {
                try
                {
                    this.FullUrl = MediaServerUtil.UrlUnFormat(value);
                }
                catch (Exception)
                {
                    throw new Exception("��Ч��������ʽ��·��:" + value);
                }
            }
        }


        /// <summary>
        /// ��������·��
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        private string TrimPath(string sPath)
        {
            if (sPath.Contains("?"))
            {
                sPath = sPath.Substring(0, sPath.IndexOf("?"));
            }


            return sPath;
        }


        public override string ToString()
        {
            string sRet = "";

            sRet += "����URL:" + this.FullUrl + "\r\n";
            sRet += "���URL:" + this.RelativeUrl + "\r\n";
            sRet += "����·��:" + this.FullPath + "\r\n";
            sRet += "���·��:" + this.RelativePath + "\r\n";
            sRet += "��ʽ��URL:" + this.FormatFullUrl + "\r\n";

            return sRet;
        }


    }
}
