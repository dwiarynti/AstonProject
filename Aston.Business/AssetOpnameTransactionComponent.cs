using Aston.Business.Data;
using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business
{
    public class AssetOpnameTransactionComponent
    {
        AstonContext _context = new AstonContext();
        AssetOpnameTransactionExtensions service = new AssetOpnameTransactionExtensions();


        public ResultViewModel GetAssetStockOpname(AssetOpnameTransactionViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            result.assetOpname = new List<AssetOpnameTransactionViewModel>();
            if (obj != null)
            {
                var stockopname = service.GetAssetStockOpname(obj.LocationID, obj.RecordDate);
                foreach (var item in stockopname)
                {
                    AssetOpnameTransactionViewModel model = new AssetOpnameTransactionViewModel();
                    model.ID = item.ID;
                    model.LocationID = item.LocationID;
                    model.LocationName = item.Location != null ? item.Location.Name : "";
                    model.AssetID = item.AssetID;
                    model.AssetName = item.Asset != null ? item.Asset.Name : "";

                    result.assetOpname.Add(model);

                }
            }
            return result;
        }
    }
}
