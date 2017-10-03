using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class AssetLocationExtensions
    {
        AstonContext context = new AstonContext();
        public AssetLocation GetAssetLocationByID(int id)
        {
            var obj = context.AssetLocation.Include(p => p.Asset).Include(p => p.Location).Where(p => p.ID == id).FirstOrDefault();
         
            return obj;
        }

        public List<AssetLocation> GetAssetLocationByLocationID(int id)
        {
            var obj = context.AssetLocation.Include(p => p.Asset).Include(p => p.Location).Where(p => p.LocationID == id && p.DeletedDate == null && p.DeletedBy == null).ToList();
            return obj;
        }

        public List<AssetLocation> GetAssetLocation()
        {
            var obj = context.AssetLocation.Include(p=>p.Asset).Include(p=>p.Location).Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }

        public List<AssetLocation> GetAssetLocationByMovementDetailID(int id )
        {
            var obj = context.AssetLocation.Include(p=>p.Asset).Include(p=>p.Location).Where(p => p.MovementRequestDetailID == id && p.DeletedDate == null).ToList();
            return obj;
        }

        public List<AssetLocationViewModel> Pagination_AssetLocation_SP(int Skip)
        {
            var result = new List<AssetLocationViewModel>();
            var obj = new AssetLocationViewModel();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_AssetLocation_Pagination";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {
                    var assetlocationlist = dbContext.DataReaderMapToList<AssetLocationPagination>(reader);
                    foreach (var assetlocation in assetlocationlist)
                    {
                        result.Add(new AssetLocationViewModel() {AssetLocation = assetlocation});
                    }
                    cmd.Connection.Close();
                }
            }

            return result;
        }
    }
}
