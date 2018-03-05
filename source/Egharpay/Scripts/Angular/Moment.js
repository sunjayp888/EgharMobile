(function () {
    'use strict';

    angular
        .module('moment-module', [])
        .factory('moment', moment);

    moment.$inject = ['$window'];

    function moment($window) {
        $window.moment.locale("en-gb");
        return $window.moment;
    }
})();