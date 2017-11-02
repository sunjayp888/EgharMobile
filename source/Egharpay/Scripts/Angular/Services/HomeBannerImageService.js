(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('HomeBannerImageService', HomeBannerImageService);

    HomeBannerImageService.$inject = ['$http'];

    function HomeBannerImageService($http) {
        var service = {
            UploadPhoto: UploadPhoto,
            DeletePhoto: DeletePhoto,
            retrieveHomeBannerImage: retrieveHomeBannerImage
        };

        return service;

        function UploadPhoto(homeBannerId, blob) {
            var formData = new FormData();
            formData.append('croppedImage', blob);

            var url = "/HomeBanner/UploadPhoto/" + homeBannerId;

            return $http.post(url, formData, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            });
        };

        function DeletePhoto(homeBannerId) {
            var url = "/HomeBanner/DeletePhoto/" + homeBannerId;
            return $http.post(url);
        };

        function retrieveHomeBannerImage(homeBannerId) {
            var url = "/HomeBanner/RetrieveHomeBannerImage/" + homeBannerId;
            return $http.post(url);
        }
    }
})();