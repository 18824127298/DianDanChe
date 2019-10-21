using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// �����
    /// </summary>
    public class BIPDataSet
    {
        #region ��ֵ
        /// <summary>
        /// 2012��ֵ
        /// </summary>
        /// <param name="dsSrc">Դ���ݼ�</param>
        public void AssignBy(BIPDataSet dsSrc)
        {
            //=========== 1. ��յ�ǰ�����ݼ� =============
            this.Clear();

            //========= 2. ��������� ============
            this.DataSetName = dsSrc.DataSetName;

            //============ 3. �ֶ� =============
            for (int i = 0; i < dsSrc.GetFieldCount(); i++)
            {
                string sFieldName = dsSrc.GetFieldName(i);
                BIPFieldType ftFieldType = dsSrc.GetFieldType(i);
                int nFieldLength = dsSrc.GetFieldLength(i);
                int nFieldPrecision = dsSrc.GetFieldPrecision(i);

                this.AddField(sFieldName, ftFieldType, nFieldLength, nFieldPrecision);
            }

            //=========== 4. ���� ===============
            for (int rec = 1; rec <= dsSrc.GetRecordCount(); rec++)
            {
                for (int fld = 0; fld < dsSrc.GetFieldCount(); fld++)
                {
                    string sItemValue = dsSrc.GetItemString(rec, fld);
                    this.SetItemString(rec, fld, sItemValue);
                }
            }
        }
        #endregion

        #region ����
        string _dataSetName = "";
        /// <summary>
        /// ���������
        /// </summary>
        public string DataSetName
        {
            get { return _dataSetName; }
            set { _dataSetName = value; }
        }

        int _dataSetSeq = 0;
        /// <summary>
        /// �������˳���
        /// </summary>
        public int DataSetSeq
        {
            get { return _dataSetSeq; }
            set { _dataSetSeq = value; }
        }

        BIPFieldList _bipFieldList = new BIPFieldList();
        BIPRecordList _bipRecordList = new BIPRecordList();
        #endregion ����

        #region ���켰֧�ֺ���
        /// <summary>
        /// ���캯��
        /// </summary>
        public BIPDataSet()
        {
            _dataSetSeq = 0;
        }

        /// <summary>
        /// ���DataSet�е������ֶμ�ֵ
        /// </summary>
        public void Clear()
        {
            _bipFieldList.Clear();
            _bipRecordList.Clear();
        }

        /// <summary>
        /// �õ���¼��
        /// </summary>
        /// <returns>��¼��</returns>
        public int GetRecordCount()
        {
            return _bipRecordList.Count;
        }

        /// <summary>
        /// ������
        /// </summary>
        public bool IgnoreCase
        {
            get
            { return _bipFieldList.IgnoreCase; }
            set
            { _bipFieldList.IgnoreCase = value; }
        }
        #endregion ���켰֧�ֺ���

        #region �ֶ�
        /// <summary>
        /// �õ��ֶ���
        /// </summary>
        /// <returns>�ֶ���</returns>
        public int GetFieldCount()
        {
            return _bipFieldList.Count;
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <param name="nFieldLength">�ֶγ���</param>
        /// <param name="nFieldPrecision">�ֶξ���</param>
        /// <remarks>
        /// ȱʡ����£�fieldTypeΪ�ַ��ͣ��ֶγ��Ȳ����塣����ֶ�����
        /// �Ѿ����ڣ����׳����⡣ָ�����ȵ�����£����fieldType������
        /// �����ͣ����׳����⡣
        /// </remarks>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength, int nFieldPrecision)
        {
            if (sFieldName != "")
            {
                if (_bipFieldList.LocateFieldName(sFieldName) != -1)
                    throw new BIPCallException("BIPDataSet::AddField() : "
                            + "Field already exists - " + sFieldName);
            }

            BIPField bipField = new BIPField();
            bipField.FieldName = sFieldName;
            bipField.FieldType = fieldType;
            bipField.FieldLength = nFieldLength;
            bipField.FieldPrecision = nFieldPrecision;

            if (nFieldPrecision != 0 && fieldType != BIPFieldType.Number)
                throw new BIPCallException("BIPDataSet::AddField() : "
                        + "field_precision != 0 of not number field_type - "
                        + sFieldName);

            _bipFieldList.AddField(bipField);
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <param name="nFieldLength">�ֶγ���</param>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength)
        {
            AddField(sFieldName, fieldType, nFieldLength, 0);
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="fieldType">�ֶ�����</param>
        public void AddField(string sFieldName, BIPFieldType fieldType)
        {
            AddField(sFieldName, fieldType, 0, 0);
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        public void AddField(string sFieldName)
        {
            AddField(sFieldName, BIPFieldType.Char, 0, 0);
        }

        /// <summary>
        /// �Կ����Ƶķ�ʽ����ָ���������ֶ�
        /// </summary>
        /// <param name="nFieldCount">�ֶ�����</param>
        public void AddFields(int nFieldCount)
        {
            for (int i = 0; i < nFieldCount; i++)
            {
                AddField("");
            }
        }

        /// <summary>
        /// �õ�ָ���ֶε�����
        /// </summary>
        /// <param name="nSeq">�ֶ����</param>
        /// <returns>�ֶε�����</returns>
        public string GetFieldName(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldName() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldName;
        }

        /// <summary>
        /// �õ�ָ���ֶε�����
        /// </summary>
        /// <param name="nSeq">�ֶ����</param>
        /// <returns>�ֶ�����</returns>
        public BIPFieldType GetFieldType(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldType() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldType;
        }

        /// <summary>
        /// �õ�ָ���ֶεĳ���
        /// </summary>
        /// <param name="nSeq">�ֶ����</param>
        /// <returns>�ֶγ���</returns>
        public int GetFieldLength(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldLength() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldLength;
        }

        /// <summary>
        /// �õ�ָ���ֶεľ���
        /// </summary>
        /// <param name="nSeq">�ֶ����</param>
        /// <returns>�ֶξ���</returns>
        public int GetFieldPrecision(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldPrecision() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldPrecision;
        }

        /// <summary>
        /// �õ�ָ�����Ƶ��ֶ����
        /// </summary>
        /// <param name="sFieldName">�ֶ�����</param>
        /// <returns>�ֶ����</returns>
        public int GetFieldSeq(string sFieldName)
        {
            int nFieldSeq;
            nFieldSeq = _bipFieldList.LocateFieldName(sFieldName);

            if (nFieldSeq == -1)
                throw new BIPCallException("BIPDataSet::GetFieldSeq() : "
                        + "Cannot locate the field name - " + sFieldName);

            return nFieldSeq;
        }
        #endregion �ֶ�

        #region ��ȡ����
        /// <summary>
        /// ��ָ����¼��ţ��ֶ��������ȡ�ֶ��ַ���ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ��ַ���ֵ</returns>
        public string GetItemString(int nRecordNum, int nFieldSeq)
        {
            if (nFieldSeq >= GetFieldCount())
                throw new BIPCallException("TCBIPDataSet::GetItemString() : "
                        + "field_seq out of range - " + nFieldSeq.ToString());

            if (nRecordNum > GetRecordCount() + 1)
                throw new BIPCallException("TCBIPDataSet::GetItemString() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            return _bipRecordList.GetItemString(nRecordNum, nFieldSeq);
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ�������ȡ�ֶ��ַ���ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ��ַ���ֵ</returns>
        public string GetItemString(int nRecordNum, string sFieldName)
        {
            return GetItemString(nRecordNum, GetFieldSeq(sFieldName));
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ��������ȡ�ֶ�����ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ�����ֵ</returns>
        public int GetItemInteger(int nRecordNum, int nFieldSeq)
        {
            return Convert.ToInt32(GetItemString(nRecordNum, nFieldSeq));
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ�������ȡ�ֶ�����ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ�����ֵ</returns>
        public int GetItemInteger(int nRecordNum, string sFieldName)
        {
            return Convert.ToInt32(GetItemString(nRecordNum, sFieldName));
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ��������ȡ�ֶ�˫����ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ�˫����ֵ</returns>
        public double GetItemDouble(int nRecordNum, int nFieldSeq)
        {
            return Convert.ToDouble(GetItemString(nRecordNum, nFieldSeq));
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ�������ȡ�ֶ�˫����ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ�˫����ֵ</returns>
        public double GetItemDouble(int nRecordNum, string sFieldName)
        {
            return Convert.ToDouble(GetItemString(nRecordNum, sFieldName));
        }

        /// <summary>
        /// �õ���һ����¼���ֶ��ַ���ֵ
        /// </summary>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ��ַ���ֵ</returns>
        public string GetAStringValue(int nFieldSeq)
        {
            return GetItemString(1, nFieldSeq);
        }

        /// <summary>
        /// �õ���һ����¼���ֶ��ַ���ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ��ַ���ֵ</returns>
        public string GetAStringValue(string sFieldName)
        {
            return GetItemString(1, sFieldName);
        }

        /// <summary>
        /// �õ���һ����¼���ֶ�����ֵ
        /// </summary>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ�����ֵ</returns>
        public int GetAIntegerValue(int nFieldSeq)
        {
            return GetItemInteger(1, nFieldSeq);
        }

        /// <summary>
        /// �õ���һ����¼���ֶ�����ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ�����ֵ</returns>
        public int GetAIntegerValue(string sFieldName)
        {
            return GetItemInteger(1, sFieldName);
        }

        /// <summary>
        /// �õ���һ����¼���ֶ�˫����ֵ
        /// </summary>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <returns>�ֶ�˫����ֵ</returns>
        public double GetADoubleValue(int nFieldSeq)
        {
            return GetItemDouble(1, nFieldSeq);
        }

        /// <summary>
        /// �õ���һ����¼���ֶ�˫����ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns>�ֶ�˫����ֵ</returns>
        public double GetADoubleValue(string sFieldName)
        {
            return GetItemDouble(1, sFieldName);
        }
        #endregion ��ȡ����

        #region ��������
        /// <summary>
        /// ��ָ����¼��ţ��ֶ�����������ֶ�ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <param name="sItemValue">�ֶ�ֵ</param>
        /// <remarks>
        /// ��ָ����¼��ţ��ֶ�����������ֶ�ֵ������ֶ����Խ�磬��
        /// �׳����⡣�����¼�ų�����¼���������Զ����Ӽ�¼�������ָ
        /// ���ļ�¼�Ŵ���(��¼����+1)�����׳����⡣
        /// </remarks>
        public void SetItemString(int nRecordNum, int nFieldSeq, string sItemValue)
        {
            if (nFieldSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::SetItemString() : "
                        + "field_seq out of range - " + nFieldSeq.ToString());

            if (nRecordNum > GetRecordCount() + 1)
                throw new BIPCallException("BIPDataSet::SetItemString() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            if (nRecordNum == GetRecordCount() + 1)
                _bipRecordList.AddRecord();

            _bipRecordList.SetItemString(nRecordNum, nFieldSeq, sItemValue);
        }

        /// <summary>
        /// ��ָ����¼��ţ��ֶ����������ֶ�ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="sItemValue">�ֶ�ֵ</param>
        public void SetItemString(int nRecordNum, string sFieldName, string sItemValue)
        {
            SetItemString(nRecordNum, GetFieldSeq(sFieldName), sItemValue);
        }

        /// <summary>
        /// ���ֶ�������õ�һ����¼���ֶ�ֵ
        /// </summary>
        /// <param name="nFieldSeq">�ֶ����</param>
        /// <param name="sItemValue">�ֶ�ֵ</param>
        /// <remarks>
        /// ����ֶ����Խ�磬�������ֶΡ��÷�����������һ����¼�������
        /// �����¼������һ����������֮�������¼������һ�������׳����⡣
        /// </remarks>
        public void SetAStringValue(int nFieldSeq, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("BIPDataSet.SetAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (nFieldSeq >= GetFieldCount())
                AddFields(nFieldSeq + 1 - GetFieldCount());

            SetItemString(1, nFieldSeq, sItemValue);
        }

        /// <summary>
        /// ���ֶ������õ�һ����¼���ֶ�ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="sItemValue">�ֶ�ֵ</param>
        /// <remarks>����ֶ��������ڣ�������һ��Ϊָ���ֶ������ֶΡ�</remarks>
        public void SetAStringValue(string sFieldName, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("TCBIPDataSet::SetAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (_bipFieldList.LocateFieldName(sFieldName) == -1)
                AddField(sFieldName);

            SetItemString(1, sFieldName, sItemValue);
        }

        /// <summary>
        /// �����ֶ�ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="sItemValue">�ֶ�ֵ</param>
        /// ˵�� : ���������һ���ֶΣ�����¼���ֶε�ֵ������ֶ�������
        /// �Ѿ����ڵ����أ����׳����⡣�÷�����������һ����¼�������
        /// �����¼������һ����������֮�������¼������һ�������׳����⡣
        public void AddAStringValue(string sFieldName, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("TCBIPDataSet::AddAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (_bipFieldList.LocateFieldName(sFieldName) != -1)
                throw new BIPCallException("TCBIPDataSet::AddAStringValue() : "
                        + "field_name already exists.");

            AddField(sFieldName);
            SetItemString(1, sFieldName, sItemValue);
        }

        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        void DeleteRecord(int nRecordNum)
        {
            if (nRecordNum < 1 || nRecordNum > GetRecordCount())
                throw new BIPCallException("TCBIPDataSet::DeleteRecord() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            _bipRecordList.RemoveAt(nRecordNum - 1);
        }
        #endregion ��������

        #region �ṹ��������ʾ
        /// <summary>
        /// �õ���ʾ�ṹ���ַ���
        /// </summary>
        /// <returns>��ʾ�ṹ���ַ���</returns>
        public string GetDisplayStruct()
        {
            string sRet = "";
            string sLine;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                sLine = GetFieldName(i).PadRight(16) + " ";
                sLine += (char)GetFieldType(i) + "    ";
                sLine += GetFieldLength(i).ToString().PadRight(6) + " ";
                sLine += GetFieldPrecision(i).ToString().PadRight(6);

                sRet += sLine;
            }

            return sRet;
        }

        /// <summary>
        /// ��ȡ��ʾ���ı�����
        /// </summary>
        /// <returns></returns>
        public string GetDisplayContentText()
        {
            string sContent = "";
            string sLine;

            //======== 0. ��ʾ��ʶ ============
            sLine = "<<DATASET>> NAME=" + DataSetName;
            sLine += "   SEQ=" + _dataSetSeq.ToString();
            sContent += sLine + "\r\n";

            //======== 1. ��ʾ�ֶ� ==========
            int i;
            int nFieldSeq;

            sLine = "";
            for (i = 0; i < GetFieldCount(); i++)
            {
                if (i != 0)
                    sLine += "\t";
                sLine += GetFieldName(i);
            }
            sContent += sLine + "\r\n";

            //========= 2. ��ʾ�ָ� =========
            sLine = StringUtil.RepeatChar('-', 70);
            sContent += sLine + "\r\n";

            //======== 3. ��ʾ���� =========
            for (i = 1; i <= GetRecordCount(); i++)
            {
                sLine = "";
                for (nFieldSeq = 0; nFieldSeq < GetFieldCount(); nFieldSeq++)
                {
                    if (nFieldSeq != 0)
                        sLine += "\t";
                    sLine += GetItemString(i, nFieldSeq);
                }
                sContent += sLine + "\r\n";
            }

            return sContent;
        }
        #endregion �ṹ��������ʾ

        #region ��������
        /// <summary>
        /// ���ֽ������ж�ȡDataSet
        /// </summary>
        /// <param name="bsDataSet">�ֽ�����</param>
        /// <param name="nPos">��ʼλ��</param>
        public void ReadFrom(byte[] bsDataSet, ref int nPos)
        {
            Clear();

            //========== 1. ���ݿ��ʶ	N(1)	�̶�Ϊ"BE" ==========
            byte cDataSetHeaderID;
            cDataSetHeaderID = BIPUtil.RXNByte(bsDataSet, ref nPos);
            if (cDataSetHeaderID != 0xBE)
                throw new BIPFormatException("TCBIPDataSet::ReadFrom() : "
                        + "DataSetHeaderID Error.");

            //======== 2. ���ݿ鳤��	N(4)	============
            int nPureDataSetLength = BIPUtil.RXNLongNumber(bsDataSet, ref nPos);

            if (bsDataSet.Length < nPureDataSetLength + 1 + 4)
                throw new BIPFormatException("TCBIPDataSet::ReadFrom() : "
                        + "data_set_length exceed - " + nPureDataSetLength.ToString());

            //======== 3. ���������� ==============
            byte[] bsPureData = BIPUtil.RXNByLength(bsDataSet, ref nPos, nPureDataSetLength);

            RXNUnpackData(bsPureData);
        }

        /// <summary>
        /// ��ȡȥ��ͷβ��ǵ��ַ�����DataSet
        /// </summary>
        /// <param name="bsPureData">ȥ��ͷβ��ǵ��ַ���</param>
        private void RXNUnpackData(byte[] bsPureData)
        {
            int nPos = 0;

            //========= 1. ���ݿ����	N(1) ===========
            _dataSetSeq = BIPUtil.RXNByteNumber(bsPureData, ref nPos);

            //======= 2. ���ݿ�����	X(16) ===========
            _dataSetName = BIPUtil.RXNStringByLength(bsPureData, ref nPos, 16);
            _dataSetName = _dataSetName.TrimEnd();

            //======= 3. �ֶ���	N(2) ==========
            int nFieldCount = BIPUtil.RXNShortNumber(bsPureData, ref nPos);

            //======= 4. �ֶ�������	N(4) ===========
            int nFieldSectionLength = BIPUtil.RXNLongNumber(bsPureData, ref nPos);

            //========= 5. ��¼��	N(4) ==========
            int nRecordCount = BIPUtil.RXNLongNumber(bsPureData, ref nPos);

            //======= 6. ��¼������	N(4) =========
            int nRecordSectionLength = BIPUtil.RXNLongNumber(bsPureData, ref nPos);
            if (nPos + nFieldSectionLength + nRecordSectionLength
                    != bsPureData.Length)
                throw new BIPFormatException("TCBIPDataSet::RXNUnpackData() : "
                        + "two section length not match - " + nFieldSectionLength.ToString()
                        + " - " + nRecordSectionLength.ToString());

            //=========== 7. �ֶ�������¼�� ==========
            byte[] bsFieldSectionData
                    = BIPUtil.RXNByLength(bsPureData, ref nPos, nFieldSectionLength);
            byte[] bsRecordSectionData
                    = BIPUtil.RXNByLength(bsPureData, ref nPos, nRecordSectionLength);

            RXNFieldSection(bsFieldSectionData, nFieldCount);
            RXNRecordSection(bsRecordSectionData, nRecordCount);
        }

        /// <summary>
        /// ��ȡ�ֶ���
        /// </summary>
        /// <param name="bsFieldSectionData">�ֶ���</param>
        /// <param name="nFieldCount">�ֶ�����</param>
        private void RXNFieldSection(byte[] bsFieldSectionData, int nFieldCount)
        {
            int nPos = 0;
            long nFieldNum;
            string sFieldName;

            for (int i = 0; i < nFieldCount; i++)
            {
                //======== 1. �ֶ���� N(2) =========
                nFieldNum = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                if (nFieldNum != i + 1)
                    throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                            + "Field No not match.");

                //======= 2. �ֶ��� STR ==========
                sFieldName = BIPUtil.RXNString(bsFieldSectionData, ref nPos);

                //======== 3. �ֶ����� X(1) =======
                BIPFieldType fieldType
                        = (BIPFieldType)(BIPUtil.RXNByte(bsFieldSectionData, ref nPos));

                if (fieldType != BIPFieldType.Number && fieldType != BIPFieldType.Char)
                    throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                            + "unknow field_type");

                //======= 4. �ֶζ��� Z(1) ==========
                int nFieldLength;
                int nFieldPrecision;

                int nFieldProperty = BIPUtil.RXNByteNumber(bsFieldSectionData, ref nPos);

                if (nFieldProperty == 0)
                {
                    nFieldLength = 0;
                    nFieldPrecision = 0;
                }
                else
                {
                    if (nFieldProperty != 1)
                        throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                                + "Unknow field_property.");

                    //========= 5. �ֶγ��ȣ�Option��	N(4) =========
                    if (fieldType == BIPFieldType.Char)
                    {
                        nFieldLength = BIPUtil.RXNLongNumber(bsFieldSectionData, ref nPos);
                        nFieldPrecision = 0;
                    }
                    else
                    {
                        nFieldLength = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                        nFieldPrecision = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                    }
                }

                AddField(sFieldName, fieldType, nFieldLength, nFieldPrecision);
            }
        }

        /// <summary>
        /// ��ȡ��¼��
        /// </summary>
        /// <param name="bsRecordSectionData">��¼��</param>
        /// <param name="nRecordCount">��¼����</param>
        private void RXNRecordSection(byte[] bsRecordSectionData, int nRecordCount)
        {
            int nPos = 0;

            for (int i = 1; i <= nRecordCount; i++)
            {
                RXNRecordSection__OneRecord(bsRecordSectionData, ref nPos, i);
            }

            if (nPos != bsRecordSectionData.Length)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection() : "
                        + "unexpected data left");
        }

        /// <summary>
        /// ��ȡ��¼����һ����¼
        /// </summary>
        /// <param name="bsRecordSectionData">��¼��</param>
        /// <param name="nPos">��ǰλ��</param>
        /// <param name="nRecordNum">��¼���</param>
        private void RXNRecordSection__OneRecord(byte[] bsRecordSectionData,
                ref int nPos, int nRecordNum)
        {
            //======== 1. ��¼��� ========
            int nFetchedRecordNum = BIPUtil.RXNLongNumber(bsRecordSectionData, ref nPos);
            if (nFetchedRecordNum != nRecordNum)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection__OneRecord() : "
                        + "record_num mismatched.");

            //====== 2. ��¼���� ===========
            long nRecordLength;
            long nValueBeginPos;

            nRecordLength = BIPUtil.RXNLongNumber(bsRecordSectionData, ref nPos);
            nValueBeginPos = nPos;

            //========= 3. �ֶ����� ========
            string sValue;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                sValue = BIPUtil.RXNString(bsRecordSectionData, ref nPos);

                SetItemString(nRecordNum, i, sValue);
            }

            if (nValueBeginPos + nRecordLength != nPos)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection__OneRecord() : "
                        + "record_length is not correct.");
        }
        #endregion ��������

        /// <summary>
        /// �õ����ݼ����ֽ�����
        /// </summary>
        /// <returns>���ݼ����ֽ�����</returns>
        public byte[] ToBytes()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            byte[] bsData = WXNData();

            ////========== 1. ���ݿ��ʶ	N(1)	�̶�Ϊ"BE" ==========
            bb.AddByte(0xBE);

            //======== 2. ���ݿ鳤��	N(4)	============
            bb.AddLongNumber(bsData.Length);

            //======== 3. ���� ===========
            bb.Add(bsData);

            return bb.ToBytes();
        }

        /// <summary>
        /// �õ����ݼ��е���������
        /// </summary>
        /// <returns>���ݼ����ݵ��ֽ�����</returns>
        private byte[] WXNData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //========= 1. ���ݿ����	N(1) ===========
            bb.AddByteNumber(_dataSetSeq);

            //======= 2. ���ݿ�����	X(16) ===========
            if (_dataSetName.Length > 16)
                throw new BIPCallException("TCBIPDataSet::WXNDataString() : "
                        + "DataSetName is too long - " + _dataSetName);
            bb.AddPureString(_dataSetName.PadRight(16));

            //======= 3. �ֶ���	N(2) ==========
            bb.AddShortNumber(GetFieldCount());

            //======= 4. �ֶ�������	N(4) ===========
            byte[] bsFieldData = WXNFieldData();
            bb.AddLongNumber(bsFieldData.Length);

            //========= 5. ��¼��	N(4) ==========
            bb.AddLongNumber(GetRecordCount());

            //======= 6. ��¼������	N(4) =========
            byte[] bsRecordData = WXNRecordData();
            bb.AddLongNumber(bsRecordData.Length);

            //======= 7. �ֶ��� ==========
            bb.Add(bsFieldData);

            //======= 8. ��¼�� ========
            bb.Add(bsRecordData);

            return bb.ToBytes();
        }

        /// <summary>
        /// �õ��ֶ�������
        /// </summary>
        /// <returns>�ֶ�������</returns>
        private byte[] WXNFieldData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();
            int nFieldLength;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                //======== 1. �ֶ����	N(2) ========
                bb.AddShortNumber(i + 1);

                //======= 2. �ֶ��� STR ==========
                bb.AddString(GetFieldName(i));

                //======== 3. �ֶ����� X(1) =======
                bb.AddByte((byte)GetFieldType(i));

                //======= 4. �ֶζ��� Z(1) ==========
                nFieldLength = GetFieldLength(i);
                if (nFieldLength == 0)
                {
                    bb.AddByteNumber(0);
                }
                else
                {
                    bb.AddByteNumber(1);

                    //========= 5. �ֶγ��ȣ�Option��	N(4) =========
                    if (GetFieldType(i) == BIPFieldType.Number)
                    {
                        bb.AddShortNumber(nFieldLength);
                        bb.AddShortNumber(GetFieldPrecision(i));
                    }
                    else
                        bb.AddLongNumber(nFieldLength);
                }
            } // end of for (i = 0; i < GetFieldCount(); i++)

            return bb.ToBytes();
        }

        /// <summary>
        /// �õ���¼������
        /// </summary>
        /// <returns>��¼������</returns>
        private byte[] WXNRecordData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            for (int i = 1; i <= GetRecordCount(); i++)
                bb.Add(WXNRecordData_OneRecord(i));

            return bb.ToBytes();
        }

        /// <summary>
        /// �õ�һ����¼��BIP���ݱ�ʾ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <returns>һ����¼��BIP���ݱ�ʾ</returns>
        private byte[] WXNRecordData_OneRecord(int nRecordNum)
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //======== 1. ��¼���	N(4) ==========
            bb.AddLongNumber(nRecordNum);

            //========= 2. ��¼����	N(4) =========
            int nTotalLength = 0;
            for (int i = 0; i < GetFieldCount(); i++)
            {
                string sItem = GetItemString(nRecordNum, i);
                nTotalLength += BIPUtil.BIPStringBytes(sItem).Length;
            }
            bb.AddLongNumber(nTotalLength);

            //======= 3. �ֶ�1����	STR ==========
            for (int i = 0; i < GetFieldCount(); i++)
            {
                string sItem = GetItemString(nRecordNum, i);
                bb.AddString(sItem);
            }

            return bb.ToBytes();
        }
    }
}
