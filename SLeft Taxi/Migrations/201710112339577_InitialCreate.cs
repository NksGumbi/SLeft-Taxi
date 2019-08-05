namespace SLeft_Taxi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        paymentId = c.Int(nullable: false, identity: true),
                        paymentMode = c.String(),
                        totalAmount = c.Double(nullable: false),
                        bankDetails = c.String(),
                    })
                .PrimaryKey(t => t.paymentId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ticketId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        taxiId = c.Int(nullable: false),
                        scheduleId = c.Int(nullable: false),
                        paymentId = c.Int(nullable: false),
                        dateOfJourney = c.DateTime(nullable: false),
                        passengerName = c.String(),
                        phoneNumber = c.String(),
                        address = c.String(),
                        emergencyContact = c.String(),
                    })
                .PrimaryKey(t => t.ticketId)
                .ForeignKey("dbo.Payment", t => t.paymentId)
                .ForeignKey("dbo.Schedule", t => t.scheduleId)
                .ForeignKey("dbo.Taxi", t => t.taxiId)
                .Index(t => t.taxiId)
                .Index(t => t.scheduleId)
                .Index(t => t.paymentId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        scheduleId = c.Int(nullable: false, identity: true),
                        taxiId = c.Int(nullable: false),
                        source = c.String(nullable: false),
                        sourceId = c.Int(nullable: false),
                        destination = c.String(nullable: false),
                        destinationId = c.Int(nullable: false),
                        scheduleDate = c.DateTime(nullable: false),
                        depatureTime = c.Time(nullable: false, precision: 7),
                        arrivalDate = c.DateTime(nullable: false),
                        arrivalTime = c.Time(nullable: false, precision: 7),
                        status = c.String(nullable: false),
                        cost = c.Double(nullable: false),
                        AvailSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.scheduleId)
                .ForeignKey("dbo.Taxi", t => t.taxiId)
                .Index(t => t.taxiId);
            
            CreateTable(
                "dbo.Taxi",
                c => new
                    {
                        taxiId = c.Int(nullable: false, identity: true),
                        taxiName = c.String(nullable: false),
                        AvailableSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.taxiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "taxiId", "dbo.Taxi");
            DropForeignKey("dbo.Ticket", "scheduleId", "dbo.Schedule");
            DropForeignKey("dbo.Schedule", "taxiId", "dbo.Taxi");
            DropForeignKey("dbo.Ticket", "paymentId", "dbo.Payment");
            DropIndex("dbo.Schedule", new[] { "taxiId" });
            DropIndex("dbo.Ticket", new[] { "paymentId" });
            DropIndex("dbo.Ticket", new[] { "scheduleId" });
            DropIndex("dbo.Ticket", new[] { "taxiId" });
            DropTable("dbo.Taxi");
            DropTable("dbo.Schedule");
            DropTable("dbo.Ticket");
            DropTable("dbo.Payment");
        }
    }
}
