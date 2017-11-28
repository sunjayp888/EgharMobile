(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileController', MobileController);

    MobileController.$inject = ['$window', 'MobileService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileController($window, MobileService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobiles = [];
        vm.mobilesInStore = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.searchMobile = searchMobile;
        vm.detailMobile = detailMobile;
        vm.addPincode = addPincode;
        vm.searchSeller = searchSeller;
        vm.requestMobile = requestMobile;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.currentAddress;
        vm.Address = [];
        vm.PinCode;
        vm.modalInstance = null;
        var country, state, city, pinCode, map, latitude, longitude, count, pin;
        vm.initialise = initialise;
        vm.retrieveMobilesInStore = retrieveMobilesInStore;
        vm.filter;
        vm.priceChange = priceChange;
        vm.fromPrice;
        vm.toPrice;
        vm.ramSize;
        vm.retrieveMobileByBrandId = retrieveMobileByBrandId;
        vm.cameraSize;
        vm.batterySize;
        vm.internalMemorySize;


        function initialise(filter) {
            vm.filter = filter;
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function retrieveMobiles() {
            return MobileService.retrieveMobiles(vm.filter, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function searchMobile(searchKeyword) {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.searchKeyword = searchKeyword;
            return MobileService.searchMobile(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchMobile(vm.searchKeyword)();
            }
            return retrieveMobiles();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchMobile(vm.searchKeyword)();
            }
            return retrieveMobiles();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function detailMobile(mobileId) {
            return MobileService.detailMobile(mobileId).then(function (response) {
                vm.mobiles = response.data;
                return vm.mobiles;
            });
        }

        function addPincode() {
            geoLocation();
        }

        function geoLocation() {
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    latitude = position.coords.latitude;
                    longitude = position.coords.longitude;
                    getLocationDetails();
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
                //openPincodeModal(false);
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
                    $("#txtSearchPincode").val(vm.Address.PinCode);
                }
                else {
                    vm.Address = { Error: "No location available for provided details." }
                    //openPincodeModal(false);
                }
            };
            xhr.onerror = function () {
                vm.Address = { Error: "Woops, there was an error making the request." }
                //openPincodeModal(false);
            };
            xhr.send();
        }

        function searchSeller(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return MobileService.searchSeller(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function requestMobile(mobileId) {
            //$window.location.href = "/Order/Create/" + mobileId;
            return MobileService.requestMobile(mobileId).then(function (response) {
                vm.mobiles = response.data;
                return vm.mobiles;
            });
        }

        function retrieveMobilesInStore() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            return MobileService.retrieveMobilesInStore(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobilesInStore = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobiles.length === 0 ? "No Records Found" : "";
                    return vm.mobiles;
                });
        }

        function priceChange() {
            vm.filter = { IsFilter: true, FromPrice: vm.fromPrice, ToPrice: vm.toPrice }
            retrieveMobiles();
        }

        function retrieveMobileByBrandId(brandId) {
            vm.filter = { IsBrandFilter: true, BrandId: brandId }
            order("Name");
        }

    }
})();
