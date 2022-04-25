using Merchant.Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        
        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Models.Merchant>))]
        public async Task<IEnumerable<Models.Merchant>> Get()
        {
            return await _merchantService.GetMerchants();
        }

        [HttpGet("{merchantId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Merchant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Merchant>> Get(Guid merchantId)
        {
            var merchant = await _merchantService.GetMerchant(merchantId);
            if (merchant == null)
            {
                return NotFound();
            }

            return Ok(merchant);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Models.CreateMerchant merchant)
        {
            var merchantId = await _merchantService.AddMerchant(merchant);

            return CreatedAtAction(nameof(Get),new {id = merchantId }, merchantId);
        }
    }
}