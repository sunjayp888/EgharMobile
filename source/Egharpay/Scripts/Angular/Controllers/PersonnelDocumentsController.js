(function () {
    'use strict';

    angular
        .module('TEMP')
        .controller('PersonnelDocumentsController', PersonnelDocumentsController);

    PersonnelDocumentsController.$inject = ['$window', '$scope', 'PersonnelDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal'];

    function PersonnelDocumentsController($window, $scope, PersonnelDocumentService, Paging, OrderService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        vm.workerId = null;
        vm.clientPersonnelId = null;
        vm.clientId = null;
        vm.documents = [];
        vm.clientDocuments = null;
        vm.searchMessage = null;
        vm.paging = new Paging();
        vm.orderBy = new OrderBy;
        vm.isLoading = false;
        vm.isLoaded = false;
        vm.documentCategories = [];
        vm.initialise = initialise;
        vm.getClientPersonnelDocuments = getClientPersonnelDocuments;
        vm.getWorkerDocuments = getWorkerDocuments;
        vm.pageChanged = pageChanged;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.downloadFile = downloadFile;
        vm.showDocumentModal = showDocumentModal;
        vm.retrieveDocumentCategories = retrieveDocumentcategories;

        return vm;

        function initialise(personnelId) {
            vm.personnelId = personnelId;
            if (!(vm.documents.length > 0)) {
                vm.orderBy.direction = 'Descending';
                vm.orderBy.property = 'CreatedDateUtc';
                getDocuments();
            }
            retrieveDocumentcategories();
        }

        function pageChanged() {
            getDocuments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            getDocuments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function getDocuments() {
            vm.isLoading = true;
            getWorkerDocuments();
        }

        function getWorkerDocuments() {
            PersonnelDocumentService.retrievePersonnelDocuments(vm.workerId, vm.paging, vm.orderBy).then(getDocuments_OnSuccess, getDocuments_OnFail);
        }

        function getClientPersonnelDocuments(workerId, clientPersonnelId, clientId) {

            retrieveDocumentcategories();
            vm.workerId = workerId;
            vm.clientPersonnelId = clientPersonnelId;
            vm.clientId = clientId;
            if (!(vm.documents.length > 0)) {
                vm.orderBy.direction = 'Descending';
                vm.orderBy.property = 'CreatedDateUtc';
            }
            WorkerProfileService.retrieveClientWorkerDocuments(vm.clientPersonnelId, vm.paging, vm.orderBy).then(getDocuments_OnSuccess, getDocuments_OnFail);
        }

        function getDocuments_OnSuccess(response) {
            //vm.documents = [];
            vm.documents = response.data.Items;
            vm.paging.totalPages = response.data.TotalPages;
            vm.paging.totalResults = response.data.TotalResults;

            vm.searchMessage = vm.documents.length === 0 ? "No Records Found" : "";
            vm.isLoading = false;
            vm.isLoaded = true;
        }

        function getDocuments_OnFail(response) {
            //vm.documents = [];
            vm.isLoading = false;
            vm.isLoaded = true;
            vm.searchMessage = "There was an error while trying to retrieve the worker documents.";
        }

        function downloadFile(guid) {
            $window.location.href = '/Worker/DownloadFile/' + guid;
        }


        function showDocumentModal() {
            vm.modalInstance = $uibModal.open({
                templateUrl: "WorkerDocument/" + vm.workerId + "/UploadDocumentModal",
                size: 'm',
                controller: ['parent', '$uibModalInstance', 'uiUploader', function (parent, $uibModalInstance, uiUploader) {
                    var $modal = this;
                    $modal.parent = parent;
                    $modal.Document = {
                        clientPersonnelId: parent.clientPersonnelId,
                        clientId: parent.clientId,
                        workerId: parent.workerId
                    };
                    $modal.modalClose = modalClose;
                    $modal.modalSubmit = modalSubmit;
                    $modal.errorMessage = null;
                    $modal.retrieveDocumentcategories = retrieveDocumentcategories();
                    $modal.isWorkerDocument = isWorkerDocument;
                    $modal.DocumentCategoryId = null;
                    $modal.Description = "";
                    $modal.errorMessages = [];
                    $modal.submitting = false;
                    $modal.submitted;
                    $modal.Url = null;
                    $modal.data = null;
                    $modal.filevalue;
                    $modal.fileFormatError;
                    $modal.fileError;
                    $modal.documentCategories = parent.documentCategories;

                    $modal.onFileChange = function (event) {
                        $modal.selectedFile = null;
                        uiUploader.removeAll();

                        var files = event.target.files;
                        if (files && files.length > 0) {
                            uiUploader.addFiles(files);
                            $scope.$apply(function () {
                                $modal.selectedFile = uiUploader.files[0];
                                $modal.errorMessages = [];
                            });
                        }
                    };

                    function modalClose() { $uibModalInstance.dismiss(); }

                    function modalSubmit() {
                        $modal.submitting = true;
                        if (isValidFile()) {
                            $modal.Document.DocumentCategoryId = $modal.DocumentCategoryId;
                            $modal.Document.Description = $modal.Description;
                            createDocument($modal.Document);
                        }
                    }

                    function isValidFile() {
                        var validFormats = ['jpg', 'gif', 'jpeg', 'png', 'bmp', 'csv', 'pdf'];
                        $modal.filevalue = angular.element('#documentFile').val();
                        if ($modal.filevalue === "") {
                            $modal.errorMessages = [];
                            $modal.errorMessages.push("Please Select File");
                            $modal.submitting = false;
                            return false;
                        }
                        else {
                            var ext = $modal.filevalue.substring($modal.filevalue.lastIndexOf('.') + 1).toLowerCase();
                            if (!(validFormats.indexOf(ext) !== -1)) {
                                $modal.errorMessages = [];
                                $modal.errorMessages.push("Please Upload .jpg,.jpeg,.png,.gif,.bmp File");
                                $modal.submitting = false;
                                return false;
                            }
                            return true;
                        }
                    }

                    function createDocument(document) {
                        if ($modal.isWorkerDocument) {
                            $modal.Url = "/Workers/" + document.workerId + "/Documents/Upload";
                            $modal.data = { workerId: document.workerId, documentCategoryId: document.DocumentCategoryId, description: document.Description }
                        } else {
                            $modal.Url = "/Clients/" + document.clientId + "/Workers/" + document.clientPersonnelId + "/Documents/Upload";
                            $modal.data = { clientId: document.clientId, clientPersonnelId: document.clientPersonnelId, documentCategoryId: document.DocumentCategoryId, description: document.Description }
                        }

                        uiUploader.startUpload({
                            url: $modal.Url,
                            data: $modal.data,
                            concurrency: 1,
                            headers: {
                                'Accept': 'application/json'
                            },
                            onCompleted: function (file, responseText, status) {
                                var responseData = angular.fromJson(responseText);
                                if (responseData.Success) {
                                    if (isWorkerDocument) {
                                        parent.getWorkerDocuments();
                                    } else {
                                        parent.getClientPersonnelDocuments(document.workerId, document.clientPersonnelId, document.clientId);
                                    }
                                    $uibModalInstance.close();
                                }
                                else {
                                    $scope.$apply(function () {
                                        clearSelectedFile(); //need to clear to let user re-upload the file. uiuploader doesn't work when retrying unless the file is selected again
                                        $modal.errorMessages.push(responseData.Error + ' Please try again.');
                                    });
                                }

                                $scope.$apply(function () {
                                    $modal.submitting = false;
                                });
                            }
                        });
                    }

                    function clearSelectedFile() {
                        angular.element("input[type='file']").val(null);
                        uiUploader.removeAll();
                        $modal.selectedFile = null;
                    }

                    function uploadWorkerDocument(document) {
                        $modal.parent.uploadWorkerDocument(document)
                        .then(
                            function (response) {
                                if (!response.data.Succeeded)
                                    $modal.errorMessage = response.data.Message;
                                else {
                                    $modal.parent.getWorkerDocuments();
                                    modalClose();
                                }
                            },
                            function (response) {
                                $modal.errorMessage = 'There was a problem while trying to save the record.';
                            });
                    }

                    function uploadClientPersonnelDocument(document) {
                        $modal.parent.uploadClientPersonnelDocument(document)
                         .then(
                             function (response) {
                                 if (!response.data.Succeeded)
                                     $modal.errorMessage = response.data.Message;
                                 else {
                                     $modal.parent.getWorkerDocuments();
                                     modalClose();
                                 }
                             },
                             function (response) {
                                 $modal.errorMessage = 'There was a problem while trying to save the record.';
                             });
                    }

                }],
                controllerAs: 'model',
                resolve: {
                    parent: function () { return vm; }
                }
            }).result.then(
                function (response) {
                    if (onchange) {
                        onchange(response);
                    }
                    //getExpenses();
                },
                function () { });
        }

        function retrieveDocumentcategories() {
            WorkerDocumentService.retrieveDocumentCategories().then(function (response) {
                vm.documentCategories = response.data;
            });
        };

    }

})(); //IIFE