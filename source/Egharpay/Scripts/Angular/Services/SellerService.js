(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerService', SellerService);

    SellerService.$inject = ['$http'];

    function SellerService($http) {
        var service = {
            retrieveSellers: retrieveSellers,
            searchSeller: searchSeller,
            sellerApproveState: sellerApproveState
        };

        return service;

        function retrieveSellers(Paging, OrderBy) {
            var url = "/Seller/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchSeller(SearchKeyword, Paging, OrderBy) {
            var url = "/Seller/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function sellerApproveState(sellerId) {
            var url = "/Seller/UpdateSellerApprovalState",
                data = {
                    sellerId: sellerId
                };
            return $http.post(url, data);
        }
    }
})();