using Aston.Business.Data;
using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore.Storage;
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

        public ResultViewModel OpnameAsset(AssetOpnameTransactionViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            List<AssetOpnameTransaction> assetopname = new List<AssetOpnameTransaction>();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                var listAsset = _context.Asset.Where(p => p.Code.Any(o => obj.AssetOpname.Contains(p.Code))).ToList();
                if (listAsset != null)
                {
                    if (listAsset.Count() == obj.AssetOpname.Count())
                    {
                        foreach(var item in listAsset.ToList())
                        {
                            var asset = obj.AssetIDList.Where(p => p == item.ID).FirstOrDefault();
                            if(asset != null)
                            {
                                AssetOpnameTransaction model = new AssetOpnameTransaction();
                                model.AssetID = item.ID;
                                model.LocationID = obj.LocationID;
                                model.CreatedDate = obj.CreatedDate;
                                model.RecordDate = obj.CreatedDate.Date.ToString("ddMMyyyy");
                                model.CreatedBy = obj.CreatedBy;
                                assetopname.Add(model);
                                listAsset.Remove(item);
                            }
                        }

                        foreach(var item in assetopname)
                        {
                            _context.AssetOpnameTransaction.Add(item);
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                        result.status = true;
                    }
                }
            }
            return result;
        }
    }
}
