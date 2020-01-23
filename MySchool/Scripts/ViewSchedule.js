/**
 * Created by soheil on 9/5/14.
 */
var app = angular.module('SchApp', []);

app.controller("SchCntrl", function ($scope, $http) {

    $scope.searchtag1 = "";
    $scope.searchtag2 = "";
    var x = angular.element(document.getElementById("classid"));
    $scope.class = x.val();
    
    $http.get('http://localhost:51207/api/Demo/GetStdSchedule/' + $scope.class).then(function (result) {
        $scope.people = result.data;

    });


}

    )

