(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('TrendCommentService', TrendCommentService);

    TrendCommentService.$inject = ['$http'];

    function TrendCommentService($http) {
        var service = {
            retrieveTrendComments: retrieveTrendComments,
            approve:approve
        };

        return service;

        function retrieveTrendComments(Paging, OrderBy) {
            var url = "/TrendComment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function approve(trendCommentId) {
            var url = "/TrendComment/Approve",
                data = {
                    trendCommentId: trendCommentId
                };
            return $http.post(url, data);
        }
    }
})();