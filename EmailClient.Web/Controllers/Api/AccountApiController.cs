using EmailClient.Web.Core;
using EmailClient.Web.Models;
using EmailClient.Web.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace EmailClient.Web.Controllers.Api
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountApiController(IAccountRepository accountRepository)
        {
            _AccountRepository = accountRepository;
        }

        IAccountRepository _AccountRepository;

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request,
           [FromBody]AccountLoginViewModel accountLoginModel)
        {
            return GetHttpResponse(request,
                () =>
                {
                    HttpResponseMessage response = null;
                    bool success = _AccountRepository.Login(accountLoginModel);

                    if (success)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK);

                        //FormsAuthentication.SetAuthCookie(accountLoginModel.LoginEmail, true);


                    }
                    else
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "unauthorized login");
                    }
                    return response;

                });
        }


        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request,
            [FromBody]AccountRegisterViewModel accountRegisterViewModel)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _AccountRepository.Register(accountRegisterViewModel);


                response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            });

        }

    }
}
