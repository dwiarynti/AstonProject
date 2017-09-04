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
    public class AssetLocationComponent
    {
        AstonContext _context = new AstonContext();
        AssetExtensions _asset = new AssetExtensions();
        LocationExtensions _location = new LocationExtensions();
        AssetLocationExtensions _assetlocation = new AssetLocationExtensions();

        public AssetLocation GetAssetLocationByID(int id)
        {
            AssetLocation result = new AssetLocation();
            result =  _assetlocation.GetAssetLocationByID(id);
            return result;

        }

        public List<AssetLocation> GetAssetLocationByLocationID(int id)
        {
            List<AssetLocation> result = new List<AssetLocation>();
            result = _assetlocation.GetAssetLocationByLocationID(id);
            return result;
        }

        public List<AssetLocation> GetAssetLocation()
        {
            List<AssetLocation> result = new List<AssetLocation>();
            result = _assetlocation.GetAssetLocation();
            return result;
        }
        public bool MoveAsset(AssetViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    var location = _location.GetLocationByCode(obj.location);
                    if (obj.listAsset != null)
                    {
                        foreach (var item in obj.listAsset)
                        {
                            AssetLocation assetlocationobj = new AssetLocation();
                            var date = DateTime.Now;
                            var asset = _asset.GetAssetInfoByCode(item);
                            assetlocationobj.AssetID = asset.ID;
                            assetlocationobj.LocationID = location.ID;
                            assetlocationobj.OnTransition = false;
                            assetlocationobj.CreatedDate = date.Date.ToString("ddMMyyyy");
                            assetlocationobj.CreatedBy = obj.CreatedBy;
                            assetlocationobj.MovementRequestDetailID = null;
                            _context.AssetLocation.Add(assetlocationobj);
                            _context.SaveChanges();
                        }


                        transaction.Commit();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

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

        public bool UpdateAssetLocation(AssetLocation obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var assetlocation = _assetlocation.GetAssetLocationByID(obj.ID);
            if(assetlocation != null)
            {
                try
                {
                    assetlocation.AssetID = obj.AssetID;
                    assetlocation.LocationID = obj.LocationID;
                    assetlocation.OnTransition = obj.OnTransition;
                    assetlocation.MovementRequestDetailID = obj.MovementRequestDetailID;
                    assetlocation.UpdatedBy = obj.UpdatedBy;
                    assetlocation.UpdatedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    _context.Entry(assetlocation).State = EntityState.Modified;
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

        public bool DeleteAssetLocation(AssetLocation obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var assetlocation = _assetlocation.GetAssetLocationByID(obj.ID);
            if (assetlocation != null)
            {
                try
                {
                  
                    assetlocation.DeletedBy = obj.DeletedBy;
                    assetlocation.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    _context.Entry(assetlocation).State = EntityState.Modified;
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
    }
}
