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
        vm.retrieveMobileRepairOrders = retrieveMobileRepairOrders;
        vm.createMobileRepairManageOrderOtp = createMobileRepairManageOrderOtp;
        vm.retrieveMobileRepairOrders = retrieveMobileRepairOrders;
        vm.deleteMobileRepairRequest = deleteMobileRepairRequest;
        vm.isRepair = true;
        vm.mobileRepairOrders = [];
        function initialise() {

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

        function retrieveMobileRepairOrders() {
            vm.errorMessages = [];
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (!vm.OTP) vm.errorMessages.push('Enter OTP.');
            if (vm.errorMessages.length > 0) return;
            return MobileRepairService.retrieveMobileRepairOrders(vm.mobileNumber, vm.OTP).then(function (response) {
                if (!response.data.Succeeded && response.data.Succeeded !== undefined) {
                    vm.errorMessages.push(response.data.Message);
                }
                else { vm.mobileRepairOrders = response.data; }
            });
        }

        function deleteMobileRepairRequest(mobileRepairId) {
            return MobileRepairService.deleteMobileRepairRequest(mobileRepairId, vm.mobileNumber, vm.OTP).then(function (response) {
                retrieveMobileRepairOrders();
            });
        }
    }
})();
//sellerbymobileid  mobile service