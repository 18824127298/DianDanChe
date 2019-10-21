using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Common;

namespace Sigbit.Framework
{
    /// <summary>
    /// 任务规则库，汇总了所有的规则
    /// </summary>
    public class SbtTaskRuleLib : Hashtable
    {
        private static SbtTaskRuleLib _thisIntance = null;
        /// <summary>
        /// 返回唯一的实例
        /// </summary>
        public static SbtTaskRuleLib Instance
        {
            get
            {
                if (_thisIntance == null)
                    _thisIntance = new SbtTaskRuleLib();

                return _thisIntance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SbtTaskRuleLib()
        {
            DataSet ds = TbSysDaemonType.GetDataSetOfAllValid();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                TbSysDaemonType tbl = new TbSysDaemonType();
                tbl.AssignByDataRow(row);

                SbtTaskRuleItem item = new SbtTaskRuleItem();
                item.FromTbSysDaemonType(tbl);

                AddRuleItem(item);
            }
        }

        /// <summary>
        /// 增加一个规则项
        /// </summary>
        /// <param name="item">任务规则项</param>
        /// <remarks>在初始化时调用</remarks>
        private void AddRuleItem(SbtTaskRuleItem item)
        {
            Add(item.DaemonType, item);
        }

        /// <summary>
        /// 根据任务的类型，得到相关的规则
        /// </summary>
        /// <param name="sDaemonType">任务的类型</param>
        /// <returns>任务的规则</returns>
        public SbtTaskRuleItem GetRuleItem(string sDaemonType)
        {
            return (SbtTaskRuleItem)this[sDaemonType];
        }

        /// <summary>
        /// 初始化任务表
        /// </summary>
        public void InitTaskTableByRule()
        {
            TbSysDaemonTask.TruncateTable();

            foreach (DictionaryEntry entry in this)
            {
                SbtTaskRuleItem ruleItem = (SbtTaskRuleItem)entry.Value;
                TbSysDaemonTask tblTask = ruleItem.GenerateTbTaskRecord(DateTimeUtil.Now, false);
                tblTask.Insert();
            }
        }
    }
}
