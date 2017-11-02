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
    public class RoleComponent
    {
        AstonContext _context = new AstonContext();
        RoleExtensions _role = new RoleExtensions();
        public List<RolePaginationViewModel> GetRolePagination (int Skip)
        {
            return _role.GetRole_Pagination(Skip);
        }

    }
}
