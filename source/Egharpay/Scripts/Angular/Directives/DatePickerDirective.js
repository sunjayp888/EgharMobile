/// <reference path="../../_references.js" />
(function (angular,$) {
    "use strict";

    angular.module('Egharpay').directive('datePicker', datePicker);

    function datePicker() {
        // Usage:
        //     <date-picker></date-picker>
        // Creates:
        //      a data picker
        var directive = {
            template:
                ('<span class="input-group">'
                + '<input format="yyyy-MM-dd" type="text" ng-change="change" class="form-control date-picker" ng-model="dateModel" is-open="isOpen" uib-datepicker-popup="dd MMM yyyy" ng-readonly="true" '// ng-model-options="{timezone: \'gmt\'}" '
                        + 'datepicker-options="datepickerOptions" close-text="Close" placeholder="{{ placeholder }}"/>'
                    + '<span class="input-group-btn">'
                    + '<button type="button" ng-show="dateModel && showClear" ng-click="dateModel=\'\'" class="btn btn-default"><i class="glyphicon glyphicon-remove" aria-label="Clear Date"></i></button>'
                        + '<button type="button" class="btn btn-default" ng-click="isOpen = !isOpen" ng-disabled="disabled"><i class="glyphicon glyphicon-calendar" aria-label="Select Date"></i></button>'
                    + '</span>'
                    + '</span><input type="hidden" name="{{ name }}" id="{{ id }}" />'),
            restrict: 'E',
            scope: {
                dateModel: '=?dateModel',
                weekSelectable: '=weekSelectable',
                preventKeyPress: '=preventKeyPress',
                placeholder: "@?placeholder",
                name: "@?name",
                id: "@?id",
                showClear: "=?showClear",
                value: "@?value",
                disabled: "=?disabled",
                dataValRequired: "=?dataValRequired",
                change: "=?change"
            },
            link: {
                pre: function (scope) {
                    var corrected = false;
                    scope.$watch(function () {
                        return scope.dateModel;
                    },
                    function (newValue, oldValue) {
                        if (newValue === '') {
                            scope.dateModel = null;
                        }

                        //// This code is necessary because UIB date picker does UTC "cleverness"
                        //// Move a day forward for forward timezones because angular datepicker will
                        //// adjust the day backwards 24 hours
                        //if (newValue.getHours() == 0 && !corrected && newValue.getTimezoneOffset() < 0) {
                        //    // Shift one day forward
                        //    newValue.setHours(24);
                        //    // Ensure this happens only on first load
                        //    corrected = true;
                        //}
                    });
                    scope.datepickerOptions = {
                        showWeeks: false
                    };
                    scope.required = !!scope.required;
                    scope.disabled = !!scope.disabled;
                },
                post: link
            }
        };

        var inputFormats = ["DD/MM/YYYY", "dd MMMM yyyy", "YYYY-MM-DD", "DD MMM YYYY"];        
        var outputFormat = "dd MMM yyyy";

        return directive;

        function realDate(valueEntered) {
            var asMoment = moment(valueEntered, inputFormats);
            var asDate = asMoment.toDate();
            return asDate;
        }

        function prettyDate(valueEntered) {
            var asDate = realDate(valueEntered);
            var asString = moment(asDate).format(outputFormat);

            if (!asString || !asString.match(/[0-9]/)) {
                asString = "";
            }

            return asString;
        }

        function link(scope, element, attr) {
           
            // Because the data-picker doesn't work with strings...
            if (typeof scope.dateModel == "string") {
                scope.dateModel = moment.utc(scope.dateModel).toDate();
            }

            var $hidden = $(element).find('input[type=hidden]');
            var $dateInput = $(element).find("input.date-picker");
            scope.isOpen = false;
            if (typeof attr.valRequired == "string") {
                $dateInput.attr("data-val-required", true);
            }

            $dateInput.on('blur', function () {
                var valueEntered = $(this).val();
                setInput(valueEntered);
            });

            $dateInput.on("change", function () {
                var valueEntered = $(this).val();
                setHidden(valueEntered);
            });

            function setHidden(valueEntered) {
                if ($hidden.length) {
                    var asDate = realDate(valueEntered);
                    var asString = moment(asDate).format("YYYY-MM-DD");
                    if (valueEntered.length > 0) {
                        $hidden.val(asString);
                    }
                    else {
                        //when empty string is set. it automatically flags the property as invalid
                        $hidden.val(null);
                    }
                }
            }

            function setInput(valueEntered) {
                var asString = prettyDate(valueEntered);
                $dateInput.val(asString).change();
            }

            if (scope.weekSelectable) {
                $(element).addClass('weekSelectable');
            }
            if (scope.preventKeyPress) {
                preventKeyPress(element);
            }
            if (scope.value) {
                setInput(prettyDate(scope.value));
            }
        }

        function preventKeyPress(element) {
            $(element).find('input').on('keydown', function () {
                return false;
            });
        }
    };

    $(document).on('mouseenter', '.weekSelectable tr.uib-weeks', function () {
        $(this).find('.uib-day').addClass('highlight');
        $(this).find('.uib-day > button').addClass('active');
    });

    $(document).on('mouseleave', '.weekSelectable tr.uib-weeks', function () {
        $(this).find('.uib-day').removeClass('highlight');
        $(this).find('.uib-day > button').removeClass('active');
    });
})(window.angular,$);