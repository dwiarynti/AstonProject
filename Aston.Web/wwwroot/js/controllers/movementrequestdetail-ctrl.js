/**
 * movementrequest Controller
 */

app.controller('MovementRequestDetailCtrl', function ($scope, $state, $filter, $rootScope, $window, transferobjectService, movementrequestResource, lookuplistResource, assetResource, locationResource, assetLocationResource, commonService) {
    var movementrequestResources = new movementrequestResource();
    var lookuplistResources = new lookuplistResource();
    var locationResources = new locationResource();
    var assetLocationResources = new assetLocationResource();

    var assetResources = new assetResource();
    $scope.isValidate = true;
    $scope.movementrequestobj = movementrequesModel();
    $scope.movementdetailaction = transferobjectService.addObj.action;
    $scope.movementrequest = {};
    $scope.movementrequestdetailList = [];
    $scope.categorylist = [];
    $scope.movementrequestdetailBackup = {};
    $scope.movementrequestdetail = {};
    $scope.departmentlist = [];
    $rootScope.PageName = "Movement Request Detail";
    $scope.validationmessagelist = [];
    $scope.selectedassetlist = [];
    $scope.selectedasset = {};
    $scope.asseterrormessage = '';
    $scope.assetlist = [];
    $scope.locationlist = [];
    $scope.OnTransitionStatus = false;
    $scope.onprocess = false;

    $scope.GetLocation = function () {
        $scope.locationlist = [];
        locationResources.$GetLocation(function (data) {
            $scope.locationlist = data.obj;
        });
    }

    $scope.init = function () {
        $scope.movementrequestobj = transferobjectService.addObj.data;
        $scope.addvalidationobj($scope.movementrequestobj.MovementRequestDetail);
        $scope.movementrequestobj.MovementRequest.Notes = $scope.movementrequestobj.MovementRequest.Notes == '' && $scope.movementdetailaction == 'edit' || $scope.movementrequestobj.MovementRequest.Notes == null && $scope.movementdetailaction == 'edit' ? '--' : $scope.movementrequestobj.MovementRequest.Notes;
        if ($scope.movementrequestobj.MovementRequest.ID != 'temp' && $scope.movementdetailaction != "detail") {
            $scope.GetMovementRequest($scope.movementrequestobj.MovementRequest.ID);
        } else if ($scope.movementdetailaction != "detail") {
            $scope.GetLocation();
        }
    }

    $scope.GetMovementRequest = function(id) {
        var movementrequestResources = new movementrequestResource();

        movementrequestResources.$GetMovementRequestByID({ id: id }, function (data) {
            $scope.movementrequestobj = data.obj;
            $scope.movementrequestobj.MovementRequest.Notes = $scope.movementrequestobj.MovementRequest.Notes == '' && $scope.movementdetailaction == 'edit' || $scope.movementrequestobj.MovementRequest.Notes == null && $scope.movementdetailaction == 'edit' ? '--' : $scope.movementrequestobj.MovementRequest.Notes;

            $scope.addvalidationobj($scope.movementrequestobj.MovementRequestDetail);
            $scope.GetLocation();
        });
    }

    $scope.addvalidationobj = function(obj) {
        angular.forEach(obj, function (data) {
            data.editmode = false;
            data.IsDelete = false;
            data.IsUpdate = false;
        });
    }


    $scope.GetDepartment = function() {
        lookuplistResources.$GetDepartment(function(data) {
            $scope.departmentlist = data.obj;
        });
    }

    $scope.GetDepartment();

    if (transferobjectService.addObj.data == undefined) {
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

    //function movementrequesModel() {
    //    return {
    //        ID: "temp",
    //        MovementDate: null,
    //        LocationID: null,
    //        Description: null,
    //        ApprovedDate: null,
    //        ApprovedBy: null,
    //        Notes: null,
    //        MovementRequestDetail: []
    //        //CreatedDate: null,
    //        //CreatedBy: null,
    //        //UpdatedDate: null,
    //        //UpdatedBy: null,
    //        //DeletedDate: null,
    //        //DeletedBy: null
    //    };
    //}

    function movementrequesModel() {
        return {
            ID: "temp",
            MovementDate: null,
            Description: null,
            LocationID:null,
            //ApprovedDate: null,
            //ApprovedBy: null,
            //MovementRequestDetail: []
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
        $scope.onprocess = true;
        $scope.movementrequestobj.MovementRequestDetail.push(obj);
    }
    $scope.GetCategory = function () {
        lookuplistResources.$GetCategory(function (data) {
            $scope.categorylist = data.obj;
        });
    }

    $scope.GetCategory();

    $scope.addMRD = function (obj) {
        obj.editmode = false;
        obj.IsUpdate = true;
        $scope.onprocess = false;
        $scope.getCategoryName(obj);
        $scope.getDepartmentName(obj);
    }

    $scope.editMRD = function (obj) {
        obj.editmode = true;
        $scope.onprocess = true;
        $scope.movementrequestdetailBackup = angular.copy(obj);
    }

    $scope.turnoffaddmode = function (index) {
        $scope.onprocess = false;
        $scope.movementrequestobj.MovementRequestDetail.splice(index, 1);
    }

    $scope.turnoffeditmode = function (obj) {
        obj.CategoryCDName = $scope.movementrequestdetailBackup.CategoryCDName;
        obj.Description = $scope.movementrequestdetailBackup.Description;
        obj.Quantity = $scope.movementrequestdetailBackup.Quantity;
        obj.RequestTo = $scope.movementrequestdetailBackup.RequestTo;
        obj.AssetCategoryCD = $scope.movementrequestdetailBackup.AssetCategoryCD;
        obj.editmode = false;
        $scope.onprocess = false;
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
        var validationformstatus = commonService.validationform(movementrequesModel(), $scope.movementrequestobj.MovementRequest);
        if (!validationformstatus) {
            validationstatus = validationformstatus;
        } else {
            var validationtableresultlist = [];
            for (var i = 0; i < $scope.movementrequestobj.MovementRequestDetail.length; i++) {
                var data = $scope.movementrequestobj.MovementRequestDetail[i];
                var row = i + 1;
                var result = $scope.validationtable(movementrequestDetailModel(), data, row);
                validationtableresultlist.push(result);
            }
            var validationresult = $filter('filter')(validationtableresultlist, function (obj) { return obj === false });
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

    $scope.GetApprovalNumber = function () {
        $scope.movementrequestlist = [];
        movementrequestResources.$GetMovementRequestNeedApproval(function (data) {
            $rootScope.ApprovalNumber = data.obj.length;
        });
    }

    $scope.SaveMovementRequest = function () {

        $scope.isValidate = $scope.validationform();

        if ($scope.isValidate) {
            $scope.onprocess = true;
            var movementrequestResources = new movementrequestResource();
            movementrequestResources.MovementRequest = {
                MovementDate: $scope.movementrequestobj.MovementRequest.MovementDate,
                Description: $scope.movementrequestobj.MovementRequest.Description,
                ApprovalStatus: 2,
                LocationID: parseInt($scope.movementrequestobj.MovementRequest.LocationID),
            };
            movementrequestResources.MovementRequestDetail = $scope.movementrequestobj.MovementRequestDetail;
            //movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
            //movementrequestResources.Description = $scope.movementrequestobj.Description;
            //movementrequestResources.ApprovalStatus = 2;
            //movementrequestResources.LocationID = parseInt($scope.movementrequestobj.LocationID);
            //movementrequestResources.MovementRequestDetail = angular.copy($scope.movementrequestobj.MovementRequestDetail);
            angular.forEach(movementrequestResources.MovementRequestDetail, function (data) {

                delete data.ID;
                delete data.editmode;
                delete data.MovementRequestID;
                data.AssetCategoryCD = parseInt(data.AssetCategoryCD);
                data.Quantity = parseInt(data.Quantity);
                data.RequestTo = parseInt(data.RequestTo);
            });
            movementrequestResources.$CreateMovementRequest(function (data) {
                if (data.success) {
                    $window.alert("Data saved successfully");
                    $scope.onprocess = false;
                    $scope.movementrequestobj = data.obj;
                    $scope.addvalidationobj($scope.movementrequestobj.MovementRequestDetail);
                    $scope.GetApprovalNumber();
                }
            });
        }
    }


    $scope.UpdateMovementRequest = function () {

        $scope.isValidate = $scope.validationform();

        if ($scope.isValidate) {
            $scope.onprocess = true;
            var movementrequestResources = new movementrequestResource();
            movementrequestResources.MovementRequest = {
                ID : $scope.movementrequestobj.MovementRequest.ID,
                MovementDate: $scope.movementrequestobj.MovementRequest.MovementDate,
                Description: $scope.movementrequestobj.MovementRequest.Description,
                ApprovalStatus: 2,
                LocationID: parseInt($scope.movementrequestobj.MovementRequest.LocationID),
            };
            //movementrequestResources.MovementRequest = $scope.movementrequestobj.MovementRequest;
            movementrequestResources.MovementRequestDetail = $scope.movementrequestobj.MovementRequestDetail;
            //movementrequestResources.ID = $scope.movementrequestobj.ID;
            //movementrequestResources.MovementDate = $scope.movementrequestobj.MovementDate;
            //movementrequestResources.Description = $scope.movementrequestobj.Description;
            //movementrequestResources.ApprovalStatus = 2;
            //movementrequestResources.LocationID = parseInt($scope.movementrequestobj.LocationID);
            //movementrequestResources.MovementRequestDetail = angular.copy($scope.movementrequestobj.MovementRequestDetail);
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
            movementrequestResources.$UpdateMovementRequest(function (data) {
                if (data.success) {
                    $window.alert("Data saved successfully");
                    $scope.onprocess = false;
                    $scope.movementrequestobj = data.obj;
                    $scope.addvalidationobj($scope.movementrequestobj.MovementRequestDetail);
                    $scope.GetApprovalNumber();
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

    $scope.GetAssetLocationByMovementDetailID = function (obj) {
        var assetLocationResources = new assetLocationResource();
        $scope.selectedassetlist = [];
        assetLocationResources.$GetAssetLocationByMovementDetailID({ id: obj.ID }, function (data) {
            if (data.success) {
                angular.forEach(data.obj, function(obj) {
                    $scope.selectedassetlist.push(obj.AssetLocation);
                });
                $scope.GetAsset(obj.AssetCategoryCD); // get asset list

            }
        });
    }

    $scope.selectasset = function (obj) {
        $scope.onprocess = false;
        $scope.GetAssetLocationByMovementDetailID(obj); // get selested asset by movementdetail
        $("#modal-addasset").modal('show');


        $scope.movementrequestdetailBackup = angular.copy(obj);
        $scope.movementrequestdetail = obj;
    }

    $scope.GetAsset = function (AssetCategoryCD) {
        assetResources.$GetAssetByCategoryCode({ id: parseInt(AssetCategoryCD) }, function (data) {
            $scope.assetlist = data.obj;
            angular.forEach($scope.selectedassetlist, function (obj) {
                angular.forEach($scope.assetlist, function (asset, index) {
                    if (asset.ID == obj.AssetID) {
                        $scope.assetlist.splice(index, 1);
                    }
                });
            });
        });
    }

    $scope.addassetlocation = function (obj) {
        if (!obj.hasOwnProperty("$$hashKey")) {
            var data = JSON.parse(obj);
            for (i = 0; i <= $scope.assetlist.length; i++) {
                var asset = $scope.assetlist[i];
                if (asset.ID == data.ID) {
                    $scope.selectedassetlist.push({
                        AssetID: data.ID,
                        AssetName: data.Name,
                        LocationID: $scope.movementrequestobj.MovementRequest.LocationID,
                        LocationName: $scope.movementrequestobj.MovementRequest.LocationName,
                        OnTransition: $scope.OnTransitionStatus,
                        MovementRequestDetailID: data.ID,
                    });
                    $scope.assetlist.splice(i, 1);
                    $scope.asseterrormessage = '';
                    break;
                } else {
                    $scope.asseterrormessage = 'Please select asset';
                }
            }
        } else {
            $scope.asseterrormessage = 'Please select asset';
        }
        
        //angular.forEach($scope.assetlist, function (asset, index) {
            
        //});
        //var getasset = $filter('filter')($scope.assetlist, function (asset) { return asset.ID == data.ID });
        //if (getasset.length > 0) {
        //    $scope.selectedassetlist.push({
        //        AssetID: data.ID,
        //        AssetName: data.Name,
        //        LocationID: $scope.movementrequestobj.MovementRequest.LocationID,
        //        LocationName: $scope.movementrequestobj.MovementRequest.LocationName,
        //        OnTransition: $scope.OnTransitionStatus,
        //        MovementRequestDetailID: data.ID,
        //    });
        //    $scope.assetlist.splice(getasset[0], 1);
        //    $scope.asseterrormessage = '';
        //} else {
        //    $scope.asseterrormessage = 'Please select asset';
        //}

    }

    $scope.CreateAssetLocation = function () {
        $scope.onprocess = true;
        var assetLocationResources = new assetLocationResource();
        assetLocationResources.AssetLocation = {
            LocationID: parseInt($scope.movementrequestobj.MovementRequest.LocationID),
            OnTransition: $scope.OnTransitionStatus,
            MovementRequestDetailID: $scope.movementrequestdetailBackup.ID,
        };

        var validationresult = $filter('filter')($scope.selectedassetlist, function (selectedasset) { return selectedasset.ID == undefined });
        assetLocationResources.AssetLocationList = validationresult;

        assetLocationResources.$CreateAssetLocation(function (data) {
            if (data.success) {
                $scope.movementrequestdetail.Transfered = $scope.selectedassetlist.length;
                $window.alert("Data saves successfully");
                $("#modal-addasset").modal('hide');
                $scope.selectedassetlist = [];
                $scope.init();
            }
        });
    }

    $scope.Approve = function (obj) {
        
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.ID = obj.MovementRequest.ID;
        movementrequestResources.ApprovalStatus = 1;
        movementrequestResources.Notes = obj.MovementRequest.Notes;
        movementrequestResources.$ApproveMovementRequest(function (data) {
            if (data.success) {
                $window.alert("Data approved successfully");
                obj.MovementRequest.ApprovalStatus = 1;
                $scope.init();
                $scope.GetApprovalNumber();
            }
        });
    }

    $scope.Reject = function (obj) {
        if (obj.MovementRequest.Notes == '' || obj.MovementRequest.Notes == null) {
            $scope.isValidate = false;
        } else {
            var movementrequestResources = new movementrequestResource();
            movementrequestResources.ID = obj.MovementRequest.ID;
            movementrequestResources.ApprovalStatus = 3;
            movementrequestResources.Notes = obj.MovementRequest.Notes;
            movementrequestResources.$ApproveMovementRequest(function (data) {
                if (data.success) {
                    $scope.isValidate = true;
                    $window.alert("Data rejected successfully");
                    obj.MovementRequest.ApprovalStatus = 3;
                    $scope.init();
                    $scope.GetApprovalNumber();
                }
            });
        }
        
    }

    $scope.setToNumberPatern = function (obj) {
        obj.Quantity = obj.Quantity != null ? obj.Quantity.toString().replace(/[^\d]/g, '') : obj.Quantity;
    }

});