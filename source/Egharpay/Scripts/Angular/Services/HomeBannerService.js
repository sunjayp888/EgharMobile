﻿(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('HomeBannerService', HomeBannerService);

    HomeBannerService.$inject = ['$http'];

    function HomeBannerService($http) {
        var service = {
            retrieveHomeBanner: retrieveHomeBanner,
            retrieveHomeBannerImage: retrieveHomeBannerImage
        };

        return service;

        function retrieveHomeBanner(Paging, OrderBy) {
            var url = "/HomeBanner/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveHomeBannerImage(startDate, endDate, pincode) {
            var url = "/HomeBanner/HomeBannerImage",
                data = {
                    startDateTime: startDate,
                    endDateTime: endDate,
                    pincode: pincode
                };
            return $http.post(url, data);
        }
    }
})();