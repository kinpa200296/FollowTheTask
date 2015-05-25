namespace FollowTheTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksQuestsUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quests", "CompletionDate", c => c.DateTime());
            AlterColumn("dbo.Quests", "HoursSpent", c => c.Int());
            AlterColumn("dbo.TrackedTasks", "CompletionDate", c => c.DateTime());
            AlterColumn("dbo.TrackedTasks", "HoursSpent", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrackedTasks", "HoursSpent", c => c.Int(nullable: false));
            AlterColumn("dbo.TrackedTasks", "CompletionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Quests", "HoursSpent", c => c.Int(nullable: false));
            AlterColumn("dbo.Quests", "CompletionDate", c => c.DateTime(nullable: false));
        }
    }
}
