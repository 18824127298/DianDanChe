using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Common.WordProcess.XQPatternTree
{
    class PatternNodeList : Hashtable
    {
        public void AddNode(PatternNode node)
        {
            Add(node.NodeChar, node);
        }

        public PatternNode GetNode(char ch)
        {
            if (this[ch] == null)
                return null;
            else
                return (PatternNode)this[ch];
        }

        public void AddPatternString(string sString)
        {
            //========= 1. ������ַ������벻Ϊ�� ========
            Debug.Assert(sString.Length != 0);

            //======== 2. �õ���һ���ַ� ==========
            char chFirst = sString[0];

            //======== 3. �õ���һ���ַ���Ӧ�Ľڵ� =========
            PatternNode node = GetNode(chFirst);

            //========= 4. ���û������ڵ㣬�򴴽��ýڵ㣬����ӽ�ȥ ========
            if (node == null)
            {
                node = new PatternNode(sString);
                AddNode(node);
            }
            else
            {
                //========= 5. ����нڵ㣬�򽫸��ַ����������ڹ����Ӵ� ======
                node.BuildFromString(sString);
            }
        }
    }

    class PatternNode
    {
        private char _nodeChar;
        /// <summary>
        /// �ڵ��ַ�
        /// </summary>
        public char NodeChar
        {
            get { return _nodeChar; }
            set { _nodeChar = value; }
        }

        private PatternNodeList _childNodes = new PatternNodeList();
        /// <summary>
        /// �ӽڵ��б�
        /// </summary>
        public PatternNodeList ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }

        /// <summary>
        /// �ӽڵ������
        /// </summary>
        public int ChildCount
        {
            get
            {
                return ChildNodes.Count;
            }
        }

        private bool _isLeafNode = false;
        /// <summary>
        /// �Ƿ�Ҷ�ڵ�
        /// </summary>
        public bool IsLeafNode
        {
            get { return _isLeafNode; }
            set { _isLeafNode = value; }
        }

        private string _patternString = "";
        /// <summary>
        /// �����Ҷ�ڵ㣬���¼Pattern�ַ���
        /// </summary>
        public string PatternString
        {
            get { return _patternString; }
            set { _patternString = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sString">�ַ���</param>
        public PatternNode(string sString)
        {
            Debug.Assert(sString.Length > 0);

            char ch;
            ch = sString[0];
            _nodeChar = ch;

            BuildFromString(sString);
        }

        public PatternNode()
        {
            _nodeChar = '\0';
        }

        public void BuildFromString(string sString)
        {
            //========== 1. ȡ����һ���ַ�������ַ������ǽڵ���ַ� =======
            char ch = sString[0];
            Debug.Assert(ch == _nodeChar);

            //========== 2. �������Ϊ1����ΪҶ�ڵ� =========
            if (sString.Length == 1)
            {
                _patternString = PatternTree.CurrentAddingPatternString;
                _isLeafNode = true;
            }
            else
            {
                //======= 3. ���򣬹������� ==========
                string sTrimHeadString = sString.Substring(1);
                _childNodes.AddPatternString(sTrimHeadString);
            }
        }

        /// <summary>
        /// �жϽڵ��Ƿ��ܹ�ƥ���ַ���
        /// </summary>
        /// <param name="sWantMatchString">ϣ��ƥ����ַ���</param>
        /// <returns>�Ƿ�ƥ��</returns>
        /// <remarks>
        /// ����һ���ؼ��ּ�ʹƥ���ϣ������ĳ���������ڣ����Բ���ֱ��
        /// ����true����ˣ������޶���(HISTORY:20070508:oldix)
        /// </remarks>
        public bool IsMatched(string sWantMatchString)
        {
            //========= 1. �õ���һ���ַ� ========
            //========= ����HISTORY:20070508:oldix ==============
            bool bThisMatched = false;
            //============ end of ���� =================
            if (_isLeafNode)
            {
                if (PatternTree.MatchedPatterns.IndexOfKey(_patternString) == -1)
                    PatternTree.MatchedPatterns.Add(_patternString, _patternString);
                //========= ע�͵�HISTORY:20070508:oldix ==============
                //return true;
                //============ end of ע�͵� =================
                //========= ����HISTORY:20070508:oldix ==============
                bThisMatched = true;
                //============ end of ���� =================
            }

            if (sWantMatchString.Length == 0)
                return bThisMatched;

            char cFirstChar = sWantMatchString[0];
            string sTrimHeadString = sWantMatchString.Substring(1);

            PatternNode aNode, bNode, cNode, dNode;
            bool aMatched = false, bMatched = false, cMatched = false, dMatched = false;

            //======== 2. ������ڵ���"*"�ڵ� =========
            if (_nodeChar == '*')
            {
                //======= 2.1 �ҵ������ֿ�ͷ�Ľڵ� ========
                aNode = _childNodes.GetNode(cFirstChar);
                if (aNode == null)
                    aMatched = false;
                else
                    aMatched = aNode.IsMatched(sTrimHeadString);

                //========== 2.2 ���ñ��ڵ� =========
                bNode = this;
                bMatched = bNode.IsMatched(sTrimHeadString);
            }
            //======= 3. ���򣨱��ڵ��Ƿ�"*"�ڵ㣩======
            else
            {
                //========= 3.1 �ҵ������ֿ�ͷ�ŵĽڵ� =========
                cNode = _childNodes.GetNode(cFirstChar);
                if (cNode == null)
                    cMatched = false;
                else
                    cMatched = cNode.IsMatched(sTrimHeadString);

                //======= 3.2 �ҵ���"*"��ͷ�Ľڵ� =========
                dNode = _childNodes.GetNode('*');
                if (dNode == null)
                    dMatched = false;
                else
                    dMatched = dNode.IsMatched(sTrimHeadString);
            }

            //========= 4. �������յ�ƥ���� =========
            //========= ע�͵�HISTORY:20070508:oldix ==============
            //return aMatched || bMatched || cMatched || dMatched;
            //============ end of ע�͵� =================
            //========= ����HISTORY:20070508:oldix ==============
            return aMatched || bMatched || cMatched || dMatched || bThisMatched;
            //============ end of ���� =================
        }
    }

}
