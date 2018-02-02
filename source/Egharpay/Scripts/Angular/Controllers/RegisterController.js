(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$window', 'Paging', 'OrderService', 'OTPService', 'OrderBy', 'Order', '$uibModal'];

    function RegisterController($window, Paging, OrderService, OTPService, OrderBy, Order, $uibModal) {
        /* jshint validthis:true */
        var vm = this;
        var country, state, city, pinCode, map, latitude, longitude, count, pin;

        vm.addPincode = addPincode;
        vm.modalInstance = null;
        vm.currentAddress;
        vm.Address = [];
        vm.PinCode;
        vm.isOtpCreated = false;
        vm.mobileNumber;
        vm.createLoginOtp = createLoginOtp;
        vm.isSeller = false;
        vm.otpMessage;
        vm.showOtpCreatedMessage = false;
        function addPincode() {
            geoLocation();
        }

        function openPincodeModal(location) {
            vm.modalInstance = $uibModal.open({
                size: 'sm',
                templateUrl: '/Scripts/Angular/Templates/PincodeModal.html',
                controller: ['parent', '$uibModalInstance', 'location',
                function (parent, $uibModalInstance, location) {
                    var $modal = this;
                    $modal.parent = parent;
                    $modal.modalClose = modalClose;
                    $modal.modalSubmit = modalSubmit;
                    $modal.errorMessage = null;
                    $modal.currentAddress = location == true ?
                                           vm.Address.City + ',' + vm.Address.State + ',' + vm.Address.Country + ' ' + vm.Address.PinCode : vm.Address.Error;

                    function modalClose() { $uibModalInstance.dismiss(); }
                    function modalSubmit() {
                        $("#Pincode").val(vm.Address.PinCode);
                        $uibModalInstance.dismiss();
                    }
                }],
                controllerAs: 'model',
                resolve: {
                    parent: function () { return vm; },
                    location: location
                }
            });
        }

        function geoLocation() {
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    latitude = position.coords.latitude;
                    longitude = position.coords.longitude;
                    getLocationDetails();
                    //openPincodeModal(location);
                });
            } else {
                console.log("Browser doesn't support geolocation!");
            }
        }

        function createCORSRequest(method, url) {
            var xhr = new XMLHttpRequest();
            if ("withCredentials" in xhr) {
                // XHR for Chrome/Firefox/Opera/Safari.
                xhr.open(method, url, true);
            } else if (typeof XDomainRequest != "undefined") {
                // XDomainRequest for IE.
                xhr = new XDomainRequest();
                xhr.open(method, url);
            } else {
                // CORS not supported.
                xhr = null;
            }
            return xhr;
        }

        function getLocationDetails() {
            var url = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&sensor=true";
            var xhr = createCORSRequest('POST', url);
            if (!xhr) {
                vm.Address = { Error: "CORS not supported" }
                openPincodeModal(false);
            }
            xhr.onload = function () {
                var data = JSON.parse(xhr.responseText);
                if (data.results.length > 0) {
                    var locationDetails = data.results[0].formatted_address;
                    var value = locationDetails.split(",");
                    count = value.length;
                    country = value[count - 1];
                    state = value[count - 2];
                    city = value[count - 3];
                    pin = state.split(" ");
                    pinCode = pin[pin.length - 1];
                    state = state.replace(pinCode, ' ');
                    vm.currentAddress = locationDetails;
                    vm.Address = { City: city, State: state, Country: country, PinCode: pinCode }
                    openPincodeModal(true);
                }
                else {
                    vm.Address = { Error: "No location available for provided details." }
                    openPincodeModal(false);
                }
            };
            xhr.onerror = function () {
                vm.Address = { Error: "Woops, there was an error making the request." }
                openPincodeModal(false);
            };
            xhr.send();
        }

        function createLoginOtp(mobileNumber) {
            vm.showOtpCreatedMessage = false;
            return OTPService.createLoginOtp(mobileNumber).then(function (response) {
                vm.showOtpCreatedMessage = response.data.Succeeded;
                vm.isOtpCreated = response.data.Succeeded;
                vm.otpMessage = response.data.Message;
            });
        }
    }

})();
