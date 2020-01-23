/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('myApp', []);

app.controller("MainCtrl", function ($scope, $http) {

    $scope.searchtag1 = "";
    $scope.searchtag2 = "";
    
       $http.get('http://localhost:51207/api/Demo/GetStudents').then(function (result) {
                $scope.people = result.data;

            });


        }
       
    )
    
