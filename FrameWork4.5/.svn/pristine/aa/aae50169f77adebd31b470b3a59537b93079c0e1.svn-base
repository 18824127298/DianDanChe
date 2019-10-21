using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

namespace Sigbit.Common.MMLib.ImageFileProcess
{
    public class ImageFileCompareUtil
    {
        #region 比较两个文件
        public static ImageFileCompareResult CompareTwoFiles(string sSrcFileName, string sDestFileName)
        {
            //======== 1. 读取文件 ===========
            Bitmap imgSrc = (Bitmap)Bitmap.FromFile(sSrcFileName);
            Bitmap imgDest = (Bitmap)Bitmap.FromFile(sDestFileName);

            if (imgSrc.Width != imgDest.Width)
            {
                imgSrc.Dispose();
                imgDest.Dispose();
                throw new Exception("两个图像的宽度不同，无法比较");
            }
            if (imgSrc.Height != imgDest.Height)
            {
                imgSrc.Dispose();
                imgDest.Dispose();
                throw new Exception("两个图像的高度不同，无法比较");
            }

            //=========== 2. 根据读取像素的方法，调用不同的函数进行处理 ==========
            ImageFileCompareResult result;
            if (ImageFileCompareConfig.Instance.GetPixelMethod == ImageFileCompare_GetPixelMethod.GetPixel)
                result = CompareTwoFiles_GetPixel(imgSrc, imgDest);
            else
                result = CompareTwoFiles_Memory(imgSrc, imgDest);

            imgSrc.Dispose();
            imgDest.Dispose();
            return result;
        }

        private static ImageFileCompareResult CompareTwoFiles_GetPixel(Bitmap imgSrc, Bitmap imgDest)
        {
            //========= 1. 逐像素比较 =============
            int nWidth = imgSrc.Width;
            int nHeight = imgSrc.Height;

            ImageFileCompareResult compareResult = new ImageFileCompareResult();
            compareResult.TotalPixelCount = nWidth * nHeight;
            compareResult.DifferentPixelCount = 0;

            for (int w = 0; w < nWidth; w++)
            {
                for (int h = 0; h < nHeight; h++)
                {
                    Color pixelColorSrc = imgSrc.GetPixel(w, h);
                    Color pixcelColorDest = imgDest.GetPixel(w, h);

                    if (!IsSamePixel(pixelColorSrc, pixcelColorDest))
                        compareResult.DifferentPixelCount++;
                }
            }

            //========= 2. 返回结果 =============
            compareResult.DifferentRate = compareResult.DifferentPixelCount * 100.0 / compareResult.TotalPixelCount;

            return compareResult;
        }

        private static ImageFileCompareResult CompareTwoFiles_Memory(Bitmap imgSrc, Bitmap imgDest)
        {
            int nWidth = imgSrc.Width;
            int nHeight = imgSrc.Height;
            Rectangle rect = new Rectangle(0, 0, nWidth, nHeight);

            ImageFileCompareResult compareResult = new ImageFileCompareResult();
            compareResult.TotalPixelCount = nWidth * nHeight;
            compareResult.DifferentPixelCount = 0;

            //========= 1. 将Bitmap锁定到系统内存中,获得BitmapData ==========
            BitmapData srcBmData = imgSrc.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData destBmData = imgDest.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //===== 2. 位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行 ======
            System.IntPtr srcPtr = srcBmData.Scan0;
            System.IntPtr destPtr = destBmData.Scan0;

            //===== 3. 将Bitmap对象的信息存放到byte数组中 ========
            int nSrcBytesCount = srcBmData.Stride * nHeight;
            byte[] srcValues = new byte[nSrcBytesCount];

            int nDestBytesCount = destBmData.Stride * nHeight;
            byte[] destValues = new byte[nDestBytesCount];

            //======== 5. 复制GRB信息到byte数组 ===========
            System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcValues, 0, nSrcBytesCount);
            System.Runtime.InteropServices.Marshal.Copy(destPtr, destValues, 0, nDestBytesCount);

            //======== 6. 根据Y=0.299*R+0.114*G+0.587B,Y为亮度 =========
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    //======= (1) 只处理每行中图像像素数据,舍弃未用空间 ========
                    //======= (2) 注意位图结构中RGB按BGR的顺序存储 ============
                    int k = 3 * j;

