using STCAPI.DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.DataLayer.AdminPortal
{
    [Table("QlikDataAccess")]
    public class QlikDataAccess:BaseModel<int>
    {
        public string StreamName { get; set; }
        public string UserName { get; set; }
        public string AppName { get; set; }
        public string AccessLevel { get; set; }
        public string DataGranularity { get; set; }
        public string ActionName { get; set; }
        public string Comments { get; set; }
    }
}
