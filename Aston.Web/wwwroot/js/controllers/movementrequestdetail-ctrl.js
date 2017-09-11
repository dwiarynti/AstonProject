/**
 * movementrequest Controller
 */

app.controller('MovementRequestDetailCtrl', function ($scope, $state, $rootScope, transferobjectService, movementrequestResource, lookuplistResource) {
    var movementrequestResources = new movementrequestResource();
    var lookuplistResources = new lookuplistResource();
    $scope.isValidate = true;
    $scope.movementrequestobj = transferobjectService.addObj;
    $scope.movementrequest = {};
    $scope.movementrequestdetailList = [];
    $scope.categorylist = [];
    $rootScope.PageName = "Movement Request Detail";

    //console.log($scope.movementrequestobj.MovementRequestDetail);
    
    $scope.init = function () {
        angular.forEach($scope.movementrequestobj.MovementRequestDetail, function(data) {
            data.editmode = false;
        });
    }

    if ($scope.movementrequestobj.ID == undefined) {
        $state.go('movementrequestmanagement');
    } else {
        $scope.init();
    }

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-movementdate').datepicker({
        todayHighlight: true,
        format: "dd-MM-yyyy"
    });

    $scope.showDatePickerMovementDate = function () {
        $('#datepicker-movementdate').datepicker('show');
    };

    function movementrequesModel() {
        return {
            ID: "temp",
            MovementDate: null,
            Description: null,
            ApprovedDate: null,
            ApprovedBy: null,
            MovementRequestDetail: []
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
        };
    }

    function movementrequestDetailModel() {
        return {
            ID: "temp",
            MovementRequestID: null,
            Description: null,
            AssetCategoryCD: null,
            Quantity: null,
            RequestedTo: null,
            editmode:false,
        //CreatedDate: null,
        //CreatedBy: null,
        //UpdatedDate: null,
        //UpdatedBy: null,
        //DeletedDate: null,
        //DeletedBy: null
    };
    }

    $scope.Add = function () {
        var obj = movementrequestDetailModel();
        obj.editmode = true;
        $scope.movementrequestobj.MovementRequestDetail.push(obj);
    }
    $scope.GetCategory = function () {
        lookuplistResources.$GetCategory(function (data) {
            $scope.categorylist = data.obj;
        });
    }

    $scope.GetCategory();

    $scope.addMRD = function(obj) {
        obj.editmode = false;
    }

    $scope.editMRD = function (obj) {
        obj.editmode = true;
    }

    $scope.turnoffaddmode = function (index) {
        $scope.movementrequestobj.MovementRequestDetail.splice(index, 1);
    }

    $scope.turnoffeditmode = function (obj) {
        obj.editmode = false;
    }


    $scope.Save = function () {
        $scope.isValidate = $scope.validationform();
        if ($scope.isValidate) {
            $scope.SaveAsset();
        }
    }

    $scope.validationform = function () {
        var validationstatus = true;
        var keys = Object.keys(movementrequesModel());
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            var value = $scope.movementrequestobj[key];
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

    $scope.SaveMovementRequest = function () {
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
        movementrequestResources.Description = $scope.movementrequestobj.Description;
        movementrequestResources.ApprovalStatus = 1;
        movementrequestResources.MovementRequestDetail = $scope.movementrequestobj.MovementRequestDetail;

        angular.forEach(movementrequestResources.MovementRequestDetail, function(data) {
            delete data.ID;
            delete data.editmode;
            delete data.MovementRequestID;
            data.AssetCategoryCD = parseInt(data.AssetCategoryCD);
            data.Quantity = parseInt(data.Quantity);
            data.RequestedTo = parseInt(data.RequestedTo);
        });
        console.log(movementrequestResources);
        movementrequestResources.$CreateMovementRequest(function (data) {
            if (data.success) {
                //$scope.movementrequestobj = ;
                //$scope.init();
            }
        });
    }


    $scope.UpdateMovementRequest = function () {
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
        movementrequestResources.Description = $scope.movementrequestobj.Description;
        movementrequestResources.ApprovalStatus = 1;
        movementrequestResources.MovementRequestDetail = $scope.movementrequestobj.MovementRequestDetail;

        angular.forEach(movementrequestResources.MovementRequestDetail, function (data) {
            delete data.ID;
            delete data.editmode;
            delete data.MovementRequestID;
            data.AssetCategoryCD = parseInt(data.AssetCategoryCD);
            data.Quantity = parseInt(data.Quantity);
            data.RequestedTo = parseInt(data.RequestedTo);
        });
        console.log(movementrequestResources);
        movementrequestResources.$UpdateMovementRequest(function (data) {
            if (data.success) {
                //$scope.movementrequestobj = ;
                //$scope.init();
            }
        });
    }


   
    $scope.convertdate = function(stringdate) {
        var a = Date.parse(stringdate.replace(/^(\d\d)(\d\d)(\d\d\d\d)$/, "$2-$1-$3"));
        var myDate = new Date(parseInt(a));
        var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
        var day = ("0" + myDate.getDate()).slice(-2);
        var year = myDate.getFullYear();
        var date = day + "/" + month + "/" + year;
        return date;
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

});