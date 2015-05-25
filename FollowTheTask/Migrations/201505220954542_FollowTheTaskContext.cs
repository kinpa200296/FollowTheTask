namespace FollowTheTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FollowTheTaskContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Quests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        TimeSpent = c.DateTime(nullable: false),
                        TrackedTaskId = c.Int(nullable: false),
                        WorkerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrackedTasks", t => t.TrackedTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.TrackedTaskId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.TrackedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        TimeSpent = c.DateTime(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Managers", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quests", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Workers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Workers", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Quests", "TrackedTaskId", "dbo.TrackedTasks");
            DropForeignKey("dbo.TrackedTasks", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Workers", new[] { "ManagerId" });
            DropIndex("dbo.Workers", new[] { "UserId" });
            DropIndex("dbo.TrackedTasks", new[] { "ManagerId" });
            DropIndex("dbo.Quests", new[] { "WorkerId" });
            DropIndex("dbo.Quests", new[] { "TrackedTaskId" });
            DropIndex("dbo.Managers", new[] { "UserId" });
            DropTable("dbo.Workers");
            DropTable("dbo.TrackedTasks");
            DropTable("dbo.Quests");
            DropTable("dbo.Managers");
        }
    }
}
