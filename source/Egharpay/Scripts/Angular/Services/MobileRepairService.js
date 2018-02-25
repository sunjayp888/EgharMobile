(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileRepairService', MobileRepairService);

    MobileRepairService.$inject = ['$http'];

    function MobileRepairService($http) {
        var service = {
            createMobileRepairRequest: createMobileRepairRequest,
            retrieveMobileRepairOrders: retrieveMobileRepairOrders,
            deleteMobileRepairRequest: deleteMobileRepairRequest
        };

        return service;

        function createMobileRepairRequest(model) {
            var url = "/MobileRepair/Create",
                data = {
                    model: model
                };
            return $http.post(url, data);
        }

        function retrieveMobileRepairOrders(mobileNumber, otp) {
            var url = "/MobileRepair/RetrieveMobileRepairOrders/" + mobileNumber + "/" + otp;
            return $http.get(url);
        }

        function deleteMobileRepairRequest(mobileRepairId, mobileNumber, otp) {
            var url = "/MobileRepair/DeleteMobileRepairRequest",
                 data = {
                     mobileRepairId: mobileRepairId,
                     mobileNumber: mobileNumber,
                     otp: otp
                 };
            return $http.post(url, data);
        }

    }
})();