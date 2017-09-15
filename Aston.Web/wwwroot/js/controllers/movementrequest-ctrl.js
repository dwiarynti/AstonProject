/**
 * movementrequest Controller
 */

app.controller('MovementRequestCtrl', function ($scope, $rootScope, $state, transferobjectService, movementrequestResource, commonService) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request";


    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    $scope.init = function() {
        movementrequestResources.$GetMovementRequest(function (data) {
            angular.forEach(data.obj, function(obj) {
                obj.MovementDate = commonService.convertdate(obj.MovementDate);
                obj.ApprovedDate = obj.ApprovedDate != null ? commonService.convertdate(obj.ApprovedDate) : obj.ApprovedDate;
            });
            $scope.movementrequestlist = data.obj;
            console.log(data);
        });
    }

    function movementrequesModel() {
        return {
            ID: "temp",
            MovementDate: null,
            LocationID: null,
            Description: null,
            ApprovedDate: null,
            ApprovedBy: null,
            MovementRequestDetail:[]
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
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