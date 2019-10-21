using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;

namespace Sigbit.Common.WordProcess.Excel
{
    public class ExcelUtil
    {
        public static void MergeFile(ArrayList inputFiles, ArrayList sheetNames, string sOutPutFile)
        {
            MergeFile((string[])inputFiles.ToArray(typeof(string)), (string[])sheetNames.ToArray(typeof(string)), sOutPutFile);
        }

        public static void MergeFile(string[] inputFiles, string[] sheetNames, string sOutPutFile)
        {
            //========== 1.检查参数 ==========
            if ((sheetNames != null) && (sheetNames.Length > 0))
            {
                if (sheetNames.Length != inputFiles.Length)
                {
                    throw new Exception("参数sheetName的数量和输入文件数量不相等!");
                }
            }
            else
            {
                sheetNames = new string[inputFiles.Length];
                for( int i=0;i<inputFiles.Length;i++)
                {
                    sheetNames[i] = Path.GetFileNameWithoutExtension(inputFiles[i]);
                }
            }

            if (!Directory.Exists(Path.GetDirectoryName(sOutPutFile)))
            {
                throw new Exception("输出文件路径不存在：" + Path.GetFullPath(sOutPutFile));
            }

            ArrayList alSheetName = new ArrayList();
            alSheetName.Add("");
            ExcelFile outputFile = new ExcelFile();
            //========== 2.复制Sheet ==========
            int nIndex = 0;
            foreach (string sFileName in inputFiles)
            {
                
                if (!File.Exists(sFileName))
                {
                    throw new Exception("输入文件不存在：" + sFileName);
                }
                ExcelFile inputFile = new ExcelFile();
                inputFile.LoadXls(sFileName);


                string sheetName = sheetNames[nIndex];

                int nSheetNameIndex = 2;
                while (alSheetName.IndexOf(sheetName) >=0)
                {
                    sheetName = sheetNames[nIndex] + "_" + nSheetNameIndex.ToString();
                    nSheetNameIndex++;
                }

                alSheetName.Add(sheetName);

                ExcelWorksheet worksheet = outputFile.Worksheets.Add(sheetName);
                CellRange range = inputFile.Worksheets[0].GetUsedCellRange();
                //CellRange range = inputFile.Worksheets[0].Cells.GetSubrangeAbsolute(0, 0, inputFile.Worksheets[0].Rows, inputFile.Worksheets[0].Columns.Count);
                range.CopyTo(worksheet, 0, 0);
                for(int i=0;i<inputFile.Worksheets[0].Columns.Count;i++)
                {
                    //worksheet.Columns[i].Style = inputFile.Worksheets[0].Columns[i].Style;
                    worksheet.Columns[i].Width = inputFile.Worksheets[0].Columns[i].Width;
                }

                nIndex++;
            }

            //========== 3.输出文件 ==========
            outputFile.SaveXls(sOutPutFile);
        }
    }
}
