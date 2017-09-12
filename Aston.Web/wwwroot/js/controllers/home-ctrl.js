/**
 * Asset Controller
 */

app.controller('HomeCtrl', function ($scope, $rootScope, movementrequestResource, commonService) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $rootScope.PageName = "Home";
    $scope.data = "Welcome";


    $scope.init = function () {
        movementrequestResources.$GetMovementRequestNeedApproval(function (data) {
            angular.forEach(data.obj, function (obj) {
                obj.MovementDate = commonService.convertdate(obj.MovementDate);
                $scope.movementrequestlist.push(obj);

            });
        });
    }
    $scope.init();

    $scope.Approve = function (obj) {
        movementrequestResources.ID = obj.ID;
        movementrequestResources.ApprovalStatus = 1;
        movementrequestResources.$ApproveMovementRequest(function (data) {
            if (data.success) {
                $scope.init();
            }
        });
    }

});