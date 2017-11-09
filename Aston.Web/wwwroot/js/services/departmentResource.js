(function () {
    "use strict";
   app.factory("departmentResource",
                ["$resource",
                 departmentResource]);
    function departmentResource($resource) {
        return $resource("/api/Department/:action/:id",
        { id: '@id' },
        {
            GetDepartments: { method: 'GET', params: { action: 'GetDepartments' } },
            GetActiveDepartments: { method: 'GET', params: { action: 'GetActiveDepartments' } },
            CreateDepartment: { method: 'POST', params: { action: 'CreateDepartment' } },
            UpdateDepartment: { method: 'POST', params: { action: 'UpdateDepartment' } },
            DeleteDepartment: { method: 'POST', params: { action: 'DeleteDepartment' } },
            GetDepartmentByID: { method: 'GET', params: { action: 'GetDepartmentByID' } }
           
        });
    }
}());