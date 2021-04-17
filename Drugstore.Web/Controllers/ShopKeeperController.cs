using Drugstore.Domain.Entities;
using Drugstore.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drugstore.Web.Controllers
{
    [Route("v1/shopkeeper")]
    public class ShopKeeperController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Shopkeeper>>> Get([FromServices] DrugstoreContext context)
        {
            var shopkeepers = await context.Shopkeepers.AsNoTracking().ToListAsync();
            return Ok(shopkeepers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Shopkeeper>>Get([FromServices] DrugstoreContext context, int id)
        {
            var shopkeeper = await context.Shopkeepers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(shopkeeper == null)
                return BadRequest(new { Message = "shoopkeer not found"});
            else
                return Ok(shopkeeper);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Shopkeeper>>>Post([FromServices]DrugstoreContext context, [FromBody] Shopkeeper model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Shopkeepers.Add(model);
                await context.SaveChangesAsync();

            }catch(Exception E)
            {
                return BadRequest(new { message = "fail register shopkeeper " + E.Message});
            }

            return Ok(model);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Shopkeeper>>Put([FromServices] DrugstoreContext context, Shopkeeper model, int id)
        {
            if(model.Id != id)
                return BadRequest(new { message = "Shopkeeper not found"});
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Shopkeeper>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

            }catch(DbUpdateConcurrencyException E)
            {
                return BadRequest(new { message = E.Message});

            }catch(Exception E)
            {
                return BadRequest(new { message = E.Message});
            }

            return Ok(model);

        }

       [HttpDelete]
       [Route("{id:int}")]
       public async Task<ActionResult<Shopkeeper>>Delete([FromServices]DrugstoreContext context, int id)
        {
            var drugstore = await context.Shopkeepers.FindAsync(id);
            if(drugstore == null)
                return BadRequest(new { Message = "ShopKeeper not found"});
            try
            {
                context.Shopkeepers.Remove(drugstore);
                await context.SaveChangesAsync();
            }catch(Exception E)
            {
                return BadRequest(E.Message);
            }
            return Ok(drugstore);
        }
    }
}
