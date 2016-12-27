angular.module('superQuery')
	.controller('AppController', ['$scope', '$http', function ($scope, $http) {
	    $scope.query = {
	        engines: [],
	        qur: 'jerusalem'
	    };
	    $scope.test = 'frere';
	    $scope.searchRequest = function (que) {
	        
	        req = {
	            method: 'GET',
	            url: '/api/search/',
	            headers: {
	                'Content-Type': 'application/json',
	                'Accept': 'application/json'
	            },
	            data: JSON.stringify(que)
	        }
	        return $http(req);
	    }

	    $scope.search = function (q) {
	        $scope.searchRequest(q).then(function (results) {
	            //on success
	            alert(results.data);
	        }, function (e) {
	            //on fail
	        });
	    }
	}]);
