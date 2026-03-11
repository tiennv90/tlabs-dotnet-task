using Microsoft.AspNetCore.Mvc;
using TestApp.ToDoList.Entity;
using TestApp.ToDoList.Module;

namespace TestApp.Server.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagTracker tagTracker;

        public TagsController(ITagTracker tagTracker)
        {
            this.tagTracker = tagTracker;
        }

        [HttpPost("{taskId}")]
        public Tag AddTag(int taskId, [FromBody] string tagName)
        {
            var tag = tagTracker.AddTag(taskId, tagName);
            return tag;
        }

        [HttpDelete("{taskId}/{tagName}")]
        public Tag RemoveTag(int taskId, string tagName)
        {
            var tag = tagTracker.RemoveTag(taskId, tagName);
            return tag;
        }
    }
}
