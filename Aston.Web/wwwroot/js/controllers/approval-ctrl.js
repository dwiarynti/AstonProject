/**
 * Asset Controller
 */

app.controller('ApprovalCtrl', function ($scope, $rootScope, $window, $state, movementrequestResource, commonService, transferobjectService) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $rootScope.PageName = "Approval";
    $scope.data = "Welcome";


    $scope.init = function () {
        $scope.movementrequestlist = [];
        movementrequestResources.$GetMovementRequestNeedApproval(function (data) {
            $scope.ApprovalNumber = data.obj.length;
            angular.forEach(data.obj, function (obj) {
                obj.MovementRequest.MovementDate = commonService.convertdate(obj.MovementRequest.MovementDate);
                $scope.movementrequestlist.push(obj);

            });
        });
    }
    $scope.init();

    $scope.Approve = function (obj) {
        movementrequestResources.ID = obj.MovementRequest.ID;
        movementrequestResources.ApprovalStatus = 1;
        movementrequestResources.$ApproveMovementRequest(function (data) {
            if (data.success) {
                $window.alert("Data approved successfully");
                $scope.init();
            }
        });
    }

    $scope.Details = function (obj) {
        transferobjectService.addObj = { action: 'detail', data: obj };
        $state.go('movementrequestdetailmanagement');
    }

});