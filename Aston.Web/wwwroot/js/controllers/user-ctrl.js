/**
 * User Controller
 */

app.controller('UserCtrl', function ($scope, $rootScope, $window, $state, lookuplistResource, userResource) {
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

    $scope.init = function () {
        userResources.Skip = 0;
        userResources.$GetUserPagination(function (data) {
            $scope.UserList = data.obj;
            console.log(data);
        });
    }
    $scope.init();
    $scope.GetDepartment = function () {
        lookuplistResources.$GetDepartment(function (data) {
            $scope.departmentlist = data.obj;
        });
    }

    $scope.GetDepartment();

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
        userResources.$UserRegister(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }
});