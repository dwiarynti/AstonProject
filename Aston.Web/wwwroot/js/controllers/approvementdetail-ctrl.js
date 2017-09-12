/**
 * Asset Controller
 */

app.controller('ApprovementDetailCtrl', function ($scope, $rootScope, movementrequestResource) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $rootScope.PageName = "Home";
    $scope.data = "Welcome";

    $scope.init = function () {
        movementrequestResources.$GetMovementRequest(function (data) {
            $scope.movementrequestlist = data.obj;
        });
    }
    $scope.init();

});