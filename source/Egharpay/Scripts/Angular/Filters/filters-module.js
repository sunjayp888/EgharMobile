(function () {
    'use strict';

    angular
        .module('Egharpay')
        .filter('filters-module', []).filter('trustAsResourceUrl',
        [
            '$sce', function($sce) {
                return function(val) {
                    return $sce.trustAsResourceUrl(val);
                };
            }
        ]);
})();