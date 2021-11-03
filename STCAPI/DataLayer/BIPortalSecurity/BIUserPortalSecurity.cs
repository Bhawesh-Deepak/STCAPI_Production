using STCAPI.DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.DataLayer.BIPortalSecurity
{
    [Table("BIUserPortalSecurity")]
    public class BIUserPortalSecurity: BaseModel<int>
    {
        public string MyProperty { get; set; }
    }
}
