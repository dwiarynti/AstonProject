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
        public Asset GetAssetByCode (string barcode)
        {
            Asset result = new Asset();
            result = _asset.GetAssetInfoByCode(barcode);
            return result;
        }

        public List<Asset> GetAsset()
        {
            List<Asset> result = new List<Asset>();
            result = _asset.GetAsset();
            return result;

        }

        public Asset GetAssetByID(int id)
        {
            Asset result = new Asset();
            result = _asset.GetAssetInfoByID(id);
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
                    asset.PurchaseDate = obj.PurchaseDate;
                    asset.PurchasePrice = obj.PurchasePrice;
                    asset.DepreciationDuration = obj.DepreciationDuration;
                    asset.DisposedDate = obj.DisposedDate;
                    asset.ManufactureDate = obj.ManufactureDate;
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

        public bool UpdateAsset(Asset obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var asset = _asset.GetAssetInfoByID(obj.ID);
            if(asset!= null)
            {
                try
                {
                    asset.Code = obj.Code;
                    asset.Description = obj.Description;
                    asset.No = obj.No;
                    asset.Name = obj.Name;
                    asset.IsMovable = obj.IsMovable;
                    asset.Owner = obj.Owner;
                    asset.PurchaseDate = obj.PurchaseDate;
                    asset.PurchasePrice = obj.PurchasePrice;
                    asset.DepreciationDuration = obj.DepreciationDuration;
                    asset.DisposedDate = obj.DisposedDate;
                    asset.ManufactureDate = obj.ManufactureDate;
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
