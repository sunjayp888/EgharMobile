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
        vm.fromPrice;
        vm.toPrice;
        vm.fromRamSize;
        vm.toRamSize;
        vm.fromPrimaryCameraSize;
        vm.toPrimaryCameraSize;
        vm.fromSecondaryCameraSize;
        vm.toSecondaryCameraSize;
        vm.retrieveMobileByBrandId = retrieveMobileByBrandId;
        vm.cameraSize;
        vm.batterySize;
        vm.internalMemorySize;
        vm.batterySize;
        vm.primaryCameraSize;
        vm.secondaryCameraSize;
        vm.isPriceFilter;
        vm.isRamSizeFilter;
        vm.isPrimaryCameraFilter;
        vm.isSecondaryCameraFilter;
        vm.isBrandFilter;
        vm.isInternalMemoryFilter;
        vm.onPriceFilter = onPriceFilter;
        vm.onRamSizeFilter = onRamSizeFilter;
        vm.onPrimaryCameraFilter = onPrimaryCameraFilter;
        vm.onSecondaryCameraFilter = onSecondaryCameraFilter;
        vm.searchMobile = searchMobile;
        vm.isAssignButtonEnable = true;
        vm.canWeAssign = canWeAssign;
        vm.zoomOptions1 = {
            defaultImage: 0,
            style: 'box',
            boxPos: 'right-top',
            boxW: 400,
            boxH: 400,
            method: 'lens',
            cursor: 'crosshair',
            lens: true,
            zoomLevel: 3,
            immersiveMode: '769',
            immersiveModeOptions: {
            },
            prevThumbButton: '&#9665;',
            nextThumbButton: '&#9655;',
            thumbsPos: 'bottom',
            thumbCol: 4,
            thumbColPadding: 4,
            images: [
                {
                    thumb: 'MobileImage/Acer/Acer Allegro/acer-allegro.jpg',
                    medium: 'MobileImage/Acer/Acer Allegro/acer-allegro.jpg',
                    large: 'MobileImage/Acer/Acer Allegro/acer-allegro.jpg'
                }
            ]
        };
        function initialise(filter) {
            vm.filter = filter;
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";

            order("Name");
        }

        function retrieveMobiles() {
            vm.paging.pageSize = 12;
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
            vm.paging.pageSize = 12;
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
                return searchMobile(vm.searchKeyword);
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

        function requestMobile(mobileId, sellerId) {
            for (var i = 0; i < vm.mobiles.length; i++) {
                //  var result = $("#seller" + vm.mobiles[i]).is(':checked');
                vm.mobiles[i].MobileId = mobileId;
            }
            return MobileService.requestMobile(vm.mobiles, sellerId).then(function (response) {
                vm.mobiles = response.data;
                searchSeller(vm.searchKeyword);
                vm.isAssignButtonEnable = true;
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

        function onPriceFilter() {
            vm.filter = {
                IsFilter: true,
                IsPriceFilter: vm.ramSize,
                FromPrice: vm.fromPrice,
                ToPrice: vm.toPrice
            }
            retrieveMobiles();
        }

        function onRamSizeFilter() {
            vm.filter = {
                IsFilter: true,
                IsRamSizeFilter: true,
                FromRamSize: vm.fromRamSize,
                ToRamSize: vm.toRamSize
            }
            retrieveMobiles();
        }

        function onPrimaryCameraFilter() {
            vm.filter = {
                IsFilter: true,
                IsPrimaryCameraFilter: true,
                FromPrimaryCameraSize: vm.fromPrimaryCameraSize,
                ToPrimaryCameraSize: vm.toPrimaryCameraSize
            }
            retrieveMobiles();
        }

        function onSecondaryCameraFilter() {
            vm.filter = {
                IsFilter: true,
                IsSecondaryCameraFilter: true,
                FromSecondaryCameraSize: vm.fromSecondaryCameraSize,
                ToSecondaryCameraSize: vm.toSecondaryCameraSize
            }
            retrieveMobiles();
        }

        function retrieveMobileByBrandId(brandId) {
            vm.filter = { IsBrandFilter: true, BrandId: brandId, IsFilter: true }
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            order("Name");
        }

        function canWeAssign() {
            var count = 0;
            angular.forEach(vm.mobiles,
                function (value, key) {
                    if (value.Ischecked) {
                        count++;
                    }
                });
            vm.isAssignButtonEnable = (count === 0);
        }
    }
})();
