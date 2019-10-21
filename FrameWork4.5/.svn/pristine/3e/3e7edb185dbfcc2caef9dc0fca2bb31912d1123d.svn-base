using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Web
{
    #region 辅助类

    /// <summary>
    /// CodeTable处理过程的每一项
    /// </summary>
    class CodeTableHZTreeBuilder_TableItem
    {
        private string _code = "";
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _text = "";
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private int _indentLevel = 0;
        /// <summary>
        /// 缩进级别
        /// </summary>
        public int IndentLevel
        {
            get { return _indentLevel; }
            set { _indentLevel = value; }
        }

        private string _treeText = "";
        /// <summary>
        /// 带有制表符的文字
        /// </summary>
        public string TreeText
        {
            get { return _treeText; }
            set { _treeText = value; }
        }

        private bool _isEndMark = false;
        /// <summary>
        /// 是否结束标记
        /// </summary>
        public bool IsEndMark
        {
            get { return _isEndMark; }
            set { _isEndMark = value; }
        }

        /// <summary>
        /// 解析文本，并属放到文本及缩进级别的属性中
        /// </summary>
        /// <param name="sItemText">CodeTable文本</param>
        /// <param name="cIndentChar">缩进字符</param>
        public void ParseItemText(string sItemText, char cIndentChar)
        {
            //=========== 1. 计数缩进级别 ===========
            int nLevel = 0;

            for (int i = 0; i < sItemText.Length; i++)
            {
                if (sItemText[i] == cIndentChar)
                    nLevel++;
                else
                    break;
            }

            //========== 2. 赋值 ============
            this.IndentLevel = nLevel;
            this.Text = sItemText.Substring(nLevel);
        }
    }

    /// <summary>
    /// CodeTable过程处理项列表
    /// </summary>
    class CodeTableHZTreeBuilder_TableItemList : ArrayList
    {
        public CodeTableHZTreeBuilder_TableItem GetItem(int nIndex)
        {
            return (CodeTableHZTreeBuilder_TableItem)this[nIndex];
        }

        public void AddItem(CodeTableHZTreeBuilder_TableItem item)
        {
            this.Add(item);
        }

        /// <summary>
        /// 由CodeTable创建过程处理项
        /// </summary>
        /// <param name="ctSrc">CodeTable</param>
        /// <param name="cIndentChar">缩进字符</param>
        public void BuildFromCodeTable(CodeTableBase ctSrc, char cIndentChar)
        {
            this.Clear();

            for (int i = 0; i < ctSrc.Count; i++)
            {
                string sText = ctSrc.GetDes(i);

                CodeTableHZTreeBuilder_TableItem item = new CodeTableHZTreeBuilder_TableItem();
                item.ParseItemText(sText, cIndentChar);
                item.Code = ctSrc.GetCode(i);

                this.AddItem(item);
            }
        }

        /// <summary>
        /// 建立树状CodeTable
        /// </summary>
        public void TreeTextBuild()
        {
            for (int i = 0; i < this.Count; i++)
            {
                CodeTableHZTreeBuilder_TableItem item = GetItem(i);

                //=========== 1. 之前的全是“│” ============
                string sTreeText = "";

                if (item.IndentLevel > 1)
                {
                    //sTreeText += StringUtil.RepeatChar('│', item.IndentLevel - 1);

                    for (int nCurPos = 1; nCurPos < item.IndentLevel; nCurPos++)
                    {
                        if (IsBlankSep(i, nCurPos))
                            sTreeText += '　';
                        else
                            sTreeText += '│';
                    }
                }

                //========== 2. 临近的那一个为“├” =============
                if (item.IndentLevel >= 1)
                {
                    if (IsEndMark(i))
                    {
                        item.IsEndMark = true;
                        sTreeText += "└";
                    }
                    else
                        sTreeText += "├";
                }

                //============ 3. 加上文本 ==========
                sTreeText += item.Text;

                item.TreeText =sTreeText;
            }
        }

        /// <summary>
        /// 是否显示为结束符号
        /// </summary>
        /// <param name="nItemIndex">处理项下标</param>
        /// <returns>是否显示为结束符号</returns>
        private bool IsEndMark(int nItemIndex)
        {
            //========= 1. 得到本级的Level ========
            CodeTableHZTreeBuilder_TableItem item = GetItem(nItemIndex);
            int nThisLevel = item.IndentLevel;

            //========= 2. 往下找，如果在遇到更高级Level或结束前，未找到同级节点，就是end节点 ========
            for (int i = nItemIndex + 1; i < this.Count; i++)
            {
                int nCurrentLevel = GetItem(i).IndentLevel;
                if (nCurrentLevel < nThisLevel)
                    return true;

                if (nCurrentLevel == nThisLevel)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 是否显示为空的占位符
        /// </summary>
        /// <param name="nItemIndex">处理项下标</param>
        /// <param name="nIndentPos">当前的占位位置</param>
        /// <returns>是否显示为空的占位符</returns>
        private bool IsBlankSep(int nItemIndex, int nIndentPos)
        {
            //=========== 1. 如当前节点往回看 ===========
            for (int i = nItemIndex - 1; i >= 0; i--)
            {
                CodeTableHZTreeBuilder_TableItem item = GetItem(i);

                //======= 2. 如果前面的项为结束符，则显示空的占位符，否则显示为竖线 ==========
                if (item.IndentLevel <= nIndentPos)
                {
                    if (item.IsEndMark)
                        return true;
                    else
                        return false;
                }
            }

            return false;
        }
    }
    #endregion

    /// <summary>
    /// 用汉字的表格字符来“勾勒”出CodeTable的表格
    /// </summary>
    /// <remarks>
    /// 【注意】
    /// 【在界面显示时，需要为汉字宽度一致的汉字字体，“宋体”字较佳】
    /// </remarks>
    public class CodeTableHZTreeBuilder
    {
        private char _indentChar = '+';
        /// <summary>
        /// 缩进的代表字符
        /// </summary>
        public char IndentChar
        {
            get { return _indentChar; }
            set { _indentChar = value; }
        }

        /// <summary>
        /// 将CodeTable的缩进字符置换为汉字的表格字符
        /// </summary>
        /// <param name="ctSrc">CodeTable</param>
        public void ReplaceCodeTable(CodeTableBase ctSrc)
        {
            //========= 1. 根据CodeTable，建立itemList ===========
            CodeTableHZTreeBuilder_TableItemList tableItemList = new CodeTableHZTreeBuilder_TableItemList();
            tableItemList.BuildFromCodeTable(ctSrc, this.IndentChar);

            //========= 2. 建立树状显示 ===========
            tableItemList.TreeTextBuild();

            //========= 3. 置换CodeTable ==========
            ctSrc.Clear();

            for (int i = 0; i < tableItemList.Count; i++)
            {
                CodeTableHZTreeBuilder_TableItem item = tableItemList.GetItem(i);
                ctSrc.AddItem(item.Code, item.TreeText);
            }
        }
    }
}

/*
 * 

【没有表格线的缩进列表】
139邮箱
MM平台
  MM业务
  应用宝（腾讯）
    应用宝（版本1.0）
    应用宝（版本2.0）
  安卓市场
移动微博
  私信
  我的微博

【表格线】
┌┬┐
├┼┤
└┴┘

【一层缩进】
139邮箱
MM平台
├MM业务
├应用宝（腾讯）
│├应用宝（版本1.0）
│└应用宝（版本2.0）
└安卓市场
移动微博
├私信
└我的微博

【两层缩进】
终端拨测
├139邮箱
├MM平台
│├MM业务
│├应用宝（腾讯）
││├应用宝（版本1.0）
││└应用宝（版本2.0）
│└安卓市场
└移动微博
  ├私信
  └我的微博

【处理规则】
1. 最接近汉字的那个，为“├”或“└”(下一个的Level更高或结束，或再往下无平级节点）
2. 其余的，往上找，非“└”则为“│”否则为“　”

【复杂缩进场景】
终端拨测
├139邮箱
├MM平台
│├MM业务
│├应用宝（腾讯）
││├应用宝（版本1.0）
││└应用宝（版本2.0）
││  └应用宝（版本2.0+）
│└安卓市场
└移动微博
  ├私信
  └我的微博

【注意】
【在界面显示时，需要为汉字宽度一致的汉字字体，“宋体”字较佳】

 * * 
 */