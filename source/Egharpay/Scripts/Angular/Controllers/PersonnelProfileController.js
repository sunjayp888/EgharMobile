(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('PersonnelProfileController', PersonnelProfileController);

    PersonnelProfileController.$inject = ['$window', 'PersonnelProfileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function PersonnelProfileController($window, PersonnelProfileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.personnelId;
        vm.initialise = initialise;
        vm.uploadPhoto = uploadPhoto;
        vm.deletePhoto = deletePhoto;
        vm.imageUploadError = false;
        vm.fileFormatError = false;
        vm.fileError = false;
        vm.retrieveProfileImage = retrieveProfileImage;

        var cropImage;


        //Cropper
        var cropImage = $('#UploadProfilePicture');
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

        function initialise(personnelId) {
            vm.personnelId = personnelId;
            retrieveProfileImage();
        }

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
                    return PersonnelProfileService.UploadPhoto(vm.personnelId, blobImage)
                        .then(function (response) {
                            var randomNumber = Math.random();//This will force the browsers to reload the image url
                            angular.element('#ProfilePicture').attr('src', '/Personnel/' + vm.personnelId + '/Photo?' + randomNumber);
                            angular.element('#ProfilePictureModal').modal('toggle');
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
            return PersonnelProfileService.DeletePhoto(vm.personnelId)
                    .then(function (response) {
                        document.getElementById('ProfilePicture').setAttribute('src', location.protocol + '//' + location.host + '/Images/Avatar.png');
                        $("#ProfilePictureModal").modal("hide");
                    });
        }

        function retrieveProfileImage() {
            return PersonnelProfileService.retrieveProfileImage(vm.personnelId)
                .then(function (response) {
                    //If response is null then default image
                    if (response.data === "")
                        document.getElementById('ProfilePicture').setAttribute('src', location.protocol + '//' + location.host + "/Images/Avatar.png");
                    else
                        document.getElementById('ProfilePicture').setAttribute('src', response.data.RelativePath);
                });
        }
    }
})();
