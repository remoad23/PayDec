using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Model;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class ItemController : Controller
    {
        private PayDecContext Context { get; set; }

        ItemController(PayDecContext context)
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            return Ok(Context.Item.ToList());
        }

        public IActionResult Get(int id)
        {
            return Ok(Context.Item.First(p => p.Id == id));
        }

        public IActionResult Post(Item item)
        {
            Context.Item.Add(item);
            Context.SaveChanges();
            return Ok();
        }

        public IActionResult Put(Item item)
        {
            Context.Update(item);
            Context.SaveChanges();
            return Ok();
        }

        public IActionResult Delete(Item item)
        {
            Context.Item.Remove(item);
            Context.SaveChanges();
            return Ok();
        }
    }
}
