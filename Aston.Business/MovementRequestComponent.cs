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
    public class MovementRequestComponent
    {
        AstonContext _context = new AstonContext();
        MovementRequestExtensions _movementrequest = new MovementRequestExtensions();
        LookupListComponent _pref = new LookupListComponent();
        DepartmentExtensions _department = new DepartmentExtensions();
        AssetLocationComponent _assetlocation = new AssetLocationComponent();
        public List<MovementRequestViewModel> GetMovementRequest()
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();
            var movement = _movementrequest.GetMovementRequest();
            
            foreach(var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
             
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                
                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                foreach (var item2 in item.MovementRequestDetail)
                {
                    if (item2.DeletedBy == null && item2.DeletedDate == null)
                    {
                        MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                        var categoryname = _pref.GetLookupByCategoryCode(item2.AssetCategoryCD);
                        var deparment = _department.GetDepartmentByID(item2.RequestedTo);
                        var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item2.ID);

                        detail.ID = item2.ID;
                        detail.MovementRequestID = item2.MovementRequestID;
                        detail.Description = item2.Description;
                        detail.AssetCategoryCD = item2.AssetCategoryCD;
                        detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                        detail.RequestTo = item2.RequestedTo;
                        detail.RequestToName = deparment != null ? deparment.Name : null;
                        detail.Quantity = item2.Quantity;
                        detail.Transfered = moveasset != null ? moveasset.Count : 0;
                        model.MovementRequestDetail.Add(detail);
                    }
                }
                result.Add(model);
            }
            return result;
        }

        
        public List<MovementRequestViewModel> GetMovementRequestNeedApproval()
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();
            var movement = _movementrequest.GetMovementRequestNeedApproval();

            foreach (var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
                model.MovementRequest = new MovementRequestSearchResult();
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;

                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                foreach (var item2 in item.MovementRequestDetail)
                {
                    if (item2.DeletedBy == null && item2.DeletedDate == null)
                    {
                        MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                        var categoryname = _pref.GetLookupByCategoryCode(item2.AssetCategoryCD);
                        var deparment = _department.GetDepartmentByID(item2.RequestedTo);
                        var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item2.ID);

                        detail.ID = item2.ID;
                        detail.MovementRequestID = item2.MovementRequestID;
                        detail.Description = item2.Description;
                        detail.AssetCategoryCD = item2.AssetCategoryCD;
                        detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                        detail.RequestTo = item2.RequestedTo;
                        detail.RequestToName = deparment != null ? deparment.Name : null;
                        detail.Quantity = item2.Quantity;
                        detail.Transfered = moveasset != null ? moveasset.Count : 0;
                        model.MovementRequestDetail.Add(detail);
                    }
                }
                result.Add(model);
            }
            return result;
        }
        public MovementRequestViewModel GetMovementRequestByID(int id)
        {

            var movement = _movementrequest.GetMovementRequestByID(id);

            MovementRequestViewModel result = new MovementRequestViewModel();
            result.MovementRequest = new MovementRequestSearchResult();
            var approvalname = _pref.GetLookupByApprovalStatusCode(movement.ApprovalStatus);
            result.MovementRequest.ID = movement.ID;
            result.MovementRequest.MovementDate = movement.MovementDate;
            result.MovementRequest.LocationID = movement.LocationID;
            result.MovementRequest.LocationName = movement.Location != null ? movement.Location.Name : null;
            result.MovementRequest.Description = movement.Description;
            result.MovementRequest.ApprovedDate = movement.ApprovedDate;
            result.MovementRequest.ApprovedBy = movement.ApprovedBy;
            result.MovementRequest.Notes = movement.Notes;
            result.MovementRequest.ApprovalStatus = movement.ApprovalStatus;
            result.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
            result.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
            foreach (var item in movement.MovementRequestDetail)
            {
                if (item.DeletedDate == null && item.DeletedBy == null)
                {
                    MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                    var categoryname = _pref.GetLookupByCategoryCode(item.AssetCategoryCD);
                    var deparment = _department.GetDepartmentByID(item.RequestedTo);
                    var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item.ID);
                    detail.ID = item.ID;
                    detail.MovementRequestID = item.MovementRequestID;
                    detail.Description = item.Description;
                    detail.AssetCategoryCD = item.AssetCategoryCD;
                    detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                    detail.RequestTo = item.RequestedTo;
                    detail.RequestToName = deparment != null ? deparment.Name : null;
                    detail.Quantity = item.Quantity;
                    detail.Transfered = moveasset != null ? moveasset.Count : 0;
                    result.MovementRequestDetail.Add(detail);
                }
            }
            return result;

        }

        public ResultViewModel CreateMovementRequest(MovementRequestViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    MovementRequest movement = new MovementRequest();
                    movement.MovementDate = obj.MovementRequest.MovementDate.Replace("/", string.Empty);
                    movement.Description = obj.MovementRequest.Description;
                    movement.LocationID = obj.MovementRequest.LocationID;
                    movement.ApprovalStatus = Convert.ToInt16(obj.MovementRequest.ApprovalStatus);
                    movement.CreatedDate = obj.CreatedDate;//DateTime.Now;
                    movement.CreatedBy = obj.CreatedBy;
                    movement.Notes = obj.MovementRequest.Notes;
                    foreach (var item in obj.MovementRequestDetail)
                    {
                        MovementRequestDetail detail = new MovementRequestDetail();
                        detail.Description = item.Description;
                        detail.AssetCategoryCD = item.AssetCategoryCD;
                        detail.Quantity = item.Quantity;
                        detail.RequestedTo = item.RequestTo;
                        detail.CreatedDate = movement.CreatedDate;
                        detail.CreatedBy = movement.CreatedBy;
                        movement.MovementRequestDetail.Add(detail);
                    }
                    _context.MovementRequest.Add(movement);
                    _context.SaveChanges();
                    transaction.Commit();
                    result.status = true;
                    result.movementRequest = GetMovementRequestByID(movement.ID);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.status = false;
                    result.movementRequest = null;
                }
            }
            else
            {
                result.status = false;
                result.movementRequest = null;
            }

            return result;
        }

        public bool ApproveMovementRequest(MovementRequest obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            var request = _movementrequest.GetMovementRequestByID(obj.ID);
            if (request != null)
            {
                try
                {
                    request.ApprovedBy = obj.UpdatedBy;
                    request.ApprovedDate = obj.ApprovedDate;//DateTime.Now.ToString("ddMMyyyy");
                    request.ApprovalStatus = obj.ApprovalStatus;
                    request.UpdatedBy = obj.UpdatedBy;
                    request.UpdatedDate = obj.UpdatedDate;//DateTime.Now;
                    request.Notes = obj.Notes;
                    _context.Entry(request).State = EntityState.Modified;
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

        public ResultViewModel UpdateMovementRequest(MovementRequestViewModel obj)
        {
            ResultViewModel result = new ResultViewModel();
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {

                var movement = _movementrequest.GetMovementRequestByID(obj.MovementRequest.ID);
                movement.Description = obj.MovementRequest.Description;
                movement.LocationID = obj.MovementRequest.LocationID;
                movement.MovementDate = obj.MovementRequest.MovementDate.Replace("/",string.Empty);
                movement.ApprovalStatus = Convert.ToInt16(obj.MovementRequest.ApprovalStatus);
                movement.UpdatedBy = obj.UpdatedBy;
                movement.UpdatedDate = obj.UpdatedDate;//DateTime.Now;
                movement.Notes = obj.MovementRequest.Notes;
                foreach(var item in movement.MovementRequestDetail)
                {
                    var data = obj.MovementRequestDetail.Where(p => p.ID == item.ID).FirstOrDefault();
                    if (data != null)
                    {
                        if (data.IsUpdate == true)
                        {
                            item.Description = data.Description;
                            item.AssetCategoryCD = data.AssetCategoryCD;
                            item.Quantity = data.Quantity;
                            item.RequestedTo = data.RequestTo;
                            item.UpdatedBy = obj.UpdatedBy;
                            item.UpdatedDate = obj.UpdatedDate;//DateTime.Now;
                        }
                        else if (data.IsDelete == true)
                        {
                            item.DeletedDate = obj.DeletedDate;//DateTime.Now;
                            item.DeletedBy = obj.UpdatedBy;
                        }
                        obj.MovementRequestDetail.Remove(data);
                    }
                }

                foreach (var item in obj.MovementRequestDetail)
                {
                    MovementRequestDetail detail = new MovementRequestDetail();
                    detail.Description = item.Description;
                    detail.AssetCategoryCD = item.AssetCategoryCD;
                    detail.Quantity = item.Quantity;
                    detail.RequestedTo = item.RequestTo;
                    detail.CreatedBy = obj.UpdatedBy;
                    detail.CreatedDate = obj.CreatedDate;//DateTime.Now;
                       
                    movement.MovementRequestDetail.Add(detail);
                        
                }

                _context.Entry(movement).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Commit();
                result.movementRequest = GetMovementRequestByID(movement.ID);
                result.status = true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result.movementRequest = null;
                result.status = false;
            }
            return result;
        }

        public bool DeleteMovementRequest(MovementRequest obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var movement = _movementrequest.GetMovementRequestByID(obj.ID);
            if (movement != null)
            {
                try
                {
                    movement.DeletedBy = obj.DeletedBy;
                    movement.DeletedDate = obj.DeletedDate;//DateTime.Now;

                    foreach (var item in movement.MovementRequestDetail)
                    {
                        item.DeletedBy = obj.DeletedBy;
                        item.DeletedDate = obj.DeletedDate;//DateTime.Now;
                    }
                    _context.Update(movement);
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
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
        public bool DeleteMovementRequestDetail(MovementRequestDetail obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var detail = _movementrequest.GetMovementRequestDetailByID(obj.ID);
            if (detail != null)
            {
                try
                {
                    detail.DeletedBy = obj.DeletedBy;
                    detail.DeletedDate = obj.DeletedDate;//DateTime.Now.Date;

                    _context.Entry(detail).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
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

        public List<MovementRequestViewModel> GetMovementRequestToMove()
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();
            var movement = _movementrequest.GetMovementRequestToMove();

         

            foreach (var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();

             
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
                model.MovementRequest = new MovementRequestSearchResult();
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;

                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                foreach (var item2 in item.MovementRequestDetail)
                {
                    if (item2.DeletedBy == null && item2.DeletedDate == null)
                    { 
                        MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                        var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item2.ID);
                        detail.Quantity = item2.Quantity;
                        detail.Transfered = moveasset != null ? moveasset.Count : 0;
                      
                        if (detail.Quantity != detail.Transfered)
                        {
                            model.NeedMove = true;
                            var categoryname = _pref.GetLookupByCategoryCode(item2.AssetCategoryCD);
                            var deparment = _department.GetDepartmentByID(item2.RequestedTo);

                            detail.ID = item2.ID;
                            detail.MovementRequestID = item2.MovementRequestID;
                            detail.Description = item2.Description;
                            detail.AssetCategoryCD = item2.AssetCategoryCD;
                            detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                            detail.RequestTo = item2.RequestedTo;
                            detail.RequestToName = deparment != null ? deparment.Name : null;

                            model.MovementRequestDetail.Add(detail);
                        }


                       
                    }
                }
                if (model.NeedMove == true)
                {
                    result.Add(model);
                }
            }
            
            return result;
        }

        public List<MovementRequestViewModel> GetMovementRequestCompleted()
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();

            var movement = _movementrequest.GetMovementRequestToMove();
            List<MovementRequestSearchResult> movementrequestlist = new List<MovementRequestSearchResult>();

            foreach (var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();
                model.MovementRequest = new MovementRequestSearchResult();
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                movementrequestlist.Add(model.MovementRequest);


                foreach (var detail in item.MovementRequestDetail)
                {
                    if (detail.DeletedDate == null)
                    {
                        
                            MovementRequestDetailViewModel mvdetail = new MovementRequestDetailViewModel();
                            var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(detail.ID);
                            mvdetail.Quantity = detail.Quantity;
                            mvdetail.Transfered = moveasset != null ? moveasset.Count : 0;
                            if (mvdetail.Quantity == mvdetail.Transfered)
                            {

                                var categoryname = _pref.GetLookupByCategoryCode(detail.AssetCategoryCD);
                                var department = _department.GetDepartmentByID(detail.RequestedTo);

                                mvdetail.ID = detail.ID;
                                mvdetail.MovementRequestID = detail.MovementRequestID;
                                mvdetail.Description = detail.Description;
                                mvdetail.AssetCategoryCD = detail.AssetCategoryCD;
                                mvdetail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                                mvdetail.RequestTo = detail.RequestedTo;
                                mvdetail.RequestToName = department != null ? department.Name : null;
                                model.MovementRequestDetail.Add(mvdetail);
                            }

                        
                    }
                }
                if (model.MovementRequestDetail.Count != 0)
                {
                    result.Add(model);
                }
            }

            return result;
        }

        public List<MovementRequestViewModel> GetMovementRequestToMoveByDepartment(int Departmentid)
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();

            var movement = _movementrequest.GetMovementRequestToMove();
            List<MovementRequestSearchResult> movementrequestlist = new List<MovementRequestSearchResult>();

            foreach (var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();
                model.MovementRequest = new MovementRequestSearchResult();
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                movementrequestlist.Add(model.MovementRequest);
                

                foreach (var detail in item.MovementRequestDetail)
                {
                    if (detail.DeletedDate == null)
                    {
                        if (detail.RequestedTo == Departmentid)
                        {
                            MovementRequestDetailViewModel mvdetail = new MovementRequestDetailViewModel();
                            var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(detail.ID);
                            mvdetail.Quantity = detail.Quantity;
                            mvdetail.Transfered = moveasset != null ? moveasset.Count : 0;
                            if (mvdetail.Quantity != mvdetail.Transfered)
                            {

                                var categoryname = _pref.GetLookupByCategoryCode(detail.AssetCategoryCD);
                                var department = _department.GetDepartmentByID(detail.RequestedTo);

                                mvdetail.ID = detail.ID;
                                mvdetail.MovementRequestID = detail.MovementRequestID;
                                mvdetail.Description = detail.Description;
                                mvdetail.AssetCategoryCD = detail.AssetCategoryCD;
                                mvdetail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                                mvdetail.RequestTo = detail.RequestedTo;
                                mvdetail.RequestToName = department != null ? department.Name : null;
                                model.MovementRequestDetail.Add(mvdetail);
                            }

                        }
                    }
                }
                if (model.MovementRequestDetail.Count != 0)
                {
                    result.Add(model);
                }
            }
      
            return result;
        }

        public List<MovementRequestViewModel> GetMovementRequestcompletedByDepartment(int Departmentid)
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();

            var movement = _movementrequest.GetMovementRequestToMove();
            List<MovementRequestSearchResult> movementrequestlist = new List<MovementRequestSearchResult>();

            foreach (var item in movement)
            {
                MovementRequestViewModel model = new MovementRequestViewModel();
                model.MovementRequest = new MovementRequestSearchResult();
                var approvalname = _pref.GetLookupByApprovalStatusCode(item.ApprovalStatus);
                model.MovementRequest.ID = item.ID;
                model.MovementRequest.MovementDate = item.MovementDate;
                model.MovementRequest.Description = item.Description;
                model.MovementRequest.ApprovedDate = item.ApprovedDate;
                model.MovementRequest.LocationID = item.LocationID;
                model.MovementRequest.LocationName = item.Location != null ? item.Location.Name : null;
                model.MovementRequest.ApprovedBy = item.ApprovedBy;
                model.MovementRequest.Notes = item.Notes;
                model.MovementRequest.ApprovalStatus = item.ApprovalStatus;
                model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                movementrequestlist.Add(model.MovementRequest);


                foreach (var detail in item.MovementRequestDetail)
                {
                    if (detail.DeletedDate == null)
                    {
                        if (detail.RequestedTo == Departmentid)
                        {
                            MovementRequestDetailViewModel mvdetail = new MovementRequestDetailViewModel();
                            var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(detail.ID);
                            mvdetail.Quantity = detail.Quantity;
                            mvdetail.Transfered = moveasset != null ? moveasset.Count : 0;
                            if (mvdetail.Quantity == mvdetail.Transfered)
                            {

                                var categoryname = _pref.GetLookupByCategoryCode(detail.AssetCategoryCD);
                                var department = _department.GetDepartmentByID(detail.RequestedTo);

                                mvdetail.ID = detail.ID;
                                mvdetail.MovementRequestID = detail.MovementRequestID;
                                mvdetail.Description = detail.Description;
                                mvdetail.AssetCategoryCD = detail.AssetCategoryCD;
                                mvdetail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                                mvdetail.RequestTo = detail.RequestedTo;
                                mvdetail.RequestToName = department != null ? department.Name : null;
                                model.MovementRequestDetail.Add(mvdetail);
                            }

                        }
                    }
                }
                if (model.MovementRequestDetail.Count != 0)
                {
                    result.Add(model);
                }
            }

            return result;
        }

        public MovementRequestViewModel GetMovementRequestToMoveByID(int id)
        {

            var movement = _movementrequest.GetMovementRequestByID(id);

            MovementRequestViewModel result = new MovementRequestViewModel();
            result.MovementRequest = new MovementRequestSearchResult();
            var approvalname = _pref.GetLookupByApprovalStatusCode(movement.ApprovalStatus);
            result.MovementRequest.ID = movement.ID;
            result.MovementRequest.MovementDate = movement.MovementDate;
            result.MovementRequest.LocationID = movement.LocationID;
            result.MovementRequest.LocationName = movement.Location != null ? movement.Location.Name : null;
            result.MovementRequest.Description = movement.Description;
            result.MovementRequest.ApprovedDate = movement.ApprovedDate;
            result.MovementRequest.ApprovedBy = movement.ApprovedBy;
            result.MovementRequest.Notes = movement.Notes;
            result.MovementRequest.ApprovalStatus = movement.ApprovalStatus;
            result.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
            result.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
            foreach (var item in movement.MovementRequestDetail)
            {
                if (item.DeletedDate == null && item.DeletedBy == null)
                {
                    MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                    var categoryname = _pref.GetLookupByCategoryCode(item.AssetCategoryCD);
                    var deparment = _department.GetDepartmentByID(item.RequestedTo);
                    var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item.ID);
                    detail.Quantity = item.Quantity;
                    detail.Transfered = moveasset != null ? moveasset.Count : 0;

                    if (detail.Quantity != detail.Transfered)
                    {
                        detail.ID = item.ID;
                        detail.MovementRequestID = item.MovementRequestID;
                        detail.Description = item.Description;
                        detail.AssetCategoryCD = item.AssetCategoryCD;
                        detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                        detail.RequestTo = item.RequestedTo;
                        detail.RequestToName = deparment != null ? deparment.Name : null;
                        result.MovementRequestDetail.Add(detail);
                    }
                }
            }
            return result;

        }

        public List<MovementRequestViewModel> SearchMovementRequest(MovementRequestViewModel obj)
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();
            if (obj != null)
            {
                result = _movementrequest.SearchMovementRequests_SP(Convert.ToInt16(obj.MovementRequest.LocationID), Convert.ToInt16(obj.MovementRequest.ApprovalStatus), obj.Skip);
            }
            return result;
        }
    }
}
