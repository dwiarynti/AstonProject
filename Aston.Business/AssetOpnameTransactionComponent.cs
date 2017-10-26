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
        AssetLocationComponent assetlocationcomponent = new AssetLocationComponent();
        AssetComponent assetcomponent = new AssetComponent();

        public ResultViewModel GetAssetStockOpname(AssetOpnameTransactionViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            result.assetOpname = new List<AssetOpnameTransactionViewModel>();
            if (obj != null)
            {
                var stockopname = assetlocationcomponent.GetAssetLocationOpnameLatestByLocationID(obj.LocationID, obj.CreatedDate);
                var assetlocation = assetlocationcomponent.GetAssetLatestLocationByLocationID(obj.LocationID, obj.CreatedDate);
                if(stockopname != null)
                {
                    foreach(var item in assetlocation)
                    {
                        var asset = assetcomponent.GetAssetByID(item.AssetLatest.AssetID);
                        item.AssetName = asset.Asset.Name;
                        item.AssetBarcode = asset.Asset.Code;
                        int count = stockopname.Where(p => p.AssetLatest.AssetID == item.AssetLatest.AssetID).Count();
                        if(count > 0)
                        {
                            item.isOpname = true;
                        }
                        else
                        {
                            item.isOpname = false;
                        }
                    }
                }
                
                result.assetlocation = assetlocation;
            }
            return result;
        }
    }
}
