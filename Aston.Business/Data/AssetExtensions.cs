using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Aston.Business.Data
{
    public class AssetExtensions
    {
        AstonContext context = new AstonContext();
        public Asset GetAssetInfoByID(int id)
        {
            var obj = context.Asset.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }
        public Asset GetAssetInfoByCode (string code)
        {
            var obj = context.Asset.Where(p => p.Code == code).FirstOrDefault();
            return obj;
        }
        public List<Asset> GetAsset()
        {
            var obj = context.Asset.Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }
        public string GetLastNumberAsset()
        {
            List<int> listNo = context.Asset.ToList().Select(o => Convert.ToInt32(o.No)).ToList();

            int lastNumber = listNo.Count > 0 ? listNo.Max() : 0;
            return Convert.ToString(lastNumber+1);

        }
    
        public List<Asset> GetAssetByCategoryCode(int code)
        {
            var obj = context.Asset.Where(p => p.CategoryCD == code && p.DeletedDate == null && p.StatusCD == 1).ToList();
            return obj;
        }

        public List<Asset> SearchAsset(int categorycode, bool? ismovable, string owner)
        {
            List<Asset> obj = new List<Asset>();
            if (categorycode != null)
            {
                obj = context.Asset.Where(p => p.CategoryCD == categorycode && p.DeletedDate == null).ToList();
            }
            if (ismovable != null)
            {
                if (obj.Count != 0)
                {
                    obj = obj.Where(p => p.IsMovable == ismovable && p.DeletedDate == null).ToList();
                }
                else
                {
                    obj = context.Asset.Where(p => p.IsMovable == ismovable && p.DeletedDate == null).ToList();
                }

            }
            if (owner != null)
            {
                if (obj.Count != 0)
                {
                    obj = obj.Where(p => p.Owner == owner && p.DeletedDate == null).ToList();
                }
                else
                {
                    obj = context.Asset.Where(p => p.Owner == owner && p.DeletedDate == null).ToList();
                }
            }



            return obj;
        }

        public List<AssetViewModel> SearchAsset_SP(int categorycode, bool? ismovable, string owner, int Skip)
        {
            var result = new List<AssetViewModel>();
            var obj = new AssetViewModel();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_SearchAsset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CategoryCD", SqlDbType.Int) { Value = categorycode });
                cmd.Parameters.Add(new SqlParameter("@IsMovable", SqlDbType.Bit) { Value = ismovable });
                cmd.Parameters.Add(new SqlParameter("@Owner", SqlDbType.NVarChar) { Value = owner });
                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {

                    //a = reader.<AssetViewModel>():
                    var assetlist = dbContext.DataReaderMapToList<AseetSearchResult>(reader);
                    foreach (var asset in assetlist)
                    {
                        result.Add(new AssetViewModel() {Asset = asset });
                    }
                    cmd.Connection.Close();

                }
            }
            
            return result;
        }
    }
}
