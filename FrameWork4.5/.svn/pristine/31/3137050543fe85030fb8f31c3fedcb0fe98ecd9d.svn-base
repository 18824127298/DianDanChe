using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Sigbit.Data.RomitSQL.RemoteServiceEngine
{
    public class RomitRemoteServiceEngine
    {
        private static RomitRemoteServiceEngine _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static RomitRemoteServiceEngine Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new RomitRemoteServiceEngine();
                return _thisInstance;
            }
        }

        public ROMMSQLResult DealWithRequest(ROMMSQLRequest rommRequest)
        {
            ROMMSQLResult ret;

            switch (rommRequest.MessageID)
            {
                case ROMXMessageID.ExecuteDataSet:
                    ret = DealWithRequest__DataSet(rommRequest);
                    break;
                case ROMXMessageID.ExecuteNonQuery:
                    ret = DealWithRequest__NonQuery(rommRequest);
                    break;
                case ROMXMessageID.ExecuteScalar:
                    ret = DealWithRequest__Scalar(rommRequest);
                    break;
                default:
                    ret = new ROMMSQLResult();
                    ret.MessageID = ROMXMessageID.None;
                    ret.ExceptionString = "(ROMIT-SERVICE-6352)未知的命令标识，MessageID=" 
                            + rommRequest.MessageID.ToString();
                    break;
            }

            return ret;
        }

        public ROMMSQLResult DealWithRequest__DataSet(ROMMSQLRequest rommRequest)
        {
            ROMMSQLResult ret = new ROMMSQLResult();
            ret.MessageID = ROMXMessageID.ExecuteDataSetResp;

            try
            {
                DataSet ds = DataHelper.Instance.ExecuteDataSet(rommRequest.SQLStatement);
                ret.ResultDataSet = ds;
            }
            catch (Exception ex)
            {
                ret.ExceptionString = ex.Message;
            }

            return ret;
        }

        public ROMMSQLResult DealWithRequest__NonQuery(ROMMSQLRequest rommRequest)
        {
            ROMMSQLResult ret = new ROMMSQLResult();
            ret.MessageID = ROMXMessageID.ExecuteNonQueryResp;

            try
            {
                int nAffectedRows = DataHelper.Instance.ExecuteNonQuery(rommRequest.SQLStatement);
                ret.ResultAffectedRowsCount = nAffectedRows;
            }
            catch (Exception ex)
            {
                ret.ExceptionString = ex.Message;
            }

            return ret;
        }

        public ROMMSQLResult DealWithRequest__Scalar(ROMMSQLRequest rommRequest)
        {
            ROMMSQLResult ret = new ROMMSQLResult();
            ret.MessageID = ROMXMessageID.ExecuteScalarResp;

            try
            {
                object objScalarResult = DataHelper.Instance.ExecuteScalar(rommRequest.SQLStatement);
                ret.ResultScalarResult = objScalarResult.ToString();
            }
            catch (Exception ex)
            {
                ret.ExceptionString = ex.Message;
            }

            return ret;
        }
    }
}
