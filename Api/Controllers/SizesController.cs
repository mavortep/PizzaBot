using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Infrastructure;

namespace Api.Controllers
{
    public class SizesController : ApiController
    {
        private ApiProductsEntities db = new ApiProductsEntities();

        // GET: api/Sizes
        public IQueryable<Size> GetSizes()
        {
            return db.Sizes;
        }

        // GET: api/Sizes/5
        [ResponseType(typeof(Size))]
        public async Task<IHttpActionResult> GetSize(int id)
        {
            Size size = await db.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            return Ok(size);
        }

        // PUT: api/Sizes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSize(int id, Size size)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != size.Id)
            {
                return BadRequest();
            }

            db.Entry(size).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sizes
        [ResponseType(typeof(Size))]
        public async Task<IHttpActionResult> PostSize(Size size)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sizes.Add(size);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = size.Id }, size);
        }

        // DELETE: api/Sizes/5
        [ResponseType(typeof(Size))]
        public async Task<IHttpActionResult> DeleteSize(int id)
        {
            Size size = await db.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            db.Sizes.Remove(size);
            await db.SaveChangesAsync();

            return Ok(size);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SizeExists(int id)
        {
            return db.Sizes.Count(e => e.Id == id) > 0;
        }
    }
}