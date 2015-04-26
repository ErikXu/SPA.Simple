define(['plugins/router', 'jquery', 'knockout'], function (router, $, ko) {

    var defaultRoutes = [
       { route: '', title: 'Welcome', moduleId: 'viewmodels/welcome' }
    ];

    var isAjaxing = ko.observable(false);
    $(document).ajaxStart(function () { isAjaxing(true); })
               .ajaxStop(function () { isAjaxing(false); });

    var vm = {
        router: router,
        isLoading: ko.computed(function () {
            return (router.isNavigating() || isAjaxing());
        }),
        activate: function () {
            router.map(defaultRoutes).buildNavigationModel();
            return router.activate();
        }
    };

    return vm;
});