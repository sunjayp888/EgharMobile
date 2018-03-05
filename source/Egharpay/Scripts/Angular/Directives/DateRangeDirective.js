/// <reference path="../../_references.js" />
(function (angular) {
    'use strict';

    angular
        .module('Egharpay')
        .directive('dateRange', dateRange);

    function dateRange() {
        var directive = {
            template:
                ('<div class="row ng-cloak">'
                      + '<div class="col-xs-1">'
                          + '<a class="btn btn-primary input-group pull-left" ng-click="previous(previousValue)">&laquo;</a>'
                      + '</div>'
                      + '<div class="col-xs-10">'
                    + '<div class="col-xs-12 col-sm-6 col-sm-offset-3">'
                              + '<daterange-picker model="dateModel" week-selectable="true" clearable="false"></daterange-picker>'
                          + '</div>'
                      + '</div>'
                      + '<div class="col-xs-1">'
                          + '<a class="btn btn-primary input-group pull-right" ng-click="next(nextValue)">&raquo;</a>'
                      + '</div>'
                  + '</div>'),
            restrict: 'E',
            scope: {
                previous: '=previous',
                next: '=next',
                dateModel: '=dateModel'
            }
        };
        return directive;
    };

})(window.angular);