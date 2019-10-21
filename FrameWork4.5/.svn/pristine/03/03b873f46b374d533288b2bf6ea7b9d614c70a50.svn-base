using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sigbit.Common.MMLib.ImageFileProcess
{
    public class ImageFileUtil
    {
        #region 判断图像格式是否正确
        
        /// <summary>
        /// 检查图像格式是否正确  Jim 201511
        /// </summary>
        /// <param name="sImageFileName"></param>
        /// <returns></returns>
        public static bool CheckImage(string sImageFileName)
        {
            try
            {
                Image img = System.Drawing.Image.FromFile(sImageFileName);
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        #endregion

        #region 得到图像的大小

        public static void ImageSize(string sImageFileName, ref int nWidth, ref int nHeight)
        {
            if (!File.Exists(sImageFileName))
                throw new Exception("ImageSize(): 图像文件不存在 - " + sImageFileName);

            Image img = Image.FromFile(sImageFileName);

            nWidth = img.Width;
            nHeight = img.Height;

            img.Dispose();
        }

        #endregion


        #region 转换图像格式
        public static void ConvertImage(string sSrcFileName, string sDestFileName)
        {
            //========= 1. 读取源图像 =========
            if (!File.Exists(sSrcFileName))
                throw new Exception("ResizeImage(): 图像文件不存在 - " + sSrcFileName);

            Image imgSrc = Image.FromFile(sSrcFileName);

            //========== 2. 目标文件名 ===============
            string sDestExtension = Path.GetExtension(sDestFileName);
            ImageFormat imageFormat = ImageFormatOfFileExtension(sDestExtension);

            imgSrc.Save(sDestFileName, imageFormat);

            imgSrc.Dispose();
        }
        #endregion


        #region 改变图像大小
        public static void ResizeImage(string sSrcFileName, string sDestFileName, int nNewWidth, int nNewHeight)
        {
            //========= 1. 读取源图像 =========
            if (!File.Exists(sSrcFileName))
                throw new Exception("ResizeImage(): 图像文件不存在 - " + sSrcFileName);

            Image imgSrc = Image.FromFile(sSrcFileName);

            //======== 2. 变为指定大小 ===========
            Bitmap imgDest = new Bitmap(imgSrc, nNewWidth, nNewHeight);

            //========== 3. 目标文件名 ===============
            string sDestExtension = Path.GetExtension(sDestFileName);
            ImageFormat imageFormat = ImageFormatOfFileExtension(sDestExtension);

            imgDest.Save(sDestFileName,imageFormat);

            imgSrc.Dispose();
            imgDest.Dispose();
        }
        #endregion 改变图像大小

        #region 截取图像大小
        /// <summary>
        /// 截取图像大小。截取的图像取源图像的中部。
        /// </summary>
        /// <param name="sSrcFileName">源文件</param>
        /// <param name="sDestFileName">目标文件</param>
        /// <param name="nNewWidth">新的宽度</param>
        /// <param name="nNewHeight">新的高度</param>
        public static void CropImage(string sSrcFileName, string sDestFileName, int nNewWidth, int nNewHeight)
        {
            //========= 1. 读取源图像 =========
            if (!File.Exists(sSrcFileName))
                throw new Exception("CropImage(): 图像文件不存在 - " + sSrcFileName);

            Image imgSrc = Image.FromFile(sSrcFileName);

            //======== 2. 截取部分图像 ===========
            int nNewLeft = (imgSrc.Width - nNewWidth) / 2;
            int nNewTop = (imgSrc.Height - nNewHeight) / 2;

            Rectangle cropRect = new Rectangle(nNewLeft, nNewTop, nNewWidth, nNewHeight);
            CropImage(sSrcFileName, sDestFileName, cropRect);

            imgSrc.Dispose();
        }

        /// <summary>
        /// 截取图像大小。按指定的矩形区域截取。
        /// </summary>
        /// <param name="sSrcFileName">源文件</param>
        /// <param name="sDestFileName">目标文件</param>
        /// <param name="rect">矩形区域</param>
        public static void CropImage(string sSrcFileName, string sDestFileName, Rectangle rect)
        {
            //========= 1. 读取源图像 =========
            if (!File.Exists(sSrcFileName))
                throw new Exception("CropImage(): 图像文件不存在 - " + sSrcFileName);

            Image imgSrc = Image.FromFile(sSrcFileName);

            //======== 2. 截取部分图像 ===========
            int nNewWidth = rect.Width;
            int nNewHeigth = rect.Height;

            Bitmap imgDest = new Bitmap(nNewWidth, nNewHeigth);

            Graphics g = Graphics.FromImage(imgDest);
            Rectangle destRectangle = new Rectangle(0, 0, nNewWidth, nNewHeigth);
            Rectangle cropRectangle = rect;
            g.DrawImage(imgSrc, destRectangle, cropRectangle, GraphicsUnit.Pixel);

            //========== 3. 保存为目标文件名 ===============
            string sDestExtension = Path.GetExtension(sDestFileName);
            ImageFormat imageFormat = ImageFormatOfFileExtension(sDestExtension);

            imgDest.Save(sDestFileName, imageFormat);

            imgSrc.Dispose();
            imgDest.Dispose();
        }
        #endregion 截取图像大小

        #region 截取并改变大小（用于比例不一致的缩略图处理）
        public static void CropResizeImage(string sSrcFileName, string sDestFileName, int nNewWidth, int nNewHeight)
        {
            //============ 1. 读取源图像 ================
            if (!File.Exists(sSrcFileName))
                throw new Exception("CropResizeImage(): 图像文件不存在 - " + sSrcFileName);

            Image imgSrc = Image.FromFile(sSrcFileName);

            //========== 2. 得到原图像的大小 ============
            int nSrcWidth = imgSrc.Width;
            int nSrcHeight = imgSrc.Height;

            //========== 3. 比较图像的长宽比，判断如何截取到一致比例的中间图像 =============
            int nCropWidth = nSrcWidth;
            int nCropHeight = nSrcHeight;

            if (nNewHeight * 1.0 / nNewWidth > nSrcHeight * 1.0 / nSrcWidth)
                nCropWidth = nSrcHeight * nNewWidth / nNewHeight;
            else
                nCropHeight = nSrcWidth * nNewHeight / nNewWidth;

            //============ 4. 截取图像 ================
            int nCropLeft = (nSrcWidth - nCropWidth) / 2;
            int nCropTop = (nSrcHeight - nCropHeight) / 2;

            Rectangle cropRect = new Rectangle(nCropLeft, nCropTop, nCropWidth, nCropHeight);

            Bitmap imgCrop = new Bitmap(nCropWidth, nCropHeight);

            Graphics g = Graphics.FromImage(imgCrop);
            Rectangle cropDestRect = new Rectangle(0, 0, nCropWidth, nCropHeight);
            g.DrawImage(imgSrc, cropDestRect, cropRect, GraphicsUnit.Pixel);

            //============ 5. 把截取的图像改变大小 ============
            Bitmap imgDest = new Bitmap(imgCrop, nNewWidth, nNewHeight);

            //========== 6. 目标文件名 ===============
            string sDestExtension = Path.GetExtension(sDestFileName);
            ImageFormat imageFormat = ImageFormatOfFileExtension(sDestExtension);

            string sCropFileName = "c:\\temp\\crop.jpg";
            imgCrop.Save(sCropFileName, imageFormat);
            
            imgDest.Save(sDestFileName, imageFormat);

            imgSrc.Dispose();
            imgCrop.Dispose();
            imgDest.Dispose();
        }
        #endregion

        private static ImageFormat ImageFormatOfFileExtension(string sFileExtension)
        {
            sFileExtension = sFileExtension.ToLower();
            ImageFormat formatRet = ImageFormat.Bmp;

            switch (sFileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    formatRet = ImageFormat.Jpeg;
                    break;
                case ".png":
                    formatRet = ImageFormat.Png;
                    break;
                case ".bmp":
                    formatRet = ImageFormat.Bmp;
                    break;
                case ".gif":
                    formatRet = ImageFormat.Gif;
                    break;
                default:
                    throw new Exception("ImageFileUtil.ImageFormatOfFileExtension(): 未预期的文件扩展名 - "
                            + sFileExtension);
            }

            return formatRet;
        }
    }
}
