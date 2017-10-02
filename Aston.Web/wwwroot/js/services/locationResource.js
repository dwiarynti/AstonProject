(function () {
    "use strict";
    app.factory("locationResource",
                ["$resource",
                 locationResource]);
    function locationResource($resource) {
        return $resource("/api/Location/:action",
        { id: '@id' },
        {
            GetLocation: { method: 'GET', params: { action: 'GetLocation' } },
            CreateLocation: { method: 'POST', params: { action: 'CreateLocation' } },
            UpdateLocation: { method: 'POST', params: { action: 'UpdateLocation' } },
            DeleteLocation: { method: 'POST', params: { action: 'DeleteLocation' } },
            SearchLocation: { method: 'POST', params: { action: 'SearchLocation' } },

        });
    }
}());