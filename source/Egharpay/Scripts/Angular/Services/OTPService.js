(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('OTPService', OTPService);

    OTPService.$inject = ['$http'];

    function OTPService($http) {
        var service = {
            createLoginOtp: createLoginOtp,
            retrieveLoginOtp: retrieveLoginOtp
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


    }
})();