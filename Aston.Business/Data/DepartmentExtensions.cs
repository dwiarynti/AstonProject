using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class DepartmentExtensions
    {
        AstonContext context = new AstonContext();


        public List<Department> GetDepartment ()
        {
            var obj = context.Department.Where(p => p.IsActive == true).ToList();
            return obj;
        }

        public Department GetDepartmentByID(int id)
        {
            var obj = context.Department.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }
    }
}
