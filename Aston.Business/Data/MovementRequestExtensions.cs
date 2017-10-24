using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class MovementRequestExtensions
    {
        AstonContext _context = new AstonContext();

        public MovementRequest GetMovementRequestByID(int id)
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Include(p=>p.Location).Where(p => p.ID == id).FirstOrDefault();
        }

        public MovementRequestDetail GetMovementRequestDetailByID(int id)
        {
            return _context.MovementRequestDetail.Where(p => p.ID == id).FirstOrDefault();
        }
        public List<MovementRequest> GetMovementRequest()
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Include(p=>p.Location).Where(p => p.DeletedDate == null && p.DeletedBy == null).ToList();
        }
        public List<MovementRequest> GetMovementRequestNeedApproval()
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Include(p=>p.Location).Where(p => p.DeletedDate == null && p.DeletedBy == null && p.ApprovalStatus == 2).ToList();
        }

        public List<MovementRequest> GetMovementRequestToMove()
        {
            var obj = _context.MovementRequest.Include(p => p.MovementRequestDetail).Include(p => p.Location).Where(p => p.DeletedDate == null && p.DeletedBy == null && p.ApprovalStatus == 1).ToList();
            return obj;
        }
        public List<MovementRequestDetail> GetMovementRequestToMoveByDepartment(int depatmentid)
        {
            var mv = _context.MovementRequestDetail.Where(p => p.DeletedDate == null && p.RequestedTo == depatmentid).ToList();               
            return mv;
        }
        public List<MovementRequestViewModel> SearchMovementRequests_SP(int LocationID, int ApprovalStatus, int Skip)
        {
            var result = new List<MovementRequestViewModel>();
            var obj = new MovementRequestViewModel();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_SearchMovementRequest";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int) { Value = LocationID });
                cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", SqlDbType.Int) { Value = ApprovalStatus });
                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {

                    //a = reader.<AssetViewModel>():
                    var assetlist = dbContext.DataReaderMapToList<MovementRequestSearchResult>(reader);
                    foreach (var asset in assetlist)
                    {
                        result.Add(new MovementRequestViewModel() { MovementRequest = asset });
                    }
                    cmd.Connection.Close();

                }
            }

            return result;
        }
        public int NumberofMovementRequest()
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Include(p => p.Location).Where(p => p.DeletedDate == null && p.DeletedBy == null).ToList().Count;
        }

        public List<HistoryViewModel> AssetHistory_SP(int AssetID, int Skip)
        {
            var result = new List<HistoryViewModel>();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_AssetHistory_Pagination";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@AssetID", SqlDbType.Int) { Value = AssetID });
                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {

                    //a = reader.<AssetViewModel>():
                    var historyList = dbContext.DataReaderMapToList<HistoryViewModel>(reader);
                    foreach (var history in historyList)
                    {
                        result.Add(history);
                    }
                    cmd.Connection.Close();

                }
            }

            return result;
        }
    }
}
