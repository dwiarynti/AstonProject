/**
 * Asset Controller
 */

app.controller('AssetLocationCtrl', function ($scope, $rootScope, assetResource, locationResource, assetLocationResource, commonService) {
    var assetResources = new assetResource();
    var locationResources = new locationResource();
    var assetLocationResources = new assetLocationResource();
    $scope.isValidate = true;
    $scope.locationlist = [];
    $scope.assetlist = [];
    $scope.assetlocationlist = [];
    $scope.assetlocation = {};
    $scope.actionstatus = "";
    $rootScope.PageName = "Asset Location";
    $scope.OnTransitionStatus = false;

    //pagination
    $scope.NumberofLocation = 0;
    $scope.bigCurrentPage = 1;

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    function AssetLocationModel() {
        return {
            ID: "temp",
            AssetID: null,
            LocationID: null,
            OnTransition: $scope.OnTransitionStatus,
            //MovementRequestDetailID: null,
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
        };
    }

    $scope.GetAsset = function() {
        assetResources.$GetAsset(function (data) {
            $scope.assetlist = data.obj;
        });
    }

    $scope.GetLocation = function() {
        locationResources.$GetLocation(function (data) {
            $scope.locationlist = data.obj;
        });
    }

    $scope.init = function () {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.$AssetLocation_Pagination({ Skip: $scope.bigCurrentPage-1 }, function (data) {
            if (data.success) {
                $scope.NumberofAsset = data.obj.length != 0 ? data.obj[0].AssetLocation.TotalRow : 0;

                //$scope.NumberofAsset = data.obj[0].AssetLocation.TotalRow;
                $scope.assetlocationlist = data.obj;
            }
        });
        //assetLocationResources.$GetAssetLocation(function (data) {
        //    $scope.assetlocationlist = data.obj;
        //});
        $scope.GetAsset();
        $scope.GetLocation();
    }

    $scope.init();

    $scope.add = function () {
        $scope.assetlocation = AssetLocationModel();
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.create = function () {
        $scope.isValidate = commonService.validationform(AssetLocationModel(), $scope.assetlocation);
        if ($scope.isValidate) {
            $scope.CreateAssetLocation();
        }
    }

    $scope.CreateAssetLocation = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.AssetID = parseInt($scope.assetlocation.AssetID);
        assetLocationResources.LocationID = parseInt($scope.assetlocation.LocationID);
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        assetLocationResources.$CreateAssetLocation(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function(obj) {
        $scope.assetlocation = angular.copy(obj);
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.update = function () {

        $scope.assetlocation.OnTransition = $scope.OnTransitionStatus ? $scope.assetlocation.OnTransition : $scope.OnTransitionStatus;
        $scope.isValidate = commonService.validationform(AssetLocationModel(), $scope.assetlocation);
        if ($scope.isValidate) {
            $scope.UpdateAssetLocation();
        }
    }

    $scope.UpdateAssetLocation = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.ID = $scope.assetlocation.ID;
        assetLocationResources.AssetID = parseInt($scope.assetlocation.AssetID);
        assetLocationResources.LocationID = parseInt($scope.assetlocation.LocationID);
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        assetLocationResources.$UpdateAssetLocation(function (data) {
            if (data.success) {
                $scope.assetlocation = AssetLocationModel();
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

    $scope.deletemodal = function (obj) {
        $scope.assetlocation = angular.copy(obj);
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }
    $scope.delete = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.ID = $scope.assetlocation.ID;
        assetLocationResources.AssetID = parseInt($scope.assetlocation.AssetID);
        assetLocationResources.LocationID = parseInt($scope.assetlocation.LocationID);
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        assetLocationResources.$DeleteAssetLocation(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    //
    $scope.selected ="";
    $scope.names = ["john", "bill", "charlie", "robert", "alban", "oscar", "marie", "celine", "brad", "drew", "rebecca", "michel", "francis", "jean", "paul", "pierre", "nicolas", "alfred", "gerard", "louis", "albert", "edouard", "benoit", "guillaume", "nicolas", "joseph"];

});