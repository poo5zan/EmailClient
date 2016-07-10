var commonModule = angular.module('common', ['ngRoute', 'ui.bootstrap']);

//non-SPA views will use Angular controllers created on the appMainModule
var appMainModule = angular.module('appMain', ['common','ngMessages']);

//SPA-views will attach to their own module and use their own data-ng-app
// and nested controllers.
// Each MVC-delivered top-spa-level view will link it's needed JS files
// Services attached to the commonModule will be available to all other
// Angular modules

commonModule.factory('viewModelHelper', function ($http, $q) {
    return EmailClient.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
    return valJs.validator();
});

(function (ec) {
    var viewModelHelper = function ($http, $q) {
        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
           
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(window.EmailClient.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null) {
                        always();
                    }
                    self.isLoading = false;
                },
                function (result) {
                    debugger;
                    if (failure == null) {
                        self.modelErrors = [result.data];
                        //if (result.status != 400) {
                        //    self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        //}
                        //else {
                        //    self.modelErrors = [result.data.Message];
                        //}
                        self.modelIsValid = false;
                    }
                    else {
                        failure(result);
                        if (always != null) {
                            always();
                        }
                        self.isLoading = false;
                    }
                }
                );
        }

        //posts the request, parameters: uri, data, success ,failure, always
        self.apiPost = function (uri, data, success, failure, always) {
            //debugger;            
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(window.EmailClient.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                    {
                        always();
                    }
                    self.isLoading = false;
                },
                function (result) {
                    debugger;
                    if (failure == null) {
                        self.modelErrors = [result.data];
                        //if (result.status != 400) {
                        //    debugger;
                        //    self.modelErrors = [result.data];
                        //}
                        //else {
                        //    self.modelErrors = [result.data];
                        //}
                        self.modelIsValid = false;
                    }
                    else {
                        failure(result);
                        if (always != null) {
                            always();
                        }
                        self.isLoading = false;
                    }
                }
                );
        };

        return this;
    }
    ec.viewModelHelper = viewModelHelper;
}(window.EmailClient));

(function (ec) {
    var mustEqual = function (value, other) {
        return value == other;
    }
    ec.mustEqual = mustEqual;
}(window.EmailClient));

//validation
window.valJs = {};

(function (val) {
    //validator
    var validator = function () {
        var self = this;

        //propertyRule, rules for property, parameters: propertyName, rules
        self.PropertyRule = function (propertyName, rules) {
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
        };

        //validateModel validates model, parameters: model, allPropertyRules
        self.ValidateModel = function (model, allPropertyRules) {
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (var j = 0; j < allPropertyRules.length; j++) {
                    var propertyRule = allPropertyRules[j];
                    if (prop == propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++) {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp != 'custom') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params')) {
                                    params = propertyRules[propertyRuleProp].params;
                                }
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRuleProp, rule.message));
                                }
                            }
                            else {
                                var validator = propertyRules.custom.validator;
                                var value = null;
                                if (propertyRules.custom.hasOwnProperty('params')) {
                                    value = propertyRules.custom.params;
                                }
                                var result = validator(model[prop], value());
                                if (result != true) {
                                    errors.push(getMessage(prop, propertyRules.custom,'Invalid value'));
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length == 0);
        };

        var getMessage = function (prop, rule, defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message')) {
                message = rule.message;
            }
            else {
                message = prop + ': ' + defaultMessage;
            }
            return message;
        };

        var rules = [];

        var setupRules = function () {
            rules['required'] = {
                validator: function (value, params) {
                    return !(value.trim() == '');
                },
                message: 'value is required'
            };
            rules['minLength'] = {
                validator: function (value, params) {
                    return !(value.trim().length < params);
                },
                message: 'value does not meet minimum length'
            };
            rules['pattern'] = {
                validator: function (value, params) {                    
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.trim()) == null);
                },
                message: 'value must match regular expression'
            };
        };

        setupRules();

        return this;
    };
    val.validator = validator;
}(window.valJs));

//directives
appMainModule.directive("isEqualTo", function () {
    return {
        require: "ngModel",
        scope: {
            isEqualTo: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;
                if (scope.isEqualTo || ctrl.$viewValue) {
                    combined = scope.isEqualTo + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        var origin = scope.isEqualTo;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("isEqualTo", false);
                            return undefined;
                        }
                        else {
                            ctrl.$setValidity("isEqualTo", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});