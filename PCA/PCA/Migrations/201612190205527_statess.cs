namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Initials = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.States");
        }
    }
}
