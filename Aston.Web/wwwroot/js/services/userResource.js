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
            GetRoles: { method: 'GET', params: { action: 'GetRoles' } },
            UserRegister: { method: 'POST', params: { action: 'UserRegister' } },
            GenerateUserCode: { method: 'POST', params: { action: 'GenerateUserCode' } },
            UserEdit: { method: 'POST', params: { action: 'UserEdit' } },
            ResetUserPassword: { method: 'POST', params: { action: 'ResetUserPassword' } },
            AssignUserRole: { method: 'POST', params: { action: 'AssignUserRole' } },
            DeleteUser: { method: 'POST', params: { action: 'DeleteUser' } },

        });
    }
}());