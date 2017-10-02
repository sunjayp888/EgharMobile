(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('YouTubeController', YouTubeController);

    YouTubeController.$inject = ['$window','$sce', 'YouTubeService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function YouTubeController($window, $sce, YouTubeService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.YouTubes = [];
        vm.search = search;
        vm.initialise = initialise;

        function initialise() {

        }

        function search(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return YouTubeService.search(vm.searchKeyword)
                .then(function (response) {
                    vm.YouTubes = response.data;
                    vm.searchMessage = vm.YouTubes.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }
    }
})();
