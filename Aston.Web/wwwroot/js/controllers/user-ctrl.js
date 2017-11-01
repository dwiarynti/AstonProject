/**
 * User Controller
 */

app.controller('UserCtrl', function ($scope, $rootScope, $window, $state, $filter, lookuplistResource, userResource) {
    var userResources = new userResource();
    var lookuplistResources = new lookuplistResource();

    $scope.isValidate = true;
    $scope.Userlist = [];
    $scope.User = {};
    $scope.actionstatus = "";
    $scope.departmentlist = [];
    $scope.UserList = [];
    $scope.User = {};
    $rootScope.PageName = "User Management";
    $scope.usercode = "";
    $scope.Roles = [];
    //$scope.searchobj = SearchModel();
    $scope.SelectedReport = "";

    //pagination
    $scope.NumberofUser = 0;
    $scope.bigCurrentPage = 1;

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-purchasedate,#datepicker-manufacturedate ').datepicker({
        todayHighlight: true,
        format: "ddMMyyyy"
    });


    $scope.GetDepartment = function () {
        lookuplistResources.$GetDepartment(function (data) {
            $scope.departmentlist = data.obj;
        });
    }

    $scope.GetRoles = function () {
        userResources.$GetRoles(function (data) {
            $scope.Roles = data.obj;
        });
    }

    $scope.init = function () {
        $scope.UserList = [];
        userResources = new userResource();
        userResources.Skip = 0;
        userResources.$GetUserPagination(function (data) {
            $scope.UserList = data.obj;
            console.log(data.obj);
        });
        $scope.GetDepartment();
        $scope.GetRoles();
    }
    $scope.init();

    $scope.add = function () {
        $scope.User = {};
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.CreateUser = function () {
        userResources.Email = $scope.User.Email;
        userResources.Username = $scope.User.Username;
        userResources.Password = $scope.User.Password;
        userResources.ConfirmPassword = $scope.User.ConfirmPassword;
        userResources.DepartmentID = $scope.User.DepartmentID;
        userResources.Role = $scope.User.RoleId;
        userResources.$UserRegister(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function (obj) {
        $scope.User = obj;
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.delete = function (obj) {
        $scope.User = obj;
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }

    $scope.GenerateUserCode = function (obj) {
        userResources.Id = obj.ID;
        userResources.$GenerateUserCode(function (data) {
            if (data.success) {
                obj.Code = data.obj;
            }
        });
    }

    $scope.EditUser = function () {
        userResources.Id = $scope.User.ID;
        userResources.Username = $scope.User.Username;
        userResources.Email = $scope.User.Email;
        userResources.Code = $scope.User.Code;
        userResources.Role = $scope.User.RoleId;
        userResources.DepartmentID = $scope.User.DepartmentID;

        userResources.$UserEdit(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.resetpassword = function (obj) {
        $scope.User = obj;
        $scope.GenerateUserCode(obj);
        $scope.actionstatus = "Reset Password";
        $("#modal-action").modal('show');
    }

    $scope.assignrole = function (obj) {
        $scope.User = obj;
        $scope.actionstatus = "Assign Role";
        $("#modal-action").modal('show');
    }

    $scope.ResetUserPassword = function () {
        userResources.Id = $scope.User.ID;
        userResources.Password = $scope.User.Password;
        userResources.ConfirmPassword = $scope.User.ConfirmPassword;
        userResources.Code = $scope.User.Code;
        userResources.$ResetUserPassword(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.AssignUserRole = function () {
        var RoleName = $filter('filter')($scope.Roles, function (role) { return role.Id === $scope.User.RoleId })[0].Name;
        var userResources = new userResource();
        userResources.Id = $scope.User.ID;
        userResources.Role = RoleName;
        userResources.$AssignUserRole(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.DeleteUser = function () {
        var userResources = new userResource();
        userResources.Id = $scope.User.ID;
        userResources.$DeleteUser(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }
});