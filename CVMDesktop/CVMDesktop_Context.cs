using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Wd3eCore.CVMDesktop.dbModel;
using Wd3eCore.Data;
using Wd3eCore.Environment.Shell;

namespace Wd3eCore.CVMDesktop
{

    public class CVMDesktop_Context : DbContext
    {
        private readonly ShellSettings _shellSettings;

        public DbSet<SwaggerUi> swaggerUis;

        public CVMDesktop_Context(DbContextOptions<CVMDesktop_Context> options,
         ShellSettings shellSettings) : base(options)
        {
            _shellSettings = shellSettings;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            foreach(IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(_shellSettings["TablePrefix"] +"_"+ entity.GetTableName());
            }
        }
    }
}
