(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('TrendService', TrendService);

    TrendService.$inject = ['$http'];

    function TrendService($http) {
        var service = {
            retrieveTrends: retrieveTrends,
            detailTrend: detailTrend,
            createTrendComment: createTrendComment
        };

        return service;

        function retrieveTrends(Paging, OrderBy) {
            var url = "/Trend/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function detailTrend(trendId) {
            var url = "/Trend/TrendData",
                data = {
                    id: trendId
                };
            return $http.post(url, data);
        }

        function createTrendComment(trendCommentData) {
            var url = "/TrendComment/Create",
                data = {
                    trendComment: trendCommentData
                };
            return $http.post(url, data);
        }
    }
})();