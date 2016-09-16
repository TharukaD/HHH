namespace EMSM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentDateIdentifiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        stored_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        curDate = c.DateTime(nullable: false),
                        is_Attempt = c.Int(nullable: false),
                        startTime = c.DateTime(nullable: false),
                        endTime = c.DateTime(nullable: false),
                        Employees_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employees_ID)
                .Index(t => t.Employees_ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Emp_First_Name = c.String(maxLength: 10),
                        Emp_Last_Name = c.String(maxLength: 10),
                        Emp_NIC = c.String(nullable: false),
                        Emp_Contact = c.String(nullable: false),
                        Emp_Pic_Path = c.String(),
                        Emp_Money_Per_Hour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Emp_Recomended_Work_Time = c.Int(nullable: false),
                        is_Identify = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeSalaries",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Year_and_Month = c.DateTime(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Short_Leaves = c.Int(nullable: false),
                        Holidays = c.Int(nullable: false),
                        Employee_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employee_ID)
                .Index(t => t.Employee_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployeeSalaries", "Employee_ID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeAttendances", "Employees_ID", "dbo.Employees");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.EmployeeSalaries", new[] { "Employee_ID" });
            DropIndex("dbo.EmployeeAttendances", new[] { "Employees_ID" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmployeeSalaries");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeAttendances");
            DropTable("dbo.CurrentDateIdentifiers");
        }
    }
}
