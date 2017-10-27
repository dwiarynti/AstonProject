/**
 * movementrequest Controller
 */

app.controller('MovementRequestCtrl', function ($scope, $rootScope, $state, transferobjectService, movementrequestResource, commonService, locationResource, lookuplistResource) {
    var movementrequestResources = new movementrequestResource();
    var locationResources = new locationResource();
    var lookuplistResources = new lookuplistResource();

    $scope.movementrequestlist = [];
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request";
    $scope.searchobj = SearchModel();
    $scope.approvalstatuslist = [];

        //pagination
    $scope.NumberofMovementRequest = 0;
    $scope.bigCurrentPage = 1;
    $scope.locationlist = [];

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    function SearchModel() {
        return {
            LocationID: null,
            ApprovalStatus: null,
        };
    }

    $scope.GetLocation = function () {
        $scope.locationlist = [];
        locationResources.$GetLocation(function (data) {
            $scope.locationlist = data.obj;
        });
    }

    $scope.GetApprovalStatus = function () {
        lookuplistResources.$GetApprovalStatus(function (data) {
            $scope.approvalstatuslist = data.obj;
        });
    }

    $scope.Search = function () {
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.MovementRequest = {
            LocationID: $scope.searchobj.LocationID == null ? $scope.searchobj.LocationID : parseInt($scope.searchobj.LocationID),
            ApprovalStatus: $scope.searchobj.ApprovalStatus == null ? $scope.searchobj.ApprovalStatus : parseInt($scope.searchobj.ApprovalStatus)
        };
        movementrequestResources.Skip = $scope.bigCurrentPage-1;
        movementrequestResources.$SearchMovementRequest(function (data) {
            if (data.success) {
                $scope.NumberofMovementRequest = data.obj.length != 0 ? data.obj[0].MovementRequest.TotalRow : 0;
                $scope.movementrequestlist = data.obj;
                angular.forEach($scope.movementrequestlist, function(data) {
                    data.MovementRequest.MovementDate = commonService.convertdate(data.MovementRequest.MovementDate);
                });
            }
        });
    }

    $scope.init = function () {
        $scope.Search();
        $scope.GetLocation();
        $scope.GetApprovalStatus();
        //movementrequestResources.$GetMovementRequest(function (data) {
        //    angular.forEach(data.obj, function(obj) {
        //        obj.MovementDate = commonService.convertdate(obj.MovementDate);
        //        obj.ApprovedDate = obj.ApprovedDate != null ? commonService.convertdate(obj.ApprovedDate) : obj.ApprovedDate;
        //    });
        //    $scope.movementrequestlist = data.obj;
        //});
    }

    function movementrequesModel() {
        return {
            //ID: "temp",
            //MovementDate: null,
            //LocationID: null,
            //Description: null,
            //ApprovedDate: null,
            //ApprovedBy: null,
            //Notes: null,
            //MovementRequestDetail:[]

            ApplicationCode:null,
            CategoryCDName:null,
            CompanyCode:null,
            CreatedBy:null,
            CreatedDate:null,
            DeletedBy:null,
            DeletedDate: null,
            MainCategory: null,
            MovementRequest : {
                ApprovalStatus : null,
                ApprovalStatusName: null,
                ApprovedBy: null,
                ApprovedDate: null,
                Description: null,
                ID: "temp",
                LocationID: null,
                LocationName: null,
                MovementDate: null,
                Notes : null,
                TotalRow: null
            },
            MovementRequestDetail:[], 
            NeedMove:false,
            Number:null, 
            RequestToName:null,
            Skip: 0,
            SubCategory: null,
            UpdatedBy: null,
            UpdatedDate: null,

        };
    }

    $scope.init();

    $scope.add = function () {
        transferobjectService.addObj = { action: 'edit', data: movementrequesModel() };
        $state.go('movementrequestdetailmanagement');
    }

    $scope.edit = function (obj) {
        transferobjectService.addObj = { action: 'edit', data: obj };
        $state.go('movementrequestdetailmanagement');
    }

    $scope.deletemodal = function (obj) {
        $scope.movementrequest = angular.copy(obj);
        $("#modal-action").modal('show');
    }
    $scope.DeleteMovementRequest = function () {
        var movementrequestResources = new movementrequestResource();
        //movementrequestResources.MovementRequest = {
        //    ID : $scope.movementrequest.MovementRequest.ID,
        //    MovementDate: $scope.movementrequest.MovementRequest.MovementDate,
        //    Description: $scope.movementrequest.MovementRequest.Description,
        //    ApprovedDate: $scope.movementrequest.MovementRequest.ApprovedDate,
        //    ApprovedBy: $scope.movementrequest.MovementRequest.ApprovedBy,
        //};
        //movementrequestResources.MovementRequestDetail = $scope.movementrequest.MovementRequestDetail;

        movementrequestResources.ID = $scope.movementrequest.MovementRequest.ID;
        movementrequestResources.MovementDate = $scope.movementrequest.MovementRequest.MovementDate;
        movementrequestResources.Description = $scope.movementrequest.MovementRequest.Description;
        movementrequestResources.ApprovedDate = $scope.movementrequest.MovementRequest.ApprovedDate;
        movementrequestResources.ApprovedBy = $scope.movementrequest.MovementRequest.ApprovedBy;
        //movementrequestResources.MovementRequestDetail = $scope.movementrequest.MovementRequestDetail;
        movementrequestResources.$DeleteMovementRequest(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

    $scope.CancelSearch = function () {
        $scope.searchobj = SearchModel();
        $scope.Search();
    }

});