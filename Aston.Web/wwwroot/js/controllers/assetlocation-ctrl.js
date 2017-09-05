/**
 * Asset Controller
 */

app.controller('AssetLocationCtrl', function ($scope, assetLocationResource) {
    var assetLocationResources = new assetLocationResource();
    $scope.isValidate = true;
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
            ID: 0,
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

    assetLocationResources.$GetAssetLocation(function (data) {
        console.log(data);
        $scope.assetlocationlist = data.obj;
    });

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
        var keys = Object.keys($scope.location);
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            if ($scope.location[key] == null || $scope.location[key] == '') {
                validationstatus = false;
                break;
            }
        }
        return validationstatus;
    }

    $scope.CreateAssetLocation = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.AssetID = $scope.assetlocation.AssetID;
        assetLocationResources.LocationID = $scope.assetlocation.LocationID;
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        console.log(assetLocationResources);
        assetLocationResources.$CreateAsset(function (data) {
            $scope.assetlocationlist = data.obj;
        });
    }

    $scope.edit = function(obj) {
        $scope.assetlocation = obj;
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
        assetLocationResources.AssetID = $scope.assetlocation.AssetID;
        assetLocationResources.LocationID = $scope.assetlocation.LocationID;
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        assetLocationResources.$UpdateAsset(function (data) {
            $scope.assetlocationlist = data.obj;
        });
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

    $scope.deletemodal = function () {
        $scope.assetlocation = AssetLocationModel();
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }
    $scope.delete = function() {
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.ID = $scope.assetlocation.ID;
        assetLocationResources.AssetID = $scope.assetlocation.AssetID;
        assetLocationResources.LocationID = $scope.assetlocation.LocationID;
        assetLocationResources.OnTransition = $scope.assetlocation.OnTransition;
        assetLocationResources.$DeleteAssetLocation(function (data) {
            $scope.assetlocationlist = data.obj;
        });
    }

});