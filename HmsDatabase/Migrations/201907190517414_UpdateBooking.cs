namespace HmsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bookings", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Duration", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "EndDate");
        }
    }
}
