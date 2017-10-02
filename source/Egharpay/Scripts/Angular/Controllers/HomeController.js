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
        vm.retrieveSearchField = retrieveSearchField;
        vm.change = change;

        initialise();

        function initialise() {
            retrieveSearchField();
        }

        function retrieveSearchField() {
            MobileService.retrieveSearchField().then(function(response) {
                vm.SearchFields = response.data;
                $(".typeahead_2").typeahead({ source: vm.SearchFields });
            });
        };

        function change(centreId) {
            retrieveStatisticsByCentre(centreId);
        }
    }

})();
