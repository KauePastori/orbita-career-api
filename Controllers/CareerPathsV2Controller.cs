using Microsoft.AspNetCore.Mvc;

namespace Orbita.CareerApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CareerPathsV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "Esta é a versão 2 da API de rotas de carreira.",
                version = "2.0",
                generatedAt = DateTime.UtcNow
            });
        }
    }
}
