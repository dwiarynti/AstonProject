(function () {
    "use strict";
    app.factory("movementrequestResource",
                ["$resource",
                 movementrequestResource]);
    function movementrequestResource($resource) {
        return $resource("/api/movementrequest/:action",
        { id: '@id' },
        {
            Getmovementrequest: { method: 'GET', params: { action: 'Getmovementrequest' } },
            Createmovementrequest: { method: 'POST', params: { action: 'Createmovementrequest' } },
            Updatemovementrequest: { method: 'POST', params: { action: 'Updatemovementrequest' } },
            Deletemovementrequest: { method: 'POST', params: { action: 'Deletemovementrequest' } },

        });
    }
}());