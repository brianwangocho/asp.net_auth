namespace Auth4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvirtualtorotter1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rotters", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Rotters", new[] { "TaskId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Rotters", "TaskId");
            AddForeignKey("dbo.Rotters", "TaskId", "dbo.Tasks", "Id", cascadeDelete: true);
        }
    }
}
