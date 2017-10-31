/**
 * Master Controller
 */

angular.module('RDash')
    .controller('MasterCtrl', ['$scope', '$rootScope', '$state', 'movementrequestResource', MasterCtrl]);

function MasterCtrl($scope, $rootScope, $state, movementrequestResource) {
    var movementrequestResources = new movementrequestResource();

    //$state.go('home');
    $rootScope.PageName = '';
    $rootScope.openassetlocation = false;
    $rootScope.ApprovalNumber = 0;

    $scope.GetApprovalNumber = function () {
        $scope.movementrequestlist = [];
        movementrequestResources.$GetMovementRequestNeedApproval(function (data) {
            $rootScope.ApprovalNumber = data.obj.length;
        });
    }

    $scope.GetApprovalNumber();
}