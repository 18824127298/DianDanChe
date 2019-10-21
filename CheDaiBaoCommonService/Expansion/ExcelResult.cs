using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CheDaiBaoCommonService.Expansion
{
    /// <summary>
    /// 提供将泛型集合数据导出Excel文档。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelResult<T> : ActionResult where T : new()
    {
        #region 属性

        public IList<T> Entity { get; set; }

        public string FileName { get; set; }

        #endregion

        public ExcelResult(IList<T> entity, string fileName)
        {
            this.Entity = entity;
            this.FileName = fileName;
        }

        public ExcelResult(IList<T> entity)
        {
            this.Entity = entity;

            DateTime time = DateTime.Now;
            this.FileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Entity == null)
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            SetResponse(context);
        }

        /// <summary>
        /// 设置并向客户端发送请求响应。
        /// </summary>
        /// <param name="context"></param>
        private void SetResponse(ControllerContext context)
        {
            StringBuilder sBuilder = ConvertEntity();

            byte[] bytestr = Encoding.Unicode.GetBytes(sBuilder.ToString());

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ClearContent();
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.BufferOutput = true;
            context.HttpContext.Response.Charset = "GB2312";
            context.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("GB2312"); // System.Text.Encoding.UTF8; 
            context.HttpContext.Response.ContentType = "application/ms-excel";
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            context.HttpContext.Response.AddHeader("Content-Length", bytestr.Length.ToString());
            context.HttpContext.Response.Write(sBuilder);
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.Close();
            //context.HttpContext.Response.End();  //有close不要这个，否则报错“远程主机关闭了连接。”
        }

        /// <summary>
        /// 把泛型集合转换成组合Excel表格的字符串。
        /// </summary>
        /// <returns></returns>
        private StringBuilder ConvertEntity()
        {
            StringBuilder sb = new StringBuilder();

            AddTableHead(sb);
            AddTableBody(sb);

            return sb;
        }

        /// <summary>
        /// 根据IList泛型集合中的每项的属性值来组合Excel表格。
        /// </summary>
        /// <param name="sb"></param>
        private void AddTableBody(StringBuilder sb)
        {
            if (Entity == null || Entity.Count <= 0)
            {
                return;
            }

            PropertyDescriptorCollection properties = GetProperties();

            if (properties.Count <= 0)
            {
                return;
            }

            foreach (T t in this.Entity)
            {
                for (int j = 0; j < properties.Count; j++)
                {
                    string sign = j == properties.Count - 1 ? "\n" : "\t";
                    object obj = properties[j].GetValue(t);
                    obj = obj == null ? string.Empty : obj.ToString();
                    sb.Append(obj + sign);
                }
            }
        }

        /// <summary>
        /// 根据指定类型T的所有属性名称来组合Excel表头。
        /// </summary>
        /// <param name="sb"></param>
        private void AddTableHead(StringBuilder sb)
        {
            PropertyDescriptorCollection properties = GetProperties();

            if (properties.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];

                string sign = i == properties.Count - 1 ? "\n" : "\t";

                //如果字段有 [Display(Name = "手机")] 或 [DisplayName("姓名")] 特性，则优先显示DisplayName，如果都为空则显示字段名
                DisplayAttribute displayAttribute = property.Attributes[typeof(DisplayAttribute)] as DisplayAttribute;
                string displayName = property.DisplayName == property.Name
                                  ? (displayAttribute == null ? property.Name : displayAttribute.Name)
                                  : property.DisplayName;

                //sb.Append(properties[i].Name + sign);
                sb.Append(displayName + sign);
            }
        }

        /// <summary>
        /// 返回指定类型T的属性集合。
        /// </summary>
        /// <returns></returns>
        private static PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(typeof(T));
        }
    }
}
