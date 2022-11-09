using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminInfo
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class EmpInfo
    {
        [Key,Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofJoining { get; set; }
        [Required]
        public int Passcode { get; set; }

        public virtual ICollection<BlogInfo> BlogDetails { get; set; }
    }
    public class BlogInfo
    {
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCreation { get; set; }
        [Url]
        public string BlogUrl { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }

        [ForeignKey("EmailId")]
        public virtual EmpInfo EmpDetails { get; set; }

    }

    public class MyContext : DbContext
    {
        public MyContext() : base("MyContext")
        {
            Database.SetInitializer(new SeedMethod());
        }
        public virtual DbSet<AdminInfo> AdminInfos { get; set; }
        public virtual DbSet<EmpInfo> EmpInfos { get; set; }
        public virtual DbSet<BlogInfo> BlogInfos { get; set; }
    }

    public class SeedMethod : DropCreateDatabaseIfModelChanges<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            List<AdminInfo> admins = new List<AdminInfo>();
            admins.Add(new AdminInfo { EmailId = "admin@gmail.com", Password = "Admin@123" });
            admins.Add(new AdminInfo { EmailId = "darshan@gmail.com", Password = "darsh@123" });
            admins.Add(new AdminInfo { EmailId = "sonia@gmail.com", Password = "sonia@123" });
            context.AdminInfos.AddRange(admins);

            List<EmpInfo> empInfos = new List<EmpInfo>();
            empInfos.Add(new EmpInfo { EmailId = "sesh@yahoo.com", Passcode = 2002, DateofJoining = new DateTime(2021, 05, 20), Name = "Seshan" });
            empInfos.Add(new EmpInfo { EmailId = "aashvik@yahoo.com", Passcode = 2021, DateofJoining = new DateTime(2022, 07, 13), Name = "Aashvik" });
          
            context.EmpInfos.AddRange(empInfos);


            List<BlogInfo> blogInfos = new List<BlogInfo>();
            blogInfos.Add(new BlogInfo { EmailId = "sesh@yahoo.com", BlogUrl = "https://azure.microsoft.com/en-us/blog/", DateOfCreation = new DateTime(2022, 05, 02), Subject = "MS.Net", Title = "Cloud Computing" });
            blogInfos.Add(new BlogInfo { EmailId = "aashvik@yahoo.com", BlogUrl = "https://azure.microsoft.com/en-us/blog/", DateOfCreation = new DateTime(2022, 08, 22), Subject = "RDBMS", Title = "Cloud Computing" });
            blogInfos.Add(new BlogInfo { EmailId = "aashvik@yahoo.com", BlogUrl = "https://azure.microsoft.com/en-us/blog/", DateOfCreation = new DateTime(2022, 10, 22), Subject = "MS.Net", Title = "Cloud Computing" });
            blogInfos.Add(new BlogInfo { EmailId = "sesh@yahoo.com", BlogUrl = "https://www.google.com/", DateOfCreation = new DateTime(2022, 11, 09), Subject = "Football", Title = "Cloud Computing" });
            context.BlogInfos.AddRange(blogInfos);
            


            context.SaveChanges();
            base.Seed(context);
        }
    }
}

