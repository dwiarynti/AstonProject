using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<Asset> SearchAsset(int categorycode, bool ismovable, string owner)
        {
            List<Asset> obj = new List<Asset>();
            if (categorycode != null)
            {
                obj =  context.Asset.Where(p => p.CategoryCD == categorycode && p.DeletedDate == null).ToList();
            }
            if(ismovable != null)
            {
                if(obj.Count != 0)
                {
                    obj = obj.Where(p => p.IsMovable == ismovable && p.DeletedDate == null).ToList();
                }
                else
                {
                    obj = context.Asset.Where(p => p.IsMovable == ismovable && p.DeletedDate == null).ToList();
                }
                
            }
            if(owner != null)
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
    }
}
