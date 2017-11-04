(function (app) {
    'use strict';

    app.module('Egharpay').factory('PersonnelDocumentService', PersonnelDocumentService);

    PersonnelDocumentService.$inject = ['$http'];

    function PersonnelDocumentService($http) {
        var service = {
            retrieveDocumentCategories: retrieveDocumentCategories,
            retrievePersonnelDocuments: retrievePersonnelDocuments,
            uploadSelfie: uploadSelfie,
            retrievePersonnelSelfies: retrievePersonnelSelfies
    };

        return service;

        function uploadSelfie(personneId, tagline, blob) {
            var formData = new FormData();
            formData.append('croppedImage', blob);

            var url = "/PersonnelDocument/" + personneId + "/" + tagline + "/UploadSelfie";

            return $http.post(url, formData, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            });
        };

        function retrieveDocumentCategories() {
            var url = "/Personnel/DocumentCategories";
            return $http.post(url);
        }

        function retrievePersonnelDocuments(workerId, paging, orderBy) {

            var url = "/Worker/" + workerId + "/WorkerDocuments";
            var data = {
                paging: paging,
                orderBy: new Array(orderBy)
            };
            return $http.post(url, data);
        }

        function retrievePersonnelSelfies() {
            var url = "/PersonnelDocument/RetrievePersonnelSelfieImages";
            return $http.post(url);
        }
    };

})(window.angular);