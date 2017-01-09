namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subphasename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubPhases", "SubPhaseName", c => c.String(maxLength: 255));
            DropColumn("dbo.SubPhases", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubPhases", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.SubPhases", "SubPhaseName");
        }
    }
}
