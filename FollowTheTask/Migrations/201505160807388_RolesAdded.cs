namespace FollowTheTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "AllowDeletion", c => c.Boolean());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "AllowDeletion");
        }
    }
}
