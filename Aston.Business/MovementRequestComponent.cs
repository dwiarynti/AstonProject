﻿using Aston.Business.Data;
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
                model.LocationID = item.LocationID;
                model.LocationName = item.Location != null ? item.Location.Name : null;
                model.ApprovedBy = item.ApprovedBy;
                model.Notes = item.Notes;
                model.ApprovalStatus = item.ApprovalStatus;
                model.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
                
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

                model.ID = item.ID;
                model.MovementDate = item.MovementDate;
                model.Description = item.Description;
                model.ApprovedDate = item.ApprovedDate;
                model.LocationID = item.LocationID;
                model.LocationName = item.Location != null ? item.Location.Name : null;
                model.ApprovedBy = item.ApprovedBy;
                model.Notes = item.Notes;
                model.ApprovalStatus = item.ApprovalStatus;
                model.ApprovalStatusName = approvalname != null ? approvalname.Value : null;

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
            var approvalname = _pref.GetLookupByApprovalStatusCode(movement.ApprovalStatus);
            result.ID = movement.ID;
            result.MovementDate = movement.MovementDate;
            result.LocationID = movement.LocationID;
            result.LocationName = movement.Location != null ? movement.Location.Name : null;
            result.Description = movement.Description;
            result.ApprovedDate = movement.ApprovedDate;
            result.ApprovedBy = movement.ApprovedBy;
            result.Notes = movement.Notes;
            result.ApprovalStatus = movement.ApprovalStatus;
            result.ApprovalStatusName = approvalname != null ? approvalname.Value : null;
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
                    movement.MovementDate = obj.MovementDate.Replace("/", string.Empty);
                    movement.Description = obj.Description;
                    movement.LocationID = obj.LocationID;
                    movement.ApprovalStatus = obj.ApprovalStatus;
                    movement.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                    movement.CreatedBy = obj.CreatedBy;
                    movement.Notes = obj.Notes;
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

                var movement = _movementrequest.GetMovementRequestByID(obj.ID);
                movement.Description = obj.Description;
                movement.LocationID = obj.LocationID;
                movement.MovementDate = obj.MovementDate.Replace("/",string.Empty);
                movement.ApprovalStatus = obj.ApprovalStatus;
                movement.UpdatedBy = obj.UpdatedBy;
                movement.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");
                movement.Notes = obj.Notes;
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
    }
}
