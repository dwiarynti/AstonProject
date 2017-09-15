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

        public ResultViewModel MoveAsset(AssetViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            List<AssetLocation> listassetlocation = new List<AssetLocation>();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if(obj != null)
            {
                try
                {
                    var location = _location.GetLocationByCode(obj.location);
                    var movementrequestdetail = _context.MovementRequestDetail.Where(p => p.ID == obj.MovementRequestDetailID).FirstOrDefault();
                    var movementrequest = _context.MovementRequest.Where(p => p.ID == movementrequestdetail.MovementRequestID).FirstOrDefault();
                    int totalmoved = 0;
                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.MovementRequestDetailID && p.DeletedDate == null && p.LocationID != null ).Count();
                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);

                    if(obj.listAsset.Count() <= totalmoved)
                    {
                        if(location.ID == movementrequest.LocationID)
                        {
                            var listAsset = _context.Asset.Where(p => p.Code.Any(o => obj.listAsset.Contains(p.Code))).ToList();
                            if (listAsset != null)
                            {
                                if (listAsset.Count() == obj.listAsset.Count())
                                {
                                    foreach (var item in listAsset.ToList())
                                    {
                                        if(item.CategoryCD == movementrequestdetail.AssetCategoryCD)
                                        {
                                            AssetLocation assetlocationobj = new AssetLocation();
                                            assetlocationobj.AssetID = item.ID;
                                            assetlocationobj.LocationID = location.ID;
                                            assetlocationobj.OnTransition = false;
                                            assetlocationobj.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                                            assetlocationobj.CreatedBy = obj.CreatedBy;
                                            assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;

                                            listassetlocation.Add(assetlocationobj);
                                            listAsset.Remove(item);
                                        }
                                    }
                                    if(listAsset.Count() != 0)
                                    {
                                        result.status = false;
                                        result.statuscode = 6;                                   
                                        result.asset = listAsset;

                                    }
                                    else
                                    {
                                        _context.AssetLocation.AddRange(listassetlocation);
                                        _context.SaveChanges();
                                        transaction.Commit();
                                        result.status = true;
                                      
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
                        result.statuscode = 3;
                    }




                }
                catch(Exception ex)
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
                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.MovementRequestDetailID && p.DeletedDate == null && p.LocationID == null).Count();
                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);

                    if (obj.listAsset.Count() <= totalmoved)
                    {
                        if (location.ID == movementrequest.LocationID)
                        {
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
                                            assetlocationobj.LocationID = location.ID;
                                            assetlocationobj.OnTransition = false;
                                            assetlocationobj.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                                            assetlocationobj.CreatedBy = obj.CreatedBy;
                                            assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;

                                            listassetlocation.Add(assetlocationobj);
                                            listAsset.Remove(item);
                                        }
                                    }
                                    if (listAsset.Count() != listassetlocation.Count())
                                    {
                                        result.status = false;
                                        result.statuscode = 6;
                                        result.asset = listAsset;

                                    }
                                    else
                                    {
                                        _context.AssetLocation.AddRange(listassetlocation);
                                        _context.SaveChanges();
                                        transaction.Commit();
                                        result.status = true;

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
                    var movementrequestdetail = _context.MovementRequestDetail.Where(p => p.ID == obj.MovementRequestDetailID).FirstOrDefault();
                    var movementrequest = _context.MovementRequest.Where(p => p.ID == movementrequestdetail.MovementRequestID).FirstOrDefault();

                    var checkassetlocation = _context.AssetLocation.Where(p => p.MovementRequestDetailID == obj.MovementRequestDetailID && p.DeletedDate == null).Count();

                    int totalmoved = 0;
                    totalmoved = (movementrequestdetail.Quantity - checkassetlocation);


                    if (obj.AssetLocation.Count() <= totalmoved)
                    {
                        foreach (var item in obj.AssetLocation)
                        {


                            AssetLocation assetlocationobj = new AssetLocation();

                            assetlocationobj.AssetID = item.AssetID;
                            assetlocationobj.LocationID = movementrequest.LocationID;
                            assetlocationobj.OnTransition = obj.OnTransition;
                            assetlocationobj.CreatedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                            assetlocationobj.CreatedBy = obj.CreatedBy;
                            assetlocationobj.MovementRequestDetailID = obj.MovementRequestDetailID;
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

        public List<AssetLocationViewModel> GetAssetLocationByMovementDetailID(int id)
        {
            List<AssetLocationViewModel> result = new List<AssetLocationViewModel>();

            var assetlocation = _assetlocation.GetAssetLocationByMovementDetailID(id);
            if (assetlocation != null)
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
    }
}
