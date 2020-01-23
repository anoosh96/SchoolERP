

/**
 * Created by soheil on 9/5/14.
 */
var app2 = angular.module('AtdApp', []);

app2.controller("MyCntrl", function ($scope, $http) {
    
    $scope.date = "";
    $scope.classid = "";
    $scope.section = "";
    $scope.studentid = "";

    $http.get('http://localhost:51207/api/Demo/GetAttend').then(function (result) {
        
        $scope.people = result.data;



    });
})