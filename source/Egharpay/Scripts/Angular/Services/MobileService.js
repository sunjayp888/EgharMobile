﻿(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileService', MobileService);

    MobileService.$inject = ['$http'];

    function MobileService($http) {
        var service = {
            retrieveMobiles: retrieveMobiles,
            searchMobile: searchMobile,
            detailMobile: detailMobile,
            retrieveSearchField: retrieveSearchField,
            //retrieveSellers: retrieveSellers,
            searchSeller: searchSeller,
            retrieveGalleryImages: retrieveGalleryImages,
            retrieveYoutubeVideos: retrieveYoutubeVideos,
            requestOrder: requestOrder,
            retrieveMobilesInStore: retrieveMobilesInStore,
            retrieveMobilesLatestInStore: retrieveMobilesLatestInStore,
            retrieveGeoCoordinates: retrieveGeoCoordinates,
            retrieveSellersFromGeoLocation: retrieveSellersFromGeoLocation
        };

        return service;

        function retrieveMobiles(filter, Paging, OrderBy) {
            var url = "/Mobile/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy),
                    filter: filter
                };
            return $http.post(url, data);
        }


        function searchMobile(SearchKeyword, Paging, OrderBy) {
            var url = "/Mobile/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveSearchField() {
            var url = "/Mobile/SearchField";
            return $http.post(url);
        }

        function detailMobile(mobileId) {
            var url = "/Mobile/MobileData",
                data = {
                    id: mobileId
                };
            return $http.post(url, data);
        }


        function requestOrder(mobileId, sellerIds,shippingAddressId) {
            var url = "/Order/RequestOrder",
                data = {
                    mobileId: mobileId,
                    sellerIds: sellerIds,
                    shippingAddressId: shippingAddressId
                };
            return $http.post(url, data);
        }

        function searchSeller(SearchKeyword, Paging, OrderBy) {
            var url = "/Mobile/SellerSearch",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveGalleryImages(mobileId) {
            var url = "/Mobile/MobileGalleryImage",
            data = {
                id: mobileId
            };
            return $http.post(url, data);
        }

        function retrieveYoutubeVideos(mobileId) {
            var url = "/Mobile/Youtube",
                  data = {
                      id: mobileId
                  };
            return $http.post(url, data);
        }

        function retrieveMobilesInStore(Paging, OrderBy) {
            var url = "/Mobile/RetrieveMobilesInStore",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveMobilesLatestInStore(Paging, OrderBy) {
            var url = "/Mobile/RetrieveMobilesLatestInStore",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveGeoCoordinates() {
            var url = "/Mobile/RetrieveCurrentGeoCoordinates";
            return $http.post(url);
        }

        function retrieveSellersFromGeoLocation(pincode, latitude, longitude, Paging, OrderBy) {
            var url = "/Mobile/RetrieveSellersByGeoLocation",
                data = {
                    pincode: pincode,
                    latitude: latitude,
                    longitude: longitude,
                    paging:Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();