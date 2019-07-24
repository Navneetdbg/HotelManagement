namespace HmsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccomodationId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.AccomodationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingUsers", "AccomodationId", "dbo.Accomodations");
            DropIndex("dbo.BookingUsers", new[] { "AccomodationId" });
            DropTable("dbo.BookingUsers");
        }
    }
}
