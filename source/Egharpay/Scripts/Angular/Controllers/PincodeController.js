(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('PincodeController', PincodeController);

    PincodeController.$inject = ['$window', 'PincodeService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function PincodeController($window, PincodeService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.pincodes = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchPincode = searchPincode;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("PincodeId");
        }

        function retrievePincode() {
            return PincodeService.retrievePincode(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.pincodes = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.pincodes.length === 0 ? "No Records Found" : "";
                    return vm.pincodes;
                });
        }

        function searchPincode(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return PincodeService.searchPincode(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.pincodes = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.pincodes.length === 0 ? "No Records Found" : "";
                    return vm.pincodes;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchPincode(vm.searchKeyword)();
            }
            return retrievePincode();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchPincode(vm.searchKeyword)();
            }
            return retrievePincode();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }
})();
