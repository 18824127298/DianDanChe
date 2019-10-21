using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Sigbit.App.Net.IBXService.VCodeBreak.YunNan
{
    public class SecurityCode_Identify
    {
        /// <summary>
        /// 获取灰度值
        /// </summary>
        /// <param name="posClr"></param>
        /// <returns></returns>
        private static int GetGrayNumColor(Color posClr)
        {
            return (posClr.R + posClr.G + posClr.B) / 3;
        }

        /// <summary>
        /// 获取图片的灰化样本
        /// </summary>
        /// <param name="bmpInput"></param>
        /// <returns></returns>
        public static Bitmap GetGrayByPixels(Bitmap bmpInput)
        {
            Bitmap bmp = new Bitmap(bmpInput);
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int tmpValue = GetGrayNumColor(bmp.GetPixel(j, i));
                    bmp.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                }
            }
            return bmp;
        }

        /// <summary>
        /// 灰化图片
        /// </summary>
        /// <param name="bmpInput"></param>
        public static void GrayByPixels(Bitmap bmpInput)
        {
            for (int i = 0; i < bmpInput.Height; i++)
            {
                for (int j = 0; j < bmpInput.Width; j++)
                {
                    int tmpValue = GetGrayNumColor(bmpInput.GetPixel(j, i));
                    bmpInput.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                }
            }
        }

        /// <summary>
        /// 清除边框
        /// </summary>
        /// <param name="bmpInput"></param>
        /// <param name="nBorderWidth"></param>
        public static void ClearPicBorder(Bitmap bmpInput, int nBorderWidth)
        {
            for (int i = 0; i < bmpInput.Height; i++)
            {
                for (int j = 0; j < bmpInput.Width; j++)
                {
                    if (i < nBorderWidth || j < nBorderWidth || j > bmpInput.Width - 1 - nBorderWidth || i > bmpInput.Height - 1 - nBorderWidth)
                    {
                        bmpInput.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }

        /// <summary>
        /// 获得前/背景色的临界值，最大类间方差法
        /// </summary>
        /// <param name="bmpGrayPic"></param>
        /// <returns></returns>
        public static int GetDgGrayValue(Bitmap bmpGrayPic)
        {
            int[] pixelNum = new int[256];//图像直方图，256个点
            int n, n1, n2;
            int total;//总和，累计值。
            double m1, m2, sum, csum, fmax, sb;//sb为类间方差，fmax存储最大方差值
            int k, t, q;
            int threshValue = 1;//阙值

            //生成直方图
            for (int i = 0; i < bmpGrayPic.Width; i++)
            {
                for (int j = 0; j < bmpGrayPic.Height; j++)
                {
                    //返回各个点的颜色，以RGB表示
                    pixelNum[bmpGrayPic.GetPixel(i, j).R]++;//相应的直方图加1
                }
            }

            for (k = 0; k <= 255; k++)
            {
                total = 0;
                for (t = -2; t <= 2; t++)//与附近2个灰度做平滑化，t值应取较小的值
                {
                    q = k + t;
                    if (q < 0)
                    { q = 0; }
                    if (q > 255)
                    { q = 255; }

                    total = total + pixelNum[q];//total为总和，累计值
                }
                //平滑化，左边2个+中间1个+右边2个灰度，共5个，所以总和除以5，后面加0.5是用修正值
                pixelNum[k] = (int)((float)total / 5.0 + 0.5);
            }

            //求阙值
            sum = csum = 0.0;
            n = 0;

            //计算总的图像点数和质量矩，为后面的计算做准备
            for (k = 0; k <= 255; k++)
            {
                //x*f(x)质量矩，也就是每个灰度的值乘以其点数（归一化后为概率），sum为其总和
                sum += (double)k * (double)pixelNum[k];

                n += pixelNum[k];//n为图象总的点数，归一化后就是累积概率
            }

            fmax = -1.0; //类间方差sb不可能为负，所以fmax初始值为-1不影响计算的进行
            n1 = 0;
            for (k = 0; k <= 255; k++)
            {//对每个灰度计算（0到255）计算一次分割后的类间方差sb

                n1 += pixelNum[k];//n1为在当前阙值遍前景图像的点数
                if (n1 == 0)
                {//没分出前景后景
                    continue;
                }
                n2 = n - n1;//n2为背景图像的的点数
                if (n2 == 0)
                {//n2为0表示全部都是后景图象，与n1=0情况类似，之后的遍历不可能使前景点数增加，所以此时可以退出循环

                    break;
                }
                csum += (double)k * pixelNum[k];//前景的“灰度的值*其点数”的总和
                m1 = csum / n1;//m1为前景平均灰度
                m2 = (sum - csum) / n2;//m2为背景平均灰度
                sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);//sb为类间方差
                if (sb > fmax)
                {
                    fmax = sb;//fmax始终为最大类间方差
                    threshValue = k;//去最大类间方差时对应的的灰度K就是最佳阙值
                }
            }
            return threshValue;
        }

        #region 去噪法

        /// <summary>
        /// 去除杂点(适合噪点/噪线粗为1，但码宽大于1)
        /// </summary>
        /// <param name="deGrayValue"></param>
        /// <param name="MaxNearPoints"></param>
        public static void ClearNoise(Bitmap bmpInput, int deGrayValue, int MaxNearPoints)
        {
            Color pixel;
            int nearDots = 0;

            //逐点判断
            for (int i = 0; i < bmpInput.Width; i++)
            {
                for (int j = 0; j < bmpInput.Height; j++)
                {
                    pixel = bmpInput.GetPixel(i, j);
                    if (pixel.R < deGrayValue)
                    {
                        nearDots = 0;
                        if (i == 0 || i == bmpInput.Width - 1 || j == 0 || j == bmpInput.Height - 1)
                        {//去边框
                            bmpInput.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {//判断周围8点的情况
                            if (bmpInput.GetPixel(i - 1, j - 1).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i, j - 1).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i + 1, j - 1).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i - 1, j).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i + 1, j).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i - 1, j + 1).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i, j + 1).R < deGrayValue)
                                nearDots++;
                            if (bmpInput.GetPixel(i + 1, j + 1).R < deGrayValue)
                                nearDots++;
                        }
                        if (nearDots < MaxNearPoints)
                        {
                            bmpInput.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                    }
                    else
                    {//背景
                        bmpInput.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }

        /// <summary>
        /// 中值滤波，3*3矩形窗口滤波
        /// </summary>
        /// <param name="deGrayValue"></param>
        public static void ClearNoise_Mid33(Bitmap bmpInput)
        {
            int x, y;
            int i, j;
            byte[] byteRegion = new byte[9];//最小处理窗口3*3
            byte s;

            //=========为3*3窗口中值滤波(第一行和最后一行无法取窗口)==============
            for (y = 1; y < bmpInput.Height - 1; y++)
            {
                for (x = 1; x < bmpInput.Width - 1; x++)
                {
                    //取9个点的值
                    byteRegion[0] = bmpInput.GetPixel(x - 1, y - 1).R;
                    byteRegion[1] = bmpInput.GetPixel(x, y - 1).R;
                    byteRegion[2] = bmpInput.GetPixel(x + 1, y - 1).R;
                    byteRegion[3] = bmpInput.GetPixel(x - 1, y).R;
                    byteRegion[4] = bmpInput.GetPixel(x, y).R;
                    byteRegion[5] = bmpInput.GetPixel(x + 1, y).R;
                    byteRegion[6] = bmpInput.GetPixel(x - 1, y + 1).R;
                    byteRegion[7] = bmpInput.GetPixel(x, y + 1).R;
                    byteRegion[8] = bmpInput.GetPixel(x + 1, y + 1).R;

                    //取中值
                    for (j = 0; j < 5; j++)
                    {
                        for (i = j + 1; i < 9; i++)
                        {
                            if (byteRegion[j] > byteRegion[i])
                            {
                                s = byteRegion[j];
                                byteRegion[j] = byteRegion[i];
                                byteRegion[i] = s;
                            }
                        }
                    }
                    bmpInput.SetPixel(x, y, Color.FromArgb(byteRegion[4], byteRegion[4], byteRegion[4]));
                }
            }
        }

        /// <summary>
        /// 中值滤波，十字型滤波
        /// </summary>
        /// <param name="deGrayValue"></param>
        public static void ClearNoise_MidCross(Bitmap bmpInput)
        {
            int w, h;
            int j, i;
            byte[] byteRegion = new byte[5];//最小处理窗口5；
            byte byteTemp;

            //==============十字形中值滤波===============================
            for (h = 1; h < bmpInput.Height-1; h++)
            {
                for (w = 1; w < bmpInput.Width-1; w++)
                {
                    byteRegion[0] = bmpInput.GetPixel(w - 1, h).B;
                    byteRegion[1] = bmpInput.GetPixel(w, h - 1).B;
                    byteRegion[2] = bmpInput.GetPixel(w, h).B;
                    byteRegion[3] = bmpInput.GetPixel(w, h + 1).B;
                    byteRegion[4] = bmpInput.GetPixel(w + 1, h).B;
                    for (i = 0; i < 3; i++)
                    {
                        for (j = i + 1; j < 5; j++)
                        {
                            if (byteRegion[i] < byteRegion[j])
                            {
                                byteTemp = byteRegion[i];
                                byteRegion[i] = byteRegion[j];
                                byteRegion[j] = byteTemp;
                            }
                        }
                    }
                    bmpInput.SetPixel(w, h, Color.FromArgb(byteRegion[2], byteRegion[2], byteRegion[2]));
                }
            }
        }

        /// <summary>
        /// 均值虑波,33矩阵滤波(未完成)
        /// </summary>
        /// <param name="bmpInput"></param>
        /// <param name="rectLen"></param>
        public static void ClearNoise_Avg33(Bitmap bmpInput)
        {
            int w, h;
            byte[] byteRegion = new byte[9];
            int total, avg;

        }

        #endregion

        /// <summary>
        /// 得到有效图形，并调整为可分割的大小
        /// </summary>
        /// <param name="dgGrayValue"></param>
        /// <param name="CharsCount"></param>
        public static Bitmap GetPicValidByValue(Bitmap bmpInput, int dgGrayValue, int CharsCount)
        {
            Bitmap btmpobj = new Bitmap(bmpInput);
            int posX1 = btmpobj.Width, posY1 = btmpobj.Height;
            int posX2 = 0, posY2 = 0;
            for (int i = 0; i < btmpobj.Height; i++)
            {
                for (int j = 0; j < btmpobj.Width; j++)
                {
                    int pixelValue = btmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)
                    {
                        if (posX1 > j)
                            posX1 = j;
                        if (posY1 > i)
                            posY1 = i;
                        if (posX2 < j)
                            posX2 = j;
                        if (posY2 < i)
                            posY2 = i;
                    }
                }
            }

            int nSpan = CharsCount - (posX2 - posX1 + 1) % CharsCount;
            if (nSpan < CharsCount)
            {
                int leftSpan = nSpan / 2;
                if (posX1 > leftSpan)
                {
                    posX1 = posX1 - leftSpan;
                }
                if (posX2 + nSpan - leftSpan < btmpobj.Width)
                {
                    posX2 = posX2 + nSpan - leftSpan;
                }
            }

            //复制新图
            Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);
            btmpobj = btmpobj.Clone(cloneRect, btmpobj.PixelFormat);
            return btmpobj;
        }

        /// <summary>
        /// 平均分割图片
        /// </summary>
        /// <param name="ColCount">水平分割数</param>
        /// <param name="RowCount">垂直分割数</param>
        /// <returns></returns>
        public static Bitmap[] GetSplitPics(Bitmap bmpInput,int ColCount, int RowCount)
        {
            if (ColCount == 0 || RowCount == 0)
            { return null; }

            int singW = bmpInput.Width / ColCount;
            int singH = bmpInput.Height / RowCount;
            Bitmap[] PicArray = new Bitmap[ColCount * RowCount];

            Rectangle cloneRect;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    cloneRect = new Rectangle(j * singW, i * singH, singW, singH);
                    PicArray[i * ColCount + j] = bmpInput.Clone(cloneRect, bmpInput.PixelFormat);
                }
            }

            return PicArray;
        }

        /// <summary>
        /// 得到有效小图，调整小图大小
        /// </summary>
        /// <param name="singlePic"></param>
        /// <param name="dgGrayValue"></param>
        /// <returns></returns>
        public static Bitmap GetSinglePicValidByValue(Bitmap singlePic, int dgGrayValue)
        {
            int posX1 = singlePic.Width;
            int posY1 = singlePic.Height;
            int posX2 = 0;
            int posY2 = 0;

            for (int i = 0; i < singlePic.Height; i++)
            {
                for (int j = 0; j < singlePic.Width; j++)
                {
                    int pixelValue = singlePic.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)
                    {
                        if (posX1 > j)
                            posX1 = j;
                        if (posY1 > i)
                            posY1 = i;
                        if (posX2 < j)
                            posX2 = j;
                        if (posY2 < i)
                            posY2 = i;
                    }
                }
            }

            //复制新图
            Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);

            return singlePic.Clone(cloneRect, singlePic.PixelFormat);
        }

        public static Bitmap GetSinglePicValidByValue(Bitmap singlePic, int dgGrayValue,int width,int heigth)
        {
            int posX1 = singlePic.Width;
            int posY1 = singlePic.Height;
            int posX2 = 0;
            int posY2 = 0;

            for (int i = 0; i < singlePic.Height; i++)
            {
                for (int j = 0; j < singlePic.Width; j++)
                {
                    int pixelValue = singlePic.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)
                    {
                        if (posX1 > j)
                            posX1 = j;
                        if (posY1 > i)
                            posY1 = i;
                        if (posX2 < j)
                            posX2 = j;
                        if (posY2 < i)
                            posY2 = i;
                    }
                }
            }

            //复制新图
            Rectangle cloneRect = new Rectangle(posX1, posY1, width, heigth);

            return singlePic.Clone(cloneRect, singlePic.PixelFormat);
        }


        /// <summary>
        /// 返回灰度图片的点阵描述字串，1表示灰点，0表示背景
        /// </summary>
        /// <param name="singlePic"></param>
        /// <param name="dgGrayValue"></param>
        /// <returns></returns>
        public static string GetSingleBmpCode(Bitmap singlePic, int dgGrayValue)
        {
            Color pixel;
            string code = "";

            for (int posY = 0; posY < singlePic.Height; posY++)
            {
                for (int posX = 0; posX < singlePic.Width; posX++)
                {
                    pixel = singlePic.GetPixel(posX, posY);
                    if (pixel.R < dgGrayValue)
                    {
                        code = code + "1";
                    }
                    else
                    {
                        code = code + "0";
                    }
                }
            }
            return code;
        }

        /// <summary>
        /// 获取二值化图片
        /// </summary>
        /// <param name="bmpImput"></param>
        /// <param name="dgGrayValue"></param>
        /// <returns></returns>
        public static Bitmap GetPicByBinaryzation(Bitmap bmpImput, int dgGrayValue)
        {
            Bitmap bmpRet = new Bitmap(bmpImput);

            for (int i = 0; i < bmpRet.Height; i++)
            {
                for (int j = 0; j < bmpRet.Width; j++)
                {
                    if (bmpRet.GetPixel(j, i).B > dgGrayValue)
                    {
                        bmpRet.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmpRet.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return bmpRet;
        }

    }

    #region 一个基类，已注释，有空回头来看下
    //public class UnCodeBase
    //{
    //    public Bitmap btmpobj;

    //    public UnCodeBase(Bitmap pic)
    //    {
    //        btmpobj = new Bitmap(pic);
    //    }

    //    /// <summary>
    //    /// 根据RGB计算灰度值
    //    /// </summary>
    //    /// <param name="posClr"></param>
    //    /// <returns></returns>
    //    private int GetGrayNumColor(System.Drawing.Color posClr)
    //    {
    //        return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
    //    }

    //    /// <summary>
    //    /// 逐点转换为灰度模式
    //    /// </summary>
    //    public void GrayByPixels()
    //    {
    //        for (int i = 0; i < btmpobj.Height; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Width; j++)
    //            {
    //                int tmpValue = GetGrayNumColor(btmpobj.GetPixel(j, i));
    //                btmpobj.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 清除边框
    //    /// </summary>
    //    /// <param name="nBorderWidth">边框长度</param>
    //    public void ClearPicBorder(int nBorderWidth)
    //    {
    //        for (int i = 0; i < btmpobj.Height; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Width; j++)
    //            {
    //                if (i < nBorderWidth || j < nBorderWidth || j > btmpobj.Width - 1 - nBorderWidth || i > btmpobj.Height - 1 - nBorderWidth)
    //                {
    //                    btmpobj.SetPixel(j, i, Color.FromArgb(255, 255, 255));
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 逐行转换为灰度模式
    //    /// </summary>
    //    public void GrayByLine()
    //    {
    //        Rectangle rec = new Rectangle(0, 0, btmpobj.Width, btmpobj.Height);
    //        BitmapData btmData = btmpobj.LockBits(rec, ImageLockMode.ReadWrite, btmpobj.PixelFormat);
    //        IntPtr scan0 = btmData.Scan0;
    //        int len = btmpobj.Height * btmpobj.Width;
    //        int[] pixels = new int[len];

    //        Marshal.Copy(scan0, pixels, 0, len);

    //        //处理图片
    //        int nGrayValue = 0;
    //        for (int i = 0; i < len; i++)
    //        {
    //            nGrayValue = GetGrayNumColor(Color.FromArgb(pixels[i]));
    //            pixels[i] = (byte)(Color.FromArgb(nGrayValue, nGrayValue, nGrayValue)).ToArgb();
    //        }
    //        btmpobj.UnlockBits(btmData);
    //    }

    //    /// <summary>
    //    /// 获得前/背景色的临界值，最大类间方差法
    //    /// </summary>
    //    /// <returns></returns>
    //    public int GetDgGrayValue()
    //    {
    //        int[] pixelNum = new int[256];//图像直方图，256个点
    //        int n, n1, n2;
    //        int total;//总和，累计值。
    //        double m1, m2, sum, csum, fmax, sb;//sb为类间方差，fmax存储最大方差值
    //        int k, t, q;
    //        int threshValue = 1;//阙值

    //        //生成直方图
    //        for (int i = 0; i < btmpobj.Width; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Height; j++)
    //            {
    //                //返回各个点的颜色，以RGB表示
    //                pixelNum[btmpobj.GetPixel(i, j).R]++;//相应的直方图加1
    //            }
    //        }

    //        for (k = 0; k <= 255; k++)
    //        {
    //            total = 0;
    //            for (t = -2; t <= 2; t++)//与附近2个灰度做平滑化，t值应取较小的值
    //            {
    //                q = k + t;
    //                if (q < 0)
    //                { q = 0; }
    //                if (q > 255)
    //                { q = 255; }

    //                total = total + pixelNum[q];//total为总和，累计值
    //            }
    //            //平滑化，左边2个+中间1个+右边2个灰度，共5个，所以总和除以5，后面加0.5是用修正值
    //            pixelNum[k] = (int)((float)total / 5.0 + 0.5);
    //        }

    //        //求阙值
    //        sum = csum = 0.0;
    //        n = 0;

    //        //计算总的图像点数和质量矩，为后面的计算做准备
    //        for (k = 0; k <= 255; k++)
    //        {
    //            //x*f(x)质量矩，也就是每个灰度的值乘以其点数（归一化后为概率），sum为其总和
    //            sum += (double)k * (double)pixelNum[k];

    //            n += pixelNum[k];//n为图象总的点数，归一化后就是累积概率
    //        }

    //        fmax = -1.0; //类间方差sb不可能为负，所以fmax初始值为-1不影响计算的进行
    //        n1 = 0;
    //        for (k = 0; k <= 255; k++)
    //        {//对每个灰度计算（0到255）计算一次分割后的类间方差sb

    //            n1 += pixelNum[k];//n1为在当前阙值遍前景图像的点数
    //            if (n1 == 0)
    //            {//没分出前景后景
    //                continue;
    //            }
    //            n2 = n - n1;//n2为背景图像的的点数
    //            if (n2 == 0)
    //            {//n2为0表示全部都是后景图象，与n1=0情况类似，之后的遍历不可能使前景点数增加，所以此时可以退出循环

    //                break;
    //            }
    //            csum += (double)k * pixelNum[k];//前景的“灰度的值*其点数”的总和
    //            m1 = csum / n1;//m1为前景平均灰度
    //            m2 = (sum - csum) / n2;//m2为背景平均灰度
    //            sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);//sb为类间方差
    //            if (sb > fmax)
    //            {
    //                fmax = sb;//fmax始终为最大类间方差
    //                threshValue = k;//去最大类间方差时对应的的灰度K就是最佳阙值
    //            }
    //        }
    //        return threshValue;
    //    }

    //    /// <summary>
    //    /// 去除杂点(适合噪点/噪线粗为1，但码宽大于1)
    //    /// </summary>
    //    /// <param name="deGrayValue"></param>
    //    /// <param name="MaxNearPoints"></param>
    //    public void ClearNoise(int deGrayValue,int MaxNearPoints)
    //    {
    //        Color pixel;
    //        int nearDots = 0;
    //        int XSpan, YSpan, tmpX, tmpY;

    //        //逐点判断
    //        for (int i = 0; i < btmpobj.Width; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Height; j++)
    //            {
    //                pixel = btmpobj.GetPixel(i, j);
    //                if (pixel.R < deGrayValue)
    //                {
    //                    nearDots = 0;
    //                    if (i == 0 || i ==btmpobj.Width - 1 || j == 0 || j == btmpobj.Height - 1)
    //                    {//去边框
    //                        btmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
    //                    }
    //                    else
    //                    {//判断周围8点的情况
    //                        if (btmpobj.GetPixel(i - 1, j - 1).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i, j - 1).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i + 1, j - 1).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i - 1, j).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i + 1, j).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i - 1, j + 1).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i, j + 1).R < deGrayValue)
    //                            nearDots++;
    //                        if (btmpobj.GetPixel(i + 1, j + 1).R < deGrayValue)
    //                            nearDots++;
    //                    }
    //                    if (nearDots < MaxNearPoints)
    //                    {
    //                        btmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
    //                    }
    //                }
    //                else
    //                {//背景
    //                    btmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 中值滤波，3*3矩形窗口滤波
    //    /// </summary>
    //    /// <param name="deGrayValue"></param>
    //    public void ClearNoise(int deGrayValue)
    //    {
    //        int x, y;
    //        int i, j;
    //        byte[] byteRegion = new byte[9];//最小处理窗口3*3
    //        byte s;

    //        //=========为3*3窗口中值滤波(第一行和最后一行无法取窗口)==============
    //        for (y = 1; y < btmpobj.Height-1; y++)
    //        {
    //            for (x = 1; x < btmpobj.Width - 1; x++)
    //            {
    //                //取9个点的值
    //                byteRegion[0] = btmpobj.GetPixel(x - 1, y - 1).R;
    //                byteRegion[1] = btmpobj.GetPixel(x, y - 1).R;
    //                byteRegion[2] = btmpobj.GetPixel(x + 1, y - 1).R;
    //                byteRegion[3] = btmpobj.GetPixel(x - 1, y).R;
    //                byteRegion[4] = btmpobj.GetPixel(x, y).R;
    //                byteRegion[5] = btmpobj.GetPixel(x + 1, y).R;
    //                byteRegion[6] = btmpobj.GetPixel(x - 1, y + 1).R;
    //                byteRegion[7] = btmpobj.GetPixel(x, y +1).R;
    //                byteRegion[8] = btmpobj.GetPixel(x + 1, y + 1).R;

    //                //取中值
    //                for (j = 0; j < 5; j++)
    //                {
    //                    for (i = j + 1; i < 9; i++)
    //                    {
    //                        if (byteRegion[j] > byteRegion[i])
    //                        {
    //                            s = byteRegion[j];
    //                            byteRegion[j] = byteRegion[i];
    //                            byteRegion[i] = s;
    //                        }
    //                    }
    //                }
    //                btmpobj.SetPixel(x, y, Color.FromArgb(byteRegion[4], byteRegion[4], byteRegion[4]));
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 得到有效图形，并调整为可分割的大小
    //    /// </summary>
    //    /// <param name="dgGrayValue"></param>
    //    /// <param name="CharsCount"></param>
    //    public void GetPicValidByValue(int dgGrayValue, int CharsCount)
    //    {
    //        int posX1 = btmpobj.Width, posY1 = btmpobj.Height;
    //        int posX2 = 0, posY2 = 0;
    //        for (int i = 0; i < btmpobj.Height; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Width; j++)
    //            {
    //                int pixelValue = btmpobj.GetPixel(j, i).R;
    //                if (pixelValue < dgGrayValue)
    //                {
    //                    if (posX1 > j)
    //                        posX1 = j;
    //                    if (posY1 > i)
    //                        posY1 = i;
    //                    if (posX2 < j)
    //                        posX2 = j;
    //                    if (posY2 < i)
    //                        posY2 = i;
    //                }
    //            }
    //        }

    //        int nSpan = CharsCount - (posX2 - posX1 + 1) % CharsCount;
    //        if (nSpan < CharsCount)
    //        {
    //            int leftSpan = nSpan / 2;
    //            if (posX1 > leftSpan)
    //            {
    //                posX1 = posX1 - leftSpan;
    //            }
    //            if (posX2 + nSpan - leftSpan < btmpobj.Width)
    //            {
    //                posX2 = posX2 + nSpan - leftSpan;
    //            }
    //        }

    //        //复制新图
    //        Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);
    //        btmpobj = btmpobj.Clone(cloneRect, btmpobj.PixelFormat);
    //    }

    //    /// <summary>
    //    /// 得到有效图形
    //    /// </summary>
    //    /// <param name="dgGrayValue">前景/背景分界值</param>
    //    public void GetPicValidByValue(int dgGrayValue)
    //    {
    //        int posX1 = btmpobj.Width;
    //        int posY1 = btmpobj.Height;
    //        int posX2 = 0;
    //        int posY2 = 0;

    //        for (int i = 0; i < btmpobj.Height; i++)
    //        {
    //            for (int j = 0; j < btmpobj.Width; j++)
    //            {
    //                int pixelValue = btmpobj.GetPixel(j, i).R;
    //                if (pixelValue < dgGrayValue)
    //                {
    //                    if (posX1 > j)
    //                        posX1 = j;
    //                    if (posY1 > i)
    //                        posY1 = i;
    //                    if (posX2 < j)
    //                        posX2 = j;
    //                    if (posY2 < i)
    //                        posY2 = i;
    //                }
    //            }
    //        }

    //        //复制新图
    //        Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);
    //        btmpobj = btmpobj.Clone(cloneRect, btmpobj.PixelFormat);
    //    }

    //    /// <summary>
    //    /// 得到有效图形
    //    /// </summary>
    //    /// <param name="singlePic"></param>
    //    /// <param name="dgGrayValue"></param>
    //    /// <returns></returns>
    //    public Bitmap GetPicValidByValue(Bitmap singlePic, int dgGrayValue)
    //    {
    //        int posX1 = singlePic.Width;
    //        int posY1 = singlePic.Height;
    //        int posX2 = 0;
    //        int posY2 = 0;

    //        for (int i = 0; i < singlePic.Height; i++)
    //        {
    //            for (int j = 0; j < singlePic.Width; j++)
    //            {
    //                int pixelValue = singlePic.GetPixel(j, i).R;
    //                if (pixelValue < dgGrayValue)
    //                {
    //                    if (posX1 > j)
    //                        posX1 = j;
    //                    if (posY1 > i)
    //                        posY1 = i;
    //                    if (posX2 < j)
    //                        posX2 = j;
    //                    if (posY2 < i)
    //                        posY2 = i;
    //                }
    //            }
    //        }

    //        //复制新图
    //        Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);

    //        return singlePic.Clone(cloneRect, singlePic.PixelFormat);
    //    }

    //    /// <summary>
    //    /// 平均分割图片
    //    /// </summary>
    //    /// <param name="ColCount">水平分割数</param>
    //    /// <param name="RowCount">垂直分割数</param>
    //    /// <returns></returns>
    //    public Bitmap[] GetSplitPics(int ColCount, int RowCount)
    //    {
    //        if (ColCount == 0 || RowCount == 0)
    //        { return null; }

    //        int singW = btmpobj.Width / ColCount;
    //        int singH = btmpobj.Height / RowCount;
    //        Bitmap[] PicArray = new Bitmap[ColCount * RowCount];

    //        Rectangle cloneRect;
    //        for (int i = 0; i < RowCount; i++)
    //        {
    //            for (int j = 0; j < ColCount; j++)
    //            {
    //                cloneRect = new Rectangle(j * singW, i * singH, singW, singH);
    //                PicArray[i * ColCount + j] = btmpobj.Clone(cloneRect, btmpobj.PixelFormat);
    //            }
    //        }

    //        return PicArray;
    //    }

    //    /// <summary>
    //    /// 返回灰度图片的点阵描述字串，1表示灰点，0表示背景
    //    /// </summary>
    //    /// <param name="singlePic"></param>
    //    /// <param name="dgGrayValue"></param>
    //    /// <returns></returns>
    //    public string GetSingleBtmpCode(Bitmap singlePic, int dgGrayValue)
    //    {
    //        Color pixel;
    //        string code = "";

    //        for (int posY = 0; posY < singlePic.Height; posY++)
    //        {
    //            for (int posX = 0; posX < singlePic.Width; posX++)
    //            {
    //                pixel = singlePic.GetPixel(posX, posY);
    //                if (pixel.R < dgGrayValue)
    //                {
    //                    code = code + "1";
    //                }
    //                else
    //                {
    //                    code = code + "0";
    //                }
    //            }
    //        }
    //        return code;
    //    }
    //}
    #endregion
}
