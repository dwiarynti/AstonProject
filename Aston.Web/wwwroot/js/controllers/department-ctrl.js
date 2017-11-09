/**
 * Department Controller
 */

app.controller('DepartmentCtrl', function ($scope, $rootScope, $window, $state, $filter, departmentResource) {
    var departmentResources = new departmentResource();

    $scope.isValidate = true;
    $scope.Departmentlist = [];
    $scope.Department = {};
    $scope.actionstatus = "";
    $scope.DepartmentList = [];
    $scope.Department = {};
    $rootScope.PageName = "Department Management";
    $scope.departmentcode = "";
    $scope.Departments = [];
    //$scope.searchobj = SearchModel();
    $scope.SelectedReport = "";

    //pagination
    $scope.NumberofDepartment = 0;
    $scope.bigCurrentPage = 1;

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-purchasedate,#datepicker-manufacturedate ').datepicker({
        todayHighlight: true,
        format: "ddMMyyyy"
    });

    $scope.GetDepartments = function () {
        departmentResources.$GetDepartments(function (data) {
            $scope.Departments = data.obj;
        });
    }

    $scope.init = function () {
        $scope.DepartmentList = [];
        departmentResources = new departmentResource();
        departmentResources.Skip = 0;
        departmentResources.$GetDepartments(function (data) {
            $scope.DepartmentList = data.obj;
            $scope.NumberofDepartment = data.obj.length != 0 ? data.obj[0].TotalRow : 0;
            console.log(data.obj);
        });
    }
    $scope.init();

    $scope.add = function () {
        $scope.Department = {};
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.CreateDepartment = function () {
        departmentResources.Name = $scope.Department.Name;
        departmentResources.Description = $scope.Department.Description;

        departmentResources.$DepartmentRegister(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function (obj) {
        $scope.Department = angular.copy(obj);
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.delete = function (obj) {
        $scope.Department = obj;
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }

    $scope.EditDepartment = function () {
        departmentResources.Id = $scope.Department.ID;
        departmentResources.Name = $scope.Department.Name;
        departmentResources.Description = $scope.Department.Description;
        departmentResources.IsActive = $scope.Department.IsActive;

        departmentResources.$UpdateDepartment(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.DeleteDepartment = function () {
        departmentResources = new departmentResource();
        departmentResources.Id = $scope.Department.ID;
        departmentResources.$DeleteDepartment(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }
});