using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class OilGunService
    {
        //获取站点的商品
        public List<string> GetOilNumber(int Id)
        {
            string sql = "select OilNumber from OilGun where IsValid = 1 and GasStationId =" + Id + "Group by OilNumber";
            List<string> lstOilGun = SqlConnections.GetOpenConnection().Query<string>(sql).ToList();
            return lstOilGun;
        }

    }
}
