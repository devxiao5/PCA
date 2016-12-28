namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filesigs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileSignatures",
                c => new
                    {
                        FileSignatureId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileSignatureId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileSignatures", "AccountId", "dbo.Accounts");
            DropIndex("dbo.FileSignatures", new[] { "AccountId" });
            DropTable("dbo.FileSignatures");
        }
    }
}
