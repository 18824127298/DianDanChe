using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.RomitSQL
{
    public class ROMMSQLRequest : ROMMBase
    {
        private string _SQLStatement = "";
        /// <summary>
        /// SQLÓï¾ä
        /// </summary>
        public string SQLStatement
        {
            get { return _SQLStatement; }
            set { _SQLStatement = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("sql", this.SQLStatement);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.SQLStatement = GetAStringValue("sql");
        }
    }
}
