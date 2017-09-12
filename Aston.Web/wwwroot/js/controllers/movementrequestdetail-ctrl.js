/**
 * movementrequest Controller
 */

app.controller('MovementRequestDetailCtrl', function ($scope, $state, $filter, $rootScope, $window, transferobjectService, movementrequestResource, lookuplistResource, commonService) {
    var movementrequestResources = new movementrequestResource();
    var lookuplistResources = new lookuplistResource();
    $scope.isValidate = true;
    $scope.movementrequestobj = transferobjectService.addObj;
    $scope.movementrequest = {};
    $scope.movementrequestdetailList = [];
    $scope.categorylist = [];
    $scope.movementrequestdetailBackup = {};
    $scope.departmentlist = [];
    $rootScope.PageName = "Movement Request Detail";
    $scope.validationmessagelist = [];

    $scope.init = function () {
        if ($scope.movementrequestobj.ID != 'temp') {
            $scope.movementrequestobj.MovementDate = commonService.convertdate($scope.movementrequestobj.MovementDate);
            angular.forEach($scope.movementrequestobj.MovementRequestDetail, function (data) {
                data.editmode = false;
                data.IsDelete = false;
                data.IsUpdate = false;
            });
        }
    }

    $scope.GetDepartment = function() {
        lookuplistResources.$GetDepartment(function(data) {
            $scope.departmentlist = data.obj;
        });
    }

    $scope.GetDepartment();

    if ($scope.movementrequestobj.ID == undefined) {
        $state.go('movementrequestmanagement');
    } else {
        $scope.init();
    }

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-movementdate').datepicker({
        todayHighlight: true,
        format: "d/mm/yyyy"
    });

    $scope.showDatePickerMovementDate = function () {
        $('#datepicker-movementdate').datepicker('show');
    };

    function movementrequesModel() {
        return {
            ID: "temp",
            MovementDate: null,
            Description: null,
            //ApprovedDate: null,
            //ApprovedBy: null,
            MovementRequestDetail: []
            //CreatedDate: null,
            //CreatedBy: null,
            //IsUpdateDate: null,
            //IsUpdateBy: null,
            //IsDeleteDate: null,
            //IsDeleteBy: null
        };
    }

    function movementrequestDetailModel() {
        return {
            ID: "temp",
            //MovementRequestID: null,
            Description: null,
            AssetCategoryCD: null,
            Quantity: null,
            RequestTo : null,
            //editmode:false,
        //CreatedDate: null,
        //CreatedBy: null,
        //IsUpdateDate: null,
        //IsUpdateBy: null,
        //IsDeleteDate: null,
        //IsDeleteBy: null
    };
    }

    $scope.Add = function () {
        var obj = movementrequestDetailModel();
        obj.editmode = true;
        obj.IsUpdate = false;
        obj.IsDelete = false;
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
        obj.IsUpdate = true;
    }

    $scope.editMRD = function (obj) {
        obj.editmode = true;
        $scope.movementrequestdetailBackup = angular.copy(obj);
    }

    $scope.turnoffaddmode = function (index) {
        $scope.movementrequestobj.MovementRequestDetail.splice(index, 1);
    }

    $scope.turnoffeditmode = function (obj) {
        obj.CategoryCDName = $scope.movementrequestdetailBackup.CategoryCDName;
        obj.Description = $scope.movementrequestdetailBackup.Description;
        obj.Quantity = $scope.movementrequestdetailBackup.Quantity;
        obj.RequestTo = $scope.movementrequestdetailBackup.RequestTo;
        obj.AssetCategoryCD = $scope.movementrequestdetailBackup.AssetCategoryCD;
        obj.editmode = false;
    }

    $scope.deleteMRD = function (obj) {
        obj.IsDelete = true;
    }

    $scope.Save = function () {
        $scope.isValidate = $scope.validationform();
        if ($scope.isValidate) {
            $scope.SaveAsset();
        }
    }

    $scope.validationform = function () {
        $scope.validationmessagelist = [];
        var validationstatus = true;
        if ($scope.movementrequestobj.MovementDate == "" || $scope.movementrequestobj.MovementDate == null) {
            validationstatus = false;
        } else {
            var validationtableresultlist = [];
            for (var i = 0; i < $scope.movementrequestobj.MovementRequestDetail.length; i++) {
                var data = $scope.movementrequestobj.MovementRequestDetail[i];
                var row = i + 1;
                var result = $scope.validationtable(movementrequestDetailModel(), data, row);
                validationtableresultlist.push(result);
            }
            var validationresult = $filter('filter')(validationtableresultlist, function (obj) { return obj === false });
            //console.log(validationresult);
            validationstatus = validationresult.length != 0 ? false : true;
        }

        return validationstatus;
    }

    $scope.validationtable = function (model, data, row) {

        var result = true;
        var keys = Object.keys(model);
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            var value = data[key];
            var datatype = typeof value;
            if (datatype != "boolean") {
                if (value == null || value == "") {
                    $scope.validationmessagelist.push({ message: key + " is Required at row " + row });
                    result = false;
                    //break;
                }
            }
        }
        return result;
    }

    $scope.SaveMovementRequest = function () {

        $scope.isValidate = $scope.validationform();
        console.log($scope.isValidate);

        if ($scope.isValidate) {
            var movementrequestResources = new movementrequestResource();
            movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
            movementrequestResources.Description = $scope.movementrequestobj.Description;
            movementrequestResources.ApprovalStatus = 2;
            movementrequestResources.MovementRequestDetail = angular.copy($scope.movementrequestobj.MovementRequestDetail);

            angular.forEach(movementrequestResources.MovementRequestDetail, function (data) {

                delete data.ID;
                delete data.editmode;
                delete data.MovementRequestID;
                data.AssetCategoryCD = parseInt(data.AssetCategoryCD);
                data.Quantity = parseInt(data.Quantity);
                data.RequestTo = parseInt(data.RequestTo);
            });
            //console.log(movementrequestResources);
            movementrequestResources.$CreateMovementRequest(function (data) {
                if (data.success) {
                    $window.alert("Data saved successfully");
                    console.log(data);
                    //$scope.movementrequestobj = ;
                    //$scope.init();
                }
            });
        }

        
    }


    $scope.UpdateMovementRequest = function () {

        $scope.isValidate = $scope.validationform();

        if ($scope.isValidate) {
            var movementrequestResources = new movementrequestResource();
            movementrequestResources.ID = $scope.movementrequestobj.ID;
            movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
            movementrequestResources.Description = $scope.movementrequestobj.Description;
            movementrequestResources.ApprovalStatus = 2;
            movementrequestResources.MovementRequestDetail = angular.copy($scope.movementrequestobj.MovementRequestDetail);
            angular.forEach(movementrequestResources.MovementRequestDetail, function (data) {
                var IDdatatype = typeof data.ID;
                if (IDdatatype == "string") {
                    delete data.ID;
                    delete data.IsDelete;
                    delete data.IsUpdate;
                }
                delete data.editmode;
                data.AssetCategoryCD = parseInt(data.AssetCategoryCD);
                data.Quantity = parseInt(data.Quantity);
                data.RequestTo = parseInt(data.RequestTo);
            });
            console.log(movementrequestResources);

            movementrequestResources.$UpdateMovementRequest(function (data) {
                if (data.success) {
                    $window.alert("Data saved successfully");
                    console.log(data);
                    //$scope.movementrequestobj = ;
                    //$scope.init();
                }
            });
        }
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

    $scope.getCategoryName = function (obj) {
        var a = $filter('filter')($scope.categorylist, function (category) { return category.Code === parseInt(obj.AssetCategoryCD) })[0];
        obj.CategoryCDName = a.Value;
    }

    $scope.getDepartmentName = function (obj) {
        var a = $filter('filter')($scope.departmentlist, function (department) { return department.ID === parseInt(obj.RequestTo) })[0];
        obj.RequestToName = a.Name;
    }

});