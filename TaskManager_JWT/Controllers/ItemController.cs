using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager_JWT.Models;
using TaskManager_JWT.Services;

namespace TaskManager_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _item;
        public ItemController(IItem item)
        {
            _item = item;
        }

        [HttpPost("createTask")]
        [Authorize]
        public IActionResult createTask(Item task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("invalid data");
                }
                _item.createTask(task);
                return Ok("task created successFully");

            }catch(Exception ex)
            {
                return StatusCode(501, $"Internal Server Error {ex}");
            }
        }
        [HttpGet("getAllTask")]
        [Authorize(Roles ="admin")]
        public IActionResult getAllItem() {

            try
            {
                return Ok(_item.getAllItem());
            }catch(Exception ex)
            {
                return StatusCode(501, $"Internal server Error");
            }
        }
        [HttpGet("getById")]
        [Authorize]
        public IActionResult getById(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("invalid request");
                }
                return Ok(_item.getById(id));
            }catch(Exception ex) {
                return StatusCode(501, $"Internal Server Error {ex.Message}");
            }
        }
        [HttpPut("update")]
        [Authorize]
        public IActionResult updateItem(int id,Item item)
        {
            try
            {
                if (id == null && item == null)
                {
                    return BadRequest("invalid request");
                }
                _item.updateItem(id, item);
                return Ok("item updated");
            }catch (Exception ex) {
                return StatusCode(501, $"internal SErverError {ex.Message}");
            }
        }
        [HttpDelete("deleteId")]
        [Authorize]
        public IActionResult deleteItem(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("invalid id");
                }
                _item.deleteItem(id);
                return Ok("deletion completed");
                 
            }catch(Exception ex)
            {
                return StatusCode(501, $"internal server Error {ex.Message}");
            }
        }
    }
}
