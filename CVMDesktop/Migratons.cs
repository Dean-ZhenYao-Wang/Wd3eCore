using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text;
using Wd3eCore.CVMDesktop.dbModel;
using Wd3eCore.Data.Migration;

namespace Wd3eCore.CVMDesktop
{
    public class Migratons : DataMigration
    {
        /// <summary>
        /// 仅在第一次启用时调用，以后即使禁用在启用，或者站点重启，这里也不会在被调用
        /// </summary>
        /// <returns></returns>
        public int Create()
        {
            SchemaBuilder.CreateTable(nameof(SwaggerUi), table => table
             .Column<Guid>("SID", c => c.PrimaryKey())
             .Column<string>("SwaggerUI_Name"));
            SchemaBuilder.CreateTable(nameof(SwaggerUiIndex), table => table
             .Column<Guid>("SID")
             .Column<string>("SwaggerUIName"));
            return 1;
        }
    }
}
