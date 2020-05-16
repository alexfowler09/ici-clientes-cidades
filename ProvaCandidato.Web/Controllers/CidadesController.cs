using ProvaCandidato.Data.Entidade;
using System.Linq;

namespace ProvaCandidato.Controllers
{
    public class CidadesController : BaseController<Cidade>
    {
        protected override string CanDeleteRecord(IEntidade entidade)
        {
            if (db.Clientes.Any(x => x.CidadeId == entidade.Codigo))
                return "Registro não pode ser excluído pois existe um cliente vinculado a cidade";

            return string.Empty;
        }
    }
}
