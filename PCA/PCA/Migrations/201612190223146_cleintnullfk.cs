namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleintnullfk : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clients", new[] { "ParentId" });
            AlterColumn("dbo.Clients", "ParentId", c => c.Int());
            CreateIndex("dbo.Clients", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "ParentId" });
            AlterColumn("dbo.Clients", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "ParentId");
        }
    }
}
