using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace BC_YN
{
    public partial class Form_TestIMG : Form
    {
        public Form_TestIMG()
        {
            InitializeComponent();
        }

        #region variable

        Bitmap _bmpSrc;
        Bitmap _bmpGray;
        Bitmap _bmpClear;
        Bitmap _bmpVailid;
        Bitmap[] _bmpPics;
        List<CodeId> listCodes = new List<CodeId>();


        #endregion

        /// <summary>
        /// 获得随机码图片，可根据需要重写
        /// </summary>
        Bitmap BmpSrc
        {
            get
            {
                //_bmpSrc = SecurityCode_Generate.GenerateImgCode();
                //_bmpSrc = new Bitmap("44.bmp");
                _bmpSrc = new Bitmap("QQ图片20130617134646.jpg");
                return _bmpSrc;
            }
        }

        /// <summary>
        /// 处理图片
        /// </summary>
        private void DealWithBitmap()
        {
            int nDgGrayValue = this.textBox_dgGrayValue.Text.Trim() == "" ? 128 : int.Parse(this.textBox_dgGrayValue.Text.Trim());
            Bitmap bmpSrc = BmpSrc;
            this.label_Src.Text=SecurityCode_Identify.GetDgGrayValue(bmpSrc).ToString();

            //========(1).灰化==============================================================
            _bmpGray = SecurityCode_Identify.GetGrayByPixels(bmpSrc);
            this.label_Gray.Text = SecurityCode_Identify.GetDgGrayValue(_bmpGray).ToString();

            //========(1)-1去边框===========================================================
            SecurityCode_Identify.ClearPicBorder(_bmpGray, 3);


            //=========(2)二值化============================================================
            _bmpClear = SecurityCode_Identify.GetPicByBinaryzation(_bmpGray, nDgGrayValue);


            //========(3).获取有效图，调整分界值试试=========================================
            _bmpVailid = SecurityCode_Identify.GetPicValidByValue(_bmpClear, nDgGrayValue, 4);
            this.label_Vailid.Text = SecurityCode_Identify.GetDgGrayValue(_bmpVailid).ToString();

            _bmpPics = SecurityCode_Identify.GetSplitPics(_bmpVailid, 4, 1);
            for (int i = 0; i < _bmpPics.Length; i++)
            {
                _bmpPics[i] = SecurityCode_Identify.GetSinglePicValidByValue(_bmpPics[i], nDgGrayValue, 6, 10);
            }
        }

        /// <summary>
        /// 展示图片
        /// </summary>
        private void DisplayIMGs()
        {
            this.pictureBox_Base.Image = _bmpSrc;
            this.pictureBox_gray.Image = _bmpGray;
            this.pictureBox_Clear.Image = _bmpClear;
            this.pictureBox_Vailid.Image = _bmpVailid;
            this.pictureBox_pic1.Image = _bmpPics[0];
            this.pictureBox_pic2.Image = _bmpPics[1];
            this.pictureBox_pic3.Image = _bmpPics[2];
            this.pictureBox_pic4.Image = _bmpPics[3];
        }

        private void Identity()
        {
            int[] nNum;
            int nValue = 0;
            int nResult=0;
            string sResult = "";
            for (int i = 0; i < 4; i++)
            {
                nNum = new int[10]{0,0,0,0,0,0,0,0,0,0};
                nResult = 0;
                for (int j = 0; j < _bmpPics[i].Height; j++)
                {
                    for (int k = 0; k < _bmpPics[i].Width; k++)
                    {
                        nValue = _bmpPics[i].GetPixel(k, j).B;
                        foreach (CodeId ci in listCodes)
                        {
                            if (ci.Codes[j * 6 + k] == nValue)
                            {
                                nNum[ci.Id]++;
                            }
                        }
                    }
                }

                for (int j = 0; j < 10; j++)
                {
                    if (nNum[nResult] < nNum[j])
                    {
                        nResult = j;
                    }
                }
                sResult = sResult + nResult.ToString();
            }

            this.textBox_result.Text = sResult;
        }

        private void initListCodes()
        {
            listCodes.Add(new CodeId(0, new int[] { 255, 0, 0, 0, 0, 255, 0, 0, 0, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 255, 0, 0, 0, 0, 255 }));
            listCodes.Add(new CodeId(5, new int[] { 255, 255, 0, 0, 0, 0, 255, 0, 0, 0, 0, 255, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 255, 255, 0, 255, 0, 0, 0, 0, 255, 255 }));
            listCodes.Add(new CodeId(3, new int[] { 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 0, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 255, 255 }));
            listCodes.Add(new CodeId(4, new int[] { 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 255, 255, 0, 0, 255, 0, 0, 0, 0, 0, 0, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255 }));
            listCodes.Add(new CodeId(2, new int[] { 255, 255, 0, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
            listCodes.Add(new CodeId(7, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255 }));
            listCodes.Add(new CodeId(1, new int[] { 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255 }));
            listCodes.Add(new CodeId(9, new int[] { 255, 255, 0, 0, 0, 255, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 255, 0, 0, 0, 255, 255, 255 }));
            listCodes.Add(new CodeId(8, new int[] { 255, 0, 0, 0, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 0, 255, 255, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 255, 0, 255, 255, 0, 0, 0, 255, 255 }));
            listCodes.Add(new CodeId(6, new int[] { 255, 255, 255, 0, 0, 0, 255, 0, 0, 0, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 0, 255, 255, 0, 0, 0, 255, 255 }));
        }

        private static Bitmap[] DealWithBitmap(Image img)
        {
            Bitmap bmpSrc = new Bitmap(img);
            Bitmap[] btmRets = null;
            //========(1).灰化==============================================================
            bmpSrc = SecurityCode_Identify.GetGrayByPixels(bmpSrc);

            //========(1)-1去边框===========================================================
            SecurityCode_Identify.ClearPicBorder(bmpSrc, 1);


            //=========(2)二值化============================================================
            bmpSrc = SecurityCode_Identify.GetPicByBinaryzation(bmpSrc, 140);


            //========(3).获取有效图，调整分界值试试=========================================
            bmpSrc = SecurityCode_Identify.GetPicValidByValue(bmpSrc, 140, 4);

            //========(4).分割，并获得有效区域，这里取6*10的宽度=============================
            btmRets = SecurityCode_Identify.GetSplitPics(bmpSrc, 4, 1);
            for (int i = 0; i < btmRets.Length; i++)
            {
                btmRets[i] = SecurityCode_Identify.GetSinglePicValidByValue(btmRets[i], 140, 6, 10);
            }

            return btmRets;
        }


        private void button_execute_Click(object sender, EventArgs e)
        {
            DealWithBitmap();
            DisplayIMGs();
            Identity();

            //Bitmap btm = new Bitmap("2.jpg");
            //string result = BreakCode_YN.Identity(btm);
            //this.textBox_result.Text = result;
        }

        private void Form_TestIMG_Load(object sender, EventArgs e)
        {
            initListCodes();
        }
    }
}
