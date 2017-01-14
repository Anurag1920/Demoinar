var app = angular.module('OnlineTest', []);
app.controller('CategoryCtrl', function ($scope, $http) {

    var ctrl = "/OnlineTest";

    var config = {
        headers: {
            'Content-Type': 'application/json;charset=utf-8;'
        }
    }
    $scope.AddCategory = function () {
        var data =JSON.stringify( {'aCategoryName':  $scope.CategoryName});
        
        $http.post(ctrl+'/AddCategory',data, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }
    $scope.UpdateCategory = function (item) {
        $http.post(ctrl + '/UpdateCategory', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }
    $scope.DeleteCategory = function (item) {
        $http.post(ctrl + '/DeleteCategory', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }
    $http.get(ctrl + '/GetAllCategory')
        .then(function (data, status, headers, config) {
           
            $scope.Model = data.data.Data;
        });
});