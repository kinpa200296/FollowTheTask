namespace FollowTheTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Managers", new[] { "UserId" });
            DropIndex("dbo.Workers", new[] { "UserId" });
            AddColumn("dbo.AspNetUsers", "ManagerId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "WorkerId", c => c.Int());
            AlterColumn("dbo.Managers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Workers", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Managers", "UserId");
            CreateIndex("dbo.AspNetUsers", "ManagerId");
            CreateIndex("dbo.AspNetUsers", "WorkerId");
            CreateIndex("dbo.Workers", "UserId");
            AddForeignKey("dbo.AspNetUsers", "ManagerId", "dbo.Managers", "Id");
            AddForeignKey("dbo.AspNetUsers", "WorkerId", "dbo.Workers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.AspNetUsers", "ManagerId", "dbo.Managers");
            DropIndex("dbo.Workers", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "WorkerId" });
            DropIndex("dbo.AspNetUsers", new[] { "ManagerId" });
            DropIndex("dbo.Managers", new[] { "UserId" });
            AlterColumn("dbo.Workers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Managers", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "WorkerId");
            DropColumn("dbo.AspNetUsers", "ManagerId");
            CreateIndex("dbo.Workers", "UserId");
            CreateIndex("dbo.Managers", "UserId");
        }
    }
}
