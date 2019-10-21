using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Common;

namespace Sigbit.Framework
{
    /// <summary>
    /// �������⣬���������еĹ���
    /// </summary>
    public class SbtTaskRuleLib : Hashtable
    {
        private static SbtTaskRuleLib _thisIntance = null;
        /// <summary>
        /// ����Ψһ��ʵ��
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
        /// ���캯��
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
        /// ����һ��������
        /// </summary>
        /// <param name="item">���������</param>
        /// <remarks>�ڳ�ʼ��ʱ����</remarks>
        private void AddRuleItem(SbtTaskRuleItem item)
        {
            Add(item.DaemonType, item);
        }

        /// <summary>
        /// ������������ͣ��õ���صĹ���
        /// </summary>
        /// <param name="sDaemonType">���������</param>
        /// <returns>����Ĺ���</returns>
        public SbtTaskRuleItem GetRuleItem(string sDaemonType)
        {
            return (SbtTaskRuleItem)this[sDaemonType];
        }

        /// <summary>
        /// ��ʼ�������
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
