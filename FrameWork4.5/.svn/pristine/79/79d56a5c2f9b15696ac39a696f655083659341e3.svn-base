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
            //========= 1. 输入的字符串必须不为空 ========
            Debug.Assert(sString.Length != 0);

            //======== 2. 得到第一个字符 ==========
            char chFirst = sString[0];

            //======== 3. 得到第一个字符相应的节点 =========
            PatternNode node = GetNode(chFirst);

            //========= 4. 如果没有这个节点，则创建该节点，并添加进去 ========
            if (node == null)
            {
                node = new PatternNode(sString);
                AddNode(node);
            }
            else
            {
                //========= 5. 如果有节点，则将该字符串传入用于构造子串 ======
                node.BuildFromString(sString);
            }
        }
    }

    class PatternNode
    {
        private char _nodeChar;
        /// <summary>
        /// 节点字符
        /// </summary>
        public char NodeChar
        {
            get { return _nodeChar; }
            set { _nodeChar = value; }
        }

        private PatternNodeList _childNodes = new PatternNodeList();
        /// <summary>
        /// 子节点列表
        /// </summary>
        public PatternNodeList ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }

        /// <summary>
        /// 子节点的数量
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
        /// 是否叶节点
        /// </summary>
        public bool IsLeafNode
        {
            get { return _isLeafNode; }
            set { _isLeafNode = value; }
        }

        private string _patternString = "";
        /// <summary>
        /// 如果是叶节点，则记录Pattern字符串
        /// </summary>
        public string PatternString
        {
            get { return _patternString; }
            set { _patternString = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sString">字符串</param>
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
            //========== 1. 取出第一个字符，这个字符必须是节点的字符 =======
            char ch = sString[0];
            Debug.Assert(ch == _nodeChar);

            //========== 2. 如果长度为1，则为叶节点 =========
            if (sString.Length == 1)
            {
                _patternString = PatternTree.CurrentAddingPatternString;
                _isLeafNode = true;
            }
            else
            {
                //======= 3. 否则，构建子树 ==========
                string sTrimHeadString = sString.Substring(1);
                _childNodes.AddPatternString(sTrimHeadString);
            }
        }

        /// <summary>
        /// 判断节点是否能够匹配字符串
        /// </summary>
        /// <param name="sWantMatchString">希望匹配的字符串</param>
        /// <returns>是否匹配</returns>
        /// <remarks>
        /// 由于一个关键字即使匹配上，但它的超集还会存在，所以不能直接
        /// 返回true。因此，做了修订。(HISTORY:20070508:oldix)
        /// </remarks>
        public bool IsMatched(string sWantMatchString)
        {
            //========= 1. 得到第一个字符 ========
            //========= 增加HISTORY:20070508:oldix ==============
            bool bThisMatched = false;
            //============ end of 增加 =================
            if (_isLeafNode)
            {
                if (PatternTree.MatchedPatterns.IndexOfKey(_patternString) == -1)
                    PatternTree.MatchedPatterns.Add(_patternString, _patternString);
                //========= 注释掉HISTORY:20070508:oldix ==============
                //return true;
                //============ end of 注释掉 =================
                //========= 增加HISTORY:20070508:oldix ==============
                bThisMatched = true;
                //============ end of 增加 =================
            }

            if (sWantMatchString.Length == 0)
                return bThisMatched;

            char cFirstChar = sWantMatchString[0];
            string sTrimHeadString = sWantMatchString.Substring(1);

            PatternNode aNode, bNode, cNode, dNode;
            bool aMatched = false, bMatched = false, cMatched = false, dMatched = false;

            //======== 2. 如果本节点是"*"节点 =========
            if (_nodeChar == '*')
            {
                //======= 2.1 找到以首字开头的节点 ========
                aNode = _childNodes.GetNode(cFirstChar);
                if (aNode == null)
                    aMatched = false;
                else
                    aMatched = aNode.IsMatched(sTrimHeadString);

                //========== 2.2 利用本节点 =========
                bNode = this;
                bMatched = bNode.IsMatched(sTrimHeadString);
            }
            //======= 3. 否则（本节点是非"*"节点）======
            else
            {
                //========= 3.1 找到以首字开头号的节点 =========
                cNode = _childNodes.GetNode(cFirstChar);
                if (cNode == null)
                    cMatched = false;
                else
                    cMatched = cNode.IsMatched(sTrimHeadString);

                //======= 3.2 找到以"*"开头的节点 =========
                dNode = _childNodes.GetNode('*');
                if (dNode == null)
                    dMatched = false;
                else
                    dMatched = dNode.IsMatched(sTrimHeadString);
            }

            //========= 4. 返回最终的匹配结果 =========
            //========= 注释掉HISTORY:20070508:oldix ==============
            //return aMatched || bMatched || cMatched || dMatched;
            //============ end of 注释掉 =================
            //========= 增加HISTORY:20070508:oldix ==============
            return aMatched || bMatched || cMatched || dMatched || bThisMatched;
            //============ end of 增加 =================
        }
    }

}
