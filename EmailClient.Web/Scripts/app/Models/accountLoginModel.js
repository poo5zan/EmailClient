
(function(ec){
    var accountLoginModel = function(){
        var self = this;

        self.LoginEmail = '';
        self.Password = '';
        self.RememberMe = false;
    }
    ec.accountLoginModel = accountLoginModel;
}(window.EmailClient));