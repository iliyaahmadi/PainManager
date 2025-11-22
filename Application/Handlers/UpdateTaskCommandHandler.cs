using Application.Commands;
using Application.Interfaces;
using TaskEntity = Domain.Entities.Task;

namespace Application.Handlers;

public class UpdateTaskCommandHandler : IUpdateTaskCommandHandler
{
    private readonly IApplicationDbContext _context;

    public UpdateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task Handle(UpdateTaskCommand command)
    {
        var task = await _context.Tasks.FindAsync(command.Id);
        if (task == null) throw new Exception("Task not found");

        task.Title = command.Title;
        task.Description = command.Description;
        task.IsCompleted = command.IsCompleted;

        await _context.SaveChangesAsync();
    }
}