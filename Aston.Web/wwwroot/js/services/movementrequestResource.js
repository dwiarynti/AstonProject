(function () {
    "use strict";
    app.factory("movementrequestResource",
                ["$resource",
                 movementrequestResource]);
    function movementrequestResource($resource) {
        return $resource("/api/MovementRequest/:action",
        { id: '@id' },
        {
            GetMovementRequest: { method: 'GET', params: { action: 'GetMovementRequest' } },
            CreateMovementRequest: { method: 'POST', params: { action: 'CreateMovementRequest' } },
            UpdateMovementRequest: { method: 'POST', params: { action: 'UpdateMovementRequest' } },
            DeleteMovementRequest: { method: 'POST', params: { action: 'DeleteMovementRequest' } },

        });
    }
}());