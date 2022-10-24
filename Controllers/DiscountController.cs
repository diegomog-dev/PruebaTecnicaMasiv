using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;

namespace PruebaTecnicaMasiv.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        public DiscountService _discountService;
        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> GetAllDiscount()
        {
            return Ok(await _discountService.GetAllDiscount());
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> GetDiscountDetail(string id)
        {
            return Ok(await _discountService.GetDiscountById(id));
        }
        [HttpGet("{nameConsole}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> GetDiscountByName(string nameConsole)
        {
            return Ok(await _discountService.GetDiscountByName(nameConsole));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> DeleteDiscount(string id)
        {
            await _discountService.DeleteDiscount(id);
            return NoContent();
        }
        [HttpPost("{path}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public ActionResult ImportData(string path)
        {
            _discountService.ImportDataExcel(path);
            return Created("Created", true);
        }
    }
}
