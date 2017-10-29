(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('PersonnelProfileService', PersonnelProfileService);

    PersonnelProfileService.$inject = ['$http'];

    function PersonnelProfileService($http) {
        var service = {
            UploadPhoto: UploadPhoto,
            DeletePhoto: DeletePhoto,
            retrieveProfileImage: retrieveProfileImage
        };

        return service;

        function UploadPhoto(personnelId, blob) {
            var formData = new FormData();
            formData.append('croppedImage', blob);

            var url = "/Personnel/UploadPhoto/" + personnelId;

            return $http.post(url, formData, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            });
        };

        function DeletePhoto(personnelId) {
            var url = "/Personnel/DeletePhoto/" + personnelId;
            return $http.post(url);
        };

        function retrieveProfileImage(personnelId) {
            var url = "/Personnel/RetrieveProfileImage/" + personnelId;
            return $http.post(url);
        }
    }
})();