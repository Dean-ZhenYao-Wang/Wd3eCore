namespace Wd3eCore.Workflows.Models
{
    public enum WorkflowStatus
    {
        Idle,
        Starting,
        Resuming,
        Executing,
        Halted,
        Finished,
        Faulted,
        Aborted
    }
}
