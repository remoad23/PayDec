using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Model;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class PurchaseController : Controller
    {

        private PayDecContext Context { get; set; }

        public PurchaseController(PayDecContext context)
        {
            this.Context = context;
        }

        [Route("Purchases")]
        public IActionResult Index()
        {
            return Ok(Context.Purchase.ToList());
        }

        [Route("Purchase")]
        public IActionResult Get([FromBody]int id)
        {
            return Ok(Context.Purchase.First(p => p.Id == id));
        }

        [Route("Purchase/Create")]
        public IActionResult Post(Purchase purchase)
        {
            Context.Purchase.Add(purchase);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Purchase/Change")]
        public IActionResult Put(Purchase purchase)
        {
            Context.Update(purchase);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Purchase/Delete")]
        public IActionResult Delete(Purchase purchase)
        {
            Context.Purchase.Remove(purchase);
            Context.SaveChanges();
            return Ok();
        }
    }
}
