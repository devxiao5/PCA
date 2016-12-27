namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractstuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Contracts", "TotalAmountLiteral", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "TotalAmountLiteral");
            DropColumn("dbo.Contracts", "TotalAmount");
        }
    }
}
