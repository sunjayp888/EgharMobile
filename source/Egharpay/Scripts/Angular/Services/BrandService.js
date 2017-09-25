(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('BrandService', BrandService);

    BrandService.$inject = ['$http'];

    function BrandService($http) {
        var service = {
            retrieveBrands: retrieveBrands,
            retrieveMobileByBrandId:retrieveMobileByBrandId,
            searchBrand: searchBrand
        };

        return service;

        function retrieveBrands(OrderBy) {
            var url = "/Brand/List",
                data = {
                    //paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchBrand(SearchKeyword, Paging, OrderBy) {
            var url = "/Brand/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function retrieveMobileByBrandId(BrandId, Paging, OrderBy) {
            var url = "/Brand/MobileList",
                data = {
                    brandId: BrandId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();