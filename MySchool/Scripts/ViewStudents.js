/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('stdApp', []);

app.controller("MainCtrl", function ($scope, $http,$document) {

    $scope.city = "";
    $scope.classid = "";
    $scope.section = "";
    $scope.gender = "";

    $http.get('http://localhost:51207/api/Demo/GetStudents').then(function (result) {
        $scope.people = result.data;
        

        
    });

    $scope.showDetail = function (id) {
        $scope.visible = true;
        $scope.url = 'http://localhost:51207/User/Details/' + id;
    }

    $scope.close = function () {
        //alert("lol");
        $scope.visible = false;
    }
    

}

    )

