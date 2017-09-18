/**
 * Asset Controller
 */

app.controller('AssetCtrl', function ($scope, $rootScope, assetResource, lookuplistResource, commonService) {
    var assetResources = new assetResource();
    var lookuplistResources = new lookuplistResource();
    $scope.isValidate = true;
    $scope.assetlist = [];
    $scope.asset = {};
    $scope.actionstatus = "";
    $scope.categorylist = [];
    $rootScope.PageName = "Asset";

    $scope.dtOptions = { "aaSorting": [], "bPaginate": false, "bLengthChange": false, "bFilter": false, "bSort": false, "bInfo": false, "bAutoWidth": false };


    $('#datepicker-purchasedate,#datepicker-manufacturedate ').datepicker({
        todayHighlight: true,
        format: "ddMMyyyy"
    });

    $scope.showDatePickerPurchaseDate = function () {
        $('#datepicker-purchasedate').datepicker('show');
    };

    $scope.showDatePickerManufactureDate = function () {
        $('#datepicker-manufacturedate').datepicker('show');
    };

    function AssetModel() {
        return {
            ID: "temp",
            //Code: null,
            Description: null,
            //No: null,
            Name: null,
            IsMovable: null,
            Owner: null,
            PurchaseDate: null,
            PurchasePrice: null,
            //DepreciationDuration: null,
            //DisposedDate: null,
            ManufactureDate: null,
            CategoryCD: null,
            //StatusCD: null,
            //CreatedDate: null,
            //CreatedBy: null,
            //UpdatedDate: null,
            //UpdatedBy: null,
            //DeletedDate: null,
            //DeletedBy: null
        };
    }


    $scope.init = function() {
        assetResources.$GetAsset(function (data) {
            $scope.assetlist = data.obj;
        });
        $scope.GetCategory();
    }

    $scope.GetCategory = function() {
        lookuplistResources.$GetCategory(function (data) {
            $scope.categorylist = data.obj;
        });
    }

    $scope.init();


    $scope.add = function () {
        $scope.asset = AssetModel();
        $scope.isValidate = true;
        $scope.actionstatus = "Create";
        $("#modal-action").modal('show');
    }

    $scope.create = function () {
        $scope.isValidate = commonService.validationform(AssetModel(), $scope.asset);
        if ($scope.isValidate) {
            $scope.CreateAsset();
        }
    }

    $scope.CreateAsset = function() {
        var assetResources = new assetResource();
        assetResources.Name = $scope.asset.Name;
        assetResources.Description = $scope.asset.Description;
        assetResources.IsMovable = $scope.asset.IsMovable;
        assetResources.Owner = $scope.asset.Owner;
        assetResources.PurchaseDate = $scope.asset.PurchaseDate;
        assetResources.PurchasePrice = parseFloat($scope.asset.PurchasePrice);
        assetResources.ManufactureDate = $scope.asset.ManufactureDate;
        assetResources.CategoryCD = $scope.asset.CategoryCD;
        assetResources.CreatedBy = "test";
        assetResources.StatusCD = 1;
        assetResources.$CreateAsset(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.edit = function (obj) {
        
        $scope.asset = angular.copy(obj);
        $scope.asset.PurchaseDate = $scope.asset.PurchaseDate != null && $scope.asset.PurchaseDate != "" ? commonService.convertdate($scope.asset.PurchaseDate) : "";
        $scope.asset.ManufactureDate = $scope.asset.ManufactureDate != null && $scope.asset.ManufactureDate != "" ? commonService.convertdate($scope.asset.ManufactureDate) : "";
        $scope.isValidate = true;
        $scope.actionstatus = "Update";
        $("#modal-action").modal('show');
    }

    $scope.update = function() {
        $scope.isValidate = commonService.validationform(AssetModel(), $scope.asset);
        if ($scope.isValidate) {
            $scope.UpdateAsset();
        }
    }

    $scope.UpdateAsset = function() {
        var assetResources = new assetResource();
        assetResources.ID = $scope.asset.ID;
        assetResources.Name = $scope.asset.Name;
        assetResources.Description = $scope.asset.Description;
        assetResources.IsMovable = $scope.asset.IsMovable;
        assetResources.Owner = $scope.asset.Owner;
        assetResources.PurchaseDate = $scope.asset.PurchaseDate;
        assetResources.PurchasePrice = parseFloat($scope.asset.PurchasePrice);
        assetResources.ManufactureDate = $scope.asset.ManufactureDate;
        assetResources.CategoryCD = $scope.asset.CategoryCD;
        assetResources.UpdatedBy = "test";
        assetResources.StatusCD = 1;
        assetResources.$UpdateAsset(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    $scope.isSelectedItem = function (itemA, itemB) {
        return itemA == itemB ? true : false;
    }

    $scope.deletemodal = function (obj) {
        $scope.asset = angular.copy(obj);
        $scope.actionstatus = "Delete";
        $("#modal-action").modal('show');
    }
    $scope.delete = function() {
        var assetResources = new assetResource();
        assetResources.ID = $scope.asset.ID;
        assetResources.Name = $scope.asset.Name;
        assetResources.Description = $scope.asset.Description;
        assetResources.IsMovable = $scope.asset.IsMovable;
        assetResources.Owner = $scope.asset.Owner;
        assetResources.PurchaseDate = $scope.asset.PurchaseDate;
        assetResources.PurchasePrice = parseFloat($scope.asset.PurchasePrice);
        assetResources.ManufactureDate = $scope.asset.ManufactureDate;
        assetResources.CategoryCD = $scope.asset.CategoryCD;
        assetResources.$DeleteAsset(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

});