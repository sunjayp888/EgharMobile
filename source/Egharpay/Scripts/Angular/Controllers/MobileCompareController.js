(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileCompareController', MobileCompareController);

    MobileCompareController.$inject = ['$window', '$sce', 'MobileCompareService', 'BrandService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileCompareController($window, $sce, MobileCompareService, BrandService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.paging = new Paging;
        vm.orderBy = new OrderBy;
        vm.mobiles = [];
        vm.compareMobiles = [];
        vm.retrieveMobileByBrandIds = retrieveMobileByBrandIds;
        vm.retrieveMobileByMobileIds = retrieveMobileByMobileIds;
        vm.compareMobile = compareMobile;
        vm.mobileId = "";
        vm.brands = [];
        vm.retrieveBrands = retrieveBrands;
        vm.onBrandSelect = onBrandSelect;
        vm.onBrandRemove = onBrandRemove;
        vm.onMobileSelect = onMobileSelect;
        vm.onMobileRemove = onMobileRemove;
        vm.selectedBrands;
        vm.selectedMobiles;
        vm.selectedBrandId;
        vm.mobileInCompareCount = 0;


        function compareMobile(mobileId) {
            vm.mobileId = mobileId;
            
            return MobileCompareService.compareMobile(vm.mobileId)
                .then(function (response) {
                    vm.galleryImages = response.data;
                    vm.searchMessage = vm.galleryImages.length === 0 ? "No Records Found" : "";
                    return vm.galleryImages;
                });
        }

        function retrieveMobileByBrandIds() {
            return MobileCompareService.retrieveMobileByBrandIds(vm.selectedBrands)
                .then(function (response) {
                    
                    vm.mobiles = response.data;
                    return vm.mobiles;
                });
        }

        function retrieveMobileByMobileIds() {
            vm.selectedBrands = 283;
            return MobileCompareService.retrieveMobileByMobileIds(vm.selectedMobiles)
                .then(function (response) {
                    vm.mobileInCompareCount = response.data.length;
                    if (vm.mobileInCompareCount < 4)
                        vm.compareMobiles = response.data;
                    return vm.compareMobiles;
                });
        }

        function retrieveBrands() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            return BrandService.retrieveBrands(vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                vm.selectedBrands = vm.brands[0];
                    return vm.brands;
                });
        }

        function onBrandSelect() {
            retrieveMobileByBrandIds();
        }

        function onBrandRemove() {
            retrieveMobileByBrandIds();
        }

        function onMobileSelect() {
            retrieveMobileByMobileIds();
        }

        function onMobileRemove() {
            retrieveMobileByMobileIds();
        }
    }
})();
