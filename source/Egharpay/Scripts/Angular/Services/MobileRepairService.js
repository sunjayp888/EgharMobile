﻿(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('MobileRepairService', MobileRepairService);

    MobileRepairService.$inject = ['$http'];

    function MobileRepairService($http) {
        var service = {
            createMobileRepairRequest: createMobileRepairRequest,
            retrieveMobileRepairOrders: retrieveMobileRepairOrders,
            retrieveMobileRepairOrdersByMobile: retrieveMobileRepairOrdersByMobile,
            markAsCompleted: markAsCompleted,
            markAsCancelled: markAsCancelled,
            deleteMobileRepairRequest: deleteMobileRepairRequest,
            createMobileRepairPayment: createMobileRepairPayment,
            retrieveAvailableMobileRepairAdmin: retrieveAvailableMobileRepairAdmin,
            retrieveMobileRepairAdmins: retrieveMobileRepairAdmins,
            markAsInProgress: markAsInProgress,
            searchMobileRepairByDate:searchMobileRepairByDate
        };

        return service;

        function createMobileRepairRequest(model) {
            var url = "/MobileRepair/Create",
                data = {
                    model: model
                };
            return $http.post(url, data);
        }

        //function retrieveMobileRepairOrders() {
        //    var url = "/MobileRepair/RetrieveMobileRepairOrders/" + mobileNumber;
        //    return $http.get(url, data);
        //}

        function retrieveMobileRepairOrdersByMobile(mobileNumber, otp) {
            var url = "/MobileRepair/RetrieveMobileRepairOrdersByMobile/" + mobileNumber + "/" + otp;
            return $http.get(url);
        }

        function deleteMobileRepairRequest(mobileRepairId, mobileNumber, otp) {
            var url = "/MobileRepair/DeleteMobileRepairRequest",
                 data = {
                     mobileRepairId: mobileRepairId,
                     mobileNumber: mobileNumber,
                     otp: otp
                 };
            return $http.post(url, data);
        }

        function retrieveMobileRepairOrders(Paging, OrderBy) {
            var url = "/MobileRepair/List",
            data = {
                paging: Paging,
                orderBy: new Array(OrderBy)
            };
            return $http.post(url, data);
        }

        function markAsCompleted(mobileRepairId) {
            var url = "/MobileRepair/MarkAsCompleted",
            data = {
                mobileRepairId: mobileRepairId
            };
            return $http.post(url, data);
        }

        function markAsCancelled(mobileRepairId) {
            var url = "/MobileRepair/MarkAsCancelled",
            data = {
                mobileRepairId: mobileRepairId
            };
            return $http.post(url, data);
        }

        function markAsInProgress(mobileRepairId) {
            var url = "/MobileRepair/MarkAsInProgress",
            data = {
                mobileRepairId: mobileRepairId
            };
            return $http.post(url, data);
        }

        function createMobileRepairPayment(model) {
            var url = "/MobileRepair/CreateMobileRepairPayment",
                data = {
                    model: model
                };
            return $http.post(url, data);
        }

        function retrieveAvailableMobileRepairAdmin(date, time) {
            var url = "/MobileRepair/RetrieveAvailableMobileRepairAdmin",
            data = {
                date: date,
                time: time
            }

            return $http.post(url, data);
        }

        function retrieveMobileRepairAdmins() {
            var url = "/MobileRepair/RetrieveMobileRepairAdmins";
            return $http.get(url);
        }

        function searchMobileRepairByDate(FromDate, ToDate, Paging, OrderBy) {
            var url = "/MobileRepair/SearchByDate",
            data = {
                fromDate: FromDate,
                toDate: ToDate,
                paging: Paging,
                orderBy: new Array(OrderBy)
            };

            return $http.post(url, data);
        }
    }
})();