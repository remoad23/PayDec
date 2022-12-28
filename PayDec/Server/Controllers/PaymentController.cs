using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using PayDec.Server.Attributes;
using PayDec.Server.Model;
using PayDec.Server.Services;
using PayDec.Server.Services.Interfaces;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class PaymentController : Controller
    {
        private IBlockchainTransaction Transaction { get; set; }
        private PayDecContext Context { get; set; }
        public PaymentController(IBlockchainTransaction transction,PayDecContext context)
        {
            this.Transaction = transction;
            Context = context;
        }

        [Route("Pay")]
        [TypeFilter(typeof(JwtAuthorize))]
        public async Task<IActionResult> Pay([FromBody] string purchases)
        {
            List<Purchase> deserializedList = JsonSerializer.Deserialize<List<Purchase>>(purchases) ?? new();

            Context.Purchase.AddRange(deserializedList);
            Context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("GetBalance")]
        public async Task<ActionResult<long>> GetBalance([FromQuery] string contractAdress)
        {
            var balance = await Transaction.GetBalance(contractAdress);
            long output = (long)balance;
            return Ok(output);
        }
    }
}
