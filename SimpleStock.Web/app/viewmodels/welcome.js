define(['data/stock-api', 'knockout', 'knockout-mapping', 'bootstrap', 'shared/pager'], function (stockApi, ko, mapping) {
    ko.mapping = mapping;
    var list = ko.observableArray([]).extend({
        datasource: {
            dataLoader: function () {
                var self = this;
                return stockApi.list(ko.mapping.toJS(self.filter)).then(function (response) {
                    if (response.data.length === 0 && self.pager.page() !== 1) {
                        self.pager.page(1);
                    } else {
                        list(response.data);
                        self.pager.totalCount(response.pager.totalCount);
                    }
                });
            }
        }
    });
    var vm = {
        list: list,
        activate: function () {
            list.load();
        }
    };

    return vm;
});