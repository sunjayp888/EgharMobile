(function () {
    'use strict';

    angular
        .module('Egharpay')
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
        vm.pageChanged = pageChanged;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.downloadFile = downloadFile;
        vm.uploadSelfie = uploadSelfie;
        vm.tagline ;
        //vm.retrievePersonnelSelfies = retrievePersonnelSelfies;
        var cropImage;

        function initialise(personnelId) {
            vm.personnelId = personnelId;
            order("CreatedDateTime");
        }

        //Cropper
        var cropImage = $('#UploadHomeBannerImage');
        var options = {
            aspectRatio: 1 / 1,
            responsive: true,
            crop: function (e) {
            }
        };

        $('#inputImage').change(function () {
            var files = this.files;
            var file;

            if (!cropImage.data('cropper')) {
                return;
            }

            if (files && files.length) {
                file = files[0];
                if (/^image\/\w+$/.test(file.type)) {
                    var blobURL = URL.createObjectURL(file);
                    cropImage.one('built.cropper', function () {
                        URL.revokeObjectURL(blobURL);
                    }).cropper('reset').cropper('replace', blobURL);
                } else {
                    window.alert('Please choose an image file.');
                }
            }
        });
        //Cropper

        function uploadSelfie(base64String) {
            var filevalue = angular.element('#fileUpload').val();
            var validFormats = ['jpg', 'gif', 'jpeg', 'png', 'bmp'];
            if (filevalue == "") {
                vm.imageUploadError = true;
                vm.fileError = true;
            }
            else {
                vm.imageUploadError = false;
                vm.fileError = false;
                var ext = filevalue.substring(filevalue.lastIndexOf('.') + 1).toLowerCase();
                if (validFormats.indexOf(ext) !== -1) {
                    vm.fileFormatError = false;
                    var base64result = base64String.split(',');
                    var blobImage = new b64toBlob(base64result[1], base64result[0]);
                    return PersonnelDocumentService.uploadSelfie(vm.personnelId, vm.tagline, blobImage)
                        .then(function (response) {
                            angular.element('#ProfileSelfieModal').modal('toggle');
                            //retrievePersonnelSelfies();
                        });
                } else {
                    vm.imageUploadError = true;
                    vm.fileFormatError = true;
                }
            }
        }

        function b64toBlob(b64Data, contentType, sliceSize) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512;

            var byteCharacters = atob(b64Data);
            var byteArrays = [];

            for (var offset = 0, byteLen = byteCharacters.length; offset < byteLen; offset += sliceSize) {
                var slice = byteCharacters.slice(offset, offset + sliceSize);

                var byteNumbers = new Array(slice.length);
                for (var i = 0; i < slice.length; i++) {
                    byteNumbers[i] = slice.charCodeAt(i);
                }

                var byteArray = new Uint8Array(byteNumbers);

                byteArrays.push(byteArray);
            }

            var blob = new Blob(byteArrays, { type: contentType });
            return blob;
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

        function downloadFile(guid) {
            $window.location.href = '/Worker/DownloadFile/' + guid;
        }

    }

})(); //IIFE