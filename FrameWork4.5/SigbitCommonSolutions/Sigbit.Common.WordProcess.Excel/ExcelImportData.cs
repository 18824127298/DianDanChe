using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    public class ExcelImportData
    {
        private ArrayList _rows = new ArrayList();

        public int Count
        {
            get { return _rows.Count; }
        }

        public string[] this[int nIndex]
        {
            get { return (string[])_rows[nIndex]; }
        }

        public string this[int nRow, int nCol]
        {
            get { return ((string[])_rows[nRow])[nCol]; }
        }

        public void AddLine(string [] arrLine)
        {
            _rows.Add(arrLine);
        }
    }
}
