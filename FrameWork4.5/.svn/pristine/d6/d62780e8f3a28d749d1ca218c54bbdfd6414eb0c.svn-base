using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Common.WordProcess.XQPatternTree
{
    /// <summary>
    /// 基于词的定位树
    /// </summary>
    public class PatternTree
    {
        private PatternNode _rootNode = new PatternNode();

        static string _CurrentAddingPatternString;
        /// <summary>
        /// 当前增加的PatternString
        /// </summary>
        public static string CurrentAddingPatternString
        {
            get { return PatternTree._CurrentAddingPatternString; }
            set { PatternTree._CurrentAddingPatternString = value; }
        }

        private static SortedList _matchedPatterns = new SortedList();
        /// <summary>
        /// 匹配的结果
        /// </summary>
        internal static SortedList MatchedPatterns
        {
            get { return PatternTree._matchedPatterns; }
            set { PatternTree._matchedPatterns = value; }
        }

        /// <summary>
        /// 增加待定位的Pattern
        /// </summary>
        /// <param name="sPatternString">Pattern串</param>
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
        /// 字符串是否能够匹配
        /// </summary>
        /// <param name="sWantMatchString">待匹配串</param>
        /// <returns>是否匹配</returns>
        public bool IsMatched(string sWantMatchString)
        {
            _matchedPatterns.Clear();

            return _rootNode.IsMatched(sWantMatchString);
        }

        /// <summary>
        /// 得到匹配的Pattern串列表
        /// </summary>
        /// <returns>匹配的Pattern串列表</returns>
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
    /// 测试类
    /// </summary>
    public class TEST__PatternNode__Func
    {
        PatternTree _patternTree = new PatternTree();

        /// <summary>
        /// 建树
        /// </summary>
        public void Build()
        {
            _patternTree.AddPatternString("发轮功");
            _patternTree.AddPatternString("江泽民");
            _patternTree.AddPatternString("法轮功");
            _patternTree.AddPatternString("法*功");
            _patternTree.AddPatternString("法lun功");
            _patternTree.AddPatternString("fa轮");
            _patternTree.AddPatternString("法十轮十");
            _patternTree.AddPatternString("法×功");
            _patternTree.AddPatternString("大纪元");
            _patternTree.AddPatternString("宏法");
            _patternTree.AddPatternString("洪传");
            _patternTree.AddPatternString("大法");
            _patternTree.AddPatternString("法轮");
            _patternTree.AddPatternString("明慧");
            _patternTree.AddPatternString("洪志");
            _patternTree.AddPatternString("李宏志");
            _patternTree.AddPatternString("自焚");
            _patternTree.AddPatternString("总书记");
            _patternTree.AddPatternString("总理");
            _patternTree.AddPatternString("主席");
            _patternTree.AddPatternString("毛泽东");
            _patternTree.AddPatternString("毛老头");
            _patternTree.AddPatternString("周恩来");
            _patternTree.AddPatternString("邓小平");
            _patternTree.AddPatternString("邓矮子");
            _patternTree.AddPatternString("邓小闲");
            _patternTree.AddPatternString("朱德");
            _patternTree.AddPatternString("叶群");
            _patternTree.AddPatternString("林彪");
            _patternTree.AddPatternString("江青");
            _patternTree.AddPatternString("四人帮");
            _patternTree.AddPatternString("王张江姚");
            _patternTree.AddPatternString("王洪文");
            _patternTree.AddPatternString("张春桥");
            _patternTree.AddPatternString("姚文元");
            _patternTree.AddPatternString("刘少奇");
            _patternTree.AddPatternString("江贼");
            _patternTree.AddPatternString("泽民");
            _patternTree.AddPatternString("江泽明");
            _patternTree.AddPatternString("江折民");
            _patternTree.AddPatternString("宋祖英");
            _patternTree.AddPatternString("赖昌星");
            _patternTree.AddPatternString("杨钰莹");
            _patternTree.AddPatternString("江×民");
            _patternTree.AddPatternString("国母");
            _patternTree.AddPatternString("华国锋");
            _patternTree.AddPatternString("赵紫阳");
            _patternTree.AddPatternString("胡启立");
            _patternTree.AddPatternString("胡耀邦");
            _patternTree.AddPatternString("李鹏");
            _patternTree.AddPatternString("杨尚昆");
            _patternTree.AddPatternString("胡锦涛");
            _patternTree.AddPatternString("温家宝");
            _patternTree.AddPatternString("曾庆红");
            _patternTree.AddPatternString("李长春");
            _patternTree.AddPatternString("贾庆林");
            _patternTree.AddPatternString("吴邦国");
            _patternTree.AddPatternString("罗干");
            _patternTree.AddPatternString("黄菊");
            _patternTree.AddPatternString("吴官正");
            _patternTree.AddPatternString("陈希同");
            _patternTree.AddPatternString("陈小同");
            _patternTree.AddPatternString("共产党");
            _patternTree.AddPatternString("文革");
            _patternTree.AddPatternString("文化大革命");
            _patternTree.AddPatternString("黑五类");
            _patternTree.AddPatternString("工贼");
            _patternTree.AddPatternString("大跃进");
            _patternTree.AddPatternString("人民公社");
            _patternTree.AddPatternString("人权");
            _patternTree.AddPatternString("学潮");
            _patternTree.AddPatternString("89学潮");
            _patternTree.AddPatternString("六四");
            _patternTree.AddPatternString("暴乱");
            _patternTree.AddPatternString("64事件");
            _patternTree.AddPatternString("工自联");
            _patternTree.AddPatternString("高自联");
            _patternTree.AddPatternString("动乱");
            _patternTree.AddPatternString("王丹");
            _patternTree.AddPatternString("吾尔开希");
            _patternTree.AddPatternString("柴玲");
            _patternTree.AddPatternString("方励之");
            _patternTree.AddPatternString("鲍彤");
            _patternTree.AddPatternString("朱琳");
            _patternTree.AddPatternString("河殇");
            _patternTree.AddPatternString("赤匪");
            _patternTree.AddPatternString("赤化");
            _patternTree.AddPatternString("政府");
            _patternTree.AddPatternString("共党");
            _patternTree.AddPatternString("共产");
            _patternTree.AddPatternString("共匪");
            _patternTree.AddPatternString("中共");
            _patternTree.AddPatternString("共狗");
            _patternTree.AddPatternString("反共");
            _patternTree.AddPatternString("反攻");
            _patternTree.AddPatternString("十六大");
            _patternTree.AddPatternString("达赖");
            _patternTree.AddPatternString("回民暴动");
            _patternTree.AddPatternString("西藏独立");
            _patternTree.AddPatternString("台独");
            _patternTree.AddPatternString("藏独");
            _patternTree.AddPatternString("台湾独立");
            _patternTree.AddPatternString("国民党");
            _patternTree.AddPatternString("民进党");
            _patternTree.AddPatternString("蒋介石");
            _patternTree.AddPatternString("蒋光头");
            _patternTree.AddPatternString("周天法");
            _patternTree.AddPatternString("射精");
            _patternTree.AddPatternString("精液");
            _patternTree.AddPatternString("精子");
            _patternTree.AddPatternString("精虫");
            _patternTree.AddPatternString("包皮");
            _patternTree.AddPatternString("龟头");
            _patternTree.AddPatternString("奶子");
            _patternTree.AddPatternString("奶头");
            _patternTree.AddPatternString("乳头");
            _patternTree.AddPatternString("乳房");
            _patternTree.AddPatternString("睾丸");
            _patternTree.AddPatternString("吹箫");
            _patternTree.AddPatternString("交尾");
            _patternTree.AddPatternString("口交");
            _patternTree.AddPatternString("肛交");
            _patternTree.AddPatternString("肛门");
            _patternTree.AddPatternString("屁眼");
            _patternTree.AddPatternString("阴道");
            _patternTree.AddPatternString("阴蒂");
            _patternTree.AddPatternString("阴唇");
            _patternTree.AddPatternString("阴毛");
            _patternTree.AddPatternString("B毛");
            _patternTree.AddPatternString("B吊");
            _patternTree.AddPatternString("吊B");
            _patternTree.AddPatternString("吊毛");
            _patternTree.AddPatternString("阴茎");
            _patternTree.AddPatternString("阴囊");
            _patternTree.AddPatternString("阳具");
            _patternTree.AddPatternString("阴部");
            _patternTree.AddPatternString("会阴");
            _patternTree.AddPatternString("肉棒");
            _patternTree.AddPatternString("肉棍");
            _patternTree.AddPatternString("肉洞");
            _patternTree.AddPatternString("屄");
            _patternTree.AddPatternString("胔");
            _patternTree.AddPatternString("肏");
            _patternTree.AddPatternString("屌");
            _patternTree.AddPatternString("强奸");
            _patternTree.AddPatternString("奸污");
            _patternTree.AddPatternString("鸡奸");
            _patternTree.AddPatternString("轮奸");
            _patternTree.AddPatternString("顺奸");
            _patternTree.AddPatternString("fuck");
            _patternTree.AddPatternString("fucking");
            _patternTree.AddPatternString("趐");
            _patternTree.AddPatternString("骚逼");
            _patternTree.AddPatternString("瘙逼");
            _patternTree.AddPatternString("尻");
            _patternTree.AddPatternString("打炮");
            _patternTree.AddPatternString("打一炮");
            _patternTree.AddPatternString("十八摸");
            _patternTree.AddPatternString("搞逼");
            _patternTree.AddPatternString("搞B");
            _patternTree.AddPatternString("做爱");
            _patternTree.AddPatternString("作爱");
            _patternTree.AddPatternString("做过爱");
            _patternTree.AddPatternString("作过爱");
            _patternTree.AddPatternString("做了爱");
            _patternTree.AddPatternString("作了爱");
            _patternTree.AddPatternString("婊");
            _patternTree.AddPatternString("表子");
            _patternTree.AddPatternString("卖淫");
            _patternTree.AddPatternString("卖逼");
            _patternTree.AddPatternString("卖批");
            _patternTree.AddPatternString("卖B");
            _patternTree.AddPatternString("戳你");
            _patternTree.AddPatternString("叉你");
            _patternTree.AddPatternString("叉死");
            _patternTree.AddPatternString("干死");
            _patternTree.AddPatternString("插你");
            _patternTree.AddPatternString("插死");
            _patternTree.AddPatternString("麻屁");
            _patternTree.AddPatternString("泽东");
            _patternTree.AddPatternString("gpr");
            _patternTree.AddPatternString("gprs");
            _patternTree.AddPatternString("prs");
            _patternTree.AddPatternString("gprs15");
            _patternTree.AddPatternString("prs15");
            _patternTree.AddPatternString("prs20");
        }

        /// <summary>
        /// 得到匹配的结果串列表
        /// </summary>
        /// <param name="sWantMatchString">希望匹配的字符串</param>
        /// <returns>匹配结果</returns>
        public ArrayList GetMatchedPatterns(string sWantMatchString)
        {
            _patternTree.IsMatched(sWantMatchString);
            return _patternTree.GetMatchedPatterns();
        }
    }

}
