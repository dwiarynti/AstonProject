(function () {
    "use strict";
    app.factory("prefResource",
                ["$resource",
                 prefResource]);
    function prefResource($resource) {
        return $resource("/api/Pref/:action",
        { id: '@id' },
        {
            GetCategory: { method: 'GET', params: { action: 'GetCategory' } },
            GetLocationType: { method: 'GET', params: { action: 'GetLocationType' } },
            GetStatus: { method: 'GET', params: { action: 'GetStatus' } },

        });
    }
}());