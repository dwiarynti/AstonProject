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
        LookupListComponent _pref = new LookupListComponent();
        public LocationViewModel GetLocationByCode(string code)
        {
            LocationViewModel result = new LocationViewModel();
            result.Location = new LocationSearchResult();
            var location = _location.GetLocationByCode(code);
            var statuscdname = _pref.GetLookupByStatusCode(location.StatusCD);
            var locationtypename = _pref.GetLookupByLocationTypeCode(location.LocationTypeCD);

            result.Location.ID = location.ID;
            result.Location.Code = location.Code;
            result.Location.Description = location.Description;
            result.Location.No = location.No;
            result.Location.Name = location.Name;
            result.Location.Floor = location.Floor;
            result.Location.LocationTypeCD = location.LocationTypeCD;
            result.Location.StatusCD = location.StatusCD;
            result.Location.StatusCDName = statuscdname.Value;
            result.Location.LocationTypeCDName = locationtypename.Value;
            return result;
        }

        public LocationViewModel GetLocationByID(int id)
        {
            LocationViewModel result = new LocationViewModel();
            result.Location = new LocationSearchResult();
            var location = _location.GetLocationByID(id);
            var statuscdname = _pref.GetLookupByStatusCode(location.StatusCD);
            var locationtypename = _pref.GetLookupByLocationTypeCode(location.LocationTypeCD);

            result.Location.ID = location.ID;
            result.Location.Code = location.Code;
            result.Location.Name = location.Name;
            result.Location.Description = location.Description;
            result.Location.No = location.No;
            result.Location.Floor = location.Floor;
            result.Location.LocationTypeCD = location.LocationTypeCD;
            result.Location.StatusCD = location.StatusCD;
            result.Location.StatusCDName = statuscdname.Value;
            result.Location.LocationTypeCDName = locationtypename.Value;

            return result;
        }
       
        public List<LocationViewModel> GetLocation()
        {
            List<LocationViewModel> result = new List<LocationViewModel>();
            var location = _location.GetLocation();
            foreach(var item in location)
            {
                LocationViewModel model = new LocationViewModel();
                model.Location = new LocationSearchResult();
                var statuscdname = _pref.GetLookupByStatusCode(item.StatusCD);
                var locationtypename = _pref.GetLookupByLocationTypeCode(item.LocationTypeCD);

                model.Location.ID = item.ID;
                model.Location.Code = item.Code;
                model.Location.Name = item.Name;
                model.Location.Description = item.Description;
                model.Location.No = item.No;
                model.Location.Floor = item.Floor;
                model.Location.LocationTypeCD = item.LocationTypeCD;
                model.Location.StatusCD = item.StatusCD;
                model.Location.StatusCDName = statuscdname.Value;
                model.Location.LocationTypeCDName = locationtypename.Value;

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
                    obj.Location.No = _location.GetLastNumberLocation();
                    obj.SubCategory = _generatecode.SubCategoryLocation(Convert.ToInt16(obj.Location.LocationTypeCD), obj.Location.Floor);
                    obj.Number = _generatecode.Number(obj.Location.No);
                    obj.Location.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, obj.Number);

                    Location location = new Location();
                    location.Code = obj.Location.Code;
                    location.Description = obj.Location.Description;
                    location.No = obj.Number;
                    location.Name = obj.Location.Name;
                    location.Floor = obj.Location.Floor;
                    location.LocationTypeCD = Convert.ToInt16(obj.Location.LocationTypeCD);
                    location.StatusCD = obj.Location.StatusCD;
                    location.CreatedBy = obj.CreatedBy;
                    location.CreatedDate = obj.CreatedDate;//DateTime.Now;
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
            var location = _location.GetLocationByID(obj.Location.ID);
            if(location!= null)
            {
                try
                {
                   
                       
                    obj.SubCategory = _generatecode.SubCategoryLocation(Convert.ToInt16(obj.Location.LocationTypeCD), obj.Location.Floor);
                    obj.Location.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, location.No);
                    if (location.Code != obj.Location.Code)
                    {
                        location.Code = obj.Location.Code;
                          
                    }
                    location.Description = obj.Location.Description;
                    location.Name = obj.Location.Name;
                    location.Floor = obj.Location.Floor;
                    location.LocationTypeCD = Convert.ToInt16(obj.Location.LocationTypeCD);
                    location.StatusCD = obj.Location.StatusCD;
                    location.UpdatedBy = obj.UpdatedBy;
                    location.UpdatedDate = obj.UpdatedDate;//DateTime.Now;

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
                    location.DeletedDate = obj.DeletedDate;//DateTime.Now;
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

        public List<LocationViewModel> SearchLocation(LocationViewModel obj)
        {
            var result = _location.SearchLocation_SP(Convert.ToInt16(obj.Location.LocationTypeCD), obj.Location.Floor, obj.Skip);
            foreach (var item in result)
            {
                var statuscdname = _pref.GetLookupByStatusCode(item.Location.StatusCD);
                var locationtypename = _pref.GetLookupByLocationTypeCode(Convert.ToInt16(item.Location.LocationTypeCD));

                item.Location.StatusCDName = statuscdname.Value;
                item.Location.LocationTypeCDName = locationtypename.Value;
            }
            return result;
        }
    }
}
