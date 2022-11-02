using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Model;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class PurchaseController : Controller
    {

        private PayDecContext Context { get; set; }

        PurchaseController(PayDecContext context)
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            return Ok(Context.Purchase.ToList());
        }

        public IActionResult Get(int id)
        {
            return Ok(Context.Purchase.First(p => p.Id == id));
        }

        public IActionResult Post(Purchase purchase)
        {
            Context.Purchase.Add(purchase);
            Context.SaveChanges();
            return Ok();
        }

        public IActionResult Put(Purchase purchase)
        {
            Context.Update(purchase);
            Context.SaveChanges();
            return Ok();
        }

        public IActionResult Delete(Purchase purchase)
        {
            Context.Purchase.Remove(purchase);
            Context.SaveChanges();
            return Ok();
        }
    }
}
