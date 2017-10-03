/**
 * movementrequest Controller
 */

app.controller('MovementRequestCtrl', function ($scope, $rootScope, $state, transferobjectService, movementrequestResource, commonService) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request";
    $scope.searchobj = SearchModel();

        //pagination
    $scope.NumberofAsset = 0;
    $scope.bigCurrentPage = 1;

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    function SearchModel() {
        return {
            LocationID: null,
            ApprovalStatus: null,
        };
    }

    $scope.Search = function () {
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.MovementRequest = {
            LocationID: $scope.searchobj.LocationID == null ? $scope.searchobj.LocationID : parseInt($scope.searchobj.LocationID),
            ApprovalStatus: $scope.searchobj.ApprovalStatus == "" ? null : $scope.searchobj.ApprovalStatus
        };
        movementrequestResources.Skip = $scope.bigCurrentPage;
        movementrequestResources.$SearchMovementRequest(function (data) {
            if (data.success) {
                $scope.NumberofAsset = data.obj[0].MovementRequest.TotalRow;
                console.log(data);
                $scope.movementrequestlist = data.obj;
            }
        });
    }

    $scope.init = function () {
        $scope.Search();
        //movementrequestResources.$GetMovementRequest(function (data) {
        //    angular.forEach(data.obj, function(obj) {
        //        obj.MovementDate = commonService.convertdate(obj.MovementDate);
        //        obj.ApprovedDate = obj.ApprovedDate != null ? commonService.convertdate(obj.ApprovedDate) : obj.ApprovedDate;
        //    });
        //    $scope.movementrequestlist = data.obj;
        //    console.log(data);
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
        movementrequestResources.ID = $scope.movementrequest.ID;
        movementrequestResources.MovementDate = $scope.movementrequest.MovementDate;
        movementrequestResources.Description = $scope.movementrequest.Description;
        movementrequestResources.ApprovedDate = $scope.movementrequest.ApprovedDate;
        movementrequestResources.ApprovedBy = $scope.movementrequest.ApprovedBy;
        movementrequestResources.MovementRequestDetail = $scope.movementrequest.MovementRequestDetail;
        movementrequestResources.$DeleteMovementRequest(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

});