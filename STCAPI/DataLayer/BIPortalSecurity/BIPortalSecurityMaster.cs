using STCAPI.DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.DataLayer.BIPortalSecurity
{
    [Table ("BIPortalSecurityMaster")]
    public class BIPortalSecurityMaster:BaseModel<int>
    {
        public string StageName { get; set; }
        public string MainStreamName { get; set; }
        public string StreamName { get; set; }
        public string StreamLongName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectTypeName { get; set; }
        public string URL { get; set; }
        public bool Flag { get; set; }
    }
}
