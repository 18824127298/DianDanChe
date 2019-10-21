using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace BC_YN
{
    public class BreakCode_YN
    {
        private static List<CodeId> listCodes = new List<CodeId>() { 
            new CodeId(0, new int[] { 255, 0, 0, 0, 0, 255, 0, 0, 0, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 0, 255, 0, 0, 0, 0, 255 }),
            new CodeId(1, new int[] { 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255 }),
            new CodeId(2, new int[] { 255, 255, 0, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }),
            new CodeId(3, new int[] { 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 0, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 255, 255 }),
            new CodeId(4, new int[] { 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 255, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 255, 255, 0, 0, 255, 0, 0, 0, 0, 0, 0, 255, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 255 }),
            new CodeId(5, new int[] { 255, 255, 0, 0, 0, 0, 255, 0, 0, 0, 0, 255, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 255, 255, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 0, 0, 255, 255, 0, 255, 0, 0, 0, 0, 255, 255 }),
            new CodeId(6, new int[] { 255, 255, 255, 0, 0, 0, 255, 0, 0, 0, 255, 255, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 0, 255, 255, 0, 0, 0, 255, 255 }),
            new CodeId(7, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255, 255, 255, 0, 255, 255, 255 }),
            new CodeId(8, new int[] { 255, 0, 0, 0, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 255, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 0, 255, 255, 0, 0, 0, 0, 255, 255, 255, 0, 0, 0, 0, 255, 255, 0, 255, 255, 0, 0, 0, 255, 255 }),
            new CodeId(9, new int[] { 255, 255, 0, 0, 0, 255, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 0, 0, 0, 255, 255, 0, 255, 0, 0, 255, 255, 0, 255, 0, 0, 255, 0, 0, 255, 255, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 255, 255, 0, 0, 0, 255, 0, 0, 0, 255, 255, 255 })};

        private static Bitmap[] DealWithBitmap(Image img)
        {
            Bitmap bmpSrc = new Bitmap(img);
            Bitmap[] btmRets = null;
            //========(1).灰化==============================================================
            bmpSrc = SecurityCode_Identify.GetGrayByPixels(bmpSrc);

            //========(1)-1去边框===========================================================
            SecurityCode_Identify.ClearPicBorder(bmpSrc, 3);


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

        public static string Identity(Image img)
        {
            Bitmap[] btmSrcs = DealWithBitmap(img);
            int[] nNum;
            int nValue = 0;
            int nResult = 0;
            string sResult = "";
            for (int i = 0; i < 4; i++)
            {
                nNum = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                nResult = 0;
                for (int j = 0; j < btmSrcs[i].Height; j++)
                {
                    for (int k = 0; k < btmSrcs[i].Width; k++)
                    {
                        nValue = btmSrcs[i].GetPixel(k, j).B;
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

            return sResult;
        }
    }

    public class CodeId
    {
        private int[] _codes;

        public int[] Codes
        {
            get
            {
                return _codes;
            }
        }

        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public CodeId(int nId, int[] nCodes)
        {
            _id = nId;
            _codes = nCodes;
        }
    }

}
