using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess
{
    /// <summary>
    /// 生成性别的意图
    /// </summary>
    public enum HZGenPersonSexIntention
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male,
        /// <summary>
        /// 女性
        /// </summary>
        Female,
        /// <summary>
        /// 两者都有
        /// </summary>
        Both
    }

    /// <summary>
    /// 汉字的随机例程
    /// </summary>
    public class HZRandUtil
    {
        /// <summary>
        /// 随机生成人名
        /// </summary>
        /// <returns>生成的人名</returns>
        public static string NewPersonName()
        {
            string[] arrXing = new string[]
            {
                "赵", "钱", "孙", "李", "周",
                "吴", "郑", "王", "冯", "蒋",
                "沈", "韩", "杨", "朱", "秦", 
                "许", "何", "张", "曹", "金", 
                "陶", "谢", "章", "苏", "范", 
                "彭", "马", "方", "袁", "唐", 
                "罗", "余", "黄", "宋", "董", 
                "陈", "刘", "肖", "夏", "谢", 
                "徐", "卢", "陆", "梁", "程",
                "林", "杜", "胡", "郭", "邓"
                
            };
            string sXing = arrXing[RandUtil.NewNumber(arrXing.Length)];

            string[] arrMiddle = new string[]
            {
                "长", "栓", "大", "来", "狗",
                "守", "傻", "福", "屎", "二",
                "胖", "臭", "小", "明", "香", 
                "少", "华", "文", "春", "晓",
                "金", "海", "楚", "丹", "志",
                "子", "家", "冠", "惠", "伟",
                "世", "朝", "永", "婉", "思",
                "锦", "佳", "心", "雨", "诗",
                "舒", "嘉", "咏", "佩", "国",
                "婉", "松", "学", "曼", "红"
            };
            string sMiddle = arrMiddle[RandUtil.NewNumber(arrMiddle.Length)];

            string[] arrFinal = new string[]
            {
                "娟", "妮", "腿", "娣", "球",
                "坑", "年", "岁", "娃", "毛", 
                "剩", "姑", "英", "妹", "肥",
                "霞", "狗", "虎", "花", "凤",
                "定", "村", "蛋", "妞", "木",
                "翠", "爱", "财", "石", "美",
                "发", "丽", "慧", "浩", "石",
                "亮", "新", "琳", "茵", "梅",
                "聪", "娜", "峰", "宇", "涛",
                "敏", "威", "燕", "媚", "鸿",
                "斌", "城", "韵", "辉", "凯",
                "迪", "毅", "龙", "勇", "豪",
                "榕", "婷", "雪", "静", "雯",
                "莹", "玲", "虹", "杏", "瑾",
                "红", "杰", "胜", "瑜", "颖",
                "琪", "洁", "香", "耀", "瑞",
                "薇", "欣", "民", "超", "新",
                "强", "艳", "萍", "兰", "平",
                "贵", "富", "珍", "芬", "玉",
                "生", "冰", "成", "贞", "欢"
            };
            string sFinal = arrFinal[RandUtil.NewNumber(arrFinal.Length)];

            if (RandUtil.NewNumber(1, 100) <= 35)
                return sXing + sFinal;
            else
                return sXing + sMiddle + sFinal;
        }

        /// <summary>
        /// 随机生成人名，该函数生成的人名会好听些
        /// </summary>
        /// <param name="genIntention">生成的意图</param>
        /// <returns>生成的人名</returns>
        public static string NewPersonName(HZGenPersonSexIntention genIntention)
        {
            if (genIntention == HZGenPersonSexIntention.Both)
            {
                if (RandUtil.NewNumber(2) == 0)
                    genIntention = HZGenPersonSexIntention.Male;
                else
                    genIntention = HZGenPersonSexIntention.Female;
            }

            string[] arrXing = new string[]
            {
                "赵", "钱", "孙", "李", "周",
                "吴", "郑", "王", "冯", "蒋",
                "沈", "韩", "杨", "朱", "秦", 
                "许", "何", "张", "曹", "金", 
                "陶", "谢", "章", "苏", "范", 
                "彭", "马", "方", "袁", "唐", 
                "罗", "余", "黄", "宋", "董", 
                "陈", "刘", "肖", "夏", "谢", 
                "徐", "卢", "陆", "梁", "程",
                "林", "杜", "胡", "郭", "邓"
            };
            string sXing = arrXing[RandUtil.NewNumber(arrXing.Length)];

            string[] arrMiddleMale = new string[]
            {
                "长", 
                "福", "定",
                "小", "明", 
                "少", "华", "文", "晓",
                "金", "海", "楚", "丹", "志",
                "子", "家", "冠", "伟",
                "世", "朝", "永", 
                "佳",
                "国",
                "松", "学"
            };
            string[] arrMiddleFemale = new string[]
            {
                "华", "春", "晓",
                "海", "楚", "丹", 
                "惠", "翠", 
                "婉", "思",
                "锦", "佳", "心", "雨", "诗",
                "舒", "嘉", "咏", "佩", 
                "婉", "曼", "红"
            };
            string sMiddle = "";
            if (genIntention == HZGenPersonSexIntention.Male)
                sMiddle = arrMiddleMale[RandUtil.NewNumber(arrMiddleMale.Length)];
            else
                sMiddle = arrMiddleFemale[RandUtil.NewNumber(arrMiddleFemale.Length)];

            string[] arrFinalMale = new string[]
            {
                "虎",
                "发", "浩", "石",
                "亮", "新", 
                "聪", "峰", "宇", "涛",
                "敏", "威", "鸿",
                "斌", "城", "辉", "凯",
                "迪", "毅", "龙", "勇", "豪",
                "杰", "胜", "瑜", 
                "琪", "耀", "瑞",
                "欣", "民", "超", "新",
                "强", "平",
                "贵", "富", 
                "生", "成", "欢"
            };
            string[] arrFinalFemale = new string[]
            {
                "娟", "妮", 
                "英", "妹", 
                "霞", "花", "凤",
                "翠", "美",
                "丽", "慧", 
                "琳", "茵", "梅",
                "娜", 
                "敏", "燕", "媚", 
                "韵",
                "榕", "婷", "雪", "静", "雯",
                "莹", "玲", "虹", "杏", "瑾",
                "红", "瑜", "颖",
                "琪", "洁", "香", 
                "薇", "欣", 
                "艳", "萍", "兰",
                "珍", "芬", "玉",
                "冰", "贞"
            };
            string sFinal = "";
            if (genIntention == HZGenPersonSexIntention.Male)
                sFinal = arrFinalMale[RandUtil.NewNumber(arrFinalMale.Length)];
            else
                sFinal = arrFinalFemale[RandUtil.NewNumber(arrFinalFemale.Length)];

            if (RandUtil.NewNumber(1, 100) <= 35)
                return sXing + sFinal;
            else
                return sXing + sMiddle + sFinal;
        }
    }
}
