using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto1._5.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _service;

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }
        // GET: api/<InvoiceController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetInvoice());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GeInvoicetById(int id)
        {
            try
            {
                var product = _service.GetInvoiceById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult SaveInvoice([FromBody] Invoice invoice)
        {
            try
            {
                if (invoice == null)
                    return BadRequest();

                _service.SaveInvoice(invoice);
                return Ok(invoice);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al guardar la factura" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            try
            {
                var existing = _service.GetInvoiceById(id);
                if (existing == null)
                    return NotFound();

                _service.DeleteInvoice(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Error al eliminar la factura" });
            }
        }

      
    }
}
