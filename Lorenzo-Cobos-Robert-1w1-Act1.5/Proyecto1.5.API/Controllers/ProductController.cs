using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;

namespace Proyecto1._5.API.Controllers
{
    public class ProductController : Controller
    {

        private IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        // GET: api/<ComponentesvController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetProducts());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }
    }
}
