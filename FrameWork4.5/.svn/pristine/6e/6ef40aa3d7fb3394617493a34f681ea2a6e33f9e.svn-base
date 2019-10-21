using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Net.WebVisitor
{
    class WVT__ResponseList : ArrayList
    {
        /// <summary>
        /// 取出一个事件。
        /// </summary>
        /// <returns>取中的事件。如池中无事件，则返回null。</returns>
        public WVWebResponse PopResponse()
        {
            lock (this)
            {
                if (this.Count <= 0)
                    return null;

                WVWebResponse response = (WVWebResponse)this[0];
                this.RemoveAt(0);
                return response;
            }
        }

        /// <summary>
        /// 放入一个事件
        /// </summary>
        /// <param name="ev">事件</param>
        public void PushResponse(WVWebResponse response)
        {
            lock (this)
            {
                this.Add(response);
            }
        }
    }
}
