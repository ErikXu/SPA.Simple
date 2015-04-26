define(['plugins/http'], function (http) {
    // ReSharper disable once InconsistentNaming
    var URL = 'api/stock/';

    return {
        list: function (filter) {
            return http.get(URL + 'list', filter);
        }
    };
});