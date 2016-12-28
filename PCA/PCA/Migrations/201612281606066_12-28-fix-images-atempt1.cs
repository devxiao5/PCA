namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1228fiximagesatempt1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileSignatures", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Files", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.FileSignatures", new[] { "AccountId" });
            DropIndex("dbo.Files", new[] { "InvoiceId" });
            AddColumn("dbo.Files", "AccountId", c => c.Int());
            AlterColumn("dbo.Files", "InvoiceId", c => c.Int());
            CreateIndex("dbo.Files", "InvoiceId");
            CreateIndex("dbo.Files", "AccountId");
            AddForeignKey("dbo.Files", "AccountId", "dbo.Accounts", "AccountId");
            AddForeignKey("dbo.Files", "InvoiceId", "dbo.Invoices", "InvoiceId");
            DropTable("dbo.FileSignatures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileSignatures",
                c => new
                    {
                        FileSignatureId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileTypeSignature = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileSignatureId);
            
            DropForeignKey("dbo.Files", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Files", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Files", new[] { "AccountId" });
            DropIndex("dbo.Files", new[] { "InvoiceId" });
            AlterColumn("dbo.Files", "InvoiceId", c => c.Int(nullable: false));
            DropColumn("dbo.Files", "AccountId");
            CreateIndex("dbo.Files", "InvoiceId");
            CreateIndex("dbo.FileSignatures", "AccountId");
            AddForeignKey("dbo.Files", "InvoiceId", "dbo.Invoices", "InvoiceId", cascadeDelete: true);
            AddForeignKey("dbo.FileSignatures", "AccountId", "dbo.Accounts", "AccountId", cascadeDelete: true);
        }
    }
}
