using Joy.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Type = Joy.Infrastructure.Domain.Models.Type;

namespace Joy.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
          : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Patient> patients = new List<Patient>();
            List<Payment> payments = new List<Payment>();
            List<Role> roles = new List<Role>();
            List<User> users = new List<User>();
            List<UserLogin> userLogins = new List<UserLogin>();
            List<UserRole> userRoles = new List<UserRole>();


            roles.Add(new Role()
            {
                Id = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
                Name = "Staff",
                Description = "People who work in school but not teaching",
                Abbreviation = "Stf"
            });

            roles.Add(new Role()
            {
                Id = Guid.Parse("1fb7085a-762f-440c-87de-59f75f85e934"),
                Name = "Admin",
                Description = "Admin",
                Abbreviation = "Adm"
            });

            modelBuilder.Entity<Role>().HasData(roles);

             // USERRRRRRRRRRRRRRR
            users.Add(new User()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                Name = "Reniel Mallari David",
                DateOfBirth = DateTime.Now,
                EmailAddress = "Reniel@mailinator.com",
                Gender = Gender.Male,
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96")
            });

            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("123")
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });

            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac0"),
                RoleId = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
            });




            users.Add(new User()
            {
                Id = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                Name = "Clarissa Joy Flore",
                DateOfBirth = DateTime.Now,
                EmailAddress = "joy@mailinator.com",
                Gender = Gender.Female,
                RoleId = Guid.Parse("1fb7085a-762f-440c-87de-59f75f85e93")
            });

            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("123")
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId =Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });

            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("1d72f000-dbbd-419b-8af2-f571e1486ac1"),
                RoleId = Guid.Parse("1fb7085a-762f-440c-87de-59f75f85e93"),
            });

          
            //PATIENTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTS
            patients.Add(new Patient()
            {
                PatientId = Guid.Parse("2fb7085a-762f-440c-87de-59f75f85e94"),
                Code = "123",
                FullName = "Reniel David",
                DateOfBirth = DateTime.Now,
                Type = Type.Chemo,
                PaymentId = Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac2"),

            });
            payments.Add(new Payment()
            {
                PaymentId = Guid.Parse("2d72f000-dbbd-419b-8af2-f571e1486ac2"),
                Tittle = "Cash Payment",
                Description = "A cash payment",
                Abbrevitation = "Cash",

            });
            // number 222222222222

            patients.Add(new Patient()
            {
                PatientId = Guid.Parse("3fb7085a-762f-440c-87de-59f75f85e95"),
                Code = "123",
                FullName = "Clarissa Joy Flores",
                DateOfBirth = DateTime.Now,
                Type = Type.Chemo,
                PaymentId = Guid.Parse("3d72f000-dbbd-419b-8af2-f571e1486ac3"),

            });
            payments.Add(new Payment()
            {
                PaymentId = Guid.Parse("3d72f000-dbbd-419b-8af2-f571e1486ac3"),
                Tittle = "Cash Payment",
                Description = "A cash payment",
                Abbrevitation = "Cash",

            });
            // number 3333333333333333

            patients.Add(new Patient()
            {
                PatientId = Guid.Parse("4fb7085a-762f-440c-87de-59f75f85e95"),
                Code = "123",
                FullName = "Luisa Katrina Reyes",
                DateOfBirth = DateTime.Now,
                Type = Type.Chemo,
                PaymentId = Guid.Parse("4d72f000-dbbd-419b-8af2-f571e1486ac4"),

            });
            payments.Add(new Payment()
            {
                PaymentId = Guid.Parse("4d72f000-dbbd-419b-8af2-f571e1486ac4"),
                Tittle = "Cash Payment",
                Description = "A cash payment",
                Abbrevitation = "Cash",

            });

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserLogin>().HasData(userLogins);
            modelBuilder.Entity<UserRole>().HasData(userRoles);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<Patient>().HasData(patients);
            modelBuilder.Entity<Payment>().HasData(payments);
        }

    }
}
