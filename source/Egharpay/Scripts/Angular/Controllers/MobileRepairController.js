(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('MobileRepairController', MobileRepairController);

    MobileRepairController.$inject = ['$window', '$filter', '$sce', 'MobileRepairService', 'BrandService', 'OTPService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function MobileRepairController($window, $filter, $sce, MobileRepairService, BrandService, OTPService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.errorMessages = [];
        vm.isOtpCreated = false;
        vm.mobileNumber;
        vm.createMobileRepairOtp = createMobileRepairOtp;
        vm.createMobileRepairRequest = createMobileRepairRequest;
        vm.errorMessages = [];
        vm.OTP;
        vm.modelName;
        vm.description;
        vm.couponCode;
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.retrieveMobileRepairOrdersByMobile = retrieveMobileRepairOrdersByMobile;
        vm.createMobileRepairManageOrderOtp = createMobileRepairManageOrderOtp;
        vm.retrieveMobileRepairOrders = retrieveMobileRepairOrders;
        vm.deleteMobileRepairRequest = deleteMobileRepairRequest;
        vm.markAsCompleted = markAsCompleted;
        vm.markAsCancelled = markAsCancelled;
        vm.createMobileRepairPaymentOtp = createMobileRepairPaymentOtp;
        vm.createMobileRepairPayment = createMobileRepairPayment;
        vm.isRepair = true;
        vm.mobileRepairOrders = [];
        vm.mobileFaults = [];
        vm.brands = [];
        vm.partners = [];
        vm.selectedMobileFaults = [];
        vm.initialise = initialise;
        vm.amount;
        vm.disablePay = true;
        vm.AppointmentDate = null;
        vm.appointmentTime;
        vm.otpInputChanged = otpInputChanged;
        vm.retrieveAvailableMobileRepairAdmin = retrieveAvailableMobileRepairAdmin;
        vm.mobileRepairAdmins = [];
        vm.selectedMobileRepairAdmin = selectMobileRepairAdmin;
        vm.selectedMobileRepairAdmin = null;
        vm.clockChange = clockChange;
        vm.retrieveMobileRepairAdmins = retrieveMobileRepairAdmins;
        vm.markAsInProgress = markAsInProgress;
        //vm.mobileRepairState = mobileRepairState;
        vm.searchMobileRepairByDate = searchMobileRepairByDate;
        vm.search = search;
        vm.fromDate;
        vm.toDate;
        vm.retrieveMobileRepairFaults = retrieveMobileRepairFaults;
        vm.deleteMobileRepairMobileFault = deleteMobileRepairMobileFault;
        vm.initialise = initialise;
        vm.searchTerm;
        vm.searchBrand = searchBrand;
        vm.brandName;
        vm.brandSelected = brandSelected;
        vm.mobiles = [];
        vm.brandId;
        vm.calculateRepairFee = calculateRepairFee;
        vm.mobileFees = [];
        vm.mobileFeesSelected = [];
        vm.addPrice = addPrice;
        vm.TotalPrice = 0;
        vm.showFeeMessage = false;
        vm.partnerName;
        vm.partnerMobile;
        vm.partnerEmailId;
        vm.partnerComment;
        vm.partnerFollowupDate;
        vm.createPartner = createPartner;
        vm.retrievePartners = retrievePartners;
        vm.showEnquiryCreated = false;

        function initialise() {
            vm.orderBy.property = "CreatedDateTime";
            vm.orderBy.direction = "Descending";
            vm.orderBy.class = "desc";
            order("MobileRepairId");
        }

        $('.clockpicker').clockpicker({ afterDone: retrieveAvailableMobileRepairAdmin });

        function retrieveMobileRepairPersonnel() {
            retrieveMobileRepairAdmins(vm.AppointmentDate, vm.appointmentTime);
        }

        function clockChange() {
            alert(vm.appointmentTime);
        };

        function createMobileRepairOtp() {
            vm.showMessage = false;
            vm.isRepair = true;
            vm.errorMessages = [];
            vm.OTP = "";
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (vm.errorMessages.length > 0) return;
            return OTPService.createMobileRepairOtp(vm.mobileNumber).then(function (response) {
                vm.showMessage = true;
                vm.isOtpCreated = response.data.Succeeded;
                vm.errorMessages.push(response.data.Message);
            });
        }

        function createMobileRepairPaymentOtp() {
            vm.errorMessages = [];
            if (!vm.amount) vm.errorMessages.push('Enter amount.');
            if (vm.errorMessages.length > 0) return;
            return OTPService.createMobileRepairPaymentOtp(vm.mobileNumber, vm.amount).then(function (response) {
                vm.showMessage = true;
                vm.isOtpCreated = response.data.Succeeded;
                vm.errorMessages.push(response.data.Message);
            });
        }

        function createMobileRepairRequest() {
            vm.showMessage = false;
            vm.errorMessages = [];
            var model = {
                MobileNumber: vm.mobileNumber,
                ModelName: vm.modelName,
                Description: vm.description,
                CouponCode: vm.couponCode,
                OTP: vm.OTP
            }
            return MobileRepairService.createMobileRepairRequest(model).then(function (response) {
                vm.showMessage = true;
                vm.errorMessages.push(response.data.Message);
            });
        }

        function createMobileRepairManageOrderOtp() {
            vm.showMessage = false;
            vm.errorMessages = [];
            vm.OTP = "";
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (vm.errorMessages.length > 0) return;
            return OTPService.createMobileRepairOtp(vm.mobileNumber).then(function (response) {
                vm.showMessage = true;
                vm.isRepair = false;
                vm.isOtpCreated = response.data.Succeeded;
                vm.errorMessages.push(response.data.Message);
            });
        }

        function retrieveMobileRepairOrders() {
            return MobileRepairService.retrieveMobileRepairOrders(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileRepairOrders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileRepairOrders.length === 0 ? "No Records Found" : "";
                    return vm.mobileRepairOrders;
                });
        }

        function pageChanged() {
            return retrieveMobileRepairOrders();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveMobileRepairOrders();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function markAsInProgress(mobileRepairId) {
            return MobileRepairService.markAsInProgress(mobileRepairId).then(function (response) {
                retrieveMobileRepairOrders();
            });
        }

        function markAsCancelled(mobileRepairId) {
            return MobileRepairService.markAsCancelled(mobileRepairId).then(function (response) {
                retrieveMobileRepairOrders();
            });
        }

        function markAsCompleted(mobileRepairId, mobileNumber) {
            vm.amount = "";
            vm.mobileRepairId = mobileRepairId;
            vm.mobileNumber = mobileNumber;
            vm.errorMessages = [];
            //return MobileRepairService.markAsCompleted(mobileRepairId).then(function (response) {
            //    retrieveMobileRepairOrders();
            //});
        }

        function retrieveMobileRepairOrdersByMobile() {
            vm.errorMessages = [];
            if (!vm.mobileNumber) vm.errorMessages.push('Enter mobile number.');
            if (!vm.OTP) vm.errorMessages.push('Enter OTP.');
            if (vm.errorMessages.length > 0) return;
            return MobileRepairService.retrieveMobileRepairOrdersByMobile(vm.mobileNumber, vm.OTP).then(function (response) {
                if (!response.data.Succeeded && response.data.Succeeded !== undefined) {
                    vm.errorMessages.push(response.data.Message);
                } else {
                    vm.mobileRepairOrders = response.data;
                }
            });
        }

        function deleteMobileRepairRequest(mobileRepairId) {
            return MobileRepairService.deleteMobileRepairRequest(mobileRepairId, vm.mobileNumber, vm.OTP).then(function (response) {
                retrieveMobileRepairOrdersByMobile();
            });
        }

        function createMobileRepairPayment() {
            vm.disablePay = true;
            vm.errorMessages = [];
            var model = { Amount: vm.amount, OTP: vm.OTP, MobileRepairId: vm.mobileRepairId, MobileNumber: vm.mobileNumber }
            return MobileRepairService.createMobileRepairPayment(model).then(function (response) {
                vm.showMessage = true;
                vm.isOtpCreated = response.data.Succeeded;
                vm.errorMessages.push(response.data.Message);
                if (response.data.Succeeded) {
                    retrieveMobileRepairOrders();
                    angular.element('#mobileRepairPaymentModal').modal('toggle');
                }
            });
        }

        function otpInputChanged() {
            if (vm.OTP.length === 6) {
                vm.disablePay = false;
            } else {
                vm.disablePay = true;
            }
        }

        function retrieveAvailableMobileRepairAdmin() {
            var appointmentTime = $('#MobileRepair_AppointmentTime').val();
            return MobileRepairService.retrieveAvailableMobileRepairAdmin(vm.AppointmentDate, appointmentTime).then(function (response) {
                vm.mobileRepairAdmins = response.data;
                selectMobileRepairAdmin();
            });
        }

        function retrieveMobileRepairAdmins() {
            retrieveMobileRepairFaults();
            return MobileRepairService.retrieveMobileRepairAdmins().then(function (response) {
                vm.mobileRepairAdmins = response.data;
                selectMobileRepairAdmin();
            });
        }

        function selectMobileRepairAdmin() {
            if (vm.mobileRepairAdmins.length > 0) {
                var admin = $filter('filter')(vm.mobileRepairAdmins,
                    { PersonnelId: vm.selectedMobileRepairAdmin.PersonnelId }, true);

                if (admin.length > 0) vm.selectedMobileRepairAdmin = admin[0];
                //else vm.selectedAssignment = vm.assignments[0];
            }
        }

        function searchMobileRepairByDate() {
            vm.searchKeyword = null;
            return MobileRepairService.searchMobileRepairByDate(vm.fromDate, vm.toDate, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileRepairOrders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileRepairOrders.length === 0 ? "No Records Found" : "";
                    return vm.mobileRepairOrders;
                });
        }


        function retrieveMobileRepairFaults() {
            vm.orderBy.property = "MobileFaultId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return MobileRepairService.retrieveMobileRepairFaults(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileFaults = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileFaults.length === 0 ? "No Records Found" : "";
                    return vm.mobileFaults;
                });
        }

        function search() {
            return MobileRepairService.search(vm.searchTerm, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.mobileRepairOrders = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.mobileRepairOrders.length === 0 ? "No Records Found" : "";
                    return vm.mobileRepairOrders;
                });
        }

        function searchBrand() {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BrandService.searchBrand(vm.searchTerm, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                });
        }

        function retrieveMobiles(brandId) {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return BrandService.searchBrand(vm.searchTerm, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.brands = response.data.Items;
                });
        }

        function deleteMobileRepairMobileFault(mobileRepairId, $item) {
            return MobileRepairService.deleteMobileRepairMobileFault(mobileRepairId, $item.MobileFaultId)
                .then(function () {

                });
        }

        function brandSelected(item) {
            vm.BrandId = item.BrandId;
            return MobileRepairService.retrieveMobileByBrand(vm.BrandId)
                .then(function (response) {
                    vm.mobiles = response.data.Items;
                });
        }

        function calculateRepairFee(item) {
            $('.full-height-scroll').slimscroll({
                height: '200px'
            })
            return MobileRepairService.calculateRepairFee(vm.BrandId, item.MobileId)
                .then(function (response) {
                    vm.mobileFees = response.data;
                    vm.showFeeMessage = response.data.length == 0;
                });
        }

        function addPrice(value, checked) {
            var idx = vm.mobileFees.indexOf(value);
            if (idx >= 0 && !checked) {
                vm.mobileFeesSelected.splice(idx, 1);
                vm.TotalPrice = vm.TotalPrice - value.Amount;
            }
            if (idx < 0 && checked) {
                vm.mobileFeesSelected.push(value);
            }
            if (checked) {
                vm.TotalPrice = value.Amount + vm.TotalPrice;
            }

        }

        function createPartner() {
            vm.errorMessages = [];
            if (!vm.partnerName) vm.errorMessages.push('Enter Name.');
            if (!vm.partnerMobile) vm.errorMessages.push('Enter Mobile.');
            if (!vm.partnerEmailId) vm.errorMessages.push('Enter email.');
            if (vm.errorMessages.length > 0) return;
            var partner = {
                Name: vm.partnerName,
                Mobile: vm.partnerMobile,
                EmailId: vm.partnerEmailId,
                Comment: vm.partnerComment
            }
            return MobileRepairService.createPartner(partner)
                .then(function (response) {
                    if (response.data.Succeeded) {
                        vm.showEnquiryCreated = true;
                    }
                });
        }

        function retrievePartners() {
            vm.orderBy.property = "CreatedDate";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            return MobileRepairService.retrievePartners(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.partners = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.partners.length === 0 ? "No Records Found" : "";
                    return vm.partners;
                });
        }
    }

})();
//sellerbymobileid  mobile service