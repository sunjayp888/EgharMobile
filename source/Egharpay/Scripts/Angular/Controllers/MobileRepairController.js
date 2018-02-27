(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileRepairController', MobileRepairController);

    MobileRepairController.$inject = ['$window', '$sce', 'MobileRepairService', 'OTPService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileRepairController($window, $sce, MobileRepairService, OTPService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.errorMessages = [];
        vm.isOtpCreated = false;
        vm.mobileNumber;
        vm.createMobileRepairOtp = createMobileRepairOtp;
        vm.createMobileRepairRequest = createMobileRepairRequest;
        vm.errorMessages = [];
        vm.OTP;
        vm.modelName;
        vm.description;
        vm.couponCode;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.retrieveMobileRepairOrdersByMobile = retrieveMobileRepairOrdersByMobile;
        vm.createMobileRepairManageOrderOtp = createMobileRepairManageOrderOtp;
        vm.retrieveMobileRepairOrders = retrieveMobileRepairOrders;
        vm.deleteMobileRepairRequest = deleteMobileRepairRequest;
        vm.markAsCompleted = markAsCompleted;
        vm.markAsCancelled = markAsCancelled;
        vm.isRepair = true;
        vm.mobileRepairOrders = [];
        vm.initialise = initialise;
        //vm.mobileRepairState = mobileRepairState;

        function initialise() {
            vm.orderBy.property = "MobileRepairId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("MobileRepairId");
        }

        function createMobileRepairOtp() {
            vm.showMessage = false;
            vm.isRepair = true;
            vm.errorMessages = [];
            vm.OTP = "";
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (vm.errorMessages.length > 0) return;
                return OTPService.createMobileRepairOtp(vm.mobileNumber).then(function (response) {
                    vm.showMessage = true;
                    vm.isOtpCreated = response.data.Succeeded;
                    vm.errorMessages.push(response.data.Message);
                });
        }


        function createMobileRepairRequest() {
            vm.showMessage = false;
            vm.errorMessages = [];
            var model = {
                MobileNumber: vm.mobileNumber,
                ModelName: vm.modelName,
                Description: vm.description,
                CouponCode: vm.couponCode,
                OTP: vm.OTP
            }
            return MobileRepairService.createMobileRepairRequest(model).then(function (response) {
                vm.showMessage = true;
                vm.errorMessages.push(response.data.Message);
            });
        }


        function createMobileRepairManageOrderOtp() {
            vm.showMessage = false;
            vm.errorMessages = [];
            vm.OTP = "";
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (vm.errorMessages.length > 0) return;
            return OTPService.createMobileRepairOtp(vm.mobileNumber).then(function (response) {
                vm.showMessage = true;
                vm.isRepair = false;
                vm.isOtpCreated = response.data.Succeeded;
                vm.errorMessages.push(response.data.Message);
            });
        }

        //function retrieveMobileRepairOrders() {
        //    return MobileRepairService.retrieveMobileRepairOrdersByMobile(vm.mobileNumber).then(function (response) {
        //        vm.mobileRepairOrders = response.data;
        //    });
        //}

        function retrieveMobileRepairOrders() {
            return MobileRepairService.retrieveMobileRepairOrders(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileRepairOrders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileRepairOrders.length === 0 ? "No Records Found" : "";
                    return vm.mobileRepairOrders;
                });
        }

        function pageChanged() {
            return retrieveMobileRepairOrders();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveMobileRepairOrders();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        //function mobileRepairState(mobileRepairId,mobileRepairStateId) {
        //    return MobileRepairService.mobileRepairState(mobileRepairId, mobileRepairStateId).then(function (response) {
        //    }

        function markAsCancelled(mobileRepairId) {
            return MobileRepairService.markAsCompleted(mobileRepairId).then(function (response) {
                retrieveMobileRepairOrders();
            });
        }

        function markAsCompleted(mobileRepairId) {
            return MobileRepairService.markAsCompleted(mobileRepairId).then(function (response) {
                retrieveMobileRepairOrders();
            });
        }

        function retrieveMobileRepairOrdersByMobile() {
            vm.errorMessages = [];
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (!vm.OTP) vm.errorMessages.push('Enter OTP.');
            if (vm.errorMessages.length > 0) return;
            return MobileRepairService.retrieveMobileRepairOrdersByMobile(vm.mobileNumber, vm.OTP).then(function (response) {
                if (!response.data.Succeeded && response.data.Succeeded !== undefined) {
                    vm.errorMessages.push(response.data.Message);
                }
                else { vm.mobileRepairOrders = response.data; }
            });
        }

        function deleteMobileRepairRequest(mobileRepairId) {
            return MobileRepairService.deleteMobileRepairRequest(mobileRepairId, vm.mobileNumber, vm.OTP).then(function (response) {
                retrieveMobileRepairOrdersByMobile();
            });
        }
    }

})();
//sellerbymobileid  mobile service