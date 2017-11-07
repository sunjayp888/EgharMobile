(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('SellerMobileService', SellerMobileService);

    SellerMobileService.$inject = ['$http'];

    function SellerMobileService($http) {
        var service = {
            assignMobileToSeller: assignMobileToSeller
        };

        return service;

        function assignMobileToSeller(mobileId, sellerId, price, discountPrice, isEmiAvailable) {
            var url = "/SellerMobile/AssignMobileToSeller";
            var data = { sellerMobile: { MobileId: mobileId, EMIAvailable: isEmiAvailable, SellerId: sellerId, Price: price, DiscountPrice: discountPrice } };
            return $http.post(url, data);
        }
    }
})();