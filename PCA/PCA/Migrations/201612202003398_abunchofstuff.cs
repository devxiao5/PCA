namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abunchofstuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        PhaseId = c.Int(nullable: false),
                        SubPhaseId = c.Int(nullable: false),
                        Type = c.String(),
                        Description = c.String(),
                        Quantity = c.Double(nullable: false),
                        Unit = c.String(),
                        UnitCost = c.Double(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.Phases", t => t.PhaseId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.SubPhases", t => t.SubPhaseId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.PhaseId)
                .Index(t => t.SubPhaseId);
            
            CreateTable(
                "dbo.Phases",
                c => new
                    {
                        PhaseId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                        IsMarkup = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PhaseId);
            
            CreateTable(
                "dbo.SubPhases",
                c => new
                    {
                        SubPhaseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SubPhaseId);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ContractorId = c.Int(nullable: false, identity: true),
                        ContactAccountId = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StateId = c.Int(nullable: false),
                        Zip = c.String(),
                        Retainage = c.Double(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        IsUnion = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ContractorId)
                .ForeignKey("dbo.Accounts", t => t.ContactAccountId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.ContactAccountId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.DailyReports",
                c => new
                    {
                        DailyReportId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Summary = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DailyReportId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                        AIANumber = c.String(),
                        InvoiceNumber = c.String(),
                        AccountNumber = c.String(),
                        OrderNumber = c.String(),
                        TotalAmount = c.Double(nullable: false),
                        TermInDays = c.Int(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        DateOfInvoice = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.ContractorId);
            
            CreateTable(
                "dbo.WorkItems",
                c => new
                    {
                        WorkItemId = c.Int(nullable: false, identity: true),
                        DailyReportId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                        Summary = c.String(),
                        Performance = c.Int(nullable: false),
                        MenWorked = c.Int(nullable: false),
                        HoursWorked = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.WorkItemId);
            
            AddColumn("dbo.Accounts", "CanLogin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "BillingOptionIsOne", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "PreFeeIsFlat", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "PreFeeValue", c => c.Double(nullable: false));
            AddColumn("dbo.Projects", "ConstFeeIsFlat", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "ConstFeeValue", c => c.Double(nullable: false));
            AddColumn("dbo.Projects", "MarkupValue", c => c.Double(nullable: false));
            AddColumn("dbo.Projects", "PaymentDueDay", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "ContactPerson");
            DropColumn("dbo.Clients", "AppAccess");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "AppAccess", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "ContactPerson", c => c.String());
            DropForeignKey("dbo.Invoices", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Invoices", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.DailyReports", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Contractors", "StateId", "dbo.States");
            DropForeignKey("dbo.Contractors", "ContactAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Budgets", "SubPhaseId", "dbo.SubPhases");
            DropForeignKey("dbo.Budgets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Budgets", "PhaseId", "dbo.Phases");
            DropIndex("dbo.Invoices", new[] { "ContractorId" });
            DropIndex("dbo.Invoices", new[] { "ProjectId" });
            DropIndex("dbo.DailyReports", new[] { "ProjectId" });
            DropIndex("dbo.Contractors", new[] { "StateId" });
            DropIndex("dbo.Contractors", new[] { "ContactAccountId" });
            DropIndex("dbo.Budgets", new[] { "SubPhaseId" });
            DropIndex("dbo.Budgets", new[] { "PhaseId" });
            DropIndex("dbo.Budgets", new[] { "ProjectId" });
            DropColumn("dbo.Projects", "PaymentDueDay");
            DropColumn("dbo.Projects", "MarkupValue");
            DropColumn("dbo.Projects", "ConstFeeValue");
            DropColumn("dbo.Projects", "ConstFeeIsFlat");
            DropColumn("dbo.Projects", "PreFeeValue");
            DropColumn("dbo.Projects", "PreFeeIsFlat");
            DropColumn("dbo.Projects", "BillingOptionIsOne");
            DropColumn("dbo.Accounts", "CanLogin");
            DropTable("dbo.WorkItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.DailyReports");
            DropTable("dbo.Contractors");
            DropTable("dbo.SubPhases");
            DropTable("dbo.Phases");
            DropTable("dbo.Budgets");
        }
    }
}
