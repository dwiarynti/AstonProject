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
    public class LocationComponent
    {
        AstonContext _context = new AstonContext();
        LocationExtensions _location = new LocationExtensions();
        public Location GetLocationByCode(string code)
        {
            Location result = new Location();
            result = _location.GetLocationByCode(code);
            return result;
        }

        public Location GetLocationByID(int id)
        {
            Location result = new Location();
            result = _location.GetLocationByID(id);
            return result;
        }
       
        public List<Location> GetLocation()
        {
            List<Location> result = new List<Location>();
            result = _location.GetLocation();
            return result;
        }

        public bool CreateLocation (Location obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if(obj != null)
            {
               try
                {
                    Location location = new Location();
                    location.Code = obj.Code;
                    location.Description = obj.Description;
                    location.No = obj.No;
                    location.Name = obj.Name;
                    location.Floor = obj.Floor;
                    location.LocationTypeCD = obj.LocationTypeCD;
                    location.StatusCD = obj.StatusCD;
                    location.CreatedBy = obj.CreatedBy;
                    location.CreatedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    _context.Location.Add(location);
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

        public bool UpdateLocation(Location obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var location = _location.GetLocationByID(obj.ID);
            if(location!= null)
            {
                try
                {
                    location.Code = obj.Code;
                    location.Description = obj.Description;
                    location.No = obj.No;
                    location.Name = obj.Name;
                    location.Floor = obj.Floor;
                    location.LocationTypeCD = obj.LocationTypeCD;
                    location.StatusCD = obj.StatusCD;
                    location.UpdatedBy = obj.UpdatedBy;
                    location.UpdatedDate = DateTime.Now.Date.ToString("ddMMyyyy");

                    _context.Entry(location).State = EntityState.Modified;
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

        public bool DeleteLocation(Location obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var location = _location.GetLocationByID(obj.ID);
            if (location != null)
            {
                try
                {
                    location.DeletedBy = obj.DeletedBy;
                    location.DeletedDate = DateTime.Now.Date.ToString("ddMMyyyy");
                    _context.Entry(location).State = EntityState.Modified;
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
