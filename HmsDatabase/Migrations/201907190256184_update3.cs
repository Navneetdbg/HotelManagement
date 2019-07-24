namespace HmsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accomodations", "Booking_Id", "dbo.Bookings");
            DropIndex("dbo.Accomodations", new[] { "Booking_Id" });
            CreateIndex("dbo.BookingUsers", "BookingId");
            AddForeignKey("dbo.BookingUsers", "BookingId", "dbo.Bookings", "Id", cascadeDelete: true);
            DropColumn("dbo.Accomodations", "Booking_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accomodations", "Booking_Id", c => c.Int());
            DropForeignKey("dbo.BookingUsers", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookingUsers", new[] { "BookingId" });
            CreateIndex("dbo.Accomodations", "Booking_Id");
            AddForeignKey("dbo.Accomodations", "Booking_Id", "dbo.Bookings", "Id");
        }
    }
}
