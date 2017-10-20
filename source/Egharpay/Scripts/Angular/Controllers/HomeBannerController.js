(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('HomeBannerController', HomeBannerController);

    HomeBannerController.$inject = ['$window', 'HomeBannerService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function HomeBannerController($window, HomeBannerService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.homeBanners = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editHomeBanner = editHomeBanner;
        //vm.searchApartment = searchApartment;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveHomeBanner() {
            return HomeBannerService.retrieveHomeBanner(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.homeBanners = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.homeBanners.length === 0 ? "No Records Found" : "";
                    return vm.homeBanners;
                });
        }

        //function searchApartment(searchKeyword) {
        //    vm.searchKeyword = searchKeyword;
        //    return ApartmentService.searchApartment(vm.searchKeyword, vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.apartments = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            vm.searchMessage = vm.apartments.length === 0 ? "No Records Found" : "";
        //            return vm.apartments;
        //        });
        //}

        function pageChanged() {
            //if (vm.searchKeyword) {
            //    return searchApartment(vm.searchKeyword)();
            //}
            return retrieveHomeBanner();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            //if (vm.searchKeyword) {
            //    return searchApartment(vm.searchKeyword)();
            //}
            return retrieveHomeBanner();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editHomeBanner(homeBannerId) {
            $window.location.href = "/HomeBanner/Edit/" + homeBannerId;
        }

    }
})();
