using System;
using System.Collections.Generic;
using System.Web;

namespace JiaYouWxPayApi
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}