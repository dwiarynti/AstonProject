using Aston.Business.Data;
using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business
{
    public class AssetComponent
    {
        AstonContext _context = new AstonContext();
        AssetExtensions _asset = new AssetExtensions();
        LocationExtensions _location = new LocationExtensions();
        GenerateCodeComponent _generatecode = new GenerateCodeComponent();
        LookupListComponent _pref = new LookupListComponent();
        public AssetViewModel GetAssetByCode (string barcode)
        {
            AssetViewModel result = new AssetViewModel();
         
            var asset = _asset.GetAssetInfoByCode(barcode);
            var categoryCDName = _pref.GetLookupByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetLookupByStatusCode(asset.StatusCD);

            result.Asset.Code = asset.Code;
            result.Asset.Description = asset.Description;
            result.Asset.No = asset.No;
            result.Asset.Name = asset.Name;
            result.Asset.IsMovable = asset.IsMovable;
            result.Asset.Owner = asset.Owner;
            result.Asset.PurchaseDate = asset.PurchaseDate;
            result.Asset.PurchasePrice = asset.PurchasePrice;
            result.Asset.DepreciationDuration = asset.DepreciationDuration;
            result.Asset.DisposedDate = asset.DisposedDate;
            result.Asset.ManufactureDate = asset.ManufactureDate;
            result.Asset.CategoryCD = asset.CategoryCD;
            result.CategoryCDName = categoryCDName.Value;
            result.Asset.StatusCD = asset.StatusCD;
            result.StatusCDName = statusCDName.Value;
            return result;
        }

       public List<AssetViewModel> GetAsset()
        {
            List<AssetViewModel> result = new List<AssetViewModel>();
            var asset = _asset.GetAsset();
            foreach(var item in asset)
            {
                AssetViewModel model = new AssetViewModel();
                var categoryCDName = _pref.GetLookupByCategoryCode(item.CategoryCD);
                var statusCDName = _pref.GetLookupByStatusCode(item.StatusCD);

                model.Asset.ID = item.ID;
                model.Asset.Code = item.Code;
                model.Asset.Description = item.Description;
                model.Asset.No = item.No;
                model.Asset.Name = item.Name;
                model.Asset.IsMovable = item.IsMovable;
                model.Asset.Owner = item.Owner;
                model.Asset.PurchaseDate = item.PurchaseDate;
                model.Asset.PurchasePrice = item.PurchasePrice;
                model.Asset.DepreciationDuration = item.DepreciationDuration;
                model.Asset.DisposedDate = item.DisposedDate;
                model.Asset.ManufactureDate = item.ManufactureDate;
                model.Asset.CategoryCD = item.CategoryCD;              
                model.CategoryCDName = categoryCDName.Value;             
                model.Asset.StatusCD = item.StatusCD;
                model.StatusCDName = statusCDName.Value;

                result.Add(model);

            }
            return result;

        }

        public AssetViewModel GetAssetByID(int id)
        {
            AssetViewModel result = new AssetViewModel();

            var asset = _asset.GetAssetInfoByID(id);
            var categoryCDName = _pref.GetLookupByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetLookupByStatusCode(asset.StatusCD);

            result.Asset.Code = asset.Code;
            result.Asset.Description = asset.Description;
            result.Asset.No = asset.No;
            result.Asset.Name = asset.Name;
            result.Asset.IsMovable = asset.IsMovable;
            result.Asset.Owner = asset.Owner;
            result.Asset.PurchaseDate = asset.PurchaseDate;
            result.Asset.PurchasePrice = asset.PurchasePrice;
            result.Asset.DepreciationDuration = asset.DepreciationDuration;
            result.Asset.DisposedDate = asset.DisposedDate;
            result.Asset.ManufactureDate = asset.ManufactureDate;
            result.Asset.CategoryCD = asset.CategoryCD;
            result.CategoryCDName = categoryCDName.Value;
            result.Asset.StatusCD = asset.StatusCD;
            result.StatusCDName = statusCDName.Value;

            return result;
        }

        public bool CreateAsset(AssetViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if(obj != null)
            {
                try
                {
                    obj.Asset.No = _asset.GetLastNumberAsset();
                    obj.SubCategory = _generatecode.SubCategoryAsset(Convert.ToInt16(obj.Asset.CategoryCD));
                    obj.Number = _generatecode.Number(obj.Asset.No);
                    obj.Asset.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, obj.Number);


                    Asset asset = new Asset();
                    asset.ID = obj.Asset.ID;
                    asset.Code = obj.Asset.Code;
                    asset.Description = obj.Asset.Description;
                    asset.No = obj.Number;
                    asset.Name = obj.Asset.Name;
                    asset.IsMovable = obj.Asset.IsMovable;
                    asset.Owner = obj.Asset.Owner;
                    asset.PurchaseDate =Convert.ToDateTime(obj.Asset.PurchaseDate).ToString("ddMMyyyy");
                    asset.PurchasePrice = obj.Asset.PurchasePrice;
                    asset.DepreciationDuration = obj.Asset.DepreciationDuration != null ? Convert.ToDateTime(obj.Asset.DepreciationDuration).ToString("ddMMyyyy") : null;
                    asset.DisposedDate = obj.Asset.DisposedDate != null ? Convert.ToDateTime(obj.Asset.DisposedDate).ToString("ddMMyyyy") : null;
                    asset.ManufactureDate = Convert.ToDateTime(obj.Asset.ManufactureDate).ToString("ddMMyyyy");
                    asset.CategoryCD = Convert.ToInt16(obj.Asset.CategoryCD);
                    asset.StatusCD = obj.Asset.StatusCD;
                    asset.CreatedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    asset.CreatedBy = obj.CreatedBy;

                    _context.Asset.Add(asset);
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool UpdateAsset(AssetViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var asset = _asset.GetAssetInfoByID(obj.Asset.ID);
            if(asset!= null)
            {
                try
                {
                  
                    obj.SubCategory = _generatecode.SubCategoryAsset(Convert.ToInt16(obj.Asset.CategoryCD));
                    obj.Asset.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, asset.No);
                    if (asset.Code != obj.Asset.Code)
                    {

                        asset.Code = obj.Asset.Code;
                    }
                    asset.Description = obj.Asset.Description;
                    asset.Name = obj.Asset.Name;
                    asset.IsMovable = obj.Asset.IsMovable;
                    asset.Owner = obj.Asset.Owner;
                    asset.PurchaseDate = obj.Asset.PurchaseDate.Replace("/", string.Empty); 
                    asset.PurchasePrice = obj.Asset.PurchasePrice;
                    asset.DepreciationDuration = obj.Asset.DepreciationDuration;
                    asset.ManufactureDate = obj.Asset.ManufactureDate.Replace("/", string.Empty);
                    asset.CategoryCD = Convert.ToInt16(obj.Asset.CategoryCD);
                    asset.StatusCD = obj.Asset.StatusCD;
                    asset.UpdatedBy = obj.UpdatedBy;
                    asset.UpdatedDate = DateTime.Now.Date.ToString("ddMMyyyy");

                    _context.Entry(asset).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool DeleteAsset(Asset obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var asset = _asset.GetAssetInfoByID(obj.ID);
            if(asset != null)
            {
                try
                {
                    asset.DeletedBy = obj.DeletedBy;
                    asset.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    _context.Entry(asset).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch(Exception ex)
                {
                    result = false;
                    transaction.Rollback();
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public List<Asset> GetAssetByCategoryCode(int code)
        {
          return  _asset.GetAssetByCategoryCode(code);
        }
       
        public List<AssetViewModel> SearchAsset(AssetViewModel obj)
        {
            List<AssetViewModel> result = new List<AssetViewModel>();

            //if (Convert.ToBoolean(obj.isSearch))
            //{
                if (obj != null)
                {
                    var asset = _asset.SearchAsset_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner, obj.Skip);
                    foreach (var item in asset)
                    {
                        AssetViewModel model = new AssetViewModel();
                        model.Asset = new AseetSearchResult();
                        var categoryCDName = _pref.GetLookupByCategoryCode(Convert.ToInt16(item.Asset.CategoryCD));
                        var statusCDName = _pref.GetLookupByStatusCode(item.Asset.StatusCD);

                        model.Asset.ID = item.Asset.ID;
                        model.Asset.Code = item.Asset.Code;
                        model.Asset.Description = item.Asset.Description;
                        model.Asset.No = item.Asset.No;
                        model.Asset.Name = item.Asset.Name;
                        model.Asset.IsMovable = item.Asset.IsMovable;
                        model.Asset.Owner = item.Asset.Owner;
                        model.Asset.PurchaseDate = item.Asset.PurchaseDate;
                        model.Asset.PurchasePrice = item.Asset.PurchasePrice;
                        model.Asset.DepreciationDuration = item.Asset.DepreciationDuration;
                        model.Asset.DisposedDate = item.Asset.DisposedDate;
                        model.Asset.ManufactureDate = item.Asset.ManufactureDate;
                        model.Asset.CategoryCD = item.Asset.CategoryCD;
                        model.CategoryCDName = categoryCDName.Value;
                        model.Asset.StatusCD = item.Asset.StatusCD;
                        model.StatusCDName = statusCDName.Value;
                        model.Asset.TotalRow = item.Asset.TotalRow;

                    result.Add(model);
                    }
                }
            //}
            //else
            //{
            //    result = GetAsset();
            //}

            return result;
        }

    }
}
