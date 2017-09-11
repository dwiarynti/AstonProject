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
             
                model.ID = item.ID;
                model.MovementDate = item.MovementDate;
                model.Description = item.Description;
                model.ApprovedDate = item.ApprovedDate;
                model.ApprovedBy = item.ApprovedBy;
                model.Notes = item.Notes;
                model.ApprovalStatus = item.ApprovalStatus;
                model.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                
                model.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
                foreach (var item2 in item.MovementRequestDetail)
                {
                    MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                    var categoryname = _pref.GetLookupByCategoryCode(item2.AssetCategoryCD);
                    var deparment = _department.GetDepartmentByID(item2.RequestedTo);
                    var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item2.ID,item2.AssetCategoryCD);

                    detail.ID = item2.ID;
                    detail.MovementRequestID = item2.MovementRequestID;
                    detail.Description = item2.Description;
                    detail.AssetCategoryCD = item2.AssetCategoryCD;
                    detail.CategoryCDName = categoryname!= null ? categoryname.Value: null;
                    detail.RequestTo = item2.RequestedTo;
                    detail.RequestToName = deparment != null ? deparment.Name : null;
                    detail.Quantity = item2.Quantity;
                    detail.Transfered = moveasset != null ? moveasset.Count : 0;
                    model.MovementRequestDetail.Add(detail);
                }
                result.Add(model);
            }
            return result;
        }
        public MovementRequestViewModel GetMovementRequestByID(int id)
        {

            var movement = _movementrequest.GetMovementRequestByID(id);

            MovementRequestViewModel result = new MovementRequestViewModel();
            var approvalname = _pref.GetLookupByApprovalStatusCode(movement.ApprovalStatus);
            result.ID = movement.ID;
            result.MovementDate = movement.MovementDate;
            result.Description = movement.Description;
            result.ApprovedDate = movement.ApprovedDate;
            result.ApprovedBy = movement.ApprovedBy;
            result.Notes = movement.Notes;
            result.ApprovalStatus = movement.ApprovalStatus;
            result.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
            result.MovementRequestDetail = new List<MovementRequestDetailViewModel>();
            foreach (var item in movement.MovementRequestDetail)
            {
                MovementRequestDetailViewModel detail = new MovementRequestDetailViewModel();
                var categoryname = _pref.GetLookupByCategoryCode(item.AssetCategoryCD);
                var deparment = _department.GetDepartmentByID(item.RequestedTo);
                var moveasset = _assetlocation.GetAssetLocationByMovementDetailID(item.ID,item.AssetCategoryCD);
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
                    movement.MovementDate = Convert.ToDateTime(obj.MovementDate).ToString("ddMMyyyy");
                    movement.Description = obj.Description;
                    movement.ApprovalStatus = obj.ApprovalStatus;
                    movement.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                    movement.CreatedBy = obj.CreatedBy;

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
                    result.resultstatus = true;
                    result.movementrequest = movement;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.resultstatus = false;
                    result.movementrequest = null;
                }
            }
            else
            {
                result.resultstatus = false;
                result.movementrequest = null;
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
                    request.ApprovedBy = obj.ApprovedBy;
                    request.ApprovedDate = DateTime.Now.ToString("ddMMyyyy");
                    request.ApprovalStatus = obj.ApprovalStatus;
                    request.UpdatedBy = obj.UpdatedBy;
                    request.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");

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
                var movement = _movementrequest.GetMovementRequestByID(obj.ID);
                movement.Description = obj.Description;
                movement.ApprovalStatus = obj.ApprovalStatus;
                movement.Notes = obj.Notes;
                movement.ApprovalStatus = obj.ApprovalStatus;
                movement.UpdatedBy = obj.UpdatedBy;
                movement.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
                
                movement.MovementRequestDetail = null;
                foreach (var item in obj.MovementRequestDetail)
                {
                        var detail = _movementrequest.GetMovementRequestDetailByID(item.ID);

                        if (item.IsUpdate == true)
                        {
                            detail.Description = item.Description;
                            detail.AssetCategoryCD = item.AssetCategoryCD;
                            detail.Quantity = item.Quantity;
                            detail.RequestedTo = item.RequestTo;
                            detail.UpdatedBy = obj.UpdatedBy;
                            detail.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
                        }
                        else if (item.IsDelete == true)
                        {
                            detail.DeletedDate = DateTime.Now.ToString("ddMMyyyy");
                            detail.DeletedBy = obj.UpdatedBy;
                        }
                        movement.MovementRequestDetail.Add(detail);
                        
                }

                _context.Entry(movement).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Commit();
                result.movementrequest = movement;
                result.resultstatus = true;

            }
            catch (Exception ex)
            {
                result.movementrequest = null;
                result.resultstatus = false;
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
                    _context.Entry(movement).State = EntityState.Modified;
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
    }
}
