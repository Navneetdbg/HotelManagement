namespace HmsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "UserId", c => c.String());
            AddColumn("dbo.Bookings", "Users_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "Users_Id");
            AddForeignKey("dbo.Bookings", "Users_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Users_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "Users_Id" });
            DropColumn("dbo.Bookings", "Users_Id");
            DropColumn("dbo.Bookings", "UserId");
        }
    }
}
