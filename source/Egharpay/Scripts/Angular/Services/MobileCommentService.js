(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileCommentService', MobileCommentService);

    MobileCommentService.$inject = ['$http'];

    function MobileCommentService($http) {
        var service = {
            retrieveMobileComments: retrieveMobileComments,
            approve: approve,
            createMobileComment: createMobileComment,
            retrieveMobileCommentsByMobileId: retrieveMobileCommentsByMobileId
        };

        return service;

        function retrieveMobileComments(Paging, OrderBy) {
            var url = "/MobileComment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveMobileCommentsByMobileId(mobileId, Paging, OrderBy) {
            var url = "/MobileComment/MobileCommentListByMobileId",
                data = {
                    mobileId: mobileId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function approve(mobileCommentId) {
            var url = "/MobileComment/Approve",
                data = {
                    mobileCommentId: mobileCommentId
                };
            return $http.post(url, data);
        }

        function createMobileComment(mobileCommentData) {
            var url = "/MobileComment/Create",
                data = {
                    mobileComment: mobileCommentData
                };
            return $http.post(url, data);
        }
    }
})();