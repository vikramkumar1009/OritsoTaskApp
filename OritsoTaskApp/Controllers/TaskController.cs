using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OritsoTaskApp.Models;
using System.Security.Claims;

namespace TaskManagerAPI.Controllers
{
    [Authorize] // 🔐 Enable token-based auth for all endpoints
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _context.TaskItems.ToList();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userNameClaim = User.FindFirst(ClaimTypes.Name);

            if (userIdClaim == null || userNameClaim == null)
                return Unauthorized(new { message = "User claims missing in token." });

            int userId = int.Parse(userIdClaim.Value);
            string userName = userNameClaim.Value;

            // Set audit and default values
            task.CreatedOn = DateTime.Now;
            task.UpdatedOn = DateTime.Now;
            task.Status = string.IsNullOrWhiteSpace(task.Status) ? "Pending" : task.Status;
            task.CreatedById = userId;
            task.CreatedByName = userName;
            task.LastUpdatedById = userId;
            task.LastUpdatedByName = userName;

            _context.TaskItems.Add(task);
            _context.SaveChanges();

            return Ok(task);
        }


        // PUT: api/Task/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem updatedTask)
        {
            var existingTask = _context.TaskItems.Find(id);
            if (existingTask == null) return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userNameClaim = User.FindFirst(ClaimTypes.Name);

            if (userIdClaim == null || userNameClaim == null)
                return Unauthorized(new { message = "User claims missing in token." });

            int userId = int.Parse(userIdClaim.Value);
            string userName = userNameClaim.Value;

            // ✅ Allow only specific status values on update
            var allowedStatuses = new[] { "In Progress", "Completed" };
            if (!allowedStatuses.Contains(updatedTask.Status))
            {
                return BadRequest(new { message = "Status can only be 'In Progress' or 'Completed' during update." });
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Remarks = updatedTask.Remarks;
            existingTask.Status = updatedTask.Status;

            existingTask.UpdatedOn = DateTime.Now;
            existingTask.LastUpdatedById = userId;
            existingTask.LastUpdatedByName = userName;

            _context.SaveChanges();

            return Ok(existingTask);
        }

        // DELETE: api/Task/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null) return NotFound();

            _context.TaskItems.Remove(task);
            _context.SaveChanges();

            return Ok(new { message = "Task deleted." });
        }
    }
}
