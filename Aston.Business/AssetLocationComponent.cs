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
        MovementRequestExtensions _movementrequest = new MovementRequestExtensions();
        public AssetLocationViewModel GetAssetLocationByID(int id)
        {
            AssetLocationViewModel result = new AssetLocationViewModel();

            var assetlocation =  _assetlocation.GetAssetLocationByID(id);
            if(assetlocation != null)
            {
                result.AssetLocation.ID = assetlocation.ID;
                result.AssetLocation.AssetID = assetlocation.AssetID;
                result.AssetLocation.AssetName = assetlocation.Asset.Name;
                result.AssetLocation.LocationID = assetlocation.LocationID;
                result.AssetLocation.LocationName = assetlocation.Location.Name;
                result.AssetLocation.OnTransition = assetlocation.OnTransition;
                result.AssetLocation.MovementRequestDetailID = assetlocation.MovementRequestDetailID;
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
                    model.AssetLocation.ID = item.ID;
                    model.AssetLocation.AssetID = item.AssetID;
                    model.AssetLocation.AssetName = item.Asset.Name;
                    model.AssetLocation.LocationID = item.LocationID;
                    model.AssetLocation.LocationName = item.Location.Name;
                    model.AssetLocation.OnTransition = item.OnTransition;
                    model.AssetLocation.MovementRequestDetailID = item.MovementRequestDetailID;
                    model.CreatedDate = item.CreatedDate;
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
                    model.AssetLocation.ID = item.ID;
                    model.AssetLocation.AssetID = item.AssetID;
                    model.AssetLocation.AssetName = item.Asset.Name;
                    model.AssetLocation.LocationID = item.LocationID;
                    model.AssetLocation.LocationName = item.Location.Name;
                    model.AssetLocation.OnTransition = item.OnTransition;
                    model.AssetLocation.MovementRequestDetailID = item.MovementRequestDetailID;
                    result.Add(model);
                }
            }
            return result;
        }

        public ResultViewModel MoveAsset(AssetViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            List<AssetLocation> listassetlocation = new List<AssetLocation>();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            List<string> Asset = new List<string>();
            List<string> Location = new List<string>();
            List<string> NotAssetLocation = new List<string>();

            if (obj != null)
            {
                try
                {
                    foreach (var item in obj.listAsset)
                    {
                        char[] array = item.ToCharArray();
                        string data = "";
                        int i = 0;
                        foreach (var item2 in array)
                        {
                            i += 1;
                            if (i >= 5 && i <= 8)
                            {
                                data += item2;
                            }
                        }
                        if (data == "0101")
                        {
                            Asset.Add(item);
                            data = "";
                        }
                        else if (data == "0202")
                        {
                            Location.Add(item);
                            data = "";
                        }
                        else
                        {
                            NotAssetLocation.Add(item);
                            data = "";
                        }

                        if (NotAssetLocation.Count == 0)
                        {
                            if (Location.Count > 0 && Location.Count <= 1)
                            {
                                var location = _location.GetLocationByCode(Location[0]);
                                if (location.ID == obj.locationID)
                                {
                                    var movementrequestdetail = _context.MovementRequestDetail.Where(p => p.ID == obj.MovementRequestDetailID).FirstOrDefault();
                                    var movementrequest = _context.MovementRequest.Where(p => p.ID == movementrequestdetail.MovementRequestID).FirstOrDefault();
                                    int totalmoved = 0;
                                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.MovementRequestDetailID && p.DeletedDate == null && p.LocationID != null).Count();
                                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);
                                    if (Asset.Count() <= totalmoved)
                                    {
                                        var listAsset = _context.Asset.Where(p => p.Code.Any(o => obj.listAsset.Contains(p.Code))).ToList();
                                        if (listAsset != null)
                                        {
                                            if (listAsset.Count() == Asset.Count())
                                            {
                                                foreach (var asset in listAsset.ToList())
                                                {
                                                    if (asset.CategoryCD == movementrequestdetail.AssetCategoryCD)
                                                    {
                                                        AssetLocation assetlocationobj = new AssetLocation();
                                                        assetlocationobj.AssetID = asset.ID;
                                                        assetlocationobj.LocationID = location.ID;
                                                        assetlocationobj.OnTransition = false;
                                                        assetlocationobj.CreatedDate = obj.CreatedDate;//DateTime.Now;
                                                        assetlocationobj.CreatedBy = obj.CreatedBy;
                                                        assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;

                                                        listassetlocation.Add(assetlocationobj);
                                                        listAsset.Remove(asset);
                                                    }
                                                }
                                                if (listAsset.Count() != 0)
                                                {
                                                    result.status = false;
                                                    result.statuscode = 6;
                                                    result.asset = listAsset;
                                                }
                                                else
                                                {

                                                    foreach (var items in listassetlocation)
                                                    {
                                                        _context.AssetLocation.Add(items);
                                                        _context.SaveChanges();

                                                    }

                                                    transaction.Commit();
                                                    result.status = true;
                                                    result.statuscode = 7;

                                                }


                                            }
                                            else
                                            {
                                                result.status = false;
                                                result.statuscode = 3;
                                            }
                                        }
                                        else
                                        {
                                            result.status = false;
                                            result.statuscode = 3;
                                        }
                                    }
                                    else
                                    {
                                        result.status = false;
                                        result.statuscode = 3;
                                    }
                                }
                                else
                                {
                                    result.status = false;
                                    result.statuscode = 4;
                                }
                            }
                            else
                            {
                                result.status = false;
                                result.statuscode =8;
                            }
                        }
                        else
                        {
                            result.status = false;
                            result.statuscode = 5;
                        }
                    }


                }
                catch (Exception ex)
                {
                    result.status = false;
                    result.statuscode = 2;
                    result.message = ex.Message;
                }
            }
            else
            {
                result.status = false;
                result.statuscode = 1;
            }
            return result;
        }

        public ResultViewModel TransactionAsset(AssetViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            List<AssetLocation> listassetlocation = new List<AssetLocation>();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    var location = _location.GetLocationByCode(obj.location);
                    var movementrequestdetail = _context.MovementRequestDetail.Where(p => p.ID == obj.MovementRequestDetailID).FirstOrDefault();
                    var movementrequest = _context.MovementRequest.Where(p => p.ID == movementrequestdetail.MovementRequestID).FirstOrDefault();
                    int totalmoved = 0;
                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.MovementRequestDetailID && p.DeletedDate == null && p.LocationID != null).Count();
                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);

                    if (obj.listAsset.Count() <= totalmoved)
                    {
                        //if (location.ID == movementrequest.LocationID)
                        //{
                            var listAsset = _context.Asset.Where(p => p.Code.Any(o => obj.listAsset.Contains(p.Code))).ToList();
                            if (listAsset != null)
                            {
                                if (listAsset.Count() == obj.listAsset.Count())
                                {
                                    foreach (var item in listAsset.ToList())
                                    {
                                        if (item.CategoryCD == movementrequestdetail.AssetCategoryCD)
                                        {
                                            AssetLocation assetlocationobj = new AssetLocation();
                                            assetlocationobj.AssetID = item.ID;
                                            assetlocationobj.LocationID = movementrequest.LocationID != null ? movementrequest.LocationID : null;
                                            assetlocationobj.OnTransition = true;
                                            assetlocationobj.CreatedDate = obj.CreatedDate;//DateTime.Now;
                                            assetlocationobj.CreatedBy = obj.CreatedBy;
                                            assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;

                                            listassetlocation.Add(assetlocationobj);
                                            listAsset.Remove(item);
                                        }
                                    }
                                    if (listAsset.Count() != 0)
                                    {
                                        result.status = false;
                                        result.statuscode = 6;
                                        result.asset = listAsset;

                                    }
                                    else
                                    {

                                        foreach (var item in listassetlocation)
                                        {
                                            _context.AssetLocation.Add(item);
                                            _context.SaveChanges();

                                        }

                                        transaction.Commit();
                                        result.status = true;
                                        result.statuscode = 7;

                                }
                                }
                                else
                                {
                                    result.status = false;
                                    result.statuscode = 5;
                                    result.asset = listAsset;
                                }
                            }
                            else
                            {
                                result.status = false;
                                result.statuscode = 5;
                                result.asset = listAsset;
                            }
                        //}
                        //else
                        //{
                        //    result.status = false;
                        //    result.statuscode = 4;
                        //}
                    }
                    else
                    {
                        result.status = false;
                        result.statuscode = 3;
                    }




                }
                catch (Exception ex)
                {
                    result.status = false;
                    result.statuscode = 2;
                    result.message = ex.Message;

                }
            }
            else
            {
                result.status = false;
                result.statuscode = 1;
            }
            return result;
        }

        public ResultViewModel CreateAssetLocation(AssetLocationViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    var movementrequestdetail = _context.MovementRequestDetail.Where(p => p.ID == obj.AssetLocation.MovementRequestDetailID).FirstOrDefault();
                    var movementrequest = _context.MovementRequest.Where(p => p.ID == movementrequestdetail.MovementRequestID).FirstOrDefault();

                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.AssetLocation.MovementRequestDetailID && p.DeletedDate == null).Count();

                    int totalmoved = 0;
                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);


                    if (obj.AssetLocationList.Count() <= totalmoved)
                    {
                        foreach (var item in obj.AssetLocationList)
                        {


                            AssetLocation assetlocationobj = new AssetLocation();

                            assetlocationobj.AssetID = item.AssetID;
                            assetlocationobj.LocationID = movementrequest.LocationID;
                            assetlocationobj.OnTransition = obj.AssetLocation.OnTransition;
                            assetlocationobj.CreatedDate = obj.CreatedDate;//DateTime.Now;
                            assetlocationobj.CreatedBy = obj.CreatedBy;
                            assetlocationobj.MovementRequestDetailID = obj.AssetLocation.MovementRequestDetailID;
                            _context.AssetLocation.Add(assetlocationobj);


                        }
                        _context.SaveChanges();
                        transaction.Commit();
                        result.status = true;
                    }
                    else
                    {
                        result.message = "the inputed asset exceed the requested asset";
                        result.status = false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.message = ex.Message;
                    result.status = false;
                }
            }




            else
            {
                result.status = false;
                result.message = "The object is null";
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
                    assetlocation.UpdatedDate = obj.UpdatedDate;//DateTime.Now;
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
                    assetlocation.DeletedDate = obj.DeletedDate;//DateTime.Now;
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

        public List<AssetLocationViewModel> GetAssetLocationByMovementDetailID(int id)
        {
            List<AssetLocationViewModel> result = new List<AssetLocationViewModel>();

            var assetlocation = _assetlocation.GetAssetLocationByMovementDetailID(id);
            if (assetlocation != null)
            {
                foreach (var item in assetlocation)
                {
                    AssetLocationViewModel model = new AssetLocationViewModel();
                    model.AssetLocation = new AssetLocationPagination();
                    model.AssetLocation.ID = item.ID;
                    model.AssetLocation.AssetID = item.AssetID;
                    model.AssetLocation.AssetName = item.Asset.Name;
                    model.AssetLocation.LocationID = item.LocationID;
                    model.AssetLocation.LocationName = item.Location.Name;
                    model.AssetLocation.OnTransition = item.OnTransition;
                    model.AssetLocation.MovementRequestDetailID = item.MovementRequestDetailID;
                    result.Add(model);
                }
            }
            return result;
        }

        public List<AssetLocationViewModel> AssetLocation_Pagination(int Skip)
        {
            var result = _assetlocation.Pagination_AssetLocation_SP(Skip);
            return result;
        }
        public List<AssetOpnameTransactionViewModel> GetAssetLatestLocationByLocationID(int id,DateTime opnamedate)
        {
            List<AssetOpnameTransactionViewModel> result = new List<AssetOpnameTransactionViewModel>();
            var assetlocation = _assetlocation.GetAssetLatestLocationByLocationID(id,opnamedate);
            if (assetlocation != null)
            {
                result = assetlocation;
            }
            return result;
        }

        public List<AssetOpnameTransactionViewModel> GetAssetLocationOpnameLatestByLocationID(int id, DateTime opnamedate)
        {
            List<AssetOpnameTransactionViewModel> result = new List<AssetOpnameTransactionViewModel>();
            var assetlocation = _assetlocation.GetAssetLocationOpnameLatestByLocationID(id, opnamedate);
            if (assetlocation != null)
            {
                result = assetlocation;
            }
            return result;
        }
    }
}
