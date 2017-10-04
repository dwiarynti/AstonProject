(function () {
    "use strict";
    app.factory("lookuplistResource",
                ["$resource",
                 lookuplistResource]);
    function lookuplistResource($resource) {
        return $resource("/api/LookupList/:action",
        { id: '@id' },
        {
            GetCategory: { method: 'GET', params: { action: 'GetCategory' } },
            GetLocationType: { method: 'GET', params: { action: 'GetLocationType' } },
            GetStatus: { method: 'GET', params: { action: 'GetStatus' } },
            GetDepartment: { method: 'GET', params: { action: 'GetDepartment' } },
            GetDepartmentByID: { method: 'GET', params: { action: 'GetDepartmentByID' } },
            GetApprovalStatus: { method: 'GET', params: { action: 'GetApprovalStatus' } },

        });
    }
}());