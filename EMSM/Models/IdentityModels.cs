using Microsoft.AspNet.Identity.EntityFramework;

namespace EMSM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<EMS.Models.EmployeeAttendance> EmployeeAttendances { get; set; }

        public System.Data.Entity.DbSet<EmMS.Models.CurrentDateIdentifier> CurrentDateIdentifiers { get; set; }

        public System.Data.Entity.DbSet<EMS.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<EMS.Models.EmployeeSalary> EmployeeSalaries { get; set; }

        public System.Data.Entity.DbSet<EMSM.Models.EmpAttendance> EmpAttendances { get; set; }
    }
}