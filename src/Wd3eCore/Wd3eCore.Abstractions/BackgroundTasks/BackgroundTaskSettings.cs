using System;
using Newtonsoft.Json;

namespace Wd3eCore.BackgroundTasks
{
    public class BackgroundTaskSettings
    {
        /// <summary>
        /// 如果对象不能用于更新数据库，则为True。
        /// </summary>
        [JsonIgnore]
        public bool IsReadonly { get; set; }

        public string Name { get; set; } = String.Empty;
        public bool Enable { get; set; } = true;
        public string Schedule { get; set; } = "* * * * *";
        public string Description { get; set; } = String.Empty;
    }
}
