﻿(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('HomeBannerImageController', HomeBannerImageController);

    HomeBannerImageController.$inject = ['$window', 'HomeBannerImageService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function HomeBannerImageController($window, HomeBannerImageService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.homeBannerImageList = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.homeBannerId;
        vm.initialise = initialise;
        vm.uploadPhoto = uploadPhoto;
        vm.deletePhoto = deletePhoto;
        vm.imageUploadError = false;
        vm.fileFormatError = false;
        vm.fileError = false;
        vm.retrieveHomeBannerImage = retrieveHomeBannerImage;
        vm.retrieveHomeBannerImageList = retrieveHomeBannerImageList;
        vm.deleteHomeBannerImageDocument = deleteHomeBannerImageDocument;
        var cropImage;

        function initialise(homeBannerId) {
            vm.homeBannerId = homeBannerId;
            order("Name");
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

        function uploadPhoto(base64String) {
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
                    return HomeBannerImageService.UploadPhoto(vm.homeBannerId, blobImage)
                        .then(function (response) {
                            var randomNumber = Math.random();//This will force the browsers to reload the image url
                            angular.element('#HomeBannerImage').attr('src', '/HomeBanner/' + vm.homeBannerId + '/Photo?' + randomNumber);
                            angular.element('#HomeBannerImageModal').modal('toggle');
                            retrieveHomeBannerImageList();
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

        function deletePhoto() {
            return HomeBannerImageService.DeletePhoto(vm.homeBannerId)
                .then(function (response) {
                    document.getElementById('HomeBannerImage').setAttribute('src', location.protocol + '//' + location.host + '/Content/images/user.png');
                    $("#HomeBannerImage").modal("hide");
                });
        }

        function retrieveHomeBannerImage() {
            return HomeBannerImageService.retrieveHomeBannerImage(vm.homeBannerId)
                .then(function (response) {
                    //If response is null then default image
                    document.getElementById('HomeBannerImage').setAttribute('src', response.data.RelativePath);
                });
        }

        function retrieveHomeBannerImageList() {
            return HomeBannerImageService.retrieveHomeBannerImageList(vm.homeBannerId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.homeBannerImageList = response.data.Items;
                    return vm.homeBannerImageList;
                });
        }

        function deleteHomeBannerImageDocument(documentDetailId, guid) {
            return HomeBannerImageService.deleteHomeBannerImageDocument(documentDetailId, guid).then(function () {
                retrieveHomeBannerImageList(vm.homeBannerId);
            });
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveHomeBannerImageList();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function pageChanged() {
            return retrieveHomeBannerImageList()();
        }
    }
})();
