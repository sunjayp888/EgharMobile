(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('CentreController', CentreController);

    CentreController.$inject = ['$window', 'CentreService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function CentreController($window, CentreService, Paging, OrderService, OrderBy, Order, $uibModal, $modalInstance) {
        /* jshint validthis:true */
        var vm = this;
        vm.centres = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.editCentre = editCentre;
        vm.courseInstallments = [];
        vm.retrieveCourseInstallments = retrieveCourseInstallments;
        vm.CentreCount;
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveCentres() {
            return CentreService.retrieveCentres(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.centres = response.data.Items;
                    vm.CentreCount = response.data.Items.count;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    
                    return vm.centres;

                });
        }

        function pageChanged() {
            return retrieveCentres();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveCentres();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function editCentre(id) {
            $window.location.href = "/Centre/Edit/" + id;
        }

        function retrieveCourseInstallments(courseId) {
            return CentreService.retrieveCourseInstallments(courseId).then(function () {
                vm.courseInstallments = response.data;
            });
        };
    }

})();
