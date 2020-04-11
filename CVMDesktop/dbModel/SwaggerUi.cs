using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Data;
using Wd3eCore.Environment.Shell;

namespace Wd3eCore.CVMDesktop.dbModel
{
    public class SwaggerUi
    {
        public Guid SID { get; set; }
        public string SwaggerUI_Name { get; set; }
    }
    public class SwaggerUiConfiguration : IEntityTypeConfiguration<SwaggerUi>
    {
        public void Configure(EntityTypeBuilder<SwaggerUi> builder)
        {
            builder
                .HasKey(m => m.SID);
        }
    }

    public class SwaggerUiIndex
    {
        public Guid SID { get; set; }

        public string SwaggerUIName { get; set; }
    }

}
