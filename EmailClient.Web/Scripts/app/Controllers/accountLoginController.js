
appMainModule.controller("AccountLoginController",
    function ($scope, $http, $location, viewModelHelper, validator) {
        
        $scope.viewModelHelper = viewModelHelper;
        $scope.accountModel = new EmailClient.accountLoginModel();
        $scope.returnUrl = '';

        var accountModelRules = [];
        var setupRules = function () {
            accountModelRules.push(new validator.PropertyRule("LoginEmail",
            {
                required: { message: "Login is required" },
                pattern: { message: "Email is invalid", params: '^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$' }
            }));

            accountModelRules.push(new validator.PropertyRule("Password",
                {
                    required: { message: "Password is required" }                   
                }));
        };

        $scope.login = function () {            
            validator.ValidateModel($scope.accountModel, accountModelRules);
            viewModelHelper.modelIsValid = $scope.accountModel.isValid;
            viewModelHelper.modelErrors = $scope.accountModel.errors;
            if (viewModelHelper.modelIsValid) {                
                viewModelHelper.apiPost('api/account/login',
                    $scope.accountModel,
                    function (result) {
                        debugger;
                        if ($scope.returnUrl != ''
                            && $scope.returnUrl.length > 1) {
                            window.location.href = EmailClient.rootPath + $scope.returnUrl.substring(1);
                        }
                        else {
                            window.location.href = EmailClient.rootPath + 'mail';
                        }
                    }
                    );
            }
            else {
                viewModelHelper.modelErrors = $scope.accountModel.errors;
            }
        };

        setupRules();

    });