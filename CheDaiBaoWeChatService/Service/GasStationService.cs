using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class GasStationService
    {
        public List<GasStation> GetByYouhao(string youhao)
        {
            return
            SqlConnections.GetOpenConnection().Query<GasStation>(@"
            select GETDATE(), g.Id,g.AddressName, g.Longitude, g.Dimension,g.Name,
case when o.CountryPointTime > GETDATE() then o.CountryMarkPrice else o.NewCountryPrice end as CountryMarkPrice,
case when o.PointTime > GETDATE() then o.Amount else o.NewAmount end as Amount from GasStation g 
join OilGun o on o.GasStationId = g.Id where g.IsValid= 1 
and o.IsValid = 1 and o.OilNumber = @OilNumber group by g.Id,g.AddressName,
 g.Longitude, g.Dimension,g.Name,case when o.CountryPointTime > GETDATE() then o.CountryMarkPrice else o.NewCountryPrice end, 
 case when o.PointTime > GETDATE() then o.Amount else o.NewAmount end", new { OilNumber = youhao }).ToList();
        }
    }
}
