var app = angular.module('OnlineTest', []);
app.controller('TestCtrl', function ($scope, $http,$window) {
    var ctrl = "/OnlineTest";


    var config = {
        headers: {
            'Content-Type': 'application/json;charset=utf-8;'
        }
    }
  
    $scope.SubmitTest = function (item) {
        console.log(item);
        $http.post(ctrl+'/SubmitTest', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
            $window.alert(data.data.Message);
        });
    }
    $scope.DeleteCategory = function (item) {
        $http.post(ctrl+'/DeleteCategory', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }
    $http.get(ctrl+'/GetTest')
        .then(function (data, status, headers, config) {
            console.log(data);
            $scope.Model = data.data.Data;
        });
});