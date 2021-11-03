using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.DataLayer.BIPortalSecurity;
using STCAPI.ReqRespVm;
using STCAPI.ReqRespVm.AdminPortal;
using STCAPI.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.BIPortalSecurity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserPortalAccessAPI : ControllerBase
    {
        private readonly IGenericRepository<UserPortalAccess, int> _IUserPortalAccessRepository;
        private readonly IGenericRepository<BIPortalSecurityMaster, int> _IBIPortalSecurityRepository;

        public UserPortalAccessAPI(IGenericRepository<UserPortalAccess, int> userAccessRepo,
            IGenericRepository<BIPortalSecurityMaster, int> biPortalSecurityRepo)
        {
            _IUserPortalAccessRepository = userAccessRepo;
            _IBIPortalSecurityRepository = biPortalSecurityRepo;
        }


        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreatePortalAccess(List<UserPortalAccess> models)
        {
            var deleteModels = await _IUserPortalAccessRepository
                        .GetAllEntities(x => x.UserName.Trim().ToUpper() == models.First().UserName.Trim().ToUpper());

            if (deleteModels.TEntities.Any())
            {
                deleteModels.TEntities.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                });

                var deleteResponse = await _IUserPortalAccessRepository.DeleteEntities(deleteModels.TEntities.ToArray());
            }

            var createResponse = await _IUserPortalAccessRepository.CreateEntities(models.ToArray());

            return Ok(createResponse);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserPortalAccess(string userName)
        {
            var biMaster = await _IBIPortalSecurityRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            var userAccess = await _IUserPortalAccessRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted 
            && x.UserName.Trim().ToUpper() == userName.Trim().ToUpper());

            biMaster.TEntities.ToList().ForEach(data =>
            {
                userAccess.TEntities.ToList().ForEach(item =>
                {
                    if (data.Id == item.BISecurityMasterId)
                    {
                        data.Flag = true;
                    }
                });

            });
            var response = GetFormattedResponse(biMaster, userName);
            return Ok(response);
        }


        private AdminPortalResponseModel GetFormattedResponse(ResponseModel<BIPortalSecurityMaster> portalAccessDetails, string userName)
        {
            var model = new AdminPortalResponseModel();
            var formList = new List<Form>();
            var dashBoardList = new List<Dashboard>();
            var reportList = new List<Report>();
            var subStreamList = new List<SubStream>();
            var mainStreamList = new List<MainStream>();
            var stageList = new List<Stage>();


            foreach (var stageData in portalAccessDetails.TEntities.GroupBy(x => x.StageName))
            {
                var stageModel = new Stage();
                stageModel.stageName = stageData.Key;

                foreach (var mainStreamData in stageData.GroupBy(x => x.MainStreamName))
                {
                    var mainStreamModel = new MainStream();
                    mainStreamModel.streamName = mainStreamData.Key;


                    foreach (var subStreamData in mainStreamData.GroupBy(x => x.StreamName))
                    {
                        var subStreamModel = new SubStream();
                        subStreamModel.subStreamName = subStreamData.Key;
                        subStreamList.Add(subStreamModel);

                        foreach (var objectData in subStreamData)
                        {

                            switch (objectData.ObjectType)
                            {
                                case "Form":
                                    var formModel = new Form();
                                    formModel.accessLevel = objectData.Flag;
                                    formModel.name = "Form";
                                    formList.Add(formModel);
                                    break;
                                case "Dashboard":
                                    var dashboard = new Dashboard();
                                    dashboard.accessLevel = objectData.Flag;
                                    dashboard.name = "Dashboard";
                                    dashBoardList.Add(dashboard);
                                    break;
                                case "Report":
                                    var reportModel = new Report();
                                    reportModel.accessLevel = objectData.Flag;
                                    reportModel.name = "Report";
                                    reportList.Add(reportModel);
                                    break;
                            }

                        }

                        subStreamModel.form = formList;
                        subStreamModel.dashboard = dashBoardList;
                        subStreamModel.report = reportList;
                    }
                    mainStreamModel.subStream = subStreamList;
                    mainStreamList.Add(mainStreamModel);
                }
                stageModel.mainStream = mainStreamList;
                stageList.Add(stageModel);

            }
            model.directory = "Basserah";
            model.userName = userName;
            model.stages = stageList;
            return model;
        }

    }
}
