var app = angular.module('OnlineTest', []);
app.controller('DashBoardCtrl', function ($scope, $http, $window) {
    var ctrl = "/OnlineTest";

    $http.get(ctrl+'/GetAllUser')
      .then(function (data, status, headers, config) {
          console.log(data);
          $scope.Model = data.data;
      });

});