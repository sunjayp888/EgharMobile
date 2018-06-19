/// <reference path="../../_references.js" />
(function (angular) {
    'use strict';

    angular
        .module('Egharpay')
        .directive('dropDownDirective', dropDownDirective);

    function dropDownDirective() {
        // Usage:
        //     <drop-down-directive></drop-down-directive>
        // Creates:
        // 
        var done = false;
        var directive = {
            transclude: {
                'a': '?a'
            },
            scope: {
                ngClass: '=?ngClass'
            },
            template: '<div class="btn-group pull-right" ng-class={{ngClass}}>'
            + '<a class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown" aria-expanded="false">'
            + '<i class="fa fa-chevron-down"></i>'
            + '</a>'
            + '<ul class="dropdown-menu" style="z-Index:99999;">'
            + '<li ng-transclude="a" />'
            + '</ul>'
            + '</div>'
        };

        return directive;
    };

})(window.angular);