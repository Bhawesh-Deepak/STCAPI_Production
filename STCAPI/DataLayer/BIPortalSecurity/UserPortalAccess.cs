using STCAPI.DataLayer.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace STCAPI.DataLayer.BIPortalSecurity
{
    [Table("UserPortalAccess")]
    public class UserPortalAccess:BaseModel<int>
    {
        public string UserName { get; set; }
        public int BISecurityMasterId { get; set; }
        [NotMapped]
        public bool Flag { get; set; }
    }
}
