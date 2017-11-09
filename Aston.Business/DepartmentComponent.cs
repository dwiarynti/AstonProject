using Aston.Business.Data;
using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.Utils;
using System.Globalization;
using OfficeOpenXml.Style;

namespace Aston.Business
{
    public class DepartmentComponent
    {
        AstonContext _context = new AstonContext();
        DepartmentExtensions _extension = new DepartmentExtensions();

        public List<DepartmentViewModel> GetActiveDepartments()
        {
            List<DepartmentViewModel> result = new List<DepartmentViewModel>();
            var list = _extension.GetActiveDepartment();
            foreach (var item in list)
            {
                DepartmentViewModel model = new DepartmentViewModel();

                model.ID = item.ID;
                model.Name = item.Name;
                model.Description = item.Description;
                model.IsActive = item.IsActive;

                result.Add(model);
            }
            return result;
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            List<DepartmentViewModel> result = new List<DepartmentViewModel>();
            var list = _context.Department.ToList();
            foreach (var item in list)
            {
                DepartmentViewModel model = new DepartmentViewModel();

                model.ID = item.ID;
                model.Name = item.Name;
                model.Description = item.Description;
                model.IsActive = item.IsActive;

                result.Add(model);
            }
            return result;
        }

        public DepartmentViewModel GetDepartmentByID(int id)
        {
            DepartmentViewModel result = new DepartmentViewModel();

            var obj = _extension.GetDepartmentByID(id);

            result.ID = obj.ID;
            result.Name = obj.Name;
            result.Description = obj.Description;
            result.IsActive = obj.IsActive;

            return result;
        }

        public List<DepartmentViewModel> GetDepartmentPagination(int Skip)
        {
            return _extension.GetDepartment_Pagination(Skip);
        }

        public bool CreateDepartment(DepartmentViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if (obj != null)
            {
                try
                {
                    //TODO : Check if same department name is exist

                    Department asset = new Department();
                    asset.ID = obj.ID;
                    asset.Name = obj.Name;
                    asset.Description = obj.Description;
                    asset.IsActive = obj.IsActive;

                    _context.Department.Add(asset);
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

        public bool UpdateDepartment(DepartmentViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var prevObj = _extension.GetDepartmentByID(obj.ID);
            if (prevObj != null)
            {
                try
                {
                    prevObj.Name = obj.Name;
                    prevObj.Description = obj.Description;
                    prevObj.IsActive = obj.IsActive;

                    _context.Entry(prevObj).State = EntityState.Modified;
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

        public bool DeleteDepartment(DepartmentViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var prevObj = _extension.GetDepartmentByID(obj.ID);
            if (prevObj != null)
            {
                try
                {
                    // uncomment after adding audit field on table department
                    //prevObj.DeletedBy = obj.DeletedBy;
                    //prevObj.DeletedDate = obj.DeletedDate;//DateTime.Now;

                    prevObj.IsActive = false;

                    _context.Entry(prevObj).State = EntityState.Modified;
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
