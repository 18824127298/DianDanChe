using System;
using System.Xml;
namespace JiaYouWxPayApi.lib
{
    public class SafeXmlDocument:XmlDocument
    {
        public SafeXmlDocument()
        {
            this.XmlResolver = null;
        }
    }
}
