using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Wd3eCore.Data.Migration;
using Wd3eCore.Entities;
using YesSql.Indexes;

namespace CVMDesktop.dbModel
{
    public class SwaggerUi: Entity
    {
        public Guid SID { get; set; }
        public string SwaggerUI_Name { get; set; }
    }
    //数据库中的
    public class SwaggerUiIndex
    {
        public Guid SID { get; set; }

        public string SwaggerUIName { get; set; }
    }


}
