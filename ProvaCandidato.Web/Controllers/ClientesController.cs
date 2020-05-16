using ProvaCandidato.Data.Entidade;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : BaseController<Cliente>
    {
        public ClientesController()
        {
            ViewBag.Cidades = new SelectList(new List<Cidade> { new Cidade { Codigo = -1, Nome = "Selecione uma cidade" } }, "codigo", "nome", -1);
        }

        protected override List<Cliente> GetAll()
        {
            return db.Clientes
                        .Include(x => x.Cidade)
                        .Include(x => x.Observacoes)
                        .ToList();
        }

        protected override ActionResult FindAndReturnEntity(int? id)
        {
            if (id == null)            
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cliente = db.Clientes                            
                            .Include(x => x.Cidade)
                            .Include(x => x.Observacoes)
                            .FirstOrDefault(x => x.Codigo == id);

            if (cliente == null)           
                return HttpNotFound();
            
            return View(cliente);
        }

        public ActionResult ClientesFiltroNome(string nome)
        {
            List<Cliente> clientes;

            if (!string.IsNullOrWhiteSpace(nome))
                clientes = db.Clientes.Where(x => x.Nome.ToLower().Contains(nome.ToLower())).ToList();
            else
                clientes = db.Clientes.ToList();

            ViewBag.txtFiltro = nome;

            return View("Index", clientes.ToList());
        }

        public PartialViewResult CarregarClienteObservacaoPartialView()
        {
            return PartialView("ClienteObservacaoView", new ClienteObservacao());
        }

        [HttpPost]
        public JsonResult AdicionarClienteObservacao(ClienteObservacao clienteObservacao)
        {
            if (ModelState.IsValid)
            {
                db.ClienteObservacaos.Add(clienteObservacao);
                db.SaveChanges();

                return Json(new { result = true, responseText = string.Empty });
            }

            var errorString = string.Empty;
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            foreach(var error in errors)
            {
                foreach(var erro in error)                
                    errorString += (errorString.Trim() == "" ? "" : "\n") + erro.ErrorMessage;
            }

            return Json(new { result = false, responseText = errorString });
        }
    }
}