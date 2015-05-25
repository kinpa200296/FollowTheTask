namespace FollowTheTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksQuestsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quests", "DeadLine", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quests", "HoursSpent", c => c.Int(nullable: false));
            AddColumn("dbo.TrackedTasks", "DeadLine", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrackedTasks", "HoursSpent", c => c.Int(nullable: false));
            DropColumn("dbo.Quests", "TimeSpent");
            DropColumn("dbo.TrackedTasks", "TimeSpent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackedTasks", "TimeSpent", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quests", "TimeSpent", c => c.DateTime(nullable: false));
            DropColumn("dbo.TrackedTasks", "HoursSpent");
            DropColumn("dbo.TrackedTasks", "DeadLine");
            DropColumn("dbo.Quests", "HoursSpent");
            DropColumn("dbo.Quests", "DeadLine");
        }
    }
}
