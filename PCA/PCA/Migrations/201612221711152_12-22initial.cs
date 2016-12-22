namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1222initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractCostPhases",
                c => new
                    {
                        ContractCostPhaseId = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        PhaseId = c.Int(nullable: false),
                        SubPhaseId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Description = c.String(),
                        DepreceatonPhase = c.String(),
                    })
                .PrimaryKey(t => t.ContractCostPhaseId)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.Phases", t => t.PhaseId, cascadeDelete: true)
                .ForeignKey("dbo.SubPhases", t => t.SubPhaseId, cascadeDelete: true)
                .Index(t => t.ContractId)
                .Index(t => t.PhaseId)
                .Index(t => t.SubPhaseId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ContractId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        OwnerSigAccountId = c.Int(nullable: false),
                        ContractorSigAccountId = c.Int(nullable: false),
                        Type = c.String(),
                        OwnerSignDate = c.DateTime(nullable: false),
                        ContractorSignDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.ContractorSigAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.OwnerSigAccountId, cascadeDelete: false)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .Index(t => t.ClientId)
                .Index(t => t.ProjectId)
                .Index(t => t.OwnerSigAccountId)
                .Index(t => t.ContractorSigAccountId);
            
            CreateTable(
                "dbo.ContractExhibits",
                c => new
                    {
                        ContractExhibitId = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        SubNumber = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ContractExhibitId)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.ContractorTypes",
                c => new
                    {
                        ContractorTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ContractorTypeId);
            
            CreateTable(
                "dbo.ContractorTypeUnions",
                c => new
                    {
                        ContractorTypeUnionId = c.Int(nullable: false, identity: true),
                        ContractorId = c.Int(nullable: false),
                        ContractorTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContractorTypeUnionId)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.ContractorTypes", t => t.ContractorTypeId, cascadeDelete: true)
                .Index(t => t.ContractorId)
                .Index(t => t.ContractorTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractorTypeUnions", "ContractorTypeId", "dbo.ContractorTypes");
            DropForeignKey("dbo.ContractorTypeUnions", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.ContractExhibits", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractCostPhases", "SubPhaseId", "dbo.SubPhases");
            DropForeignKey("dbo.ContractCostPhases", "PhaseId", "dbo.Phases");
            DropForeignKey("dbo.ContractCostPhases", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Contracts", "OwnerSigAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contracts", "ContractorSigAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contracts", "ClientId", "dbo.Clients");
            DropIndex("dbo.ContractorTypeUnions", new[] { "ContractorTypeId" });
            DropIndex("dbo.ContractorTypeUnions", new[] { "ContractorId" });
            DropIndex("dbo.ContractExhibits", new[] { "ContractId" });
            DropIndex("dbo.Contracts", new[] { "ContractorSigAccountId" });
            DropIndex("dbo.Contracts", new[] { "OwnerSigAccountId" });
            DropIndex("dbo.Contracts", new[] { "ProjectId" });
            DropIndex("dbo.Contracts", new[] { "ClientId" });
            DropIndex("dbo.ContractCostPhases", new[] { "SubPhaseId" });
            DropIndex("dbo.ContractCostPhases", new[] { "PhaseId" });
            DropIndex("dbo.ContractCostPhases", new[] { "ContractId" });
            DropTable("dbo.ContractorTypeUnions");
            DropTable("dbo.ContractorTypes");
            DropTable("dbo.ContractExhibits");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractCostPhases");
        }
    }
}
