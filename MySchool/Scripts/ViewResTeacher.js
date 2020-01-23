/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('ResApp', []);

app.controller("ResCntrl", function ($scope, $http) {

    $scope.class = "";
    $scope.subject = "";
    var x = angular.element(document.getElementById("tid"));
    $scope.teacher = x.val();

    $http.get('http://localhost:51207/api/Demo/GetResTeacher/' + $scope.teacher).then(function (result) {
        $scope.people = result.data;

    });


}

    )

