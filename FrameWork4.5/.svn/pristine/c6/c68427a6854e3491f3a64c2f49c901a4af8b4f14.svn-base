using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Reflection;
using System.Text.RegularExpressions;

public partial class framework_mechanism_exec_cs_function_execcsfunction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region clear cache

        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = -1;
        Response.CacheControl = "no-cache";

        #endregion

        string dllPath = Request.PhysicalApplicationPath + @"bin\";
        string AssemblyName = "", TypeName = "", FunctionName = "";
        ArrayList ReceiveParams = new ArrayList();
        object[] SendParams = new object[] { };

        Assembly assmebly;
        Type type;
        object rtnValue = new object();

        try
        {
            //接收参数
            string tempParam = Request.QueryString["param"];
            //if (CommonFunction.IsEmptyOrNull(tempParam))
            if (tempParam == null || tempParam == "")
            {
                throw new Exception("缺少param参数");
            }

            //解析参数
            ParseQueryString(tempParam, ref AssemblyName, ref TypeName, ref FunctionName, ref SendParams);

            //加载程序集            
            assmebly = Assembly.LoadFrom(dllPath + AssemblyName);
            //调用方法
            type = assmebly.GetType(TypeName);
            rtnValue = type.InvokeMember(FunctionName, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, SendParams);
        }
        catch (Exception ex)
        {
            Response.Write(@"/1ExecCSFucntion处理失败:" + ex.Message);
            Response.End();
            return;
        }

        if (rtnValue == null) rtnValue = @"/0";//表示没有返回值或返回值为null
        Response.Write(rtnValue);
        Response.End();
    }

    private void ParseQueryString(string queryString, ref string asmbName, ref string typeName, ref string funcName, ref object[] objectParam)
    {
        ArrayList param = new ArrayList();
        string split0 = "#;#", split1 = "#:#";
        typeName = "";
        funcName = "";
        asmbName = "";

        string[] splitParam = Regex.Split(queryString, split0);
        for (int i = 0; i < splitParam.Length; i++)
        {
            if (splitParam[i] == "") continue;
            string[] exps = Regex.Split(splitParam[i], split1);
            if (exps[0] == "type")
            {
                typeName = exps[1];
            }
            else if (exps[0] == "func")
            {
                funcName = exps[1];
            }
            else if (exps[0] == "asmb")
            {
                asmbName = exps[1];
            }
            else
            {
                switch (exps[0])
                {
                    case "string":
                        param.Add(exps[1]);
                        break;
                    case "int":
                        param.Add(Convert.ToInt32(exps[1]));
                        break;
                    case "decimal":
                        param.Add(Convert.ToDecimal(exps[1]));
                        break;
                }
            }
        }

        objectParam = new object[param.Count];
        for (int i = 0; i < param.Count; i++)
        {
            objectParam[i] = param[i];
        }
    }

}
