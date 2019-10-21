using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Util
{
    public class PictureUtil
    {
        //public static string PicturetoBase64(string sPicture)
        //{
        //    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(sPicture);
        //    using (MemoryStream ms1 = new MemoryStream())
        //    {
        //        bmp1.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        byte[] arr1 = new byte[ms1.Length];
        //        ms1.Position = 0;
        //        ms1.Read(arr1, 0, (int)ms1.Length);
        //        ms1.Close();
        //        return Convert.ToBase64String(arr1);
        //    }
        //}
        public static string FileToBase64(string fileName)
        {
            string result = "";
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    result = Convert.ToBase64String(byteArray);
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }
    }
}
