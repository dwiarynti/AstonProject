/**
 * Location Controller
 */

app.controller('LocationCtrl', function ($scope, $rootScope, locationResource, lookuplistResource, commonService) {
    var locationResources = new locationResource();
    var lookuplistResources = new lookuplistResource();

    $scope.isValidate = true;
    $scope.locationlist = [];
    $scope.location = {};
    $scope.actionstatus = "";
    $scope.locationtypelist = [];
    $rootScope.PageName = "Location";
    $scope.searchobj = SearchModel();


    //pagination
    $scope.NumberofLocation = 20;
    $scope.bigCurrentPage = 1;


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

    function SearchModel() {
        return {
            LocationTypeCD: null,
            Floor: null,
        };
    }

    $scope.Search = function () {
        var locationResources = new locationResource();
        locationResources.Location = {
            LocationTypeCD: $scope.searchobj.LocationTypeCD == null ? $scope.searchobj.LocationTypeCD : parseInt($scope.searchobj.LocationTypeCD),
            Floor: $scope.searchobj.Floor == "" ? null : $scope.searchobj.Floor
        };
        locationResources.Skip = $scope.bigCurrentPage;
        locationResources.$SearchLocation(function (data) {
            if (data.success) {
                $scope.NumberofLocation = data.obj[0].Location.TotalRow;
                console.log(data);
                $scope.locationlist = data.obj;
            }
        });
    }

    $scope.init = function() {
        $scope.Search();
        //locationResources.$GetLocation(function (data) {
        //    $scope.locationlist = data.obj;
        //});
        $scope.GetLocationType();
    }

    $scope.GetLocationType = function () {
        lookuplistResources.$GetLocationType(function (data) {
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
        $scope.isValidate = commonService.validationform(LocationModel(), $scope.location);
        if ($scope.isValidate) {
            $scope.CreateLocation();
        }
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
        $scope.isValidate = commonService.validationform(LocationModel(), $scope.location);
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
                $scope.location = LocationModel();
                $("#modal-action").modal('hide');
                $scope.init();
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

    $scope.CancelSearch = function () {
        $scope.Search();
        $scope.searchobj = SearchModel();
    }

});