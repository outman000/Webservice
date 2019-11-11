using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Eday.Webservice.BuildingBlocks.EventBus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using User.API.Application.Commands.OperateCommands.UserCommands;
using User.API.Application.Queries.IUserQueries;
using User.API.Application.Queries.ViewModel.UserViewModel;
using User.API.Infrastructure.Services;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueriesInterface _userQueriesInterface;
        private readonly IIdentityService _identityService;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, IUserQueriesInterface userQueriesInterface, IIdentityService identityService, ILogger<UserController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userQueriesInterface = userQueriesInterface ?? throw new ArgumentNullException(nameof(userQueriesInterface));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <returns></returns>
        /// <response code="200">查询成功</response>
        [HttpGet("{userid: int}")]
        [ProducesResponseType(typeof(IEnumerable<UserInfoViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserInfoViewModel>>> GetUserByIdAsync(int userid)
        {
            
            var userInfos = await _userQueriesInterface.GetOrderAsync(userid);
            return Ok(userInfos);
        }


        /// <summary>
        /// 新建一个用户
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <response code="200">用户创建成功</response>
        /// <response code="400">用户创建失败</response>
        [HttpPost("newuser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddUserInfoAsync([FromBody]CreateUserCommand  createUserCommand)
        {
            bool commandResult = false;
            _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    createUserCommand.GetGenericTypeName(),
                    nameof(createUserCommand.UserId),
                    createUserCommand.UserId,
                    createUserCommand);

            commandResult= await _mediator.Send(createUserCommand);
            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();

        }




    }
}