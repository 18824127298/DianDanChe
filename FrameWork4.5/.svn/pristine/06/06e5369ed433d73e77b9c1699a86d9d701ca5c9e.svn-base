using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Sigbit.App.Net.WeiXinService.Client;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public enum MenuType
    {
        Click,
        View
    }

    /// <summary>
    /// 菜单基本信息
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MenuType type { get; set; }

        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        /// <summary>
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuInfo> sub_button { get; set; }


        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="buttonType">菜单按钮类型</param>
        /// <param name="value">按钮的键值（Click)，或者连接URL(View)</param>
        public MenuInfo(string name, MenuType buttonType, string value)
        {
            this.name = name;
            this.type = buttonType;

            if (buttonType == MenuType.Click)
            {
                this.key = value;
            }
            else if (buttonType == MenuType.View)
            {
                this.url = value;
            }
        }




        /// <summary>
        /// 参数化构造函数,用于构造子菜单
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="sub_button">子菜单集合</param>
        public MenuInfo(string name, IEnumerable<MenuInfo> sub_button)
        {
            this.name = name;
            this.sub_button = new List<MenuInfo>();
            this.sub_button.AddRange(sub_button);
        }

    }


    /// <summary>
    /// 菜单的Json字符串对象
    /// </summary>
    public class MenuJson
    {
        public List<MenuInfo> button { get; set; }

        public MenuJson()
        {
            button = new List<MenuInfo>();
        }
    }

    /// <summary>
    /// 菜单列表的Json对象
    /// </summary>
    public class MenuListJson
    {
        public MenuJson menu { get; set; }
    }


    public class TWJMenuCreateReq : TWJReqBase
    {
        // https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN

        public TWJMenuCreateReq()
        {
            ReqCode = TWJBusiCode.MenuCreate;
            AccessMethod = HttpMethod.Post;

        }

        private string _accessToken = "";

        public string AccessToken
        {
            get {
                if (_accessToken == "")
                    _accessToken = TWXWeiXinClient.Instance.GetAccessToken();
                return _accessToken; }
            set { _accessToken = value; }
        }


        private MenuJson _menuButtons = new MenuJson();
        /// <summary>
        /// 菜单
        /// </summary>
        public MenuJson MenuButtons
        {
            get { return _menuButtons; }
            set { _menuButtons = value; }
        }


        public override string ToAccessUrl()
        {
            string sRet = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + this.AccessToken;

            return sRet;
        }


        public override string ToJsonString()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            string sJson = js.Serialize(this.MenuButtons);

            return sJson;
        }



    }
}
