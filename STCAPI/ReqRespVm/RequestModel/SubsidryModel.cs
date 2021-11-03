using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.ReqRespVm.RequestModel
{
    public class SubsidryModel
    {
        public IFormFile InvoiceExcelFile { get; set; }
        public string InvoiceName { get; set; }
        public string UserName { get; set; }
        public List<IFormFile> AttachmentList { get; set; }
    }
}
