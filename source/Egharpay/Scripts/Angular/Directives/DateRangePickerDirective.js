(function (angular, jQuery) {
    'use strict';

    angular
        .module('Egharpay')
        .directive('daterangePicker', DateRangePickerDirective);

    DateRangePickerDirective.$inject = ['$compile', '$timeout', 'moment'];

    function DateRangePickerDirective($compile, $timeout, moment) {
        // Usage:
        //     <daterange-picker></daterange-picker>
        // Creates:
        // 
        var format = "DD MMM YYYY";
        var directive = {
            template: '<div>' +
            '<input date-range-picker ng-disabled="model.ngDisabled" type="text" placeholder="{{model.placeholder}}" show-hide="model.showHide" class="form-control date-picker" clearable="model.clearable" max="model.max" min="model.min" options="model.options" ng-model="model.model" ng-change="model.changed()" ng-model-options="{ debounce: 1250 }"></input>' +
            '<span class="fa fa-calendar daterangepickerdirective" style="cursor:pointer;" ng-disabled="model.ngDisabled" ng-click="!model.ngDisabled && model.toggleShowHide();"></span>' +
            '<input type="hidden" name="{{model.name}}" value="{{model.modelString}}" id="{{id}}" />' +
            '</div>',
            terminal: true,
            priority: 1000,
            restrict: 'E',
            bindToController: {
                model: '=?',
                onChange: "&?",
                modelString: '=?',
                min: '=?',
                max: '=?',
                clearable: '=?',
                placeholder: '@?',
                name: '@?',
                id: '@?',
                autoUpdate: '=?',
                dataValRequired: '@?',
                dataVal: '=?',
                ngDisabled: '=?',
                value: '@?',
                weekSelectable: '=?'

            },
            scope: {},
            controller: controller,
            controllerAs: 'model',
            compile: function (element, attrs) {
                var pickerInput = angular.element(element.find('input[type=text]')[0]);

                if (attrs.required) pickerInput.attr('ng-required', true);
                if (attrs.val) pickerInput.attr('data-val', attrs.val);
                if (attrs.valRequired || attrs.valRequired === '') pickerInput.attr('data-val-required', attrs.valRequired);

                return {
                    pre: preLink,
                    post: postLink
                }

            }

        };

        return directive;

        function preLink($scope, element, attrs) {

            var scope = $scope.model;
            if (scope.value) scope.model = scope.value;
            if (scope.weekSelectable) {
                setHighlight();
            }
            var hiddenInput = angular.element(element.find('input[type=hidden]'));

            scope.changed = function () {
                var input = jQuery(element.find('input[type=text]')[0]),
                    picker = input.data('daterangepicker'),
                    date = moment(picker.element.val(), ["DD/MM/YYYY", "D MMMM YYYY", "DD MMMM YYYY", "dd MMM yyyy", picker.locale.format]),
                    dateParsed = date.format(picker.locale.format);

                picker.setStartDate(dateParsed);
                input.trigger('apply.daterangepicker', picker);
            };

            if (!scope.options) {
                scope.options = {
                    locale: { format: format },
                    autoApply: true,
                    singleDatePicker: true,
                    showDropdowns: true,
                    autoUpdateInput: false,
                    clearLabel: 'Clear',
                    eventHandlers: {
                        'apply.daterangepicker': function (ev, picker) {
                            picker.element.val(picker.startDate.format(picker.locale.format));
                            //fire the onchange
                            $timeout(function () {
                                if (typeof scope.onChange === 'function') {
                                    scope.onChange();
                                }
                            }, 0);
                        }
                    }
                }
            }
            if (scope.options && scope.clearable) {
                scope.options.eventHandlers['cancel.daterangepicker'] = function (pickerScope, picker) {

                    scope.model = null;
                    //fire the onchange
                    $timeout(function () {
                        if (typeof scope.onChange === 'function') {
                            scope.onChange();
                        }
                    }, 0);

                }
                scope.options.showClearButton = true;
            }

            if (scope.options && scope.autoUpdate) {
                scope.options.autoUpdateInput = scope.autoUpdate;
            }

        }

        function postLink($scope, element, attrs) {

            var scope = $scope.model;
            $scope.$watch(function () { return scope.model; }, function () { setDefaults(scope); });
            return $compile(element.contents())($scope);
        }

        function setDefaults(currentScope) {

            if (currentScope.model === null || currentScope.model === undefined) {
                currentScope.modelString = null;
                return;
            }

            currentScope.modelString = currentScope.model.format(format).toString();
        }

        function setHighlight() {
            jQuery(function () {
                jQuery('.daterangepicker').addClass('weekSelectable');
            });

            jQuery(document)
                .on('mouseenter', '.weekSelectable .table-condensed > tbody > tr', function () {
                    $(this).addClass('highlight');
                })
                .on('mouseleave', '.weekSelectable .table-condensed > tbody > tr', function () {
                    $(this).removeClass('highlight');
                });
        }

        function controller() {

            var model = this;
            model.showHide = false;
            model.toggleShowHide = function () {
                model.showHide = !model.showHide;

            }

        }
    }

})(window.angular, $);