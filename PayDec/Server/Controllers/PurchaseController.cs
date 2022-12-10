using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Attributes;
using PayDec.Server.Model;
using PayDec.Shared.Model;
using System.Text.Json;

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
        [TypeFilter(typeof(JwtAuthorize))]
        public IActionResult Index()
        {
            var purchases = Context.Purchase;
            return Ok(purchases);
        }

        [Route("Purchase")]
        [TypeFilter(typeof(JwtAuthorize))]
        public IActionResult Get([FromBody] int id)
        {
            return Ok(Context.Purchase.First(p => p.Id == id));
        }

        [Route("Purchase/Create")]
        public IActionResult Post([FromBody] List<Purchase> purchase)
        {
            Context.Purchase.AddRange(purchase);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Purchases/Create")]
        public IActionResult PostList([FromBody] string serializedList)
        {
            var deserializedList = JsonSerializer.Deserialize<List<Purchase>>(serializedList) ?? new List<Purchase>();
            Context.Purchase.AddRange(deserializedList);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Purchase/Change")]
        [TypeFilter(typeof(JwtAuthorize))]
        public IActionResult Put(Purchase purchase)
        {
            Context.Update(purchase);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Purchase/Delete")]
        [TypeFilter(typeof(JwtAuthorize))]
        public IActionResult Delete(Purchase purchase)
        {
            Context.Purchase.Remove(purchase);
            Context.SaveChanges();
            return Ok();
        }
    }
}
