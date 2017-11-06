(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('SellerMobileController', SellerMobileController);

    SellerMobileController.$inject = ['$window', 'SellerMobileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function SellerMobileController($window, SellerMobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.sellers = [];
        vm.paging = new Paging;
        vm.orderBy = new OrderBy;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
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
    }
})();
