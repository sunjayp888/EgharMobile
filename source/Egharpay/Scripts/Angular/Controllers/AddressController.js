(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('AddressController', AddressController);

    AddressController.$inject = ['$window', 'AddressService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function AddressController($window, AddressService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.address = [];
        vm.showErrorSummary = false;
        vm.createAddress = createAddress;
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
        vm.Errors = [];
        vm.canAddNewAddress = false;
        vm.addNewAddressButtonClick = addNewAddressButtonClick;
        vm.retrievePersonnelAddress = retrievePersonnelAddress;
        vm.addresses = [];
        vm.removePersonnelAddress = removePersonnelAddress;

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
            vm.fullname="";
            vm.email="";
            vm.company="";
            vm.address1="";
            vm.address2="";
            vm.city="";
            vm.landmark="";
            vm.pincode="";
            vm.state="";
            vm.phonenumber="";
            vm.district="";
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
                  vm.addresses = response.data;
              });
        };
    }
})();
