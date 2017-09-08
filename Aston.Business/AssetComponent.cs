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
        PrefComponent _pref = new PrefComponent();
        public AssetViewModel GetAssetByCode (string barcode)
        {
            AssetViewModel result = new AssetViewModel();
         
            var asset = _asset.GetAssetInfoByCode(barcode);
            var categoryCDName = _pref.GetPrefByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetPrefByStatusCode(asset.StatusCD);

            result.Code = asset.Code;
            result.Description = asset.Description;
            result.No = asset.No;
            result.Name = asset.Name;
            result.IsMovable = asset.IsMovable;
            result.Owner = asset.Owner;
            result.PurchaseDate = asset.PurchaseDate;
            result.PurchasePrice = asset.PurchasePrice;
            result.DepreciationDuration = asset.DepreciationDuration;
            result.DisposedDate = asset.DisposedDate;
            result.ManufactureDate = asset.ManufactureDate;
            result.CategoryCD = asset.CategoryCD;
            result.CategoryCDName = categoryCDName.Value;
            result.StatusCD = asset.StatusCD;
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
                var categoryCDName = _pref.GetPrefByCategoryCode(item.CategoryCD);
                var statusCDName = _pref.GetPrefByStatusCode(item.StatusCD);

                model.ID = item.ID;
                model.Code = item.Code;
                model.Description = item.Description;
                model.No = item.No;
                model.Name = item.Name;
                model.IsMovable = item.IsMovable;
                model.Owner = item.Owner;
                model.PurchaseDate = item.PurchaseDate;
                model.PurchasePrice = item.PurchasePrice;
                model.DepreciationDuration = item.DepreciationDuration;
                model.DisposedDate = item.DisposedDate;
                model.ManufactureDate = item.ManufactureDate;
                model.CategoryCD = item.CategoryCD;              
                model.CategoryCDName = categoryCDName.Value;             
                model.StatusCD = item.StatusCD;
                model.StatusCDName = statusCDName.Value;

                result.Add(model);

            }
            return result;

        }

        public AssetViewModel GetAssetByID(int id)
        {
            AssetViewModel result = new AssetViewModel();

            var asset = _asset.GetAssetInfoByID(id);
            var categoryCDName = _pref.GetPrefByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetPrefByStatusCode(asset.StatusCD);

            result.Code = asset.Code;
            result.Description = asset.Description;
            result.No = asset.No;
            result.Name = asset.Name;
            result.IsMovable = asset.IsMovable;
            result.Owner = asset.Owner;
            result.PurchaseDate = asset.PurchaseDate;
            result.PurchasePrice = asset.PurchasePrice;
            result.DepreciationDuration = asset.DepreciationDuration;
            result.DisposedDate = asset.DisposedDate;
            result.ManufactureDate = asset.ManufactureDate;
            result.CategoryCD = asset.CategoryCD;
            result.CategoryCDName = categoryCDName.Value;
            result.StatusCD = asset.StatusCD;
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
                    obj.No = _asset.GetLastNumberAsset();
                    obj.SubCategory = _generatecode.SubCategoryAsset(obj.CategoryCD);
                    obj.Number = _generatecode.Number(obj.No);
                    obj.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, obj.Number);


                    Asset asset = new Asset();
                    asset.ID = obj.ID;
                    asset.Code = obj.Code;
                    asset.Description = obj.Description;
                    asset.No = obj.Number;
                    asset.Name = obj.Name;
                    asset.IsMovable = obj.IsMovable;
                    asset.Owner = obj.Owner;
                    asset.PurchaseDate =Convert.ToDateTime(obj.PurchaseDate).ToString("ddMMyyyy");
                    asset.PurchasePrice = obj.PurchasePrice;
                    asset.DepreciationDuration = obj.DepreciationDuration != null ? Convert.ToDateTime(obj.DepreciationDuration).ToString("ddMMyyyy") : null;
                    asset.DisposedDate = obj.DisposedDate != null ? Convert.ToDateTime(obj.DisposedDate).ToString("ddMMyyyy") : null;
                    asset.ManufactureDate = Convert.ToDateTime(obj.ManufactureDate).ToString("ddMMyyyy");
                    asset.CategoryCD = obj.CategoryCD;
                    asset.StatusCD = obj.StatusCD;
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
            var asset = _asset.GetAssetInfoByID(obj.ID);
            if(asset!= null)
            {
                try
                {
                  
                    obj.SubCategory = _generatecode.SubCategoryAsset(obj.CategoryCD);
                    obj.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, asset.No);
                    if (asset.Code != obj.Code)
                    {

                        asset.Code = obj.Code;
                    }
                    asset.Description = obj.Description;
                    asset.Name = obj.Name;
                    asset.IsMovable = obj.IsMovable;
                    asset.Owner = obj.Owner;
                    asset.PurchaseDate = Convert.ToDateTime(obj.PurchaseDate).ToString("ddMMyyyy");
                    asset.PurchasePrice = obj.PurchasePrice;
                    asset.DepreciationDuration = obj.DepreciationDuration;
                    asset.ManufactureDate = Convert.ToDateTime(obj.ManufactureDate).ToString("ddMMyyyy");
                    asset.CategoryCD = obj.CategoryCD;
                    asset.StatusCD = obj.StatusCD;
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

       

    }
}
