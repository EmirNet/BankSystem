using BankSystem.Application.Dtos;
using BankSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlQueryService _urlQueryService;

        public UrlsController(IUrlQueryService urlQueryService)
        {
            _urlQueryService = urlQueryService;
        }
        //Return last 3 parameters
        [HttpGet]
        public ActionResult<IQueryable<UrlQueryDto>> GetLastThreeUrlQueryParametrsAsync()
        {
            return Ok(_urlQueryService.GetLastThreeUrlQueryParametersAsync());
        }
    }
}
