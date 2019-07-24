namespace HmsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Amneities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackageAmenities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmenityId = c.Int(nullable: false),
                        AccomodationPackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Amenities", t => t.AmenityId, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageId, cascadeDelete: true)
                .Index(t => t.AmenityId)
                .Index(t => t.AccomodationPackageId);
            
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomodationPackageAmenities", "AccomodationPackageId", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackageAmenities", "AmenityId", "dbo.Amenities");
            DropIndex("dbo.AccomodationPackageAmenities", new[] { "AccomodationPackageId" });
            DropIndex("dbo.AccomodationPackageAmenities", new[] { "AmenityId" });
            DropTable("dbo.Amenities");
            DropTable("dbo.AccomodationPackageAmenities");
        }
    }
}
