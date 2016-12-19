namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleintens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ParentId = c.Int(nullable: false),
                        Address = c.String(maxLength: 255),
                        City = c.String(maxLength: 50),
                        StateId = c.Int(nullable: false),
                        Zip = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        ContactPerson = c.String(),
                        Notes = c.String(),
                        ClientSinceDate = c.String(),
                        AppAccess = c.Boolean(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Clients", t => t.ParentId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "StateId", "dbo.States");
            DropForeignKey("dbo.Clients", "ParentId", "dbo.Clients");
            DropIndex("dbo.Clients", new[] { "StateId" });
            DropIndex("dbo.Clients", new[] { "ParentId" });
            DropTable("dbo.Clients");
        }
    }
}
