/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('stdApp1', []);

app.controller("atddCtrl", function ($scope, $http, $document) {

    
    $scope.class = "";
    var x = angular.element(document.getElementById("tid"));
    $scope.teacher = x.val();


    $http.get('http://localhost:51207/api/Demo/GetStuds/' + $scope.teacher).then(function (result) {
        $scope.people = result.data;



    });

    

}

    )

