(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('PincodeService', PincodeService);

    PincodeService.$inject = ['$http'];

    function PincodeService($http) {
        var service = {
            retrievePincode: retrievePincode,
            searchPincode: searchPincode
        };

        return service;

        function retrievePincode(Paging, OrderBy) {
            var url = "/Pincode/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchPincode(SearchKeyword, Paging, OrderBy) {
            var url = "/Pincode/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();