namespace Auth4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changekeysinrotters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rotters", "PertakerId", c => c.String(nullable: false));
            AlterColumn("dbo.Rotters", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rotters", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rotters", "PertakerId", c => c.Int(nullable: false));
        }
    }
}
