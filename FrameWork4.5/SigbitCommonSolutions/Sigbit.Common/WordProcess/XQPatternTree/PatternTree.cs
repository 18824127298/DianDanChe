using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Common.WordProcess.XQPatternTree
{
    /// <summary>
    /// ���ڴʵĶ�λ��
    /// </summary>
    public class PatternTree
    {
        private PatternNode _rootNode = new PatternNode();

        static string _CurrentAddingPatternString;
        /// <summary>
        /// ��ǰ���ӵ�PatternString
        /// </summary>
        public static string CurrentAddingPatternString
        {
            get { return PatternTree._CurrentAddingPatternString; }
            set { PatternTree._CurrentAddingPatternString = value; }
        }

        private static SortedList _matchedPatterns = new SortedList();
        /// <summary>
        /// ƥ��Ľ��
        /// </summary>
        internal static SortedList MatchedPatterns
        {
            get { return PatternTree._matchedPatterns; }
            set { PatternTree._matchedPatterns = value; }
        }

        /// <summary>
        /// ���Ӵ���λ��Pattern
        /// </summary>
        /// <param name="sPatternString">Pattern��</param>
        public void AddPatternString(string sPatternString)
        {
            CurrentAddingPatternString = sPatternString;
            _rootNode.ChildNodes.AddPatternString(sPatternString);

            if (sPatternString[0] != '*')
            {
                CurrentAddingPatternString = sPatternString;
                _rootNode.ChildNodes.AddPatternString("*" + sPatternString);
            }
        }

        /// <summary>
        /// �ַ����Ƿ��ܹ�ƥ��
        /// </summary>
        /// <param name="sWantMatchString">��ƥ�䴮</param>
        /// <returns>�Ƿ�ƥ��</returns>
        public bool IsMatched(string sWantMatchString)
        {
            _matchedPatterns.Clear();

            return _rootNode.IsMatched(sWantMatchString);
        }

        /// <summary>
        /// �õ�ƥ���Pattern���б�
        /// </summary>
        /// <returns>ƥ���Pattern���б�</returns>
        public ArrayList GetMatchedPatterns()
        {
            ArrayList slRet = new ArrayList();

            for (int i = 0; i < _matchedPatterns.Count; i++)
            {
                string sThisPattern = (string)_matchedPatterns.GetByIndex(i);
                slRet.Add(sThisPattern);
            }

            return slRet;
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    public class TEST__PatternNode__Func
    {
        PatternTree _patternTree = new PatternTree();

        /// <summary>
        /// ����
        /// </summary>
        public void Build()
        {
            _patternTree.AddPatternString("���ֹ�");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("���ֹ�");
            _patternTree.AddPatternString("��*��");
            _patternTree.AddPatternString("��lun��");
            _patternTree.AddPatternString("fa��");
            _patternTree.AddPatternString("��ʮ��ʮ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("���Ԫ");
            _patternTree.AddPatternString("�귨");
            _patternTree.AddPatternString("�鴫");
            _patternTree.AddPatternString("��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��־");
            _patternTree.AddPatternString("���־");
            _patternTree.AddPatternString("�Է�");
            _patternTree.AddPatternString("�����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��ϯ");
            _patternTree.AddPatternString("ë��");
            _patternTree.AddPatternString("ë��ͷ");
            _patternTree.AddPatternString("�ܶ���");
            _patternTree.AddPatternString("��Сƽ");
            _patternTree.AddPatternString("�˰���");
            _patternTree.AddPatternString("��С��");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("ҶȺ");
            _patternTree.AddPatternString("�ֱ�");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("���˰�");
            _patternTree.AddPatternString("���Ž�Ҧ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("�Ŵ���");
            _patternTree.AddPatternString("Ҧ��Ԫ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����Ӣ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����Ө");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("��ĸ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("��ҫ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("�¼ұ�");
            _patternTree.AddPatternString("�����");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("�޸�");
            _patternTree.AddPatternString("�ƾ�");
            _patternTree.AddPatternString("�����");
            _patternTree.AddPatternString("��ϣͬ");
            _patternTree.AddPatternString("��Сͬ");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("�ĸ�");
            _patternTree.AddPatternString("�Ļ������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��Ծ��");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("��Ȩ");
            _patternTree.AddPatternString("ѧ��");
            _patternTree.AddPatternString("89ѧ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("64�¼�");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("�����ϣ");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����֮");
            _patternTree.AddPatternString("��ͮ");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("�໯");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("�й�");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("ʮ����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("���񱩶�");
            _patternTree.AddPatternString("���ض���");
            _patternTree.AddPatternString("̨��");
            _patternTree.AddPatternString("�ض�");
            _patternTree.AddPatternString("̨�����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("�����");
            _patternTree.AddPatternString("����ʯ");
            _patternTree.AddPatternString("����ͷ");
            _patternTree.AddPatternString("���취");
            _patternTree.AddPatternString("�侫");
            _patternTree.AddPatternString("��Һ");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��Ƥ");
            _patternTree.AddPatternString("��ͷ");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��ͷ");
            _patternTree.AddPatternString("��ͷ");
            _patternTree.AddPatternString("�鷿");
            _patternTree.AddPatternString("غ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��β");
            _patternTree.AddPatternString("�ڽ�");
            _patternTree.AddPatternString("�ؽ�");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("ƨ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��ë");
            _patternTree.AddPatternString("Bë");
            _patternTree.AddPatternString("B��");
            _patternTree.AddPatternString("��B");
            _patternTree.AddPatternString("��ë");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("�ⶴ");
            _patternTree.AddPatternString("��");
            _patternTree.AddPatternString("�h");
            _patternTree.AddPatternString("�H");
            _patternTree.AddPatternString("��");
            _patternTree.AddPatternString("ǿ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("�ּ�");
            _patternTree.AddPatternString("˳��");
            _patternTree.AddPatternString("fuck");
            _patternTree.AddPatternString("fucking");
            _patternTree.AddPatternString("�p");
            _patternTree.AddPatternString("ɧ��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��һ��");
            _patternTree.AddPatternString("ʮ����");
            _patternTree.AddPatternString("���");
            _patternTree.AddPatternString("��B");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("������");
            _patternTree.AddPatternString("���˰�");
            _patternTree.AddPatternString("���˰�");
            _patternTree.AddPatternString("�");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��B");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("����");
            _patternTree.AddPatternString("��ƨ");
            _patternTree.AddPatternString("��");
            _patternTree.AddPatternString("gpr");
            _patternTree.AddPatternString("gprs");
            _patternTree.AddPatternString("prs");
            _patternTree.AddPatternString("gprs15");
            _patternTree.AddPatternString("prs15");
            _patternTree.AddPatternString("prs20");
        }

        /// <summary>
        /// �õ�ƥ��Ľ�����б�
        /// </summary>
        /// <param name="sWantMatchString">ϣ��ƥ����ַ���</param>
        /// <returns>ƥ����</returns>
        public ArrayList GetMatchedPatterns(string sWantMatchString)
        {
            _patternTree.IsMatched(sWantMatchString);
            return _patternTree.GetMatchedPatterns();
        }
    }

}
