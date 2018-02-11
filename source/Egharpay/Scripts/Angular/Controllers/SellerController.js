(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('SellerController', SellerController);

    SellerController.$inject = ['$window', 'SellerService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function SellerController($window, SellerService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.sellers = [];
        vm.paging = new Paging;
        //vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        //vm.order = order;
        //vm.orderClass = orderClass;
        vm.searchSeller = searchSeller;
        vm.sellerApproveState = sellerApproveState;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveSellers = retrieveSellers;

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveSellers() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return SellerService.retrieveSellers(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sellers = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellers.length === 0 ? "No Records Found" : "";
                    return vm.sellers;
                });
        }

        function searchSeller(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return SellerService.searchSeller(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sellers = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellers.length === 0 ? "No Records Found" : "";
                    return vm.sellers;
                });
        }

        function sellerApproveState(sellerId) {
            return SellerService.sellerApproveState(sellerId).then(function (response) {
                retrieveSellers();
            });

            function pageChanged() {
                if (vm.searchKeyword) {
                    return searchSeller(vm.searchKeyword)();
                }
                return retrieveSellers();
            }

            function order(property) {
                vm.orderBy = OrderService.order(vm.orderBy, property);
                if (vm.searchKeyword) {
                    return searchSeller(vm.searchKeyword)();
                }
                return retrieveSellers();
            }

            function orderClass(property) {
                return OrderService.orderClass(vm.orderBy, property);
            }
        }
    }
})();
