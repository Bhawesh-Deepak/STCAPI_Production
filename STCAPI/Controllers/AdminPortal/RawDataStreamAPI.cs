using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RawDataStreamAPI : ControllerBase
    {
        private readonly IGenericRepository<RawDataStream, int> _IRawDataStreamRepository;
        public RawDataStreamAPI(IGenericRepository<RawDataStream, int> rawDataStreamRepo)
        {
            _IRawDataStreamRepository = rawDataStreamRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateRawDataStream(RawDataStream model)
        {
            var response = await _IRawDataStreamRepository.CreateEntity(model);
            return Ok(response);
        }


    }
}
