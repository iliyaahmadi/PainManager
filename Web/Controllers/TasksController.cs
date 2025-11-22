using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Commands;
using Application.Interfaces;
using Domain.Entities;

namespace PainManager.Web.Controllers;

public class TasksController : Controller
{
    private readonly IApplicationDbContext _context;
    private readonly ICreateTaskCommandHandler _createHandler;
    private readonly IUpdateTaskCommandHandler _updateHandler;

    public TasksController(IApplicationDbContext context, ICreateTaskCommandHandler createHandler, IUpdateTaskCommandHandler updateHandler)
    {
        _context = context;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return View(tasks);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _createHandler.Handle(command);
        TempData["Success"] = "Task created successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        var command = new UpdateTaskCommand
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted
        };

        return View(command);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateTaskCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _updateHandler.Handle(command);
        TempData["Success"] = "Task updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Task deleted successfully!";
        }

        return RedirectToAction(nameof(Index));
    }
}