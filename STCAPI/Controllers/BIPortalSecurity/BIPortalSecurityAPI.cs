using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.BIPortalSecurity;
using STCAPI.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.BIPortalSecurity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BIPortalSecurityAPI : ControllerBase
    {
        private readonly IGenericRepository<BIPortalSecurityMaster, int> _IBIPortalSecurityMasterRepository;

        public BIPortalSecurityAPI(IGenericRepository<BIPortalSecurityMaster, int> biPortalSecurityMasterRepo)
        {
            _IBIPortalSecurityMasterRepository = biPortalSecurityMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> CreatePortalSecurityMaster(BIPortalSecurityMaster model)
        {
            var response = await _IBIPortalSecurityMasterRepository.CreateEntity(model);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetPortalSecurityMaster()
        {
            var response = await _IBIPortalSecurityMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            return Ok(response);
        }
    }
}
