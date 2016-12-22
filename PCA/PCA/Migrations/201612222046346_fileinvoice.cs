namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileinvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
            DropColumn("dbo.Invoices", "InvoiceImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "InvoiceImage", c => c.Binary());
            DropForeignKey("dbo.Files", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Files", new[] { "InvoiceId" });
            DropTable("dbo.Files");
        }
    }
}
