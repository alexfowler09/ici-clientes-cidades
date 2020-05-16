using ProvaCandidato.Data.Entidade;
using System.Data.Entity.Migrations;

namespace ProvaCandidato.Data.Migrations
{   
    internal sealed class Configuration : DbMigrationsConfiguration<ProvaCandidato.Data.ContextoPrincipal>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContextoPrincipal context)
        {         
        }
    }
}
