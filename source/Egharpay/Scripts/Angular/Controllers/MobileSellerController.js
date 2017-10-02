(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileSellerController', MobileSellerController);

    MobileSellerController.$inject = ['$window', '$sce', 'MobileSellerService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileSellerController($window, $sce, MobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobiles = [];
        vm.retrieveSellers = retrieveSellers;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveSellers() {
                return MobileService.retrieveSellers(vm.paging, vm.orderBy)
                    .then(function (response) {
                        vm.mobiles = response.data.Items;
                        vm.paging.totalPages = response.data.TotalPages;
                        vm.paging.totalResults = response.data.TotalResults;
                        vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                        return vm.mobiles;
                    });
            }
        
    }
})();
//sellerbymobileid  mobile service