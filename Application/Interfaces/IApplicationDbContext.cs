using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TaskEntity> Tasks { get; set; }
    DbSet<User> Users { get; set; }
    System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}