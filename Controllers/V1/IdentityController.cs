using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Domain.V1;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    public class IdentityController :Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult>Register([FromBody]Identity request)
        {
            return Ok();
        }
    }
}
