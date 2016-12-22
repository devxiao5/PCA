namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "InvoiceImage");
        }
    }
}
