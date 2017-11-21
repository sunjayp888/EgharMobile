(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerOrderService', SellerOrderService);

    SellerOrderService.$inject = ['$http'];

    function SellerOrderService($http) {
        var service = {
            retrieveOrder: retrieveOrder
            //editOrder: editOrder
        };

        return service;

        function retrieveOrder(OrderBy) {
            var url = "/Order/List",
                data = {
                    //paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        }
})();