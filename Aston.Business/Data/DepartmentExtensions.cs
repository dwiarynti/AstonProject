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
    public class DepartmentExtensions
    {
        AstonContext context = new AstonContext();

        public List<Department> GetActiveDepartment()
        {
            var obj = context.Department.Where(p => p.IsActive == true).ToList();
            return obj;
        }

        public Department GetDepartmentByID(int id)
        {
            var obj = context.Department.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }

        public List<DepartmentViewModel> GetDepartment_Pagination(int Skip)
        {
            var result = new List<DepartmentViewModel>();
            var obj = new DepartmentViewModel();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_DepartmentPagination";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {
                    var list = dbContext.DataReaderMapToList<DepartmentViewModel>(reader);
                    foreach (var item in list)
                    {
                        result.Add(item);
                    }
                    cmd.Connection.Close();
                }
            }
            return result;
        }
    }
}
