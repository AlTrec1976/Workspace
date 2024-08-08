using Workspace.Helpers;

namespace Workspace.Entities;

public abstract class StatusTask : Enumeration<StatusTask>
{
    public static readonly StatusTask New = new NewStatusTask();
    public static readonly StatusTask Assigned = new  AssignedStatusTask();
    public static readonly StatusTask Accepted = new AcceptedAssignmentStatusTask();
    public static readonly StatusTask JobInProgress = new JobInProgressStatusTask();
    public static readonly StatusTask JobDown = new JobDownStatusTask();
    public static readonly StatusTask Rejected = new RejectedStatusTask();
    public static readonly StatusTask Completed = new CompletedStatusTask();

    protected StatusTask(int value, string name) : base(value, name)
    {
    }
    
    public abstract int IdStatus { get; }
    public abstract string NameForStatus { get; }

    private sealed class NewStatusTask : StatusTask
    {
        public NewStatusTask() : base(1, "NewStatusTask")
        {
        }
        public override int IdStatus => 1;
        public override string NameForStatus => "Новый";
    }
    private sealed class AssignedStatusTask : StatusTask
    {
        public AssignedStatusTask() : base(2, "AssignedStatusTask")
        {
        }
        public override int IdStatus => 2;
        public override string NameForStatus => "Назначен";
    }
    private sealed class AcceptedAssignmentStatusTask : StatusTask
    {
        public AcceptedAssignmentStatusTask() : base(3, "AcceptedAssignmentStatusTask")
        {
        }
        public override int IdStatus => 3;
        public override string NameForStatus => "Принято к исполнению";
    }
    private sealed class JobInProgressStatusTask : StatusTask
    {
        public JobInProgressStatusTask() : base(4, "JobInProgressStatusTask")
        {
        }
        public override int IdStatus => 4;
        public override string NameForStatus => "В процессе исполнения";
    }
    private sealed class JobDownStatusTask : StatusTask
    {
        public JobDownStatusTask() : base(5, "JobDownStatusTask")
        {
        }
        public override int IdStatus => 5;
        public override string NameForStatus => "Исполнено";
    }
    private sealed class RejectedStatusTask : StatusTask
    {
        public RejectedStatusTask() : base(6, "RejectedStatusTask")
        {
        }
        public override int IdStatus => 6;
        public override string NameForStatus => "Отклонено";
    }
    private sealed class CompletedStatusTask : StatusTask
    {
        public CompletedStatusTask() : base(7, "CompletedStatusTask")
        {
        }
        public override int IdStatus => 7;
        public override string NameForStatus => "Завершено";
    }
}