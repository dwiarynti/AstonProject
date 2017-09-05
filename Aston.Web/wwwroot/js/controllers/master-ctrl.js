/**
 * Master Controller
 */

angular.module('RDash')
    .controller('MasterCtrl', ['$scope', '$rootScope', '$state', MasterCtrl]);

function MasterCtrl($scope, $rootScope, $state) {
    $scope.PageName = '';

    $scope.test = "hahaha";
    $scope.SendPageName = function(name) {
        $scope.PageName = name;
    }

}