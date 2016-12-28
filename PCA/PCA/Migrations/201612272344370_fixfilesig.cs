namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixfilesig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileSignatures", "FileTypeSignature", c => c.Int(nullable: false));
            DropColumn("dbo.FileSignatures", "FileType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileSignatures", "FileType", c => c.Int(nullable: false));
            DropColumn("dbo.FileSignatures", "FileTypeSignature");
        }
    }
}
