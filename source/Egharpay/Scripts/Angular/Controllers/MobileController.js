(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileController', MobileController);


    MobileController.$inject = ['$window', 'MobileService', 'AddressService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileController($window, MobileService, AddressService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.mobiles = [];
        vm.sellers = [];
        vm.selectedSellers = [];
        vm.requestMobiles = [];
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
        vm.requestOrder = requestOrder;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.currentAddress;
        vm.Address = [];
        vm.addresses = [];
        vm.address = [];
        vm.Errors = [];
        vm.addresses = [];
        vm.PinCode;
        vm.modalInstance = null;
        var country, state, city, pinCode, map, latitude, longitude, count, pin;
        vm.filter;
        vm.fromPrice;
        vm.toPrice;
        vm.fromRamSize;
        vm.toRamSize;
        vm.fromPrimaryCameraSize;
        vm.toPrimaryCameraSize;
        vm.fromSecondaryCameraSize;
        vm.toSecondaryCameraSize;
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
        vm.fullname;
        vm.email;
        vm.company;
        vm.address1;
        vm.address2;
        vm.city;
        vm.landmark;
        vm.pincode;
        vm.state;
        vm.phonenumber;
        vm.district;
        vm.selectedShippingAddressId;
        vm.onPriceFilter = onPriceFilter;
        vm.onRamSizeFilter = onRamSizeFilter;
        vm.onPrimaryCameraFilter = onPrimaryCameraFilter;
        vm.onSecondaryCameraFilter = onSecondaryCameraFilter;
        vm.searchMobile = searchMobile;
        vm.isAssignButtonEnable = true;
        vm.canWeAssign = canWeAssign;
        vm.compareMobile = compareMobile;
        vm.showErrorSummary = false;
        vm.createAddress = createAddress;
        vm.canAddNewAddress = false;
        vm.addNewAddressButtonClick = addNewAddressButtonClick;
        vm.retrievePersonnelAddress = retrievePersonnelAddress;
        vm.removePersonnelAddress = removePersonnelAddress;
        vm.onSelectAddress = onSelectAddress;
        vm.retrieveMobileByBrandId = retrieveMobileByBrandId;
        vm.initialise = initialise;
        vm.retrieveMobilesInStore = retrieveMobilesInStore;
        vm.placeOrder = placeOrder;
        vm.selectedShippingAddressId;

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
                    vm.sellers = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.sellers.length === 0 ? "No Records Found" : "";
                    return vm.sellers;
                });
        }

        function requestOrder(mobileId) {
            vm.mobileId = mobileId;
            retrievePersonnelAddress();
        }

        function placeOrder() {
            return MobileService.requestOrder(vm.mobileId, vm.selectedSellers, vm.selectedShippingAddressId).then(function (response) {
                vm.mobiles = response.data;
                searchSeller(vm.searchKeyword);
                vm.isAssignButtonEnable = true;
            });
        }

        //function requestMobile(mobileId) {
        //    return AddressService.retrievePersonnelAddress()
        //         .then(function (response) {
        //             vm.addresses = response.data;
        //         });
        //}

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
            var selectedSellers = vm.selectedSellers;
            var count = 0;
            angular.forEach(vm.sellers,
                function (value, key) {
                    if (value.Ischecked) {
                        count++;
                    }
                });
            vm.isAssignButtonEnable = (selectedSellers.length === 0);
        }

        function compareMobile(brandId, mobileId) {
            window.location.href = "/Mobile/Compare/" + brandId + "/" + mobileId;
        }

        function createAddress() {
            var address = {
                FullName: vm.fullname,
                Email: vm.email,
                Company: vm.company,
                Address1: vm.address1,
                Address2: vm.address2,
                City: vm.city,
                Landmark: vm.landmark,
                ZipPostalCode: vm.pincode,
                StateId: 1,
                PhoneNumber: vm.phonenumber,
                District: vm.district
            }
            return AddressService.createAddress(address)
                .then(function (response) {
                    if (response.data === '' || response.data.Succeeded === true) {
                        vm.canAddNewAddress = false;
                        retrievePersonnelAddress();
                    } else {
                        $('#projectErrorSummary').show();
                        vm.showErrorSummary = true;
                        vm.Errors = response.data;

                    }
                });
        }

        function addNewAddressButtonClick() {
            vm.fullname = "";
            vm.email = "";
            vm.company = "";
            vm.address1 = "";
            vm.address2 = "";
            vm.city = "";
            vm.landmark = "";
            vm.pincode = "";
            vm.state = "";
            vm.phonenumber = "";
            vm.district = "";
            vm.canAddNewAddress = !vm.canAddNewAddress;
        }

        function retrievePersonnelAddress() {
            return AddressService.retrievePersonnelAddress()
                .then(function (response) {
                    vm.addresses = response.data;
                });
        }

        function removePersonnelAddress(addressId) {
            return AddressService.removePersonnelAddress(addressId)
              .then(function (response) {
                  retrievePersonnelAddress();
              });
        };

        function onSelectAddress(selectedIndex, list) {
            vm.selectedShippingAddressId = list[selectedIndex].AddressId;
            angular.forEach(list, function (address, index) {
                if (selectedIndex !== index)
                    address.IsChecked = false;
            });
        };
    }
})();
