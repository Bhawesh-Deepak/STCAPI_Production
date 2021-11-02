using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StageMasterAPI : ControllerBase
    {
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;
        public StageMasterAPI(IGenericRepository<StageMaster, int> stageMasterRepo)
        {
            _IStageMasterRepository = stageMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateStage(StageMaster model)
        {
            var response = await _IStageMasterRepository.CreateEntity(model);

            if (response.ResponseStatus == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetStageDetails()
        {
            var response = await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            if (response.ResponseStatus == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateStageDetails(StageMaster model)
        {
            var deleteModel = await _IStageMasterRepository.GetAllEntities(x => x.Id == model.Id);

            if (deleteModel.ResponseStatus == HttpStatusCode.OK && deleteModel.TEntities.Any())
            {
                deleteModel.TEntities.ToList().ForEach(x =>
                {
                    x.IsActive = false;
                    x.IsDeleted = true;
                });

                var deleteResponse = await _IStageMasterRepository.DeleteEntities(deleteModel.TEntities.ToArray());

                if (deleteResponse.ResponseStatus == HttpStatusCode.OK)
                {
                    model.Id = 0;

                    var createResponse = await _IStageMasterRepository.CreateEntity(model);

                    if (createResponse.ResponseStatus == HttpStatusCode.OK)
                    {
                        return Ok(createResponse);
                    }
                    return BadRequest(createResponse);
                }

                return BadRequest(deleteResponse);
            }

            return BadRequest(deleteModel);
        }

        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var deleteModel = await _IStageMasterRepository.GetAllEntities(x => x.Id == id);

            if (deleteModel.ResponseStatus == HttpStatusCode.OK && deleteModel.TEntities.Any())
            {
                deleteModel.TEntities.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                });

                var deleteResponse = await _IStageMasterRepository.DeleteEntities(deleteModel.TEntities.ToArray());

                return Ok(deleteResponse);

            }

            return BadRequest(deleteModel);
        }
    }
}
