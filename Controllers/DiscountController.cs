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
        public ActionResult<List<Discount>> Get()
        {
            return _discountService.Get();
        }
        [HttpPost]
        public ActionResult<Discount> Create(Discount discount)
        {
            _discountService.Create(discount);
            return Ok(discount);
        }
        [HttpPut]
        public ActionResult Update(Discount discount)
        {
            _discountService.Update(discount.Id, discount);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _discountService.Delete(id);
            return Ok();
        }
    }
}
