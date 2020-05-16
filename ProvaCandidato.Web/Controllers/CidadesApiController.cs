using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProvaCandidato.Controllers
{
    public class CidadesApiController : ApiController
    {
        private readonly ContextoPrincipal db = new ContextoPrincipal();        
        
        public IQueryable<Cidade> GetCidades()
        {
            return db.Cidades;
        }
        
        [ResponseType(typeof(Cidade))]
        public IHttpActionResult GetCidade(int id)
        {
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return Ok(cidade);
        }
        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCidade(int id, Cidade cidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cidade.Codigo)
            {
                return BadRequest();
            }

            db.Entry(cidade).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeExists(id))
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
        
        [ResponseType(typeof(Cidade))]
        public IHttpActionResult PostCidade(Cidade cidade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cidades.Add(cidade);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cidade.Codigo }, cidade);
        }
        
        [ResponseType(typeof(Cidade))]
        public IHttpActionResult DeleteCidade(int id)
        {
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return NotFound();
            }

            db.Cidades.Remove(cidade);
            db.SaveChanges();

            return Ok(cidade);
        }

        private bool CidadeExists(int id)
        {
            return db.Cidades.Count(e => e.Codigo == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}