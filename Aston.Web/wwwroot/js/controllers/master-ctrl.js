/**
 * Master Controller
 */

angular.module('RDash')
    .controller('MasterCtrl', ['$scope', '$rootScope', '$state', MasterCtrl]);

function MasterCtrl($scope, $rootScope, $state) {


    $scope.test = "hahaha";


}