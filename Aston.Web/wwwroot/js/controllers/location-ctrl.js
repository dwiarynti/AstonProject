/**
 * Location Controller
 */

app.controller('LocationCtrl', function ($scope, $rootScope, locationResource, prefResource) {
    var locationResources = new locationResource();
    var prefResources = new prefResource();

    $scope.isValidate = true;
    $scope.locationlist = [];
    $scope.location = {};
    $scope.actionstatus = "";
    $scope.locationtypelist = [];
    $rootScope.PageName = "Location";


    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    function LocationModel() {
        return {
            ID: "temp",
            //Code: null,
            Description: null,
            //No: null,
            Name: null,
            Floor: null,
            LocationTypeCD: null,
            //StatusCD: null,
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
        };
    }

    $scope.init = function() {
        locationResources.$GetLocation(function (data) {
            $scope.locationlist = data.obj;
        });
        $scope.GetLocationType();
    }

    $scope.GetLocationType = function () {
        prefResources.$GetLocationType(function (data) {
            $scope.locationtypelist = data.obj;
        });
    }

    $scope.init();

    $scope.add = function () {
        $scope.location = LocationModel();
        $scope.isValidate = true;
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }



    $scope.create = function () {
        $scope.isValidate = $scope.validationform();
        if ($scope.isValidate) {
            $scope.CreateLocation();
        }
    }

    $scope.validationform = function () {
        var validationstatus = true;
        var keys = Object.keys(LocationModel());
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            if ($scope.location[key] == null || $scope.location[key] == '') {
                validationstatus = false;
                break;
            }
        }
        return validationstatus;
    }

    $scope.CreateLocation = function() {
        var locationResources = new locationResource();
        locationResources.Name = $scope.location.Name;
        locationResources.Description = $scope.location.Description;
        locationResources.Floor = $scope.location.Floor;
        locationResources.LocationTypeCD = $scope.location.LocationTypeCD;
        locationResources.StatusCD = 1;
        locationResources.$CreateLocation(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

    $scope.edit = function(obj) {
        $scope.location = angular.copy(obj);
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.update = function() {
        $scope.isValidate = $scope.validationform();
        if ($scope.isValidate) {
            $scope.UpdateLocation();
        }
    }

    $scope.UpdateLocation = function() {
        var locationResources = new locationResource();
        locationResources.ID = $scope.location.ID;
        locationResources.Name = $scope.location.Name;
        locationResources.Description = $scope.location.Description;
        locationResources.Floor = $scope.location.Floor;
        locationResources.LocationTypeCD = $scope.location.LocationTypeCD;
        locationResources.StatusCD = $scope.location.StatusCD;
        locationResources.$UpdateLocation(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

    $scope.deletemodal = function (obj) {
        $scope.location = angular.copy(obj);
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }
    $scope.delete = function() {
        var locationResources = new locationResource();
        locationResources.ID = $scope.location.ID;
        locationResources.Name = $scope.location.Name;
        locationResources.Description = $scope.location.Description;
        locationResources.Floor = $scope.location.Floor;
        locationResources.LocationTypeCD = $scope.location.LocationTypeCD;
        locationResources.StatusCD = $scope.location.StatusCD;
        locationResources.$DeleteLocation(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

});