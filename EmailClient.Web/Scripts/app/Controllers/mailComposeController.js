    
appMainModule.controller("MailComposeController",
    function ($scope, $http, $location, viewModelHelper, validator) {
        debugger;
        $scope.viewModelHelper = viewModelHelper;
        $scope.mailMessageModel = new EmailClient.mailMessageModel();
      
        //var mailSettingModelRules = [];
        //var setupRules = function () {
        //    mailSettingModelRules.push(new validator.PropertyRule("LoginEmail",
        //    {
        //        required: { message: "Login is required" }
        //    }));

        //    mailSettingModelRules.push(new validator.PropertyRule("Password",
        //        {
        //            required: { message: "Password is required" }                   
        //        }));
        //};

        $scope.send = function () {            
            //validator.ValidateModel($scope.mailSettingModel, mailSettingModelRules);
           // viewModelHelper.modelIsValid = $scope.mailSettingModel.isValid;
           // viewModelHelper.modelErrors = $scope.mailSettingModel.errors;
          //  if (viewModelHelper.modelIsValid) {                
                viewModelHelper.apiPost('api/mail/send',
                    $scope.mailSettingModel,
                    function (result) {                        
                       toastr.success('Email send successfully');
                    }
                    );
           // }           
        };

        setupRules();

    });