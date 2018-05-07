(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileController', MobileController)
        .config(['usSpinnerConfigProvider', function (usSpinnerConfigProvider) {
            usSpinnerConfigProvider.setTheme('smallRed', { color: 'red', radius: 6 });
        }]);


    MobileController.$inject = ['$window', 'GoogleService', 'usSpinnerService', 'MobileService', 'AddressService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$location'];

    function MobileController($window, GoogleService, usSpinnerService, MobileService, AddressService, Paging, OrderService, OrderBy, Order, $location) {
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
        vm.country;
        vm.state,
        vm.city,
        vm.pinCode,
        vm.map,
        vm.latitude,
        vm.longitude,
        vm.count,
        vm.pin;
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
        vm.retrieveSellersFromGeoLocation = retrieveSellersFromGeoLocation;
        vm.latitude;
        vm.longitude;

        vm.removePersonnelAddress = removePersonnelAddress;
        vm.onSelectAddress = onSelectAddress;
        vm.retrieveMobileByBrandId = retrieveMobileByBrandId;
        vm.initialise = initialise;
        vm.retrieveMobilesInStore = retrieveMobilesInStore;
        vm.placeOrder = placeOrder;
        vm.selectedShippingAddressId;
        vm.sellerMobileOrder = sellerMobileOrder;
        vm.sellerMobileOrderClass = sellerMobileOrderClass;
        vm.isOrderPlacedSuccess = false;
        var count, country, state, city, pin, pinCode;

        function initialise(filter) {
            vm.filter = filter;

            //vm.orderBy.property = "Name";
            //vm.orderBy.direction = "Ascending";
            //vm.orderBy.class = "asc";
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
            usSpinnerService.spin('spinner-1');
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
                    usSpinnerService.stop('spinner-1');
                    return vm.mobiles;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchMobile(vm.searchKeyword);
            }
            return retrieveMobiles();
        }

        function sellerMobileOrder(property) {
            property = property === undefined ? "SellerDistance" : property;
            vm.orderBy = OrderService.order(vm.orderBy, property);
            //if (vm.searchKeyword) {
            //    return searchMobile(vm.searchKeyword)();
            //}
            return geoLocation();
        }

        function sellerMobileOrderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
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
            if (vm.Address.PinCode !== undefined) {
                $("#txtSearchPincode").val(vm.Address.PinCode);
                vm.searchKeyword = vm.Address.PinCode;
                usSpinnerService.stop('locationSpinner');
            }
        }

        function geoLocation() {
            if ("geolocation" in navigator) {
                usSpinnerService.spin('locationSpinner');
                navigator.geolocation.getCurrentPosition(function (position) {
                    vm.latitude = position.coords.latitude;
                    vm.longitude = position.coords.longitude;
                    GoogleService.getLocation(vm.latitude, vm.longitude).then(function (response) {
                        var data = response.data;
                        if (data.results.length > 0) {
                            usSpinnerService.stop('locationSpinner');
                            var locationDetails = data.results[0].formatted_address;
                            var value = locationDetails.split(",");
                            count = value.length;
                            vm.country = value[count - 1];
                            vm.state = value[count - 2].trim().split(" ")[0];
                            vm.city = value[count - 3];
                            vm.pinCode = value[count - 2].trim().split(" ")[1];
                            vm.currentAddress = locationDetails;
                            vm.Address = { City: city, State: state, Country: country, PinCode: vm.pinCode }
                            retrieveSellersFromGeoLocation();
                        }
                        else {
                            vm.Address = { Error: "No location available for provided details." }
                        }
                    });
                });
            } else {
                console.log("Browser doesn't support geolocation!");
            }
        }

        function searchSeller(searchKeyword) {
            vm.searchKeyword = searchKeyword == undefined ? vm.searchKeyword : searchKeyword;
            //vm.orderBy.property = "Name";
            //vm.orderBy.direction = "Ascending";
            //vm.orderBy.class = "asc";
            vm.pinCode = vm.searchKeyword;
            retrieveSellersFromGeoLocation();
            //return MobileService.searchSeller(vm.searchKeyword, vm.paging, vm.orderBy)
            //    .then(function (response) {
            //        vm.sellers = response.data.Items;
            //        vm.paging.totalPages = response.data.TotalPages;
            //        vm.paging.totalResults = response.data.TotalResults;
            //        vm.searchMessage = vm.sellers.length === 0 ? "No Records Found" : "";
            //        return vm.sellers;
            //    });
        }

        function requestOrder(mobileId, sellerId, isLoggedin) {
            vm.mobileId = mobileId;
            vm.sellerId = sellerId;
            if (!isLoggedin) {
                window.location.href = "/Account/login?returnUrl=" + window.location.pathname;
            } else {
                $('#addressModal').modal('show');
            }
            retrievePersonnelAddress();
        }

        function placeOrder() {
            return MobileService.requestOrder(vm.mobileId, vm.sellerId, vm.selectedShippingAddressId).then(function (response) {
                vm.isOrderPlacedSuccess = response.data.Succeeded;
                $('#addressModal').modal('hide');
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
                IsPriceFilter: true,
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

        function retrieveGeoCoordinates() {
            return MobileService.retrieveGeoCoordinates()
               .then(function (response) {
                   vm.latitude = response.data.Latitude;
                   vm.longitude = response.data.Longitude;
                   if (vm.latitude === 0.0 || vm.longitude === 0.0) {
                       geoLocation();
                   }
               });
        }

        function retrieveSellersFromGeoLocation() {
            return MobileService.retrieveSellersFromGeoLocation(vm.pinCode, vm.latitude, vm.longitude, vm.paging, vm.orderBy)
                           .then(function (response) {
                               vm.sellers = response.data.Items;
                               vm.paging.totalPages = response.data.TotalPages;
                               vm.paging.totalResults = response.data.TotalResults;
                               vm.searchMessage = vm.sellers.length === 0 ? "No Records Found" : "";
                               usSpinnerService.stop('locationSpinner');
                               return vm.sellers;
                           });
        }

        function createAddress() {
            vm.errorMessages = [];
            if (!vm.fullname) vm.errorMessages.push('Fullname is required.');
            if (!vm.address1) vm.errorMessages.push('Address1 is required.');
            if (!vm.address2) vm.errorMessages.push('Address2 is required.');
            if (!vm.district) vm.errorMessages.push('District is required.');
            if (!vm.pincode) vm.errorMessages.push('Pincode is required.');
            if (!vm.city) vm.errorMessages.push('City is required.');
            if (vm.errorMessages.length > 0) return;

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
            vm.errorMessages = [];
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
