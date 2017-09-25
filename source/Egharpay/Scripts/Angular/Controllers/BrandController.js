(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('BrandController', BrandController);

    BrandController.$inject = ['$window', 'BrandService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function BrandController($window, BrandService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.brands = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        //vm.viewMobile = viewMobile;
        vm.searchBrand = searchBrand;
        vm.retrieveMobileByBrandId = retrieveMobileByBrandId;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveBrands() {
            return BrandService.retrieveBrands(vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                    //vm.paging.totalPages = response.data.TotalPages;
                    //vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.brands.length === 0 ? "No Records Found" : "";
                    return vm.brands;
                });
        }

        function searchBrand(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return BrandService.searchBrand(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.brands.length === 0 ? "No Records Found" : "";
                    return vm.brands;
                });
        }

        function retrieveMobileByBrandId(brandId) {
            vm.brandId = brandId;
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BrandService.retrieveMobileByBrandId(vm.brandId,vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.brands.length === 0 ? "No Records Found" : "";
                    return vm.brands;
                });
        }
        
        function pageChanged() {
            if (vm.searchKeyword) {
                return searchBrand(vm.searchKeyword)();
            }
            return retrieveBrands();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchBrand(vm.searchKeyword)();
            }
            return retrieveBrands();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        //function viewBrand(brandId) {
        //    $window.location.href = "/Apartment/Edit/" + apartmentId;
        //}

    }
})();
