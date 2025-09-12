using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;

namespace Proyecto1._5.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : Controller
    {

        private IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

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
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _service.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }


        [HttpPut("{id}")]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                _service.SaveProduct(product); 
                return Ok(product); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al guardar el producto" });
            }
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var existing = _service.GetProductById(id);
                if (existing == null)
                    return NotFound();

                _service.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al eliminar el producto" });
            }
        }



    }

}
