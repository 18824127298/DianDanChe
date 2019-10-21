using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// ��ʾ���ݰ��ĸ�ʽ
    /// </summary>
    public enum BIPPacketFormat
    {
        /// <summary>
        /// ����
        /// </summary>
        LongFormat = 'L',
        /// <summary>
        /// �̰�
        /// </summary>
        ShortFormat = 'S'
    }

    /// <summary>
    ///  ���ݰ�������
    /// </summary>
    public enum BIPPacketType
    {
        /// <summary>
        /// �����
        /// </summary>
        Request = 'R',        // �����
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        Answer = 'A',         // ��Ӧ��
        /// <summary>
        /// ֪ͨ��
        /// </summary>
        Inform = 'I',         // ֪ͨ��
        /// <summary>
        /// �ص���
        /// </summary>
        Callback = 'C'        // �ص���
    }

    /// <summary>
    /// ͬ����ʽ
    /// </summary>
    public enum BIPPacketSyncMeth
    {
        /// <summary>
        /// ���ص��÷���
        /// </summary>
        LocalCall = '1',      // ���ص��÷���
        /// <summary>
        /// ���͵�Ŀ���ַ����
        /// </summary>
        ArriveDest = '2',     // ���͵�Ŀ���ַ����
        /// <summary>
        /// Ŀ�����ȡ������
        /// </summary>
        DestFetched = '3',    // Ŀ�����ȡ������
        /// <summary>
        /// Ŀ�������ɷ��أ�ͬ����ʽ��
        /// </summary>
        DestDone = '4'        // Ŀ�������ɷ��أ�ͬ����ʽ��
    }

    /// <summary>
    /// ��ʾ���ݰ�����֯��ʽ��Ŀǰ����һ����ʽ��֮����ܿ�������XML��
    /// ANSI��
    /// </summary>
    public enum BIPPacketOrgFormat
    {
        /// <summary>
        /// Ŀǰ����İ���֯��ʽ
        /// </summary>
        Standard            // Ŀǰ����İ���֯��ʽ
    }

    /// <summary>
    /// ���ò�����������������
    /// </summary>
    public class BIPCallException : Exception
    {
        /// <summary>
        /// ���ò�����������������
        /// </summary>
        /// <param name="sMsg"></param>
        public BIPCallException(string sMsg)
            : base(sMsg)
        {
        }
    }

    /// <summary>
    /// ���ݰ���ʽ�����������
    /// </summary>
    public class BIPFormatException : Exception
    {
        /// <summary>
        /// ���ݰ���ʽ�����������
        /// </summary>
        /// <param name="sMsg"></param>
        public BIPFormatException(string sMsg)
            : base(sMsg)
        {
        }
    }
}
