namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "FirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "LastName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "Username", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "ConfirmPassword", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Accounts", "Type", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "ConfirmPassword", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "FirstName", c => c.String(nullable: false));
        }
    }
}
