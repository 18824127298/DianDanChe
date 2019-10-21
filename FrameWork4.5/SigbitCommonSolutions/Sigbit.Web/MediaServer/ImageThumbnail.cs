using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Sigbit.Web.MediaServer
{
    /// <summary>
    /// 图片缩略图类
    /// </summary>
    public class ImageThumbnail
    {

        #region 构造函数
        public ImageThumbnail() { }
        #endregion

        #region 私有成员
        private int width;
        private int height;
        private int size;
        private string errMSG;
        #endregion

        #region 公共属性
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width { get { return width; } set { width = value; } }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height { get { return height; } set { Height = value; } }
        /// <summary>
        /// 图片尺寸
        /// </summary>
        public int Size { get { return size; } set { size = value; } }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMSG { get { return this.errMSG; } set { this.errMSG = value; } }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="imagePath">图片地址</param>
        /// <returns>成功true失败false</returns>
        public bool GetImage(string imagePath)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                this.width = image.Width;
                this.height = image.Height;
                image.Dispose();

                FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 按宽X长比例切图
        /// </summary>
        /// <param name="imagePath">源图地址</param>
        /// <param name="savePath">新图地址</param>
        /// <param name="cutWidth">宽度</param>
        /// <param name="cutHeight">高度</param>
        /// <returns>成功true失败false</returns>
        public bool CutImageCustomMin(string imagePath, string savePath, int cutWidth, int cutHeight)
        {
            try
            {
                System.Drawing.Image objImage = System.Drawing.Image.FromFile(imagePath);
                float x = objImage.Width;
                float y = objImage.Height;

                float xPercent = x / cutWidth;
                float yPercent = y / cutHeight;

                if (xPercent < yPercent)
                {
                    this.width = (int)((x * cutHeight) / y);
                    this.height = cutHeight;
                }
                else
                {
                    this.width = cutWidth;
                    this.height = (int)((cutWidth * y) / x);
                }

                System.Drawing.Image newimage = new Bitmap(objImage.Width, objImage.Height, PixelFormat.Format32bppRgb);
                Graphics g = Graphics.FromImage(newimage);
                g.DrawImage(objImage, 0, 0, objImage.Width, objImage.Height);
                g.Dispose();
                System.Drawing.Image thumbImage = newimage.GetThumbnailImage(this.width, this.height, null, IntPtr.Zero);
                thumbImage.Save(savePath, objImage.RawFormat);

                objImage.Dispose();
                newimage.Dispose();
                thumbImage.Dispose();

                FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 按宽X长比例切图
        /// </summary>
        /// <param name="imagePath">源图地址</param>
        /// <param name="savePath">新图地址</param>
        /// <param name="cutWidth">宽度</param>
        /// <param name="cutHeight">高度</param>
        /// <returns>成功true失败false</returns>
        public bool CutImageCustom(string imagePath, string savePath, int cutWidth, int cutHeight)
        {
            try
            {
                System.Drawing.Image objImage = System.Drawing.Image.FromFile(imagePath);
                float x = objImage.Width;
                float y = objImage.Height;

                float xPercent = x / cutWidth;
                float yPercent = y / cutHeight;

                if (xPercent < yPercent)
                {
                    this.width = (int)((x * cutHeight) / y);
                    this.height = cutHeight;
                }
                else
                {
                    this.width = cutWidth;
                    this.height = (int)((cutWidth * y) / x);
                }

                Bitmap newimage = new Bitmap(this.width, this.height, PixelFormat.Format32bppRgb);
                newimage.SetResolution(72f, 72f);
                Graphics gdiobj = Graphics.FromImage(newimage);
                gdiobj.CompositingQuality = CompositingQuality.HighQuality;
                gdiobj.SmoothingMode = SmoothingMode.HighQuality;
                gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gdiobj.FillRectangle(new SolidBrush(Color.White), 0, 0, this.width, this.height);
                Rectangle destrect = new Rectangle(0, 0, this.width, this.height);
                gdiobj.DrawImage(objImage, destrect, 0, 0, objImage.Width, objImage.Height, GraphicsUnit.Pixel);
                gdiobj.Dispose();

                System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
                ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);

                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg") { ici = codec; }
                }

                if (ici != null) newimage.Save(savePath, ici, ep); else newimage.Save(savePath, objImage.RawFormat);

                objImage.Dispose();
                newimage.Dispose();

                FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 将图片缩放到指定的宽度
        /// </summary>
        /// <param name="imagePath">源图地址</param>
        /// <param name="savePath">新图地址</param>
        /// <param name="square">宽度</param>
        /// <returns>成功true失败false</returns>
        public bool CutImageByWidth(string imagePath, string savePath, int square)
        {
            try
            {
                int cutWidth = square;

                System.Drawing.Image objImage = System.Drawing.Image.FromFile(imagePath);
                float x = objImage.Width;
                float y = objImage.Height;

                this.width = cutWidth;
                this.height = (int)((cutWidth * y) / x);

                System.Drawing.Image newimage = new Bitmap(objImage.Width, objImage.Height, PixelFormat.Format32bppRgb);
                Graphics g = Graphics.FromImage(newimage);
                g.DrawImage(objImage, 0, 0, objImage.Width, objImage.Height);
                g.Dispose();
                System.Drawing.Image thumbImage = newimage.GetThumbnailImage(this.width, this.height, null, IntPtr.Zero);
                thumbImage.Save(savePath, objImage.RawFormat);

                objImage.Dispose();
                newimage.Dispose();
                thumbImage.Dispose();

                FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 将图片缩放到指定的高度
        /// </summary>
        /// <param name="imagePath">源图地址</param>
        /// <param name="savePath">新图地址</param>
        /// <param name="square">高度</param>
        /// <returns>成功true失败false</returns>
        public bool CutImageByHeight(string imagePath, string savePath, int square)
        {
            try
            {
                int cutHeight = square;

                System.Drawing.Image objImage = System.Drawing.Image.FromFile(imagePath);
                float x = objImage.Width;
                float y = objImage.Height;

                this.height = cutHeight;
                this.width = (int)((cutHeight * x) / y);

                System.Drawing.Image newimage = new Bitmap(objImage.Width, objImage.Height, PixelFormat.Format32bppRgb);
                Graphics g = Graphics.FromImage(newimage);
                g.DrawImage(objImage, 0, 0, objImage.Width, objImage.Height);
                g.Dispose();
                System.Drawing.Image thumbImage = newimage.GetThumbnailImage(this.width, this.height, null, IntPtr.Zero);
                thumbImage.Save(savePath, objImage.RawFormat);

                objImage.Dispose();
                newimage.Dispose();
                thumbImage.Dispose();

                FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 将图片剪切到一个正方形
        /// </summary>
        /// <param name="imagePath">源图地址</param>
        /// <param name="savePath">新图地址</param>
        /// <param name="square">正方形边长</param>
        /// <returns>成功true失败false</returns>
        public bool CutImageSquare(string imagePath, string savePath, int square)
        {
            try
            {
                this.width = square;
                this.height = square;
                int cutWidth = square;
                int cutHeight = square;

                System.Drawing.Image objImage = System.Drawing.Image.FromFile(imagePath);

                if (objImage.Width >= objImage.Height)
                {
                    cutWidth = objImage.Height;
                    cutHeight = objImage.Height;
                }
                else
                {
                    cutWidth = objImage.Width;
                    cutHeight = objImage.Width;
                }

                System.Drawing.Image newimage = new Bitmap(cutWidth, cutHeight, PixelFormat.Format32bppRgb);
                Graphics g = Graphics.FromImage(newimage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Rectangle destRect = new Rectangle(0, 0, cutWidth, cutHeight);
                Rectangle srcRect = new Rectangle(0, 0, cutWidth, cutHeight);
                GraphicsUnit units = GraphicsUnit.Pixel;

                g.DrawImage(objImage, destRect, srcRect, units);
                g.Dispose();
                System.Drawing.Image thumbImage = newimage.GetThumbnailImage(this.width, this.height, null, IntPtr.Zero);
                thumbImage.Save(savePath, objImage.RawFormat);

                objImage.Dispose();
                newimage.Dispose();
                thumbImage.Dispose();

                FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                this.size = (int)fs.Length;
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.errMSG = ex.ToString();
                return false;
            }
        }

        #region 处理图片长宽显示
        public static void GetProperSize(int trueWidth, int trueHeight, int placeWidth, int placeHeight, out int showWidth, out int showHeight)
        {
            if (trueHeight < placeHeight && trueWidth < placeWidth)
            {
                showHeight = trueHeight;
                showWidth = trueWidth;
            }
            else
            {
                float x = (float)trueWidth;
                float y = (float)trueHeight;

                float xPercent = x / placeWidth;
                float yPercent = y / placeHeight;

                if (xPercent < yPercent)
                {
                    showWidth = (int)((x * placeHeight) / y);
                    showHeight = placeHeight;
                }
                else
                {
                    showWidth = placeWidth;
                    showHeight = (int)((placeWidth * y) / x);
                }
            }
        }
        #endregion
        #endregion
    }
}