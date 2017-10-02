(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('YouTubeService', YouTubeService);

    YouTubeService.$inject = ['$http'];

    function YouTubeService($http) {
        var service = {
            search: search
        };

        return service;

        function search(searchKeyword) {
            var url = "/Youtube/search",
                data = {
                    searchKeyword: searchKeyword
                };
            return $http.post(url, data);
        }

    }
})();