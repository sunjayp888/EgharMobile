(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('OTPService', OTPService);

    OTPService.$inject = ['$http'];

    function OTPService($http) {
        var service = {
            createLoginOtp: createLoginOtp,
            retrieveLoginOtp: retrieveLoginOtp,
            createMobileRepairOtp: createMobileRepairOtp,
            createMobileRepairPaymentOtp: createMobileRepairPaymentOtp,
            createForgetPasswordOtp: createForgetPasswordOtp
        };

        return service;

        function retrieveLoginOtp(mobileNumber) {
            var url = "/OTP/CreateLoginOtp",
                data = {
                    mobileNumber: mobileNumber
                };
            return $http.post(url, data);
        }

        function createLoginOtp(mobileNumber) {
            var url = "/OTP/CreateLoginOtp",
                data = {
                    mobileNumber: mobileNumber
                };
            return $http.post(url, data);
        }


        function createMobileRepairOtp(mobileNumber) {
            var url = "/OTP/CreateMobileRepairOtp",
                data = {
                    mobileNumber: mobileNumber
                };
            return $http.post(url, data);
        }

        function createMobileRepairPaymentOtp(mobileNumber, amount) {
            var url = "/OTP/createMobileRepairPaymentOtp",
                data = {
                    mobileNumber: mobileNumber,
                    amount: amount
                };
            return $http.post(url, data);
        }

        function createForgetPasswordOtp(mobileNumber) {
            var url = "/OTP/CreateForgetPasswordOtp",
                data = {
                    mobileNumber: mobileNumber
                };
            return $http.post(url, data);
        }



    }
})();