namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "CreateDate");
        }
    }
}
