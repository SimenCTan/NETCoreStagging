using KYExpress.Core.Domain.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYExpress.ProcessControl.Application
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProcessController:ControllerBase
    {
        private readonly IQueryService _queryService;
        public ProcessController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpPost("YdNoProcessInfo")]
        public async Task<IEnumerable<dynamic>> YdNoProcessInfo([FromBody]Dto.ProcessRequestModel processRequestModel)
        {
            return await _queryService.QueryAsync<dynamic>(SqlScriptData.GetProcessInfoSql(),
                new { processRequestModel.YDNo, processRequestModel.DTimeStart, processRequestModel.DTimeEnd });            
        }
    }
}
