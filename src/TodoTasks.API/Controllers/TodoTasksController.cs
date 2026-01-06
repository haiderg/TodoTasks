using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TodoTasks.API.Controllers;

[ApiController]
[Route("/api")]
public class TodoTasksController : ControllerBase
{
    [HttpGet("TodoTasks")]
    public ActionResult TodoTasks()
    {
        return Content("Tasks");
    }
}