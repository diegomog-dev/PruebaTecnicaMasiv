using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;
using System.Text.Json.Nodes;

namespace PruebaTecnicaMasiv.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public SaleService _saleService;
        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> GetAllSales()
        {
            return Ok(await _saleService.GetAllSales());
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> GetSaleDetail(string id)
        {
            return Ok(await _saleService.GetSaleById(id));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public ActionResult TotalDiscount()
        {
            return Ok(new { TotalDescuentos = _saleService.TotalDiscount()});
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, User")]
        public async Task<ActionResult> CreateSale([FromBody] SalesView salesView)
        {
            Sales sale = new Sales();
            var search = _saleService.FindDiscount(salesView.NameConsole);
            if (search == null)
                return NotFound();
            if (search.PriceMax == 0)
            {
                if (salesView.PriceConsole >= search.PriceMin)
                {
                    sale.DiscountValue = (search.DiscountValue * salesView.PriceConsole) / 100;
                }
                else
                {
                    sale.DiscountValue = 0;
                }
            } else if (salesView.PriceConsole >= search.PriceMin && salesView.PriceConsole <= search.PriceMax)
            {
                sale.DiscountValue = (search.DiscountValue * salesView.PriceConsole) / 100;
            }
            sale.NameConsole = salesView.NameConsole;
            sale.Price = salesView.PriceConsole;
            sale.Total = salesView.PriceConsole - sale.DiscountValue;
            await _saleService.CreateSale(sale);
            return CreatedAtAction(nameof(GetSaleDetail), new { id = sale.Id }, new { ValorCobrarCliente = sale.Total });
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> DeleteSale(string id)
        {
            await _saleService.DeleteSale(id);
            return NoContent();
        }
    }
}
