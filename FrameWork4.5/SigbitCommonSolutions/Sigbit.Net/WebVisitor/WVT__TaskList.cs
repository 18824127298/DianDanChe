using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Net.WebVisitor
{
    class WVT__TaskList : ArrayList
    {
        public void AddTask(WVT__Task task)
        {
            this.Add(task);
        }

        public WVT__Task GetTask(int nIndex)
        {
            return (WVT__Task)this[nIndex];
        }
    }
}
