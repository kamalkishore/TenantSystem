namespace TenantSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NumberOfFloors = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ElectricMeters",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        MeterType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        NickName = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        MeterId = c.Int(nullable: false),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ElectricMeters", t => t.MeterId, cascadeDelete: true)
                .Index(t => t.MeterId);
            
            CreateTable(
                "dbo.TenantBills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        TenantName = c.String(),
                        PreviousMonthReadingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PreviousMonthReading = c.Long(nullable: false),
                        CurrentMonthReadingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentMonthReading = c.Long(nullable: false),
                        LastPaidAmount = c.Double(nullable: false),
                        LastPaidAmountDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentMonthPayableAmount = c.Double(nullable: false),
                        PreviousMonthPendingAmount = c.Double(nullable: false),
                        TotalPayableAmount = c.Double(nullable: false),
                        PerUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonthName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.TenantMeterReadings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        MeterId = c.Int(nullable: false),
                        PerUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentMeterReading = c.Long(nullable: false),
                        PreviousMeterReading = c.Long(nullable: false),
                        DateOfPreviousMonthMeterReading = c.DateTime(nullable: false),
                        AmountPayable = c.Double(nullable: false),
                        DateOfMeterReading = c.DateTime(nullable: false),
                        DoesBillGenerated = c.Boolean(nullable: false),
                        PaymentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.TenantPayments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        DateOfPayment = c.DateTime(nullable: false),
                        Comments = c.String(),
                        PaymentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TenantPayments", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.TenantMeterReadings", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.Tenants", "MeterId", "dbo.ElectricMeters");
            DropForeignKey("dbo.TenantBills", "TenantId", "dbo.Tenants");
            DropIndex("dbo.TenantPayments", new[] { "TenantId" });
            DropIndex("dbo.TenantMeterReadings", new[] { "TenantId" });
            DropIndex("dbo.TenantBills", new[] { "TenantId" });
            DropIndex("dbo.Tenants", new[] { "MeterId" });
            DropTable("dbo.TenantPayments");
            DropTable("dbo.TenantMeterReadings");
            DropTable("dbo.TenantBills");
            DropTable("dbo.Tenants");
            DropTable("dbo.ElectricMeters");
            DropTable("dbo.Buildings");
        }
    }
}
