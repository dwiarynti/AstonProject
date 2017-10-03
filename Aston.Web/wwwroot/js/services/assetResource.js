(function () {
    "use strict";
   app.factory("assetResource",
                ["$resource",
                 assetResource]);
    function assetResource($resource) {
        return $resource("/api/Asset/:action/:id",
        { id: '@id' },
        {
            GetAsset: { method: 'GET', params: { action: 'GetAsset' } },
            CreateAsset: { method: 'POST', params: { action: 'CreateAsset' } },
            UpdateAsset: { method: 'POST', params: { action: 'UpdateAsset' } },
            DeleteAsset: { method: 'POST', params: { action: 'DeleteAsset' } },
            GetAssetByCategoryCode: { method: 'GET', params: { action: 'GetAssetByCategoryCode' } },
            SearchAsset: { method: 'POST', params: { action: 'SearchAsset' } },

        });
    }
}());