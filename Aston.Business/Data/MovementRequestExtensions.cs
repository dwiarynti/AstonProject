using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class MovementRequestExtensions
    {
        AstonContext _context = new AstonContext();

        public MovementRequest GetMovementRequestByID(int id)
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Where(p => p.ID == id).FirstOrDefault();
        }

        public MovementRequestDetail GetMovementRequestDetailByID(int id)
        {
            return _context.MovementRequestDetail.Where(p => p.ID == id).FirstOrDefault();
        }
        public List<MovementRequest> GetMovementRequest()
        {
            return _context.MovementRequest.Include(p => p.MovementRequestDetail).Where(p => p.DeletedDate == null && p.DeletedBy == null).ToList();
        }
    }
}
