(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('GoogleService', GoogleService);

    GoogleService.$inject = ['$http'];

    function GoogleService($http) {
        var service = {
            getLocation: getLocation
        };

        return service;

        function getLocation(lat, lng) {
            var url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng + "&sensor=true";
            return $http.get(url);
        }

    }
})();