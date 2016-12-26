namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lengths : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractCostPhases", "DepreciationPhase", c => c.String(maxLength: 255));
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Projects", "Class", c => c.String(maxLength: 255));
            AlterColumn("dbo.Projects", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.Projects", "City", c => c.String(maxLength: 255));
            AlterColumn("dbo.Projects", "Zip", c => c.String(maxLength: 255));
            AlterColumn("dbo.Projects", "Notes", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Clients", "Status", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Budgets", "Type", c => c.String(maxLength: 255));
            AlterColumn("dbo.Budgets", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Budgets", "Unit", c => c.String(maxLength: 10));
            AlterColumn("dbo.Budgets", "Status", c => c.String(maxLength: 255));
            AlterColumn("dbo.Phases", "Number", c => c.String(maxLength: 255));
            AlterColumn("dbo.Phases", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.SubPhases", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.ContractCostPhases", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contracts", "Type", c => c.String(maxLength: 255));
            AlterColumn("dbo.ContractExhibits", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Contractors", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contractors", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contractors", "City", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contractors", "Zip", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contractors", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contractors", "Email", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contractors", "Website", c => c.String(maxLength: 255));
            AlterColumn("dbo.Contractors", "Notes", c => c.String(maxLength: 1000));
            AlterColumn("dbo.ContractorTypes", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.DailyReports", "Summary", c => c.String(maxLength: 1000));
            AlterColumn("dbo.DailyReports", "Status", c => c.String(maxLength: 255));
            AlterColumn("dbo.WorkItems", "Summary", c => c.String(maxLength: 1000));
            DropColumn("dbo.ContractCostPhases", "DepreceatonPhase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractCostPhases", "DepreceatonPhase", c => c.String());
            AlterColumn("dbo.WorkItems", "Summary", c => c.String());
            AlterColumn("dbo.DailyReports", "Status", c => c.String());
            AlterColumn("dbo.DailyReports", "Summary", c => c.String());
            AlterColumn("dbo.ContractorTypes", "Name", c => c.String());
            AlterColumn("dbo.Contractors", "Notes", c => c.String());
            AlterColumn("dbo.Contractors", "Website", c => c.String());
            AlterColumn("dbo.Contractors", "Email", c => c.String());
            AlterColumn("dbo.Contractors", "Phone", c => c.String());
            AlterColumn("dbo.Contractors", "Zip", c => c.String());
            AlterColumn("dbo.Contractors", "City", c => c.String());
            AlterColumn("dbo.Contractors", "Address", c => c.String());
            AlterColumn("dbo.Contractors", "Name", c => c.String());
            AlterColumn("dbo.ContractExhibits", "Description", c => c.String());
            AlterColumn("dbo.Contracts", "Type", c => c.String());
            AlterColumn("dbo.ContractCostPhases", "Description", c => c.String());
            AlterColumn("dbo.SubPhases", "Name", c => c.String());
            AlterColumn("dbo.Phases", "Name", c => c.String());
            AlterColumn("dbo.Phases", "Number", c => c.String());
            AlterColumn("dbo.Budgets", "Status", c => c.String());
            AlterColumn("dbo.Budgets", "Unit", c => c.String());
            AlterColumn("dbo.Budgets", "Description", c => c.String());
            AlterColumn("dbo.Budgets", "Type", c => c.String());
            AlterColumn("dbo.Clients", "Status", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Projects", "Notes", c => c.String());
            AlterColumn("dbo.Projects", "Zip", c => c.String());
            AlterColumn("dbo.Projects", "City", c => c.String());
            AlterColumn("dbo.Projects", "Address", c => c.String());
            AlterColumn("dbo.Projects", "Class", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false));
            DropColumn("dbo.ContractCostPhases", "DepreciationPhase");
        }
    }
}
