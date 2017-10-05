(function () {
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
            retrieveGalleryImages: retrieveGalleryImages,
            retrieveYoutubeVideos: retrieveYoutubeVideos
        };

        return service;

        function retrieveMobiles(Paging, OrderBy) {
            var url = "/Mobile/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
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
    }
})();