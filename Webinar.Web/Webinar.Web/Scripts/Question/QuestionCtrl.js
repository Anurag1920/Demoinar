var app = angular.module('OnlineTest', []);
app.controller('QuestionCtrl', function ($scope, $http) {

    var ctrl = "/OnlineTest";

    var config = {
        headers: {
            'Content-Type': 'application/json;charset=utf-8;'
        }
    }
    $scope.const = { option: "Option", optionNo: 1, optionList: ['Option1']};


    $scope.optionNo = $scope.const.optionNo;
    $scope.optionList = $scope.const.optionList;
    $scope.CorrectAnswerIndex = 0;
    
    $scope.AddOption = function () {
        $scope.optionNo = $scope.optionNo + 1;
        $scope.optionList.push($scope.const.option + $scope.optionNo);

    }
    $scope.UpdateOption = function (value,index) {
        $scope.optionList[index] = value;
    }
    $scope.RemoveOption = function (index) {
        $scope.optionList.splice(index, 1);
    }
    $http.get(ctrl+'/GetAllCategory')
      .then(function (data, status, headers, config) {
          $scope.Categories = data.data.Data;
      });
    $scope.AddQuestion = function (form) {
        var data = JSON.stringify({ 'aCategoryId': form.CategoryId, 'aQuestion': form.Question, 'aOptions': $scope.optionList, 'aCorrectOption': form.CorrectOption });
        $http.post(ctrl+'/AddQuestionAnswer', data, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
            $scope.optionNo =1;
            $scope.optionList = ['Option1'];
            $scope.CorrectAnswerIndex = 0;
            form.CategoryId = 0;
            form.Question = "";
        });
    }
    $scope.UpdateQuestion = function (item) {
        console.log(JSON.stringify(item));
        $http.post(ctrl+'/UpdateQuestionAnswer', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }
    $scope.DeleteQuestion = function (item) {
        $http.post(ctrl+'/DeleteQuestionAnswer', item, config)
        .then(function (data, status, headers, config) {
            $scope.Model = data.data.Data;
        });
    }

    
    $http.get(ctrl+'/GetAllQuestionAnswer')
        .then(function (data, status, headers, config) {
           
            $scope.Model = data.data.Data;
        });

    $scope.EditOption = function (optionArray,questionId) {
        optionArray.push({ "AnswerId": 0, "QuestionId": questionId, "Answer1": "Option" })
    }

    $scope.EditDeleteOption = function (optionArray,index) {
        optionArray.splice(index, 1);
    }
});