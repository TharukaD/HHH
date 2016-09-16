namespace EMSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        uniqDate = c.Int(nullable: false),
                        curDate = c.DateTime(nullable: false),
                        is_Attempt = c.Int(nullable: false),
                        startTime = c.DateTime(nullable: false),
                        endTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.uniqDate })
                .ForeignKey("dbo.Employees", t => t.ID, cascadeDelete: true)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpAttendances", "ID", "dbo.Employees");
            DropIndex("dbo.EmpAttendances", new[] { "ID" });
            DropTable("dbo.EmpAttendances");
        }
    }
}
