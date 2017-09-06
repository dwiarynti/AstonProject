/**
 * Asset Controller
 */

app.controller('AssetLocationCtrl', function ($scope, assetResource, locationResource, assetLocationResource) {
    var assetResources = new assetResource();
    var locationResources = new locationResource();
    var assetLocationResources = new assetLocationResource();
    $scope.isValidate = true;
    $scope.locationlist = [];
    $scope.assetlist = [];
    $scope.assetlocationlist = [];
    $scope.assetlocation = {};
    $scope.actionstatus = "";
    $scope.categorylist = [
        { id: 1, value: "Furniture" },
        { id: 2, value: "Electronic" },
        { id: 3, value: "Storage" },
        { id: 4, value: "Computer" },
    ];

    function AssetLocationModel() {
        return {
            ID: "temp",
            AssetID: null,
            LocationID: null,
            OnTransition: null,
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

    $scope.init = function() {
        assetLocationResources.$GetAssetLocation(function (data) {
            console.log(data);
            $scope.assetlocationlist = data.obj;
        });
        $scope.GetAsset();
        $scope.GetLocation();

        console.log($scope.assetlist);
        console.log($scope.locationlist);
    }

    $scope.init();

    $scope.add = function () {
        $scope.assetlocation = AssetLocationModel();
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.create = function () {
        $scope.isValidate = $scope.validationform();
        if ($scope.isValidate) {
            $scope.CreateAssetLocation();
        }
    }

    $scope.validationform = function () {
        var validationstatus = true;
        var keys = Object.keys(AssetLocationModel());
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            var value = $scope.assetlocation[key];
            var datatype = typeof value;
            if (datatype != "boolean" && validationstatus) {
                if (value == null || value == "") {
                    validationstatus = false;
                    break;
                }
            } else if (!validationstatus) {
                validationstatus = false;
                break;
            }

        }
        return validationstatus;
    }

    $scope.CreateAssetLocation = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.AssetID = parseInt($scope.assetlocation.AssetID);
        assetLocationResources.LocationID = parseInt($scope.assetlocation.LocationID);
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        console.log(assetLocationResources);
        assetLocationResources.$CreateAssetLocation(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function(obj) {
        $scope.assetlocation = angular.copy(obj);
        console.log($scope.assetlocation);
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.update = function() {
        $scope.isValidate = $scope.validationform();
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