using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.Helpers;
using STCAPI.ReqRespVm;
using STCAPI.ReqRespVm.AdminPortal;
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
    public class MainStreamAPI : ControllerBase
    {
        private readonly IGenericRepository<MainStreamMaster, int> _IMainsStreamRepository;
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;

        public MainStreamAPI(IGenericRepository<MainStreamMaster, int> mainStreamRepo, IGenericRepository<StageMaster, int> stageMasterRepo)
        {
            _IMainsStreamRepository = mainStreamRepo;
            _IStageMasterRepository = stageMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateMainStream(MainStreamMaster model)
        {
            var responsedetail = await _IMainsStreamRepository.CreateEntity(model);

            if (responsedetail.ResponseStatus == System.Net.HttpStatusCode.Created)
            {
                return Ok(responsedetail);
            }
            return BadRequest(responsedetail);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetMainStreamDetails()
        {
            var mainStreamModel = await _IMainsStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            var stageModel = await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            return Ok(CommonServiceHelper.GetMainStreamDetail(mainStreamModel, stageModel));

        }

        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMainStream(int id)
        {
            var deleteModel = await _IMainsStreamRepository.GetAllEntities(x => x.Id == id);

            if (deleteModel.ResponseStatus == HttpStatusCode.OK && deleteModel.TEntities.Any())
            {
                deleteModel.TEntities.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                });

                var deleteResponse = await _IMainsStreamRepository.DeleteEntities(deleteModel.TEntities.ToArray());

                return Ok(deleteResponse);
            }

            return BadRequest(deleteModel);
        }


        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateMainStream(MainStreamMaster model)
        {

            var deleteModel = await _IMainsStreamRepository.GetAllEntities(x => x.Id == model.Id);

            if (deleteModel.ResponseStatus == HttpStatusCode.OK && deleteModel.TEntities.Any())
            {
                deleteModel.TEntities.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                });

                var deleteResponse = await _IMainsStreamRepository.DeleteEntities(deleteModel.TEntities.ToArray());

                model.Id = 0;

                var createResponse = await _IMainsStreamRepository.CreateEntity(model);

                if (createResponse.ResponseStatus == HttpStatusCode.Created)
                {
                    return Ok(createResponse);
                }

            }

            return BadRequest(deleteModel);
        }
    }
}
