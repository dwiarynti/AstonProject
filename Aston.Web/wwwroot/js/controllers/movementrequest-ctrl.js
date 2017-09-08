/**
 * movementrequest Controller
 */

app.controller('MovementRequestCtrl', function ($scope, $rootScope, $state, transferobjectService, movementrequestResource) {
    var movementrequestResources = new movementrequestResource();
    $scope.movementrequestlist = [];
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request";


    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    $scope.init = function() {
        movementrequestResources.$GetMovementRequest(function (data) {
            $scope.movementrequestlist = data.obj;
            console.log(data);
        });
    }

    function movementrequesModel() {
        return {
            ID: "temp",
            MovementDate: null,
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
        transferobjectService.addObj = movementrequesModel();
        $state.go('movementrequestdetailmanagement');
    }

    $scope.edit = function (obj) {
        transferobjectService.addObj = obj;
        $state.go('movementrequestdetailmanagement');
    }

    $scope.deletemodal = function (obj) {
        $scope.movementrequest = angular.copy(obj);
        $("#modal-action").modal('show');
    }
    $scope.delete = function () {
        var movementrequestResources = new movementrequestResource();
        movementrequestResources.ID = $scope.movementrequest.ID;
        movementrequestResources.Name = $scope.movementrequest.Name;
        movementrequestResources.Description = $scope.movementrequest.Description;
        movementrequestResources.Floor = $scope.movementrequest.Floor;
        movementrequestResources.movementrequestTypeCD = $scope.movementrequest.movementrequestTypeCD;
        movementrequestResources.StatusCD = $scope.movementrequest.StatusCD;
        movementrequestResources.$Deletemovementrequest(function (data) {
            if (data.success) {
                $scope.init();
                $("#modal-action").modal('hide');
            }
        });
    }

});