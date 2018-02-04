(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('SellerOrderController', SellerOrderController);

    SellerOrderController.$inject = ['$window', 'SellerOrderService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function SellerOrderController($window, SellerOrderService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.sellerOrders = [];
        vm.paging = new Paging;
        vm.orderBy = new OrderBy;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveSellerOrders = retrieveSellerOrders;
        vm.updateSellerOrder = updateSellerOrder;

        function initialise() {
            vm.orderBy.property = "OrderId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("OrderId");
        }

        function retrieveSellerOrders() {
            vm.orderBy.property = "OrderId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return SellerOrderService.retrieveSellerOrders(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sellerOrders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellerOrders.length === 0 ? "No Records Found" : "";
                    return vm.sellerOrders;
                });
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchSeller(vm.searchKeyword)();
            }
            return retrieveSellers();
        }

        function updateSellerOrder(orderId) {
            return SellerOrderService.updateSellerOrder(orderId)
                .then(function() {
                    retrieveSellerOrders();
                });
        }
    }
})();
