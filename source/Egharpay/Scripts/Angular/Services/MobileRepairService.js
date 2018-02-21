(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileRepairService', MobileRepairService);

    MobileRepairService.$inject = ['$http'];

    function MobileRepairService($http) {
        var service = {
            createMobileRepairRequest: createMobileRepairRequest,
            retrieveMobileRepairOrders: retrieveMobileRepairOrders
        };

        return service;

        function createMobileRepairRequest(model) {
            var url = "/MobileRepair/Create",
                data = {
                    model: model
                };
            return $http.post(url, data);
        }

        function retrieveMobileRepairOrders(mobileNumber) {
            var url = "/MobileRepair/RetrieveMobileRepairOrders/" + mobileNumber;
            return $http.get(url, data);
        }

    }
})();