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
                    movement.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
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
                    request.ApprovedDate = DateTime.Now.ToString("ddMMyyyy");
                    request.ApprovalStatus = obj.ApprovalStatus;
                    request.UpdatedBy = obj.UpdatedBy;
                    request.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
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
                movement.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
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
                            item.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
                        }
                        else if (data.IsDelete == true)
                        {
                            item.DeletedDate = DateTime.Now.ToString("ddMMyyyy");
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
                    detail.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                       
                    movement.MovementRequestDetail.Add(detail);
                        
                }

                _context.Update(movement);
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
                    movement.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");

                    foreach (var item in movement.MovementRequestDetail)
                    {
                        item.DeletedBy = obj.DeletedBy;
                        item.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");
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
                    detail.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");

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

        public List<MovementRequestViewModel> GetMovementRequestToMoveByDepartment(int Departmentid)
        {
            List<MovementRequestViewModel> result = new List<MovementRequestViewModel>();
            
            var movement = _movementrequest.GetMovementRequestToMoveByDepartment(Departmentid);
            List<MovementRequestSearchResult> movementrequestlist = new List<MovementRequestSearchResult>();



            foreach (var item in movement)
            {
                var check = movementrequestlist.Where(p => p.ID == item.MovementRequest.ID).Count();
                if (check == 0)
                {
                    MovementRequestViewModel model = new MovementRequestViewModel();
                    model.MovementRequest = new MovementRequestSearchResult();
                    var approvalname = _pref.GetLookupByApprovalStatusCode(item.MovementRequest.ApprovalStatus);
                 
                    model.MovementRequest.ID = item.ID;
                    model.MovementRequest.MovementDate = item.MovementRequest.MovementDate;
                    model.MovementRequest.Description = item.Description;
                    model.MovementRequest.ApprovedDate = item.MovementRequest.ApprovedDate;
                    model.MovementRequest.LocationID = item.MovementRequest.LocationID;
                    model.MovementRequest.LocationName = item.MovementRequest.Location != null ? item.MovementRequest.Location.Name : null;
                    model.MovementRequest.ApprovedBy = item.MovementRequest.ApprovedBy;
                    model.MovementRequest.Notes = item.MovementRequest.Notes;
                    model.MovementRequest.ApprovalStatus = item.MovementRequest.ApprovalStatus;
                    model.MovementRequest.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                    model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                    movementrequestlist.Add(model.MovementRequest);
                    result.Add(model);
                }  
            }
            foreach (var item2 in result)
            {
                var movementrequest = movement.Where(p => p.MovementRequestID == item2.MovementRequest.ID).ToList();
                foreach(var item3 in movementrequest)
                {
                    MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                    var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item3.ID);
                    detail.Quantity = item3.Quantity;
                    detail.Transfered = moveasset != null ? moveasset.Count : 0;
                    if (detail.Quantity != detail.Transfered)
                    {
                        
                        var categoryname = _pref.GetLookupByCategoryCode(item3.AssetCategoryCD);
                        var deparment = _department.GetDepartmentByID(item3.RequestedTo);

                        detail.ID = item3.ID;
                        detail.MovementRequestID = item3.MovementRequestID;
                        detail.Description = item3.Description;
                        detail.AssetCategoryCD = item3.AssetCategoryCD;
                        detail.CategoryCDName = categoryname != null ? categoryname.Value : null;
                        detail.RequestTo = item3.RequestedTo;
                        detail.RequestToName = deparment != null ? deparment.Name : null;

                        item2.MovementRequestDetail.Add(detail);
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
