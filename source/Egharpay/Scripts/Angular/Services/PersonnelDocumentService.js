(function (app) {
    'use strict';

    app.module('Egharpay').factory('PersonnelDocumentService', PersonnelDocumentService);

    PersonnelDocumentService.$inject = ['$http'];

    function PersonnelDocumentService($http) {
        var service = {
            retrieveDocumentCategories: retrieveDocumentCategories,
            retrievePersonnelDocuments: retrievePersonnelDocuments
        };

        return service;

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
    };

})(window.angular);