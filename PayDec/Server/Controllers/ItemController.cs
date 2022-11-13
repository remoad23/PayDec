using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Model;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class ItemController : Controller
    {
        private PayDecContext Context { get; set; }

        public ItemController(PayDecContext context)
        {
            this.Context = context;
        }

        [Route("Items")]
        public IActionResult Index()
        {
            var items = Context.Item;
            return Ok(items);
        }

        [Route("Item")]
        public IActionResult Get([FromBody]int id)
        {
            return Ok(Context.Item.First(p => p.Id == id));
        }

        [Route("Item/Create")]
        public IActionResult Post(Item item)
        {
            Context.Item.Add(item);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Item/Change")]
        public IActionResult Put(Item item)
        {
            Context.Update(item);
            Context.SaveChanges();
            return Ok();
        }

        [Route("Item/Delete")]
        public IActionResult Delete(Item item)
        {
            Context.Item.Remove(item);
            Context.SaveChanges();
            return Ok();
        }
    }
}
