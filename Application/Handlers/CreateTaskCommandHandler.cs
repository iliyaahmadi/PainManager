using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using TaskEntity = Domain.Entities.Task;

namespace Application.Handlers;

public class CreateTaskCommandHandler : ICreateTaskCommandHandler
{
    private readonly IApplicationDbContext _context;

    public CreateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task Handle(CreateTaskCommand command)
    {
        var task = new TaskEntity { Title = command.Title, Description = command.Description, IsCompleted = false };
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }
}