using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProvaCandidato.Controllers
{
    public class BaseController<T> : Controller where T : class, IEntidade
    { 
        protected readonly ContextoPrincipal db;

        public BaseController()
        {
            db = new ContextoPrincipal();
        }

        public ActionResult Index()
        {
            return View(GetAll());
        }

        public ActionResult Details(int? id)
        {
            return FindAndReturnEntity(id);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T entity)
        {
            if (ModelState.IsValid)
            {
                db.Set<T>().Add(entity);                
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, $"Registro {typeof(T).Name} incluído com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            return FindAndReturnEntity(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T entity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, $"Registro {typeof(T).Name} alterado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            return FindAndReturnEntity(id);
        }  

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            IEntidade entidade = db.Set<T>().Find(id);

            var errorOnDelete = CanDeleteRecord(entidade);
            if (string.IsNullOrWhiteSpace(errorOnDelete))
            {
                db.Entry(entidade).State = EntityState.Deleted;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, $"Registro {typeof(T).Name} excluído com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            MessageHelper.DisplaySuccessMessage(this, errorOnDelete, MessageType.Error);
            return View(entidade);
        }

        protected virtual string CanDeleteRecord(IEntidade entidade)
        {
            return string.Empty;
        }

        protected virtual List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        protected virtual ActionResult FindAndReturnEntity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEntidade entidade = db.Set<T>().Find(id);
            if (entidade == null)
            {
                return HttpNotFound();
            }
            return View(entidade);
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