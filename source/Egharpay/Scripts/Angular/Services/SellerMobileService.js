(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerMobileService', SellerMobileService);

    SellerMobileService.$inject = ['$http'];

    function SellerMobileService($http) {
        var service = {
            assignMobileToSeller: assignMobileToSeller,
            retrieveSellerMobiles: retrieveSellerMobiles,
            searchSellerMobile: searchSellerMobile
        };

        return service;

        function assignMobileToSeller(mobileId, sellerId, price, discountPrice, isEmiAvailable) {
            var url = "/SellerMobile/AssignMobileToSeller";
            var data = { sellerMobile: { MobileId: mobileId, EMIAvailable: isEmiAvailable, SellerId: sellerId, Price: price, DiscountPrice: discountPrice } };
            return $http.post(url, data);
        }

        function retrieveSellerMobiles(Paging, OrderBy) {
            var url = "/SellerMobile/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }


        function searchSellerMobile(SearchKeyword, Paging, OrderBy) {
            var url = "/SellerMobile/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();