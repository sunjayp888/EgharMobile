(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileGalleryController', MobileGalleryController);

    MobileGalleryController.$inject = ['$window','$sce', 'MobileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileGalleryController($window, $sce, MobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.galleryImages = [];
        vm.retrieveGalleryImages = retrieveGalleryImages;
        vm.mobileId = "";

        function retrieveGalleryImages(mobileId) {
            vm.mobileId = mobileId;
            return MobileService.retrieveGalleryImages(vm.mobileId)
                .then(function (response) {
                    vm.galleryImages = response.data;
                    vm.searchMessage = vm.galleryImages.length === 0 ? "No Records Found" : "";
                    return vm.galleryImages;
                });
        }
    }
})();
