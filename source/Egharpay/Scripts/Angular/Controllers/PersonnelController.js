(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('PersonnelController', PersonnelController);

    PersonnelController.$inject = ['$window', 'PersonnelService', 'AddressService', 'SellerOrderService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function PersonnelController($window, PersonnelService, AddressService, SellerOrderService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.personnel = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.viewPersonnelProfile = viewPersonnelProfile;
        vm.searchPersonnel = searchPersonnel;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        vm.openPersonnelShippingAddressModal = openPersonnelShippingAddressModal;
        vm.createAddress = createAddress;
        vm.retrievePersonnelAddress = retrievePersonnelAddress;
        vm.personnelAddress = [];
        vm.initialise = initialise;
        vm.selectedShippingAddress = selectedShippingAddress;
        vm.onSelectAddress = onSelectAddress;

        function initialise() {
            order("Forenames");
        }

        function retrievePersonnel() {
            return PersonnelService.retrievePersonnel(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.personnel = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.personnel.length === 0 ? "No Records Found" : "";
                    return vm.personnel;
                });
        }

        function searchPersonnel(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return PersonnelService.searchPersonnel(vm.searchKeyword, vm.paging, vm.orderBy)
              .then(function (response) {
                  vm.personnel = response.data.Items;
                  vm.paging.totalPages = response.data.TotalPages;
                  vm.paging.totalResults = response.data.TotalResults;
                  vm.searchMessage = vm.personnel.length === 0 ? "No Records Found" : "";
                  return vm.personnel;
              });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchPersonnel(vm.searchKeyword)();
            }
            return retrievePersonnel();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchPersonnel(vm.searchKeyword)();
            }
            return retrievePersonnel();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function viewPersonnelProfile(personnelId) {
            $window.location.href = "/Personnel/Profile/" + personnelId;
        }

        function openPersonnelShippingAddressModal() {
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
            angular.element('#addressModal').modal('toggle');
        }

        function createAddress(personnelId) {
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
                District: vm.district,
                PersonnelId: personnelId
            }
            return AddressService.createAddress(address)
                .then(function (response) {
                    if (response.data === '' || response.data.Succeeded === true) {
                        vm.canAddNewAddress = false;
                        retrievePersonnelAddress(personnelId);
                        angular.element('#addressModal').modal('toggle');
                    } else {
                        $('#projectErrorSummary').show();
                        vm.showErrorSummary = true;
                        vm.Errors = response.data;
                    }
                });
        }

        function retrievePersonnelAddress(personnelId, shippingAddressId) {
            return AddressService.retrievePersonnelAddress(personnelId)
                .then(function (response) {
                    vm.personnelAddress = response.data;
                    selectedShippingAddress(shippingAddressId);
                });
        }

        function selectedShippingAddress(shippingAddressId) {
            if (shippingAddressId === null || shippingAddressId === 0)
                return;
            angular.forEach(vm.personnelAddress, function (address, index) {
                if (address.AddressId === shippingAddressId)
                    address.IsChecked = true;
            });
        };


        function onSelectAddress(orderId, selectedIndex, list) {
            vm.selectedShippingAddressId = list[selectedIndex].AddressId;
            angular.forEach(list, function (address, index) {
                if (selectedIndex !== index)
                    address.IsChecked = false;
            });
            return SellerOrderService.updateShippingAddress(orderId, vm.selectedShippingAddressId).then(function (response) {
            });
        };
    }
})();
