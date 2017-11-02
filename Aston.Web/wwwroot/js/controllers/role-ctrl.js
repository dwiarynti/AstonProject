/**
 * Role Controller
 */

app.controller('RoleCtrl', function ($scope, $rootScope, $window, $state, $filter, roleResource) {
    var roleResources = new roleResource();

    $scope.isValidate = true;
    $scope.Rolelist = [];
    $scope.Role = {};
    $scope.actionstatus = "";
    $scope.RoleList = [];
    $scope.Role = {};
    $rootScope.PageName = "Role Management";
    $scope.rolecode = "";
    $scope.Roles = [];
    //$scope.searchobj = SearchModel();
    $scope.SelectedReport = "";

    //pagination
    $scope.NumberofRole = 0;
    $scope.bigCurrentPage = 1;

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-purchasedate,#datepicker-manufacturedate ').datepicker({
        todayHighlight: true,
        format: "ddMMyyyy"
    });

    $scope.GetRoles = function () {
        roleResources.$GetRoles(function (data) {
            $scope.Roles = data.obj;
        });
    }

    $scope.init = function () {
        $scope.RoleList = [];
        roleResources = new roleResource();
        roleResources.Skip = 0;
        roleResources.$GetRolePagination(function (data) {
            $scope.RoleList = data.obj;
            console.log(data.obj);
        });
        $scope.GetRoles();
    }
    $scope.init();

    $scope.add = function () {
        $scope.Role = {};
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.CreateRole = function () {
        roleResources.Name = $scope.Role.Name;
        roleResources.$RoleRegister(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function (obj) {
        $scope.Role = angular.copy(obj);
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.delete = function (obj) {
        $scope.Role = obj;
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }

    $scope.EditRole = function () {
        roleResources.Id = $scope.Role.ID;
        roleResources.Name = $scope.Role.Name;

        roleResources.$RoleEdit(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.DeleteRole = function () {
        roleResources = new roleResource();
        roleResources.Id = $scope.Role.ID;
        roleResources.$DeleteRole(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }
});