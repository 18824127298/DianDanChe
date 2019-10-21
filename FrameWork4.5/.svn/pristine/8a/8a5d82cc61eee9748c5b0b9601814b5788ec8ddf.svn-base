using System;
using System.Collections;
using Sigbit.Data;
using System.Data;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Sigbit.Web
{
    public class WebUser
    {
        public WebUser()
        {
            
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public static bool UserExist(string sUserName)
        {
            return true;
        
        }


        /// <summary>
        /// 判断用户是否有效
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static bool IsValidUser(string sUserID, string sPassword)
        {
            string sSQLStr = "select user_uid from sbt_user where user_uid='"+sUserID+"' and password='" + sPassword+ "'";
            string ds = DataHelper.Instance.ExecuteScalar(sSQLStr).ToString();
            if (ds != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断用户是否有某个权限
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sRightCode"></param>
        /// <returns></returns>
        public static bool HasRight(string sUserName,string sRightCode)
        {

            return true;
        }

        /// <summary>
        /// 获取用户的全部权限
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public static ArrayList GetUserRightList(string sUserName)
        {
            return null;
        
        }

    }

}