/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('myApp', []);

app.controller("SchCntrl", function ($scope, $http) {


    $scope.searchtag1 = "";
    $scope.classid = "";
    $scope.subject = "";
    $scope.section = "";

    

    $http.get('http://localhost:51207/api/Demo/GetAdminSchedule/').then(function (result) {
        $scope.people = result.data;

    });
    
    


}

    )

