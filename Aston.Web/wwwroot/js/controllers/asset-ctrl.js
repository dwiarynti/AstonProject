/**
 * Asset Controller
 */

app.controller('AssetCtrl', function ($scope, $rootScope, $window, $state, assetResource, lookuplistResource, commonService) {
    var assetResources = new assetResource();
    var lookuplistResources = new lookuplistResource();
    $scope.isValidate = true;
    $scope.assetlist = [];
    $scope.asset = {};
    $scope.actionstatus = "";
    $scope.categorylist = [];
    $rootScope.PageName = "Asset";
    $scope.searchobj = SearchModel();
    $scope.SelectedReport = "";

    //pagination
    $scope.NumberofAsset = 0;
    $scope.bigCurrentPage = 1;

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
            DepreciationDuration: null,
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

    function SearchModel() {
        return {
            CategoryCD: null,
            Owner: null,
            IsMovable: null,
        };
    }

    $scope.Search = function () {
        var assetResources = new assetResource();
        assetResources.Asset = {
            CategoryCD: $scope.searchobj.CategoryCD == null ? $scope.searchobj.CategoryCD : parseInt($scope.searchobj.CategoryCD),
            Owner : $scope.searchobj.Owner == "" ? null : $scope.searchobj.Owner
        };
        assetResources.Ismovable = $scope.searchobj.IsMovable;
        assetResources.Skip = $scope.bigCurrentPage - 1;
        assetResources.$SearchAsset(function (data) {
            if (data.success) {
                $scope.NumberofAsset = data.obj.length != 0 ? data.obj[0].Asset.TotalRow:0;
                $scope.assetlist = data.obj;
            }
        });
    }


    $scope.init = function () {
        $scope.Search(false);
        //assetResources.$GetAsset(function (data) {
        //    $scope.assetlist = data.obj;
        //});
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
        assetResources.Asset = {
            Name: $scope.asset.Name,
            Description: $scope.asset.Description,
            IsMovable: $scope.asset.IsMovable,
            Owner: $scope.asset.Owner,
            PurchaseDate: $scope.asset.PurchaseDate,
            PurchasePrice: parseFloat($scope.asset.PurchasePrice),
            ManufactureDate: $scope.asset.ManufactureDate,
            DepreciationDuration: $scope.asset.DepreciationDuration,
            CategoryCD: $scope.asset.CategoryCD,
            CreatedBy: "test",
            StatusCD: 1,
        };

        //assetResources.Name = $scope.asset.Name;
        //assetResources.Description = $scope.asset.Description;
        //assetResources.IsMovable = $scope.asset.IsMovable;
        //assetResources.Owner = $scope.asset.Owner;
        //assetResources.PurchaseDate = $scope.asset.PurchaseDate;
        //assetResources.PurchasePrice = parseFloat($scope.asset.PurchasePrice);
        //assetResources.ManufactureDate = $scope.asset.ManufactureDate;
        //assetResources.CategoryCD = $scope.asset.CategoryCD;
        //assetResources.CreatedBy = "test";
        //assetResources.StatusCD = 1;
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
        assetResources.Asset = {
            ID: $scope.asset.ID,
            Name: $scope.asset.Name,
            Description: $scope.asset.Description,
            IsMovable: $scope.asset.IsMovable,
            Owner: $scope.asset.Owner,
            PurchaseDate: $scope.asset.PurchaseDate,
            PurchasePrice: parseFloat($scope.asset.PurchasePrice),
            ManufactureDate: $scope.asset.ManufactureDate,
            DepreciationDuration: $scope.asset.DepreciationDuration,
            CategoryCD: $scope.asset.CategoryCD,
            UpdatedBy: "test",
            StatusCD: 1,
        };
        assetResources.$UpdateAsset(function (data) {
            if (data.success) {
                $scope.asset = AssetModel();
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
        assetResources.DepreciationDuration = $scope.asset.DepreciationDuration;

        assetResources.CategoryCD = $scope.asset.CategoryCD;
        assetResources.$DeleteAsset(function (data) {
            if (data.success) {
                $("#modal-action").modal('hide');
                $scope.init();
            }
        });
    }

    //$scope.Search = function () {
    //    var assetResources = new assetResource();
    //    assetResources.isSearch = true;
    //    assetResources.CategoryCD = parseInt($scope.searchobj.CategoryCD);
    //    assetResources.IsMovable = $scope.searchobj.IsMovable;
    //    assetResources.Owner = $scope.searchobj.Owner == "" ? null : $scope.searchobj.Owner;
    //    assetResources.$SearchAsset(function (data) {
    //        if (data.success) {
    //            $scope.assetlist = data.obj;
    //        }

    //    });
    //}

    $scope.CancelSearch = function () {
        $scope.searchobj = SearchModel();
        $scope.Search();
    }

    $scope.export = function () {
        if ($scope.SelectedReport != "") {
            $scope.AssetReport();
        } else {
            $window.alert("Plear select the report");
        }
    }

    $scope.AssetReport = function() {
        var assetResources = new assetResource();
        assetResources.ReportName = $scope.SelectedReport;
        assetResources.Asset = {
            CategoryCD: $scope.searchobj.CategoryCD == null ? $scope.searchobj.CategoryCD : parseInt($scope.searchobj.CategoryCD),
            Owner: $scope.searchobj.Owner == "" ? null : $scope.searchobj.Owner
        };
        assetResources.Ismovable = $scope.searchobj.IsMovable;
        assetResources.$download(function (data) {
            var blob = data.response.blob;
            saveAs(blob, data.response.fileName);
        });
    }

    $scope.setToNumberPatern = function (obj) {
        obj.PurchasePrice = obj.PurchasePrice != null ? obj.PurchasePrice.toString().replace(/[^\d]/g, '') : obj.PurchasePrice;
        obj.DepreciationDuration = obj.DepreciationDuration != null ? obj.DepreciationDuration.toString().replace(/[^\d]/g, '') : obj.DepreciationDuration;
    }

    $scope.OpenHistory = function(obj) {
        window.open($state.href("assethistory", {
            ID: obj.ID
        }));
    }

});