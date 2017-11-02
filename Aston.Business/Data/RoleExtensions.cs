using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Aston.Business.Data
{
    public class RoleExtensions
    {
        AstonContext context = new AstonContext();
 
        public List<RolePaginationViewModel> GetRole_Pagination(int Skip)
        {
            var result = new List<RolePaginationViewModel>();
            var obj = new RolePaginationViewModel();

            using (AstonContext dbContext = new AstonContext())
            {
                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_RolePagination";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {
                    var Rolelist = dbContext.DataReaderMapToList<RolePaginationViewModel>(reader);
                    foreach (var Role in Rolelist)
                    {
                        result.Add(Role);
                    }
                    cmd.Connection.Close();
                }
            }
            return result;
        }
    }
}
