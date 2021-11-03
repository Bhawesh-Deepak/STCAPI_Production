using ExcelReaderHelper;
using ExcelReaderHelper.SubsidryError;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.Helpers;
using STCAPI.ReqRespVm.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.SubsidryInvoice
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValidateSubsidry : ControllerBase
    {
        private readonly IHostingEnvironment _IHostingEnviroment;

        public ValidateSubsidry(IHostingEnvironment hostingEnvironment)
        {
            _IHostingEnviroment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateInputData([FromForm] SubsidryModel model)
        {
            var responseModel = new ReadExcelData().GetInputVATModels(model.InvoiceExcelFile);

            var validationError = new SubsidryError().ValidateInputData(responseModel.Item2);

            return await Task.Run(() => Ok(new SubsidryErrorDetail().GetSubsidryErrorDetails(validationError)));
        }
    }
}
