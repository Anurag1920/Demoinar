var app = angular.module('OnlineTest', []);
app.controller('ResultCtrl', function ($scope, $http, $window) {
    var ctrl = "/OnlineTest";
    $http.get(ctrl+'/GetAllResult')
      .then(function (data, status, headers, config) {
          
          $scope.Model = data.data.Data;
      });

});