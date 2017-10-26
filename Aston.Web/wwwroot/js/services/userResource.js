(function () {
    "use strict";
   app.factory("userResource",
                ["$resource",
                 userResource]);
    function userResource($resource) {
        return $resource("/api/User/:action/:id",
        { id: '@id' },
        {
            GetUserPagination: { method: 'POST', params: { action: 'GetUserPagination' } },
            UserRegister: { method: 'POST', params: { action: 'UserRegister' } },

        });
    }
}());