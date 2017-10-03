(function () {
    "use strict";
    app.factory("assetLocationResource",
                ["$resource",
                 assetLocationResource]);
    function assetLocationResource($resource) {
        return $resource("/api/AssetLocation/:action/:id/:Skip",
        { id: '@id' },
        {
            GetAssetLocation: { method: 'GET', params: { action: 'GetAssetLocation' } },
            CreateAssetLocation: { method: 'POST', params: { action: 'CreateAssetLocation' } },
            UpdateAssetLocation: { method: 'POST', params: { action: 'UpdateAssetLocation' } },
            DeleteAssetLocation: { method: 'POST', params: { action: 'DeleteAssetLocation' } },
            GetAssetLocationByMovementDetailID: { method: 'GET', params: { action: 'GetAssetLocationByMovementDetailID' } },
            AssetLocation_Pagination: { method: 'GET', params: { action: 'AssetLocation_Pagination' } },


        });
    }
}());