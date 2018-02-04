(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerOrderService', SellerOrderService);

    SellerOrderService.$inject = ['$http'];

    function SellerOrderService($http) {
        var service = {
            retrieveSellerOrders: retrieveSellerOrders,
            updateSellerOrder: updateSellerOrder
            //editOrder: editOrder
        };

        return service;

        function retrieveSellerOrders(Paging, OrderBy) {
            var url = "/Order/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function updateSellerOrder(orderId) {
            var url = "/Order/UpdateOrder",
                data = {
                    orderId: orderId
                };
            return $http.post(url, data);
        }
    }
})();