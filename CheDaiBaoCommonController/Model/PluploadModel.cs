using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoCommonController.Model
{
    public class PluploadModel
    {
        /// <summary>
        /// 上传路径 
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string SavaPath { get; set; }

        /// <summary>
        /// 回调方法
        /// </summary>
        public string CallBackMethod { get; set; }

        /// <summary>
        /// 是否上传多个文件
        /// </summary>
        public bool IsMultiple { get; set; }

        /// <summary>
        /// 传进来 传出去
        /// </summary>
        public string CallBackObj { get; set; }

        /// <summary>
        /// 给上传控件增加样式类
        /// </summary>
        public string DocmentClass { get; set; }

        /// <summary>
        /// 给上传控件增加样式
        /// </summary>
        public string DocmentStyle { get; set; }

        ///// <summary>
        ///// 宽度
        ///// </summary>
        //public int Width { get; set; }

        ///// <summary>
        ///// 高度
        ///// </summary>
        //public int Height { get; set; }

        ///// <summary>
        ///// 清晰度
        ///// </summary>
        //public int Quality { get; set; }
    }

    public class PluploadFileModel
    {
        /// <summary>
        /// 原名称
        /// </summary>
        public string OldFileName { get; set; }

        /// <summary>
        /// 新名称
        /// </summary>
        public string NewFileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Mvc上传集合对象
        /// </summary>
        public int Index { get; set; }
    }
}
