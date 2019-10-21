using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.RomitSQL
{
    public enum ROMXMessageID
    {
        None,
        ExecuteDataSet,
        ExecuteDataSetResp,
        ExecuteNonQuery,
        ExecuteNonQueryResp,
        ExecuteScalar,
        ExecuteScalarResp
    }
}
