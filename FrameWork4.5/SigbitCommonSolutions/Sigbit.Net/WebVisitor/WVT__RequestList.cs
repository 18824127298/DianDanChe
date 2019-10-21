using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Net.WebVisitor
{
    class WVT__RequestList : ArrayList
    {
        /// <summary>
        /// 取出一个事件。
        /// </summary>
        /// <returns>取中的事件。如池中无事件，则返回null。</returns>
        public WVWebRequest PopRequest()
        {
            lock (this)
            {
                if (this.Count <= 0)
                    return null;

                WVWebRequest request = (WVWebRequest)this[0];
                this.RemoveAt(0);
                return request;
            }
        }

        /// <summary>
        /// 放入一个事件
        /// </summary>
        /// <param name="ev">事件</param>
        public void PushRequest(WVWebRequest request)
        {
            lock (this)
            {
                this.Add(request);
            }
        }
    }
}
