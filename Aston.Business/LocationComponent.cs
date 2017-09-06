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
        GenerateCodeComponent _generatecode = new GenerateCodeComponent();
        PrefComponent _pref = new PrefComponent();
        public LocationViewModel GetLocationByCode(string code)
        {
            LocationViewModel result = new LocationViewModel();
            var location = _location.GetLocationByCode(code);
            var statuscdname = _pref.GetPrefByStatusCode(location.StatusCD);
            var locationtypename = _pref.GetPrefByLocationTypeCode(location.LocationTypeCD);

            result.ID = location.ID;
            result.Code = location.Code;
            result.Description = location.Description;
            result.No = location.No;
            result.Floor = location.Floor;
            result.LocationTypeCD = location.LocationTypeCD;
            result.StatusCD = location.StatusCD;
            result.StatusCDName = statuscdname.Value;
            result.LocationTypeCDName = locationtypename.Value;
            return result;
        }

        public LocationViewModel GetLocationByID(int id)
        {
            LocationViewModel result = new LocationViewModel();
            var location = _location.GetLocationByID(id);
            var statuscdname = _pref.GetPrefByStatusCode(location.StatusCD);
            var locationtypename = _pref.GetPrefByLocationTypeCode(location.LocationTypeCD);

            result.ID = location.ID;
            result.Code = location.Code;
            result.Description = location.Description;
            result.No = location.No;
            result.Floor = location.Floor;
            result.LocationTypeCD = location.LocationTypeCD;
            result.StatusCD = location.StatusCD;
            result.StatusCDName = statuscdname.Value;
            result.LocationTypeCDName = locationtypename.Value;

            return result;
        }
       
        public List<LocationViewModel> GetLocation()
        {
            List<LocationViewModel> result = new List<LocationViewModel>();
            var location = _location.GetLocation();
            foreach(var item in location)
            {
                LocationViewModel model = new LocationViewModel();
                var statuscdname = _pref.GetPrefByStatusCode(item.StatusCD);
                var locationtypename = _pref.GetPrefByLocationTypeCode(item.LocationTypeCD);

                model.ID = item.ID;
                model.Code = item.Code;
                model.Description = item.Description;
                model.No = item.No;
                model.Floor = item.Floor;
                model.LocationTypeCD = item.LocationTypeCD;
                model.StatusCD = item.StatusCD;
                model.StatusCDName = statuscdname.Value;
                model.LocationTypeCDName = locationtypename.Value;

                result.Add(model);
            }
            return result;
        }

        public bool CreateLocation (LocationViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if(obj != null)
            {
               try
                {
                    obj.No = _location.GetLastNumberLocation();
                    obj.SubCategory = _generatecode.SubCategoryLocation(obj.LocationTypeCD, obj.Floor);
                    obj.Number = _generatecode.Number(obj.No);
                    obj.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, obj.Number);

                    Location location = new Location();
                    location.Code = obj.Code;
                    location.Description = obj.Description;
                    location.No = obj.Number;
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

        public bool UpdateLocation(LocationViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var location = _location.GetLocationByID(obj.ID);
            if(location!= null)
            {
                try
                {
                   
                       
                    obj.SubCategory = _generatecode.SubCategoryLocation(obj.LocationTypeCD, obj.Floor);
                    obj.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, location.No);
                    if (location.Code != obj.Code)
                    {
                        location.Code = obj.Code;
                          
                    }
                    location.Description = obj.Description;
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
