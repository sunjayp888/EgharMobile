(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileCommentController', MobileCommentController);

    MobileCommentController.$inject = ['$window', 'MobileCommentService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileCommentController($window, MobileCommentService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobileComments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.mobileComment = "";
        vm.orderClass = orderClass;
        vm.approve = approve;
        vm.createMobileComment = createMobileComment;
        vm.initialise = initialise;

        function initialise() {
            vm.orderBy.property = "Comment";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Comment");
        }

        function retrieveMobileComments() {
            return MobileCommentService.retrieveMobileComments(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileComments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileComments.length === 0 ? "No Records Found" : "";
                    return vm.mobileComments;
                });
        }

        function pageChanged() {
            return retrieveMobileComments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveMobileComments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function approve(mobileCommentId) {
            return MobileCommentService.approve(mobileCommentId);
        }

        function createMobileComment(mobileId, mobileComment) {
            var mobileCommentData = {
                MobileId: mobileId,
                Comment: mobileComment
            }
            return MobileCommentService.createMobileComment(mobileCommentData);
        }
    }
})();
