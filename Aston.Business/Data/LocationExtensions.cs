using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class LocationExtensions
    {
        AstonContext context = new AstonContext();

        public Location GetLocationByCode(string code)
        {
            var obj = context.Location.Where(p => p.Code == code).FirstOrDefault();
            return obj;
        }
        public Location GetLocationByID(int id)
        {
            var obj = context.Location.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }
        public List<Location> GetLocation()
        {
            var obj = context.Location.Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }

    }
}
