/**
 * movementrequest Controller
 */

app.controller('MovementRequestDetailCtrl', function ($scope, $rootScope, transferobjectService) {
    $scope.isValidate = true;
    $scope.movementrequestobj = transferobjectService.addObj;
    $scope.movementrequest = {};
    $rootScope.PageName = "Movement Request Detail";


    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-movementdate').datepicker({
        todayHighlight: true,
        format: "ddMMyyyy"
    });

    $scope.showDatePickerMovementDate = function () {
        $('#datepicker-movementdate').datepicker('show');
    };

    function movementrequestDetailModel() {
        return {
            ID: "temp",
            MovementRequestID:null,
            Description: null,
            AssetCategoryCD: null,
            Quantity: null,
            RequestedTo: null,
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
        };
    }


   
    $scope.convertdate = function(stringdate) {
        var a = Date.parse(stringdate.replace(/^(\d\d)(\d\d)(\d\d\d\d)$/, "$2-$1-$3"));
        var myDate = new Date(parseInt(a));
        var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
        var day = ("0" + myDate.getDate()).slice(-2);
        var year = myDate.getFullYear();
        var date = day + "/" + month + "/" + year;
        return date;
    }

});