
(function(ec){
    var mailSettingModel = function(){
        var self = this;

        self.Email = '';
        self.Password = '';
        self.RePassword = '';        
    }
   ec.mailSettingModel = mailSettingModel;
}(window.EmailClient));

(function (ec) {
    var mailMessageModel = function () {
        var self = this;

        self.MailId = '';
        self.From = '';
        self.To = '';
        self.Subject = '';
        self.Date = '';
        self.Body = '';
        self.Uid = '';

    }
    ec.mailMessageModel = mailMessageModel;
}(window.EmailClient));

