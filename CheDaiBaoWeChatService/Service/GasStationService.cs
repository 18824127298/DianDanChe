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
            select g.Id,g.AddressName, g.Longitude, g.Dimension,g.Name,o.CountryMarkPrice,o.Amount from GasStation g 
join OilGun o on o.GasStationId = g.Id where g.IsValid= 1 and o.OilNumber = @OilNumber group by g.Id,g.AddressName,
 g.Longitude, g.Dimension,g.Name,o.CountryMarkPrice, o.Amount", new { OilNumber = youhao }).ToList();
        }
    }
}
