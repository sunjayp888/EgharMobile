(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('AddressService', AddressService);

    AddressService.$inject = ['$http'];

    function AddressService($http) {
        var service = {
            createAddress: createAddress,
            retrievePersonnelAddress: retrievePersonnelAddress,
            removePersonnelAddress:removePersonnelAddress
        };

        return service;

        function createAddress(address) {
            var url = "/Address/Create",
                data = {
                    address: address
                };
            return $http.post(url, data);
        }

        function retrievePersonnelAddress(personnelId) {
            var url = "/Address/RetrievePersonnelAddress/" + personnelId;
            return $http.get(url);
        }

        function removePersonnelAddress(addressId) {
            var url = "/Address/RemovePersonnelAddress",
            data = {
                addressId: addressId
            };
            return $http.post(url,data);
        }
    }
})();