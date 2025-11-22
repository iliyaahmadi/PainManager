using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using TaskEntity = Domain.Entities.Task;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}