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
    'common.services',
    'ngResource',
    'customfilter'
]);

app.config([
    '$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {

        $stateProvider
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
            //.state('subject-edit', {
            //    url: '/subject-edit',
            //    templateUrl: '/javascripts/angular-assets/partialviews/subjects/edit.html'
            //})
            //.state('question-index', {
            //    url: '/question-index',
            //    templateUrl: '/javascripts/angular-assets/partialviews/questions/index.html'
            //})
            //.state('question-create', {
            //    url: '/question-create',
            //    templateUrl: '/javascripts/angular-assets/partialviews/questions/create.html'
            //})
            //.state('question-detail', {
            //    url: '/question-detail',
            //    templateUrl: '/javascripts/angular-assets/partialviews/questions/detail.html'
            //})
            //.state('question-edit', {
            //    url: '/question-edit',
            //    templateUrl: '/javascripts/angular-assets/partialviews/questions/edit.html'
            //})
            //.state('user-index', {
            //    url: '/user-index',
            //    templateUrl: '/javascripts/angular-assets/partialviews/users/Index.html'
            //})
            //.state('login', {
            //    url: '/login',
            //    templateUrl: '/javascripts/angular-assets/partialviews/users/login.html'
            //})
            //.state('register', {
            //    url: '/register',
            //    templateUrl: '/javascripts/angular-assets/partialviews/users/register.html'
            //})
            //.state('quizindex', {
            //    url: '/quizindex',
            //    templateUrl: '/javascripts/angular-assets/partialviews/quiz/index.html'
            //})
            //.state('quiz', {
            //    url: '/quiz',
            //    templateUrl: '/javascripts/angular-assets/partialviews/quiz/quiz.html'
            //})
            //.state('choices-index', {
            //    url: '/choices-index',
            //    templateUrl: '/javascripts/angular-assets/partialviews/choices/index.html'
            //})
            //.state('exampresult-index', {
            //    url: '/exampresult-index',
            //    templateUrl: '/javascripts/angular-assets/partialviews/exampresult/index.html'
            //})
            //.state('exampresult-detail', {
            //    url: '/exampresult-detail',
            //    templateUrl: '/javascripts/angular-assets/partialviews/exampresult/detail.html'
            //})
            //.state('home', {
            //    url: '/home',
            //    templateUrl: '/javascripts/angular-assets/partialviews/home.html'
            //})

            //.state('profile-setting', {
            //    url: '/profile-setting',
            //    templateUrl: '/javascripts/angular-assets/partialviews/profile/profile-setting.html'
            //})
            //.state('profile-index', {
            //    url: '/profile-index',
            //    templateUrl: '/javascripts/angular-assets/partialviews/profile.html'

            //})
            //.state('widget', {
            //    url: '/widget',
            //    templateUrl: '/javascripts/angular-assets/partialviews/widget.html'
            //})
            //.state('dashboard-admin', {
            //    url: '/dashboard-admin',
            //    templateUrl: '/javascripts/angular-assets/partialviews/dashboard/admin.html'
            //})
            //.state('dashboard-user', {
            //    url: '/dashboard-user',
            //    templateUrl: '/javascripts/angular-assets/partialviews/dashboard/user.html'
            //})
        ;
    }
]);