//(function () { 


appMainModule.controller("AccountRegisterController",
    function ($scope, $http, $location, viewModelHelper, validator) {
        $scope.viewModelHelper = viewModelHelper;
        $scope.accountRegisterModel = new EmailClient.accountRegisterModel();
        $scope.accountRegisterModel.LoginEmail = 'pujan@pmail.com';


        // var accountRegisterModelRules = [];
        //var setupRules = function () {
        //    accountRegisterModelRules.push(new validator.PropertyRule("LoginEmail",
        //    {
        //        required: { message: "Login is required" },
        //        pattern: { message: "Email is invalid", params: '^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$' }
        //    }));
        //    accountRegisterModelRules.push(new validator.PropertyRule("Password",
        //        {
        //            required: { message: "Password is required" }                    
        //        }));
        //};

        $scope.register = function (form) {
            debugger;
            if (!form.$valid) {
                toastr.error('Invalid data', 'required pdata');
                return;
            }
            //validator.ValidateModel($scope.accountRegisterModel, accountRegisterModelRules);
            // viewModelHelper.modelIsValid = $scope.accountRegisterModel.isValid;
            // viewModelHelper.modelErrors = $scope.accountRegisterModel.errors;
            // if (viewModelHelper.modelIsValid) {                
            viewModelHelper.apiPost('api/account/register',
                $scope.accountRegisterModel,
                function (result) {
                    toastr.success('Registration is successful, Please login');
                    window.location.href = EmailClient.rootPath + 'account/login';
                }
                );
            // }
        };

       // setupRules();

    });




//}());