                    byte srcR = srcValues[i * srcBmData.Stride + k + 2];
                    byte srcG = srcValues[i * srcBmData.Stride + k + 1];
                    byte srcB = srcValues[i * srcBmData.Stride + k];

                    byte destR = destValues[i * destBmData.Stride + k + 2];
                    byte destG = destValues[i * destBmData.Stride + k + 1];
                    byte destB = destValues[i * destBmData.Stride + k];

                    if (!IsSamePixel(srcR, srcG, srcB, destR, destG, destB))
                        compareResult.DifferentPixelCount ++;
                }
            }

            //============ 7. 解锁位图 =========
            imgSrc.UnlockBits(srcBmData);
            imgDest.UnlockBits(destBmData);

            //========= 8. 返回结果 =============
            compareResult.DifferentRate = compareResult.DifferentPixelCount * 100.0 / compareResult.TotalPixelCount;
            return compareResult;
        }
        #endregion 比较两个文件

        #region 在矩形范围内比较
        public static ImageFileCompareResult CompareTwoFiles(string sSrcFileName, string sDestFileName, Rectangle rect)
        {
            //======== 1. 读取文件 ===========
            Bitmap imgSrc = (Bitmap)Bitmap.FromFile(sSrcFileName);
            Bitmap imgDest = (Bitmap)Bitmap.FromFile(sDestFileName);

            if (imgSrc.Width != imgDest.Width)
            {
                imgSrc.Dispose();
                imgDest.Dispose();
                throw new Exception("CompareTwoFiles(rect)两个图像的宽度不同，无法比较");
            }
            if (imgSrc.Height != imgDest.Height)
            {
                imgSrc.Dispose();
                imgDest.Dispose();
                throw new Exception("CompareTwoFiles(rect)两个图像的高度不同，无法比较");
            }

            //============ 2. 判断矩形范围是否正确 ===========
            if (rect.X < 0)
                throw new Exception("CompareTwoFiles(rect)传入的矩形错误，X=" + rect.X.ToString());
            if (rect.Y < 0)
                throw new Exception("CompareTwoFiles(rect)传入的矩形错误，Y=" + rect.Y.ToString());
            if (rect.Width <= 0)
                throw new Exception("CompareTwoFiles(rect)传入的矩形错误，Width=" + rect.Width.ToString());
            if (rect.Height <= 0)
                throw new Exception("CompareTwoFiles(rect)传入的矩形错误，Height=" + rect.Height.ToString());

            int nEndX = rect.X + rect.Width;
            int nEndY = rect.Y + rect.Height;

            if (nEndX > imgSrc.Width)
            {
                throw new Exception("CompareTwoFiles(rect)矩形的终点超过图像的宽度，endX="
                        + nEndX.ToString() + "，图像宽度=" + imgSrc.Width);
            }
            if (nEndY > imgSrc.Height)
            {
                throw new Exception("CompareTwoFiles(rect)矩形的终点超过图像的高度，endY="
                        + nEndY.ToString() + "，图像高度=" + imgSrc.Height);
            }

            //=========== 3. 根据读取像素的方法，调用不同的函数进行处理 ==========
            ImageFileCompareResult result;
            if (ImageFileCompareConfig.Instance.GetPixelMethod == ImageFileCompare_GetPixelMethod.GetPixel)
                result = CompareTwoFiles_Rect_GetPixel(imgSrc, imgDest, rect);
            else
                result = CompareTwoFiles_Rect_Memory(imgSrc, imgDest, rect);

            imgSrc.Dispose();
            imgDest.Dispose();
            return result;
        }
        private static ImageFileCompareResult CompareTwoFiles_Rect_GetPixel(Bitmap imgSrc, Bitmap imgDest, Rectangle rect)
        {
            //========= 1. 逐像素比较 =============
            int nWidth = imgSrc.Width;
            int nHeight = imgSrc.Height;

            ImageFileCompareResult compareResult = new ImageFileCompareResult();
            compareResult.TotalPixelCount = rect.Width * rect.Height;
            compareResult.DifferentPixelCount = 0;

            for (int w = 0; w < rect.Width; w++)
            {
                for (int h = 0; h < rect.Height; h++)
                {
                    int x = w + rect.X;
                    int y = h + rect.Y;
                    Color pixelColorSrc = imgSrc.GetPixel(x, y);
                    Color pixcelColorDest = imgDest.GetPixel(x, y);

                    if (!IsSamePixel(pixelColorSrc, pixcelColorDest))
                        compareResult.DifferentPixelCount++;
                }
            }

            //========= 2. 返回结果 =============
            compareResult.DifferentRate = compareResult.DifferentPixelCount * 100.0 / compareResult.TotalPixelCount;

            return compareResult;
        }

        private static ImageFileCompareResult CompareTwoFiles_Rect_Memory(Bitmap imgSrc, Bitmap imgDest, Rectangle rect)
        {
            ImageFileCompareResult compareResult = new ImageFileCompareResult();
            compareResult.TotalPixelCount = rect.Width * rect.Height;
            compareResult.DifferentPixelCount = 0;

            //========= 1. 将Bitmap锁定到系统内存中,获得BitmapData ==========
            BitmapData srcBmData = imgSrc.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData destBmData = imgDest.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //===== 2. 位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行 ======
            System.IntPtr srcPtr = srcBmData.Scan0;
            System.IntPtr destPtr = destBmData.Scan0;

            //===== 3. 将Bitmap对象的信息存放到byte数组中 ========
            int nSrcBytesCount = srcBmData.Stride * rect.Height;
            byte[] srcValues = new byte[nSrcBytesCount];

            int nDestBytesCount = destBmData.Stride * rect.Height;
            byte[] destValues = new byte[nDestBytesCount];

            //======== 5. 复制GRB信息到byte数组 ===========
            System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcValues, 0, nSrcBytesCount);
            System.Runtime.InteropServices.Marshal.Copy(destPtr, destValues, 0, nDestBytesCount);

            //======== 6. 根据Y=0.299*R+0.114*G+0.587B,Y为亮度 =========
            for (int i = 0; i < rect.Height; i++)
            {
                for (int j = 0; j < rect.Width; j++)
                {
                    //======= (1) 只处理每行中图像像素数据,舍弃未用空间 ========
                    //======= (2) 注意位图结构中RGB按BGR的顺序存储 ============
                    int k = 3 * j;

                    byte srcR = srcValues[i * srcBmData.Stride + k + 2];
                    byte srcG = srcValues[i * srcBmData.Stride + k + 1];
                    byte srcB = srcValues[i * srcBmData.Stride + k];

                    byte destR = destValues[i * destBmData.Stride + k + 2];
                    byte destG = destValues[i * destBmData.Stride + k + 1];
                    byte destB = destValues[i * destBmData.Stride + k];

                    if (!IsSamePixel(srcR, srcG, srcB, destR, destG, destB))
                        compareResult.DifferentPixelCount++;
                }
            }

            //============ 7. 解锁位图 =========
            imgSrc.UnlockBits(srcBmData);
            imgDest.UnlockBits(destBmData);

            //========= 8. 返回结果 =============
            compareResult.DifferentRate = compareResult.DifferentPixelCount * 100.0 / compareResult.TotalPixelCount;
            return compareResult;
        }
        #endregion 在矩形范围内比较

        #region 判断像素是否相同
        private static bool IsSamePixel(Color colorSrc, Color colorDest)
        {
            int nDiffR = Math.Abs(colorSrc.R - colorDest.R);
            int nDiffG = Math.Abs(colorSrc.G - colorDest.G);
            int nDiffB = Math.Abs(colorSrc.B - colorDest.B);

            int nTotalDiff = nDiffR + nDiffG + nDiffB;

            if (nTotalDiff < 64)
                return true;
            else
                return false;
        }

        private static bool IsSamePixel(byte srcR, byte srcG, byte srcB, byte destR, byte destG, byte destB)
        {
            int nDiffR = Math.Abs(srcR - destR);
            int nDiffG = Math.Abs(srcG - destG);
            int nDiffB = Math.Abs(srcB - destB);

            int nTotalDiff = nDiffR + nDiffG + nDiffB;

            if (nTotalDiff < 64)
                return true;
            else
                return false;
        }
        #endregion 判断像素是否相同
    }
}
