(function () {
    "use strict";
    app.factory("movementrequestResource",
                ["$resource",
                 movementrequestResource]);
    function movementrequestResource($resource) {
        return $resource("/api/MovementRequest/:action/:Skip/:id",
        { id: '@id' },
        {
            GetMovementRequest: { method: 'GET', params: { action: 'GetMovementRequest' } },
            CreateMovementRequest: { method: 'POST', params: { action: 'CreateMovementRequest' } },
            UpdateMovementRequest: { method: 'POST', params: { action: 'UpdateMovementRequest' } },
            DeleteMovementRequest: { method: 'POST', params: { action: 'DeleteMovementRequest' } },
            GetMovementRequestNeedApproval: { method: 'GET', params: { action: 'GetMovementRequestNeedApproval' } },
            ApproveMovementRequest: { method: 'POST', params: { action: 'ApproveMovementRequest' } },
            SearchMovementRequest: { method: 'POST', params: { action: 'SearchMovementRequest' } },
            GetMovementRequestByID: { method: 'GET', params: { action: 'GetMovementRequestByID' } },

        });
    }
}());