(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('SellerMobileController', SellerMobileController);

    SellerMobileController.$inject = ['$window', 'SellerMobileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function SellerMobileController($window, SellerMobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.sellerMobiles = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.retrieveSellerMobiles = retrieveSellerMobiles;
        vm.searchSellerMobile = searchSellerMobile;
        vm.price;
        vm.discountPrice;
        vm.isEmiAvailable;
        vm.mobileId;
        vm.sellerId;
        vm.assignMobileToSeller = assignMobileToSeller;

        function initialise(mobileId, sellerId) {
            //vm.orderBy.property = "Name";
            //vm.orderBy.direction = "Ascending";
            //vm.orderBy.class = "asc";
            //order("Name");
            vm.mobileId = mobileId;
            vm.sellerId = sellerId;
        }

        function assignMobileToSeller() {
            return SellerMobileService.assignMobileToSeller(vm.mobileId,
                    vm.sellerId,
                    vm.price,
                    vm.discountPrice,
                    vm.isEmiAvailable)
                .then(function (response) {
                    alert(response.data);
                });
        }

        function retrieveSellerMobiles() {
            vm.orderBy.property = "MobileName";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return SellerMobileService.retrieveSellerMobiles(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sellerMobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellerMobiles.length === 0 ? "No Records Found" : "";
                    return vm.sellerMobiles;
                });
        }

        function searchSellerMobile(searchKeyword) {
            vm.orderBy.property = "MobileName";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.searchKeyword = searchKeyword;
            return SellerMobileService.searchSellerMobile(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.sellerMobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellerMobiles.length === 0 ? "No Records Found" : "";
                    return vm.sellerMobiles;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchSellerMobile(vm.searchKeyword)();
            }
            return retrieveSellerMobiles();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchSellerMobile(vm.searchKeyword)();
            }
            return retrieveSellerMobiles();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }
})();
