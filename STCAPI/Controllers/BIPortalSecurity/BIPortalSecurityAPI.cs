using ExcelReaderHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.BIPortalSecurity;
using STCAPI.ReqRespVm.RequestModel;
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

        [HttpPost]
        public async Task<IActionResult> UploadPortalMenu([FromForm] UplaodPortalMenu model)
        {
            var excelResponse = new ReadExcelData().ConvertExcelToDat(model.InputFile);

            List<BIPortalSecurityMaster> models = new List<BIPortalSecurityMaster>();


            excelResponse.Item2.ForEach(data => {
                var model = new BIPortalSecurityMaster();
                model.StageName = data.StageName;
                model.MainStreamName = data.MainStream;
                model.StreamLongName = data.StreamLongName;
                model.StreamName = data.Stream;
                model.ObjectType = data.ObjectType;
                model.ObjectTypeName = data.ObjectTypeName;
                model.URL = data.URL;
                model.CreatedBy = "Admin";

                models.Add(model);
            });

            var response = await _IBIPortalSecurityMasterRepository.CreateEntities(models.ToArray());
            return Ok(response);

        }

    }
}
