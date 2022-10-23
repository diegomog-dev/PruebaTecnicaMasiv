using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;

namespace PruebaTecnicaMasiv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public SaleService _saleService;
        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllSales()
        {
            return Ok(await _saleService.GetAllSales());
        }
        [HttpGet("GetSaleDetail{id}")]
        public async Task<ActionResult> GetSaleDetail(string id)
        {
            return Ok(await _saleService.GetSaleById(id));
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateSale([FromBody] Sales sales)
        {
            if (sales == null)
                return BadRequest();
            if (sales.NameConsole == string.Empty)
            {
                ModelState.AddModelError("Console", "The sale shouldn't be empty");
            }
            await _saleService.CreateSale(sales);
            return Created("Created", true);
        }
        [HttpPut("UpdateSale{id}")]
        public async Task<ActionResult> UpdateSale([FromBody] Sales sale, string id)
        {
            if (sale == null)
                return BadRequest();
            if (sale.NameConsole == string.Empty)
            {
                ModelState.AddModelError("Console", "The Sale shouldn't be empty");
            }
            sale.Id = id;
            await _saleService.UpdateSale(sale.Id, sale);
            return Ok();
        }
        [HttpDelete("DeleteSale{id}")]
        public async Task<ActionResult> DeleteSale(string id)
        {
            await _saleService.DeleteSale(id);
            return NoContent();
        }
    }
}
