(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$window', 'HomeService', 'MobileService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function HomeController($window, HomeService, MobileService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.SearchFields = [];
        vm.paging = new Paging;
        //vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        // vm.order = order;
        //vm.orderClass = orderClass;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.listMobile = listMobile;
        vm.retrieveSearchField = retrieveSearchField;
        //vm.searchMobile = searchMobile;
        vm.change = change;
        vm.initialise = initialise;
        vm.searchMobiles = searchMobiles;
        vm.latestMobiles = latestMobiles;
        vm.latestMobileList = [];
        vm.showAll = false;
        vm.showAllLatestMobile = showAllLatestMobile;
        function initialise() {
            retrieveSearchField();
            latestMobiles(vm.showAll);
        }

        function retrieveSearchField() {
            MobileService.retrieveSearchField().then(function (response) {
                vm.SearchFields = response.data;
                $(".typeahead_2").typeahead({ source: vm.SearchFields });
            });
        };

        //function searchMobile(searchKeyword) {
        //    vm.searchKeyword = searchKeyword;
        //    return MobileService.searchMobile(vm.searchKeyword, vm.paging, vm.orderBy)
        //        .then(function (response) {
        //            vm.SearchFields = response.data.Items;
        //            vm.paging.totalPages = response.data.TotalPages;
        //            vm.paging.totalResults = response.data.TotalResults;
        //            vm.searchMessage = vm.SearchFields.length === 0 ? "No Records Found" : "";
        //            return vm.SearchFields;
        //        });
        //}

        function listMobile(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.orderBy.property = "MobileId";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            return HomeService.listMobile(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.SearchFields = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.SearchFields.length === 0 ? "No Records Found" : "";
                    return vm.SearchFields;
                });
        }

        function change(centreId) {
            retrieveStatisticsByCentre(centreId);
        }

        function searchMobiles() {
            //vm.searchKeyword = searchKeyword;
            $window.location.href = "/Home/Mobile/" + vm.searchKeyword;
            //return HomeService.searchMobiles(searchKeyword);
        }

        function latestMobiles(showAll) {
            vm.showAll = showAll;
            if (vm.showAll == true) {
                return HomeService.latestMobileData(vm.showAll)
                    .then(function (response) {
                        vm.latestMobileList = response.data;
                        return vm.latestMobileList;
                    });
                //$window.location.href = "/Home/latestMobileAll";
            }
            if (vm.showAll == false) {
                return HomeService.latestMobiles(vm.showAll)
                    .then(function (response) {
                        vm.latestMobileList = response.data;
                        return vm.latestMobileList;
                    });
            }
        }

        function showAllLatestMobile() {
            window.location.href = "/Mobile/AllLatestMobile";
        }
    }
})();
