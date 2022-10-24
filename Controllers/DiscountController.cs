using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;

namespace PruebaTecnicaMasiv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        public DiscountService _discountService;
        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllDiscount()
        {
            return Ok(await _discountService.GetAllDiscount());
        }
        [HttpGet("GetDiscountDetail{id}")]
        public async Task<ActionResult> GetDiscountDetail(string id)
        {
            return Ok(await _discountService.GetDiscountById(id));
        }
        [HttpGet("GetDiscountByName{nameConsole}")]
        public async Task<ActionResult> GetDiscountByName(string nameConsole)
        {
            return Ok(await _discountService.GetDiscountByName(nameConsole));
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateDiscount([FromBody] Discount discount)
        {
            if (discount == null)
                return BadRequest();
            if (discount.Console == string.Empty)
            {
                ModelState.AddModelError("Consele", "The discount shouldn't be empty");
            }
            await _discountService.CreateDiscount(discount);
            return Created("Created", true);
        }
        [HttpPut("UpdateDiscount{id}")]
        public async Task<ActionResult> UpdateDiscount([FromBody]Discount discount, string id)
        {
            if (discount == null)
                return BadRequest();
            if (discount.Console == string.Empty)
            {
                ModelState.AddModelError("Consele", "The discount shouldn't be empty");
            }
            discount.Id = id;
            await _discountService.UpdateDiscount(discount.Id, discount);
            return Ok();
        }
        [HttpDelete("DeleteDiscount{id}")]
        public async Task<ActionResult> DeleteDiscount(string id)
        {
            await _discountService.DeleteDiscount(id);
            return NoContent();
        }
        [HttpPost("ImportData{path}")]
        public ActionResult ImportData(string path)
        {
            _discountService.ImportDataExcel(path);
            return Created("Created", true);
        }
    }
}
