/**
 * movementrequest Controller
 */

app.controller('AssetHistoryCtrl', function ($scope, $rootScope, $stateParams, assetResource, commonService) {
    var assetResources = new assetResource();

    $scope.AssetMovementHistory = {};
    $rootScope.PageName = "Asset History";
    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    //pagination
    $scope.NumberofAssetHistory = 0;
    $scope.bigCurrentPage = 1;

    $scope.init = function() {
        assetResources.Asset = {
            ID: $stateParams.ID,
        };
        assetResources.Skip = $scope.bigCurrentPage - 1;
        assetResources.$AssetMovementHistory(function (data) {
            if (data.success) {
                $scope.NumberofAssetHistory = data.obj.History.length != 0 ? data.obj.History[0].TotalRow : 0;
                $scope.AssetMovementHistory = data.obj;
                angular.forEach($scope.AssetMovementHistory.History, function(data) {
                    data.MovementDate = commonService.convertdate(data.MovementDate);
                });
            }
        });
    }

    $scope.init();

});