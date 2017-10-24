using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class AssetOpnameTransactionExtensions
    {
        AstonContext context = new AstonContext();

        public List<AssetOpnameTransaction> GetAssetStockOpname(int LocationID , string opnamedate)
        {
            var obj = context.AssetOpnameTransaction.Include(p => p.Asset).Include(p => p.Location).Where(p => p.LocationID == LocationID && p.RecordDate == opnamedate && p.DeletedDate  == null).ToList();
            return obj;
        }
    }
}
