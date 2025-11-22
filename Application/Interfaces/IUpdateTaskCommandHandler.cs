using Application.Commands;

namespace Application.Interfaces;

public interface IUpdateTaskCommandHandler
{
    Task Handle(UpdateTaskCommand command);
}