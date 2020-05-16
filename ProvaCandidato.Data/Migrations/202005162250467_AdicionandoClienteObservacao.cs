using System.Data.Entity.Migrations;

namespace ProvaCandidato.Data.Migrations
{
    public partial class AdicionandoClienteObservacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClienteObservacao",
                c => new
                    {
                        codigo = c.Int(nullable: false, identity: true),
                        observacao = c.String(nullable: false, maxLength: 300),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.codigo)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteObservacao", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.ClienteObservacao", new[] { "ClienteId" });
            DropTable("dbo.ClienteObservacao");
        }
    }
}
