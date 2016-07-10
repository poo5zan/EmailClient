
var mailModule = angular.module('mailModule', ['common'])
        .config(function ($routeProvider, $locationProvider) {
            $routeProvider.when(EmailClient.rootPath + 'mail/settings', { templateUrl: EmailClient.rootPath + 'Templates/settings.html', controller: 'MailSettingsController' });
            $routeProvider.when(EmailClient.rootPath + 'mail/inbox', { templateUrl: EmailClient.rootPath + 'Templates/inbox.html', controller: 'InboxController' });
            $routeProvider.when(EmailClient.rootPath + 'mail/detail', { templateUrl: EmailClient.rootPath + 'Templates/detail.html', controller: 'DetailController' });
            $routeProvider.when(EmailClient.rootPath + 'mail/deleteConfirm', { templateUrl: EmailClient.rootPath + 'Templates/deleteConfirm.html', controller: 'DeleteConfirmController' });
            $routeProvider.when(EmailClient.rootPath + 'mail/compose', { templateUrl: EmailClient.rootPath + 'Templates/compose.html', controller: 'ComposeController' });
            $routeProvider.otherwise({ redirectTo: EmailClient.rootPath + 'mail/settings' });
            //$locationProvider.html5Mode(true);
        });

mailModule.controller('MailController',
    function ($scope, viewModelHelper) {       
        $scope.viewModelHelper = viewModelHelper;
        $scope.mailSettingModel = new EmailClient.mailSettingModel();
        $scope.mailMessageModel = new EmailClient.mailMessageModel();
        
        $scope.config = {
            isNavigatedFromInbox: false,
            isMailSettingCompleted: false
        }
    });

mailModule.controller('MailSettingsController',
    function ($scope, $http, $location, viewModelHelper, validator) {        
        var mailSettingModelRules = [];
        var setupRules = function () {
            mailSettingModelRules.push(new validator.PropertyRule("Email",
            {
                required: { message: "Login is required" }
            }));
            mailSettingModelRules.push(new validator.PropertyRule("Password",
                {
                    required: { message: "Password is required" }
                }));
        };

        //retrieve gmail settings if already saved,
        viewModelHelper.apiGet('api/mail/isMailSettingsConfigured',
            null, function (result) {                
                $scope.config.isMailSettingCompleted = result.data;
                if ($scope.config.isMailSettingCompleted) {
                    toastr.info('Loading Inbox...');
                    $location.path(EmailClient.rootPath + 'mail/inbox');
                }
            });

        $scope.save = function () {            
            validator.ValidateModel($scope.mailSettingModel, mailSettingModelRules);
            viewModelHelper.modelIsValid = $scope.mailSettingModel.isValid;
            viewModelHelper.modelErrors = $scope.mailSettingModel.errors;
            if (viewModelHelper.modelIsValid) {
                viewModelHelper.apiPost('api/mail/saveMailSettings',
                   $scope.mailSettingModel,
                   function (result) {
                       toastr.success('Settings has been saved successfully');
                       toastr.info('Loading Inbox...');                       
                       $scope.config.isMailSettingCompleted = true;
                       $location.path(EmailClient.rootPath + 'mail/inbox');
                   });
            }
        };

        setupRules();
    });

mailModule.controller('InboxController',
    function ($scope, $http, $location, viewModelHelper, validator) {
       
        if (!$scope.config.isMailSettingCompleted) {
            toastr.info('Please configure your email settings');
            $location.path(EmailClient.rootPath + 'mail/settings');
        }

        viewModelHelper.modelIsValid = true;
        viewModelHelper.modelErrors = [];

        $scope.emails = [];
        $scope.init = false;

        $scope.availableEmails = function () {
            viewModelHelper.apiGet('api/mail/getEmails',
                $scope.mailSettingModel,
                function (result) {                    
                    $scope.emails = result.data;
                    $scope.init = true;
                });
        };

        $scope.selectEmail = function (email) {            
            MapEmailToMailMessageModel(email);
            $scope.config.isNavigatedFromInbox = true;
            $location.path(EmailClient.rootPath + 'mail/detail');
        };

        $scope.deleteConfirm = function (email) {           
            MapEmailToMailMessageModel(email);
            $scope.config.isNavigatedFromInbox = true;
            toastr.warning('Are you sure to delete this email ?');
            $location.path(EmailClient.rootPath + 'mail/deleteConfirm');
        }

        var MapEmailToMailMessageModel = function (email) {            
            $scope.mailMessageModel.MailId = email.MailId;
            $scope.mailMessageModel.From = email.From;
            $scope.mailMessageModel.Subject = email.Subject;
            $scope.mailMessageModel.Date = email.Date;
            $scope.mailMessageModel.Body = email.Body;
            $scope.mailMessageModel.To = email.To;
            $scope.mailMessageModel.Uid = email.Uid;
        }

        $scope.availableEmails();
    });

mailModule.controller('DetailController',
    function ($scope, $http, $location, $window, viewModelHelper, validator) {
        
        if (!$scope.config.isNavigatedFromInbox) {
            toastr.info('Loading Inbox');
            $location.path(EmailClient.rootPath + 'mail/inbox');
        }

        $scope.previous = function () {
            $window.history.back();
        }

    });

mailModule.controller('DeleteConfirmController',
    function ($scope, $http, $location, viewModelHelper, validator) {       
        if (!$scope.config.isNavigatedFromInbox) {
            toastr.info('Loading Inbox');
            $location.path(EmailClient.rootPath + 'mail/inbox');
        }

        $scope.deleteEmail = function (email) {           
            //delete,call api post
            viewModelHelper.apiPost('api/mail/delete',
                email, function (result) {
                    toastr.success('Email deleted successfully');
                    $location.path(EmailClient.rootPath + 'mail/inbox');
                });
        }
    });

mailModule.controller('ComposeController',
    function ($scope, $http, $location, viewModelHelper, validator) {
       
        $scope.sendEmail = function () {
           // debugger;
            //delete,call api post
            viewModelHelper.apiPost('api/mail/send',
                $scope.mailMessageModel, function (result) {
                    toastr.success('Email sent successfully');
                    //$location.path(EmailClient.rootPath + 'mail/inbox');
                });
        }
    });