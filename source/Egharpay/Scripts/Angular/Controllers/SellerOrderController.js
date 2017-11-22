(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('SellerOrderController', SellerOrderController);

    SellerOrderController.$inject = ['$window', 'OrderService', 'SellerOrderService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function SellerOrderController($window, OrderService, SellerOrderService, Paging, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.orders = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.retrieveOrder = retrieveOrder;
        vm.editOrder = editOrder;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "OrderId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("OrderId");
        }

        function retrieveOrder() {
            vm.orderBy.property = "OrderId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.searchKeyword = searchKeyword;
            return SellerOrderService.retrieveOrder(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.orders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.orders.length === 0 ? "No Records Found" : "";
                    return vm.orders;
                });
        }

        function pageChanged() {
            return retrieveOrder();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveOrder();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editOrder(orderId) {
            $window.location.href = "/Order/Edit/" + orderId;
        }
    }
})();
