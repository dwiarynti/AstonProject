////var app = angular.module("RDash");

angular.module('customfilter', []).filter('getType', function () {
    return function (obj) {
        return typeof obj;
    };
});

var app = angular.module("RDash",
[
    'ui.router',
    'datatables',
    'oc.lazyLoad',
    'ngResource',
    'customfilter'
]);

app.config([
    '$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        //$urlRouterProvider.otherwise('/home');
        $stateProvider
            .state('home', {
                url: '/home',
                templateUrl: 'views/home.html'
            })
            .state('assetmanagement', {
                url: '/assetmanagement',
                templateUrl: 'views/assetmanagement.html',
                resolve: {
                    service: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            files: [
                                'js/ccsasset/bootstrap-datepicker/css/datepicker.css',
                                'js/ccsasset/bootstrap-datepicker/css/datepicker3.css',
                                'js/ccsasset/bootstrap-datepicker/js/bootstrap-datepicker.js'
                            ]
                        });
                    }]
                },
            })
            .state('locationmanagement', {
                url: '/locationmanagement',
                templateUrl: 'views/locationmanagement.html'
            })
            .state('assetlocationmanagement', {
                url: '/assetlocationmanagement',
                templateUrl: 'views/assetlocationmanagement.html'
            })
            .state('movementrequestmanagement', {
                url: '/movementrequestmanagement',
                templateUrl: 'views/movementrequestmanagement.html',
                resolve: {
                    service: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            files: [
                                'js/ccsasset/bootstrap-datepicker/css/datepicker.css',
                                'js/ccsasset/bootstrap-datepicker/css/datepicker3.css',
                                'js/ccsasset/bootstrap-datepicker/js/bootstrap-datepicker.js'
                            ]
                        });
                    }]
                },
            })
            .state('movementrequestdetailmanagement', {
                url: '/movementrequestdetailmanagement',
                templateUrl: 'views/movementrequestdetailmanagement.html',
                resolve: {
                    service: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            files: [
                                'js/ccsasset/bootstrap-datepicker/css/datepicker.css',
                                'js/ccsasset/bootstrap-datepicker/css/datepicker3.css',
                                'js/ccsasset/bootstrap-datepicker/js/bootstrap-datepicker.js'
                            ]
                        });
                    }]
                },
            })
        ;
    }
]);