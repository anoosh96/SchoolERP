

/**
 * Created by soheil on 9/5/14.
 */
var app2 = angular.module('AtdTApp', []);

app2.controller("MyCntrl", function ($scope, $http) {
    
    $scope.date = "";
    $scope.class = "";
    
    $scope.studentid = "";
    var x = angular.element(document.getElementById("tid"));
    $scope.teacher = x.val();

    $http.get('http://localhost:51207/api/Demo/GetAttendT/' + $scope.teacher).then(function (result) {
        
        $scope.people = result.data;



    });
})