(function () {
    "use strict";
   app.factory("roleResource",
                ["$resource",
                 roleResource]);
    function roleResource($resource) {
        return $resource("/api/Role/:action/:id",
        { id: '@id' },
        {
            GetRolePagination: { method: 'POST', params: { action: 'GetRolePagination' } },
            GetRoles: { method: 'GET', params: { action: 'GetRoles' } },
            RoleRegister: { method: 'POST', params: { action: 'RoleRegister' } },
            RoleEdit: { method: 'POST', params: { action: 'RoleEdit' } },
            DeleteRole: { method: 'POST', params: { action: 'DeleteRole' } },

        });
    }
}());