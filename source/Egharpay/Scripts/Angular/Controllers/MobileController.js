(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileController', MobileController);

    MobileController.$inject = ['$window', 'MobileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileController($window, MobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobiles = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchMobile = searchMobile;
        vm.detailMobile = detailMobile;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveMobiles() {
            return MobileService.retrieveMobiles(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function searchMobile(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return MobileService.searchMobile(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchMobile(vm.searchKeyword)();
            }
            return retrieveMobiles();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchMobile(vm.searchKeyword)();
            }
            return retrieveMobiles();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function detailMobile(mobileId) {
            return MobileService.detailMobile(mobileId).then(function (response) {
                vm.mobiles = response.data;
                return vm.mobiles;
            });
        }
    }
})();
