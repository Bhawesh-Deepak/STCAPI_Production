using STCAPI.DataLayer.AdminPortal;
using STCAPI.ReqRespVm;
using STCAPI.ReqRespVm.AdminPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Helpers
{
    public static  class CommonServiceHelper
    {
        public static List<MainStreamVm> GetMainStreamDetail(ResponseModel<MainStreamMaster> mainStreamModel, ResponseModel<StageMaster> stageModel)
        {
            var response = (from mainStreamData in mainStreamModel.TEntities
                            join stageData in stageModel.TEntities
                            on mainStreamData.StageId equals stageData.Id
                            select new MainStreamVm
                            {
                                Id = mainStreamData.Id,
                                StageId = stageData.Id,
                                Name = mainStreamData.Name,
                                LongName = mainStreamData.LongName,
                                ShortName = mainStreamData.ShortName,
                                StageName = stageData.Name
                            }).ToList();

            return response;
        }
        public  static List<StreamDetailVm> GetStreamDetails(ResponseModel<MainStreamMaster> mainStreamData, ResponseModel<StreamMaster> streamData)
        {
            return (from mainData in mainStreamData.TEntities
                    join strData in streamData.TEntities
                    on mainData.Id equals strData.MainStreamId
                    select new StreamDetailVm
                    {
                        MainStreamName = mainData.Name,
                        Name = strData.Name,
                        LongName = strData.LongName,
                        ShortName = strData.ShortName,
                        Description = strData.Description
                    }).ToList();
        }
    }
}
