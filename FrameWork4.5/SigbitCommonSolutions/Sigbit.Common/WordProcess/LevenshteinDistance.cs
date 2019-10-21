using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess
{
    /// <summary>
    /// 计算编辑距离
    /// </summary>
    /// <remarks>
    ///     编辑距离就是用来计算从原串(s)转换到目标串(t)所需要的最少的插
    /// 入，删除和替换的数目，在NLP中应用比较广泛，如一些评测方法中就用
    /// 到了(wer,mWer等)，同时也常用来计算你对原文本所作的改动数。编辑距
    /// 离的算法是首先由俄国科学家Levenshtein提出的，故又叫Levenshtein
    /// Distance。<br/>
    ///     Levenshtein Distance算法可以看作动态规划。它的思路就是从两个字
    /// 符串的左边开始比较，记录已经比较过的子串相似度(实际上叫做距离)，
    /// 然后进一步得到下一个字符位置时的相似度。用下面的例子: GUMBO和
    /// GAMBOL。当算到矩阵D[3,3]位置时,也就是当比较到GUM和GAM时,要从已经
    /// 比较过的3对子串GU-GAM, GUM-GA和GU-GA之中选一个差别最小的来当它的
    /// 值. 所以要从左上到右下构造矩阵。<br/>
    /// <br/>
    /// 编辑距离的伪算法：<br/>
    ///     整数Levenshtein距离(字符str1[1..lenStr1], 字符str2[1..lenStr2])
    ///     宣告int d[0..lenStr1, 0..lenStr2]
    ///     宣告int i, j, cost
    ///     对于 i 等于 由 0 至 lenStr1
    ///            d[i, 0] := i
    ///     对于 j 等于 由 0 至 lenStr2
    ///            d[0, j] := j
    ///     对于 i 等于 由 1 至 lenStr1
    ///          对于 j 等于 由 1 至 lenStr2
    ///              若 str1[i] = str2[j] 则 cost := 0
    ///                                 否则 cost := 1
    ///              d[i, j] := 最小值(
    ///                                d[i-1, j  ] + 1,     // 删除
    ///                                d[i  , j-1] + 1,     // 插入
    ///                                d[i-1, j-1] + cost   // 替换
    ///                                )
    ///    返回 d[lenStr1, lenStr2]
    /// </remarks>
    public class LevenshteinDistance
    {
        /// <summary>
        /// 得到三个值中的最小值
        /// </summary>
        /// <param name="a">第一个值</param>
        /// <param name="b">第二个值</param>
        /// <param name="c">第三个值</param>
        /// <returns>取小值</returns>
        private static int Minimum(int a, int b, int c)
        {
            int nRet;

            nRet = a;
            if (b < nRet)
            {
                nRet = b;
            }
            if (c < nRet)
            {
                nRet = c;
            }
            return nRet;
        }

        /// <summary>
        /// 计算并得到Levenshtein Distance
        /// </summary>
        /// <param name="sStr1">字符串一</param>
        /// <param name="sStr2">字符串二</param>
        /// <returns>两个字符中之间的距离</returns>
        public static int GetDistance(string sStr1, string sStr2) 
        {
            int [,] d;      // 距阵
            int nLen1;      // 字符串一的长度
            int nLen2;      // 字符串二的长度
            int i, j;          
            char ch1;       // 字符串一的字符
            char ch2;       // 字符串二的字符
            int nCost;      // 代价

            //======== 1. 第一步，构造代价距离距阵 ==========
            nLen1 = sStr1.Length;
            nLen2 = sStr2.Length;
            if (nLen1 == 0) 
            {
                return nLen2;
            }
            if (nLen2 == 0) 
            {
                return nLen1;
            }
            d = new int[nLen1+1, nLen2+1];

            //======== 2. 第二步，设置代价初始值 ==========
            for (i = 0; i <= nLen1; i++) 
            {
                d[i,0] = i;
            }

            for (j = 0; j <= nLen2; j++) 
            {
                d[0,j] = j;
            }

            //========= 3. 第三步，取得第一个串的每一个字符 =========

            for (i = 1; i <= nLen1; i++) 
            {
                ch1 = sStr1[i - 1];

                //======= 4. 第四步，取得第二个中的每一个字符 ==========
                for (j = 1; j <= nLen2; j++) 
                {
                    ch2 = sStr2[j - 1];

                    //======== 5. 第五步，计算代价 ==========
                    if (ch1 == ch2) 
                        nCost = 0;
                    else 
                        nCost = 1;

                    //========= 6. 第六步，得到最小代价 ==========
                    d[i,j] = Minimum(d[i-1,j]+1,            // 删除
                                     d[i,j-1]+1,            // 插入
                                     d[i-1,j-1] + nCost);   // 替换
                }
            }

            //========== 7. 第七步，退回最终结果 =========
            return d[nLen1,nLen2];
        }
    }



}
