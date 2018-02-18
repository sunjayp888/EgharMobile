(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileCompareService', MobileCompareService);

    MobileCompareService.$inject = ['$http'];

    function MobileCompareService($http) {
        var service = {
            compareMobile: compareMobile,
            retrieveMobileByBrandIds: retrieveMobileByBrandIds,
            retrieveMobileByMobileIds: retrieveMobileByMobileIds
        };

        return service;

        function compareMobile(mobileCommentData) {
            var url = "/MobileComment/Create",
                data = {
                    mobileComment: mobileCommentData
                };
            return $http.post(url, data);
        }

        function retrieveMobileByBrandIds(brandIds) {
            var url = "/Mobile/retrieveMobileByBrandIds",
                  data = {
                      brandIds: brandIds
                  };;
            return $http.post(url, data);
        }

        function retrieveMobileByMobileIds(mobileIds) {
            var url = "/Mobile/RetrieveMobileByMobileIds",
                  data = {
                      mobileIds: mobileIds
                  };;
            return $http.post(url, data);
        }
    }
})();