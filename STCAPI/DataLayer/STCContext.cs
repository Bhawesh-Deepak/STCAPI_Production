using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STCAPI.Controllers.AdminPortal;
using STCAPI.DataLayer.AdminPortal;


namespace STCAPI.DataLayer
{
    public class STCContext:DbContext
    {
        private readonly string _connectionString;

        public STCContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
            
        public virtual DbSet<StageMaster> StageMasters { get; set; }
        public virtual DbSet<MainStreamMaster> MainStreamMasters { get; set; }
    }
}
