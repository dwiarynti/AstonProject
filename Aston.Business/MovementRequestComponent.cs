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

        public List<MovementRequest> GetMovementRequest()
        {
            List<MovementRequest> result = new List<MovementRequest>();
            result = _movementrequest.GetMovementRequest();
            return result;
        }
        public MovementRequest GetMovementRequestByID(int id)
        {
            return _movementrequest.GetMovementRequestByID(id);
        }

        public bool CreateMovementRequest(MovementRequestViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    MovementRequest movement = new MovementRequest();
                    movement.MovementDate = Convert.ToDateTime(obj.MovementDate).ToString("ddMMyyyy");
                    movement.Description = obj.Description;
                    movement.CreatedDate = DateTime.Now.ToString("ddMMyyyy");
                    movement.CreatedBy = obj.CreatedBy;

                    foreach (var item in obj.MovementRequestDetail)
                    {
                        MovementRequestDetail detail = new MovementRequestDetail();
                        detail.Description = item.Description;
                        detail.AssetCategoryCD = item.AssetCategoryCD;
                        detail.Quantity = item.Quantity;
                        detail.RequestTo = item.RequestTo;
                        detail.CreatedDate = movement.CreatedDate;
                        detail.CreatedBy = movement.CreatedBy;
                        movement.MovementRequestDetail.Add(detail);
                    }
                    _context.MovementRequest.Add(movement);
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

        public bool UpdateMovementRequest(MovementRequestViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            int count = 0;
            try
            {
                var movement = _movementrequest.GetMovementRequestByID(obj.ID);
                movement.Description = obj.Description;
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
                        detail.RequestTo = item.RequestTo;
                        detail.UpdatedBy = obj.UpdatedBy;
                        detail.UpdatedDate = DateTime.Now.ToString("ddMMyyyy");

                    }
                    else
                    {
                        detail.DeletedBy = obj.UpdatedBy;
                        detail.DeletedDate = DateTime.Now.ToString("ddMMyyyy");
                    }

                    movement.MovementRequestDetail.Add(detail);
                }

                _context.Entry(movement).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Commit();
                result = true;

            }
            catch (Exception ex)
            {
                result = false;
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
    }
}
