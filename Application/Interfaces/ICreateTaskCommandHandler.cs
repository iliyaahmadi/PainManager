using Application.Commands;

namespace Application.Interfaces;

public interface ICreateTaskCommandHandler
{
    Task Handle(CreateTaskCommand command);
}