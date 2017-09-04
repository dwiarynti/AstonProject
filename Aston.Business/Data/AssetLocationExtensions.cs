﻿using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class AssetLocationExtensions
    {
        AstonContext context = new AstonContext();
        public AssetLocation GetAssetLocationByID(int id)
        {
            var obj = context.AssetLocation.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }

        public List<AssetLocation> GetAssetLocationByLocationID(int id)
        {
            var obj = context.AssetLocation.Where(p => p.LocationID == id && p.DeletedDate == null && p.DeletedBy == null).ToList();
            return obj;
        }

        public List<AssetLocation> GetAssetLocation()
        {
            var obj = context.AssetLocation.Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }
    }
}
