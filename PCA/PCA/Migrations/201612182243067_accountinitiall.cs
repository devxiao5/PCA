namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountinitiall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Type");
        }
    }
}
