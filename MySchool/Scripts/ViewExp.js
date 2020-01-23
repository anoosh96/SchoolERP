/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('myApp1', []);

app.controller("ExpCntrl", function ($scope, $http) {


    $scope.status = "";
    $scope.type = "";
    $scope.date = "";
    



    $http.get('http://localhost:51207/api/Demo/GetExpenses/').then(function (result) {
        $scope.people = result.data;

    });




}

    )

