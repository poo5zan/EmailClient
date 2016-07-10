using EmailClient.Common.Validation;
using EmailClient.Web.Core;
using EmailClient.Web.Models;
using EmailClient.Web.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EmailClient.Web.Controllers.Api
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mail")]
    public class MailApiController : ApiControllerBase
    {
        public MailApiController()
        {
            _MailRepository = new MailRepository();
        }
        

        [ImportingConstructor]
        public MailApiController(IMailRepository mailRepository)
        {
            _MailRepository = mailRepository;
        }

        IMailRepository _MailRepository;

        [Route("isMailSettingsConfigured")]
        [HttpGet]
        public HttpResponseMessage IsMailSettingsConfigured(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response;
                bool isSettingsConfigured = _MailRepository.IsMailSettingsConfigured(User.Identity.Name);
                response = request.CreateResponse(HttpStatusCode.OK, isSettingsConfigured);
                return response;
            });
        }

        [Route("saveMailSettings")]
        [HttpPost]
        public HttpResponseMessage SaveMailSettings(HttpRequestMessage request,
            MailSettingViewModel mailSettingViewModel)
        {
            if (mailSettingViewModel == null) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Request is null");
            }
           
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(mailSettingViewModel.Email))
            {
               sb.AppendLine("Email is required");
            }
            if (!ValidationHelper.IsEmailValid(mailSettingViewModel.Email))
            {
                sb.AppendLine("Email is invalid");
            }

            if (string.IsNullOrWhiteSpace(mailSettingViewModel.Password))
            {
                sb.AppendLine("Password is required");
            }

            //check if mail settings is already configured
            if (!_MailRepository.IsMailSettingAvailable(mailSettingViewModel.Email))
            {
                sb.AppendLine("The email is already used");
            }

            if (!string.IsNullOrWhiteSpace(sb.ToString())) {                
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,sb.ToString());
            }
            

            return GetHttpResponse(request,() =>
            {
                HttpResponseMessage response;
                bool isSaved = _MailRepository.SaveMailSettings(mailSettingViewModel,User.Identity.Name);
                if (isSaved)
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else {
                    response = request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Mail Settings couldnot be saved");
                }
                return response;
            });
        }

        [Route("getEmails")]
        [HttpGet]
        public HttpResponseMessage GetEmails(HttpRequestMessage request)
        {
            return GetHttpResponse(request,() => {
                HttpResponseMessage response;
                var emails = _MailRepository.GetEmails(User.Identity.Name,0);
                response = request.CreateResponse<List<MailMessageViewModel>>(HttpStatusCode.OK, emails);
                return response;
            });
        }

        [Route("delete")]
        [HttpPost]
        public HttpResponseMessage DeleteEmail(HttpRequestMessage request, MailMessageViewModel mailMessageViewModel)
        {
            return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response;
                 _MailRepository.DeleteEmail(mailMessageViewModel,User.Identity.Name);
                 response = request.CreateResponse(HttpStatusCode.OK);
                 return response;
             });
        }

        [Route("send")]
        [HttpPost]
        public HttpResponseMessage SendEmail(HttpRequestMessage request,
            MailMessageViewModel mailMessageViewModel)
        {
            return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response;
                 _MailRepository.SendEmail(mailMessageViewModel,User.Identity.Name);
                 response = request.CreateResponse(HttpStatusCode.OK);
                 return response;
             });
        }

    }

    
}
