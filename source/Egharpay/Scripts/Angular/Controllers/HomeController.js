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
        vm.listMobile = listMobile;
        vm.retrieveSearchField = retrieveSearchField;
        vm.searchMobile = searchMobile;
        vm.change = change;

        initialise();

        function initialise() {
            retrieveSearchField();
        }

        function retrieveSearchField() {
            MobileService.retrieveSearchField().then(function (response) {
                vm.SearchFields = response.data;
                $(".typeahead_2").typeahead({ source: vm.SearchFields });
            });
        };

        function searchMobile(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return MobileService.searchMobile(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.SearchFields = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.SearchFields.length === 0 ? "No Records Found" : "";
                    return vm.SearchFields;
                });
        }

        function listMobile(searchKeyword) {
            vm.searchKeyword = searchKeyword;
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
    }



})();
