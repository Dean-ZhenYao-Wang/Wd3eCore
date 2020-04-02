using System.Collections.Generic;
using Wd3eCore.Scripting;

namespace Wd3eCore.Workflows.Models
{
    public class WorkflowExecutionScriptContext : WorkflowExecutionHandlerContextBase
    {
        public WorkflowExecutionScriptContext(WorkflowExecutionContext workflowContext) : base(workflowContext)
        {
        }

        public IList<IGlobalMethodProvider> ScopedMethodProviders { get; } = new List<IGlobalMethodProvider>();
    }
}
