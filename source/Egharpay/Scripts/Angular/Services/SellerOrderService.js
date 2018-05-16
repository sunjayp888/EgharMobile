(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerOrderService', SellerOrderService);

    SellerOrderService.$inject = ['$http'];

    function SellerOrderService($http) {
        var service = {
            retrieveSellerOrders: retrieveSellerOrders,
            updateSellerOrder: updateSellerOrder,
            updateShippingAddress: updateShippingAddress,
            search: search,
            searchByDate: searchByDate
            //editOrder: editOrder
        };

        return service;

        function retrieveSellerOrders(Paging, OrderBy) {
            var url = "/SellerOrders/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function updateSellerOrder(orderId) {
            var url = "/SellerOrders/UpdateOrder",
                data = {
                    orderId: orderId
                };
            return $http.post(url, data);
        }

        function updateShippingAddress(orderId, shippingAddressId) {
            var url = "/SellerOrders/" + orderId + "/UpdateShippingAddress",
                data = {
                    orderId: orderId,
                    shippingAddressId: shippingAddressId
                };
            return $http.post(url, data);
        }

        function search(searchTerm, paging, orderBy) {
            var url = "/SellerOrders/Search",
                data = {
                    searchTerm: searchTerm,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };
            return $http.post(url, data);
        }

        function searchByDate(fromDate, toDate, paging, orderBy) {
            var url = "/SellerOrders/SearchByDate",
                data = {
                    fromDate: fromDate,
                    toDate:toDate,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };
            return $http.post(url, data);
        }

    }
})();