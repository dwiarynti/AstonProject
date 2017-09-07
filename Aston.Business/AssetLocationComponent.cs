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

        public AssetLocationViewModel GetAssetLocationByID(int id)
        {
            AssetLocationViewModel result = new AssetLocationViewModel();

            var assetlocation =  _assetlocation.GetAssetLocationByID(id);
            if(assetlocation != null)
            {
                result.ID = assetlocation.ID;
                result.AssetID = assetlocation.AssetID;
                result.AssetName = assetlocation.Asset.Name;
                result.LocationID = assetlocation.LocationID;
                result.LocationName = assetlocation.Location.Name;
                result.OnTransition = assetlocation.OnTransition;
                result.MovementRequestDetailID = assetlocation.MovementRequestDetailID;
            }
            return result;

        }

        public List<AssetLocationViewModel> GetAssetLocationByLocationID(int id)
        {
            List<AssetLocationViewModel> result = new List<AssetLocationViewModel>();
            var assetlocation = _assetlocation.GetAssetLocationByLocationID(id);
            if(assetlocation != null)
            {
                foreach (var item in assetlocation)
                {
                    AssetLocationViewModel model = new AssetLocationViewModel();
                    model.ID = item.ID;
                    model.AssetID = item.AssetID;
                    model.AssetName = item.Asset.Name;
                    model.LocationID = item.LocationID;
                    model.LocationName = item.Location.Name;
                    model.OnTransition = item.OnTransition;
                    model.MovementRequestDetailID = item.MovementRequestDetailID;
                    result.Add(model);
                }
            }
            return result;
        }

        public List<AssetLocationViewModel> GetAssetLocation()
        {
            List<AssetLocationViewModel> result = new List<AssetLocationViewModel>();

            var assetlocation = _assetlocation.GetAssetLocation();
            if(assetlocation != null)
            {
                foreach (var item in assetlocation)
                {
                    AssetLocationViewModel model = new AssetLocationViewModel();
                    model.ID = item.ID;
                    model.AssetID = item.AssetID;
                    model.AssetName = item.Asset.Name;
                    model.LocationID = item.LocationID;
                    model.LocationName = item.Location.Name;
                    model.OnTransition = item.OnTransition;
                    model.MovementRequestDetailID = item.MovementRequestDetailID;
                    result.Add(model);
                }
            }
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
                            assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;
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

        public bool CreateAssetLocation(AssetLocation obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    
                    AssetLocation assetlocationobj = new AssetLocation();
                           
                    assetlocationobj.AssetID = obj.AssetID;
                    assetlocationobj.LocationID = obj.LocationID;
                    assetlocationobj.OnTransition = obj.OnTransition;
                    assetlocationobj.CreatedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    assetlocationobj.CreatedBy = obj.CreatedBy;
                    assetlocationobj.MovementRequestDetailID = null;
                    _context.AssetLocation.Add(assetlocationobj);
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
