(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("assetLocationResource",
                ["$resource",
                 assetLocationResource]);
    function assetLocationResource($resource) {
        return $resource("/api/AssetLocation/:action",
        { id: '@id' },
        {
            GetAssetLocation: { method: 'GET', params: { action: 'GetAssetLocation' } },
            CreateAssetLocation: { method: 'POST', params: { action: 'MoveAsset' } },
            UpdateAssetLocation: { method: 'POST', params: { action: 'UpdateAssetLocation' } },
            DeleteAssetLocation: { method: 'POST', params: { action: 'DeleteAssetLocation' } },

        });
    }
}());