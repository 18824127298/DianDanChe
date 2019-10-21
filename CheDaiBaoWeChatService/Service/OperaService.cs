using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatModel;
using CheDaiBaoCommonService.Data;

namespace CheDaiBaoWeChatService.Service
{
    public partial class OperaService
    {
        public List<Opera> GetByTypeAndJudgeId(OperaType operaType)
        {
            List<Opera> operaList = SqlConnections.GetOpenConnection().Search<Opera>(new Opera() { OperaType=operaType  });
            return operaList;
        }

        public List<Opera> GetByGodJudgeOrType(string typeAndGodId, OperaType? operaType)
        {
            // 没有输入条件 不查询
            if (string.IsNullOrWhiteSpace(typeAndGodId) && !operaType.HasValue)
            {
                return null;
            }

            // 局部变量
            int godId = 0;
            string tableName = "";
            string whereCondition = "";
            string OrderCondition = " order by CreateTime desc ";

            // 依据条件选择查询表名 Where语句
            if (!string.IsNullOrWhiteSpace(typeAndGodId) && typeAndGodId.IndexOf('-') >= 0)
            {
                tableName = typeAndGodId.Split('-')[0];
                godId = Convert.ToInt32(typeAndGodId.Split('-')[1]);

                whereCondition += string.Format(" and {0}.Id = @{0}Id ", tableName);
                if (operaType.HasValue)
                {
                    whereCondition += " and OperaType = @OperaType ";
                }
            }
            else if (string.IsNullOrWhiteSpace(typeAndGodId) && operaType.HasValue)
            {
                tableName = (int)operaType.Value < 200 ? "God" : "Judge";
                whereCondition += "and OperaType = @OperaType ";
            }

            // 拼接查询语句
            string strSQL = string.Format(
                @"select Opera.*, {0}.FullName, {0}.Aliases from Opera
                left join {0} on Opera.GodId = {0}.Id where Opera.IsValid  = 1 {1} {2}",
                tableName, whereCondition, OrderCondition
            );

            // 查询并返回结果
            return SqlConnections.GetOpenConnection().Query<Opera>(
                strSQL, new { GodId = godId, JudgeId = godId, OperaType = operaType }
            ).ToList();
        }

        /// <summary>
        /// 审核日志
        /// </summary>
        /// <param name="operaType"></param>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public List<Opera> GetOperaList(OperaType operaType, int relationId)
        {
            return SqlConnections.GetOpenConnection().Query<Opera>(
                "select *,(select FullName from Judge where Id=Opera.GodId) as FullName from Opera where OperaType=@OperaType and RelationId=@RelationId",
                new Opera() { OperaType= operaType, RelationId= relationId}).ToList();
        }

    }
}
