(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('TrendCommentController', TrendCommentController);

    TrendCommentController.$inject = ['$window', 'TrendCommentService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function TrendCommentController($window, TrendCommentService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.trendComments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.approve = approve;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "Comment";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Comment");
        }

        function retrieveTrendComments() {
            return TrendCommentService.retrieveTrendComments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.trendComments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.trendComments.length === 0 ? "No Records Found" : "";
                    return vm.trendComments;
                });
        }

        function pageChanged() {
            return retrieveTrendComments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveTrendComments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function approve(trendCommentId) {
            return TrendCommentService.approve(trendCommentId);
        }
    }
})();
