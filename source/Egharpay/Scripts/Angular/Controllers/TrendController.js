﻿(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('TrendController', TrendController);

    TrendController.$inject = ['$window', 'TrendService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function TrendController($window, TrendService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.trends = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;   
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.detailTrend = detailTrend;
        vm.trendComment = "";
        vm.createTrendComment = createTrendComment;
        vm.initialise = initialise;
        vm.readMore = readMore;
        vm.isreadMore = false;
        vm.errorMessages = [];

        function initialise() {
            vm.orderBy.property = "CreatedDateTime";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("CreatedDateTime");
        }

        function retrieveTrends() {
            vm.paging.page = 1;
            vm.paging.pageSize = vm.paging.pageSize + 5;
            return TrendService.retrieveTrends(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.trends = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.trends.length === 0 ? "No Records Found" : "";
                    return vm.trends;
                });
        }

        function pageChanged() {
            return retrieveTrends();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveTrends();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function detailTrend(trendId) {
            return TrendService.detailTrend(trendId).then(function (response) {
                vm.trends = response.data;
                return vm.trends;
            });
        }

        function createTrendComment(trend, trendComment,isLoggedin) {
            vm.errorMessages=[];
            if (!trendComment) vm.errorMessages.push('please enter comment.');
            if (vm.errorMessages.length > 0) return;
            if (!isLoggedin) {
                window.location.href = "/Account/login?returnUrl=" + window.location.pathname;
            }
          //  vm.trendComment = trendComment;
            else{
            var trendCommentData= {
                TrendId: trend.TrendId,
                Comment:trendComment
            }
            return TrendService.createTrendComment(trendCommentData);
            }
        }

        function readMore(trend) {
            if (trend.IsReadMore === undefined)
            {
                trend.IsReadMore = true;
            }
        }
    }
})();
