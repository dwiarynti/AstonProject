/**
 * movementrequest Controller
 */

app.controller('MovementRequestCtrl', function ($scope, $rootScope, $state, transferobjectService) {

    $scope.movementrequestlist = [];
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request";


    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };

    $scope.init = function() {
        //movementrequestResources.$Getmovementrequest(function (data) {
        //    $scope.movementrequestlist = data.obj;
        //    console.log(data);
        //});
    }

    $scope.init();

    $scope.add = function () {
        transferobjectService.addObj = { "data": "test" };
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