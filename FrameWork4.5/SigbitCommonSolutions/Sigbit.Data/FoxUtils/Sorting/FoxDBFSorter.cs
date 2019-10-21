using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Data.FoxDBF;

namespace Sigbit.Data.FoxUtils.Sorting
{
    class FoxDBFSorter_KeyListItem
    {
        private string _keyValue = "";
        /// <summary>
        /// 关键字的值
        /// </summary>
        public string KeyValue
        {
            get { return _keyValue; }
            set { _keyValue = value; }
        }

        private int _recNo = 0;
        /// <summary>
        /// 记录号
        /// </summary>
        public int RecNo
        {
            get { return _recNo; }
            set { _recNo = value; }
        }

        public override string ToString()
        {
            string sRet = this.KeyValue + "〖" + this.RecNo.ToString() + "〗";
            return sRet;
        }
    }

    class FoxDBFSorter_KeyList : ArrayList
    {
        /// <summary>
        /// 比较类，关键字权值排序
        /// </summary>
        public class CompareYpValueClass : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                FoxDBFSorter_KeyListItem item1 = (FoxDBFSorter_KeyListItem)x;
                FoxDBFSorter_KeyListItem item2 = (FoxDBFSorter_KeyListItem)y;

                return item1.KeyValue.CompareTo(item2.KeyValue);
            }
        }

        public void AddItem(FoxDBFSorter_KeyListItem item)
        {
            this.Add(item);
        }

        public FoxDBFSorter_KeyListItem GetItem(int nIndex)
        {
            return (FoxDBFSorter_KeyListItem)this[nIndex];
        }

        public void SortOnKeyValue()
        {
            IComparer compareYpValue = new CompareYpValueClass();
            this.Sort(compareYpValue);
        }

        public override string ToString()
        {
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                FoxDBFSorter_KeyListItem item = this.GetItem(i);
                sbRet.AppendLine(item.ToString());
            }

            return sbRet.ToString();
        }
    }

    public class FoxDBFSorter
    {
        private string _inputSrcDBFFileName = "";
        /// <summary>
        /// 源文件名
        /// </summary>
        public string InputSrcDBFFileName
        {
            get { return _inputSrcDBFFileName; }
            set { _inputSrcDBFFileName = value; }
        }

        private string _inputDestDBFFileName = "";
        /// <summary>
        /// 目标文件名
        /// </summary>
        public string InputDestDBFFileName
        {
            get { return _inputDestDBFFileName; }
            set { _inputDestDBFFileName = value; }
        }

        private int _inputKeyFieldCount = 0;
        /// <summary>
        /// 关键字段数
        /// </summary>
        public int InputKeyFieldCount
        {
            get { return _inputKeyFieldCount; }
            set { _inputKeyFieldCount = value; }
        }

        //private string _outputMessage = "";
        ///// <summary>
        ///// 输出的信息【临时】
        ///// </summary>
        //public string OutputMessage
        //{
        //    get { return _outputMessage; }
        //    set { _outputMessage = value; }
        //}

        public void DoSort()
        {
            //========= 1. 按源文件的格式，创建目标文件 ============
            CreateDestDBF();

            ////========= 2. 复制所有数据 ===========
            //CopyAllData();

            //========= 2. 读出所有的行的关键值 ============
            FoxDBFSorter_KeyList allDataKeyList = ReadOutSrcAllDataKeyList();

            //========== 3. 排序关键字的值 ===========
            allDataKeyList.SortOnKeyValue();

            //this.OutputMessage = allDataKeyList.ToString();

            //=========== 4. 根据排序的关键字，写入到目标数据库中 =============
            WriteToDestDBFBySortedList(allDataKeyList);
        }

        private void CreateDestDBF()
        {
            FoxDBFile foxSrc = new FoxDBFile();
            foxSrc.AttachFile(this.InputSrcDBFFileName);

            DBFCreate dcCreate = new DBFCreate(this.InputDestDBFFileName, foxSrc.FieldCount);

            for (int i = 0; i < foxSrc.FieldCount; i++)
            {
                string sFieldName = foxSrc.FieldName(i);
                DBFFieldType fieldType = foxSrc.FieldType(i);
                int nFieldLength = foxSrc.FieldLength(i);
                int nFieldPoint = foxSrc.FieldPoint(i);

                dcCreate.AddField(sFieldName, fieldType, nFieldLength, nFieldPoint);
            }

            dcCreate.CreateDBF();
            foxSrc.CloseDBF();
        }

        //private void CopyAllData()
        //{
        //    FoxDBFile foxSrc = new FoxDBFile();
        //    foxSrc.AttachFile(this.InputSrcDBFFileName);

        //    FoxDBFile foxDest = new FoxDBFile();
        //    foxDest.AttachFile(this.InputDestDBFFileName);

        //    for (int i = 1; i <= foxSrc.RecCount; i++)
        //    {
        //        foxSrc.Go(i);

        //        for (int f = 0; f < foxSrc.FieldCount; f++)
        //        {
        //            string sValue = foxSrc.GetRecordString(f);
        //            foxDest.SetRecordData(f, sValue);
        //        }

        //        foxDest.Append();
        //    }


        //    foxDest.CloseDBF();
        //    foxSrc.CloseDBF();
        //}

        private FoxDBFSorter_KeyList ReadOutSrcAllDataKeyList()
        {
            FoxDBFSorter_KeyList keyListRet = new FoxDBFSorter_KeyList();

            FoxDBFile foxSrc = new FoxDBFile();
            foxSrc.AttachFile(this.InputSrcDBFFileName);

            for (int i = 1; i <= foxSrc.RecCount; i++)
            {
                foxSrc.Go(i);

                string sValue = "";
                for (int f = 0; f < this.InputKeyFieldCount; f++)
                    sValue += foxSrc.GetRecordString_WithoutTrim(f);

                FoxDBFSorter_KeyListItem item = new FoxDBFSorter_KeyListItem();
                item.RecNo = i;
                item.KeyValue = sValue;

                keyListRet.AddItem(item);
            }

            foxSrc.CloseDBF();

            return keyListRet;
        }

        private void WriteToDestDBFBySortedList(FoxDBFSorter_KeyList sortedList)
        {
            FoxDBFile foxSrc = new FoxDBFile();
            foxSrc.AttachFile(this.InputSrcDBFFileName);

            FoxDBFile foxDest = new FoxDBFile();
            foxDest.AttachFile(this.InputDestDBFFileName);

            for (int i = 0; i < sortedList.Count; i++)
            {
                FoxDBFSorter_KeyListItem item = sortedList.GetItem(i);
                foxSrc.Go(item.RecNo);

                for (int f = 0; f < foxSrc.FieldCount; f++)
                {
                    string sValue = foxSrc.GetRecordString(f);
                    foxDest.SetRecordData(f, sValue);
                }

                foxDest.Append();
            }


            foxDest.CloseDBF();
            foxSrc.CloseDBF();
        }
    }
}


