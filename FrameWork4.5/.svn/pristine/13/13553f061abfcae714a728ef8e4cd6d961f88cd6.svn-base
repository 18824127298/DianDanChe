using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{

    public class TWXNewsMessageResp_ArticleItem
    {
        private string _title = "";
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description = "";
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _picUrl = "";
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl
        {
            get { return _picUrl; }
            set { _picUrl = value; }
        }

        private string _url = "";
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

    }

    public class TWXNewsMessageResp_Articles : ArrayList
    {
        public void AddArticle(TWXNewsMessageResp_ArticleItem article)
        {
            this.Add(article);
        }

        public TWXNewsMessageResp_ArticleItem GetArticle(int nIndex)
        {
            return this[nIndex] as TWXNewsMessageResp_ArticleItem;
        }
    }


    public class TWXNewsMessageResp : TWXMessageBaseResp
    {
        public TWXNewsMessageResp()
        {
            this.MsgType = MsgType.News;
        }

        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount
        {
            get
            {
                return _articles.Count;
            }
        }

        private TWXNewsMessageResp_Articles _articles = new TWXNewsMessageResp_Articles();
        /// <summary>
        /// 
        /// </summary>
        public TWXNewsMessageResp_Articles Articles
        {
            get { return _articles; }
            set { _articles = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("ArticleCount", this.ArticleCount.ToString(), false);

            TWXNode nodeRoot = new TWXNode();
            nodeRoot.Key = "Articles";

            for (int i = 0; i < ArticleCount; i++)
            {
                TWXNewsMessageResp_ArticleItem articleItem=Articles.GetArticle(i);

                TWXNode nodeItem = new TWXNode();
                nodeItem.Key = "item";

                nodeRoot.AppendNode(nodeItem);

                nodeItem.AppendNode("Title", articleItem.Title);
                nodeItem.AppendNode("Description", articleItem.Description);
                nodeItem.AppendNode("PicUrl", articleItem.PicUrl);
                nodeItem.AppendNode("Url", articleItem.Url);
            }

            AddNode(nodeRoot);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Articles.Clear();

            TWXNode nodeRoot = base.GetNode("Articles");

            for (int i = 0; i < nodeRoot.ChildNodes.Count; i++)
            {
                TWXNode nodeItem = nodeRoot.ChildNodes.GetNode(i);

                TWXNewsMessageResp_ArticleItem articleItem = new TWXNewsMessageResp_ArticleItem();
                articleItem.Title = nodeItem.ChildNodes.GetValueOfKey("Title");
                articleItem.Description = nodeItem.ChildNodes.GetValueOfKey("Description");
                articleItem.PicUrl = nodeItem.ChildNodes.GetValueOfKey("PicUrl");
                articleItem.Url = nodeItem.ChildNodes.GetValueOfKey("Url");

                _articles.AddArticle(articleItem);
            }


        }
    }
}
