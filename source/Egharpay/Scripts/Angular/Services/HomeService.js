(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('HomeService', HomeService);

    HomeService.$inject = ['$http'];

    function HomeService($http) {
        var service = {
            retrieveStatistics: retrieveStatistics,
            retrieveStatisticsByCentre: retrieveStatisticsByCentre,
            retrieveBarGraphStatistics: retrieveBarGraphStatistics,
            retrieveCentres: retrieveCentres,
            listMobile: listMobile,
            change: change,
            searchMobiles: searchMobiles,
            latestMobiles: latestMobiles,
            latestMobileData: latestMobileData
        };

        return service;

        function retrieveStatistics() {

            var url = "/Home/Statistics";
            return $http.post(url);
        }

        function retrieveStatisticsByCentre(centreId) {

            var url = "/Home/StatisticsByCentre",
                data = {
                    id: centreId
                };

            return $http.post(url, data);
        }

        function retrieveBarGraphStatistics() {

            var url = "/Home/StatisticsBarGraph";
            return $http.post(url);
        }

        function retrieveCentres(Paging, OrderBy) {

            var url = "/Home/GetCentres",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }

        function change(centreId) {

            var url = "/Home/StatisticsByCentre",
                data = {
                    centreId: centreId
                };

            return $http.post(url, data);
        }

        function listMobile(SearchKeyword, Paging, OrderBy) {
            var url = "/Home/SearchMobile",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchMobiles(searchKeyword) {
            var url = "/Home/Mobile",
                data = {
                    searchKeyword: searchKeyword
                };
            return $http.post(url, data);
        }

        function latestMobiles(showAll) {
            var url = "/Home/LatestMobile",
                data = {
                    showAll: showAll
                };
            return $http.post(url, data);
        }

        function latestMobileData(showAll) {
            var url = "/Home/LatestMobileData",
                data = {
                    showAll: showAll
                };
            return $http.post(url, data);
        }
    }
})();