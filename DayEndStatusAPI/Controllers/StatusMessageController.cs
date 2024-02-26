using Amazon.Auth.AccessControlPolicy;
using AutoMapper;
using Azure;
using DayEndStatusAPI.Implementation;
using DayEndStatusAPI.Interface;
using DayEndStatusAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Localization;
using DayEndStatusAPI.Dtos;

using Response = DayEndStatusAPI.Dtos.Response;

namespace DayEndStatusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusMessageController : ControllerBase
    {
        private readonly IServer1compass _server1repo;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public StatusMessageController(IServer1compass server1repo, IMapper mapper, IStringLocalizer<Resource> localizer)
        {
            _server1repo = server1repo;
            _mapper = mapper;
            _localizer = localizer;
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        /// <returns code="200">List of currencies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<StatusMessage>), StatusCodes.Status200OK)]
        public IActionResult GetStatusMessages()
        {
            var objList = _server1repo.GetStatusMessages();
            if (objList == null)
            {
                return new OkObjectResult(new Response(false, _localizer["Failed to get StatusMessages"], null));
            }
            var objDto = new List<StatusMessage>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StatusMessage>(obj));
            }
            return new OkObjectResult(new Response(true, _localizer["Status Messages"], objDto));
        }
    }
}
