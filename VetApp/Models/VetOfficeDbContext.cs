using Microsoft.EntityFrameworkCore;
namespace VetOffice.Models
{
    public class VetOfficeDbContext : DbContext
    {
        public DbSet<Animals> Animal { get; set; }
        public DbSet<Doctors> Doctor { get; set; }
        public DbSet<Presence> Presen { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<Users_login> Users_Logins { get; set; }
        public DbSet<Visits> Visit { get; set; }

        string dbb = Directory.GetCurrentDirectory() + "/vetdb.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={dbb}");

        public void DefaultData()
        {
            try
            {
                RemoveData();
                using var db = new VetOfficeDbContext();
                db.Add(
                       new Roles()
                       {
                           id_role = 1,
                           role_name = "Admin"
                       });

                db.Add(
                   new Roles()
                   {
                       id_role = 2,
                       role_name = "User"
                   });
                db.Add(
                       new Roles()
                       {
                           id_role = 3,
                           role_name = "Vet"
                       });

                db.Add(
                    new Users()
                    {
                        id_user = 1,
                        name = "admin",
                        surname = "admin",
                        phone_number = "000000000"
                    });
                db.Add(
                    new Users()
                    {
                        id_user = 2,
                        name = "Megan",
                        surname = "Foc",
                        phone_number = "12587498"
                    });
                db.Add(
                       new Users()
                       {
                           id_user = 3,
                           name = "Trevor",
                           surname = "Foot",
                           phone_number = "333444555"
                       });
                db.Add(
                       new Users_login()
                       {
                           id_user = 1,
                           email = "admin@gmail.com",
                           password = "Admin!23",
                           id_role = 1,
                           role = "Admin"
                       });


                db.Add(
                       new Users_login()
                       {
                           id_user = 2,
                           email = "meganfoc@gmail.com",
                           password = "Meggi88",
                           id_role = 3,
                           role = "Vet"
                       });

                db.Add(
                       new Users_login()
                       {
                           id_user = 3,
                           email = "trevorfoot@gmail.com",
                           password = "Trev987",
                           id_role = 2,
                           role = "User"
                       });



                db.Add(
                       new Doctors()
                       {
                           id_doctor = 1,
                           nameands = "Megan Foc"
                       });

                db.Add(
                       new Animals()
                       {
                           id_animal = 1,
                           name_a = "Loki",
                           age = "2",
                           id_user = 3
                       });

                db.Add(
                   new Visits()
                   {
                       id_visit = 1,
                       id_animal = 1,
                       name_a = "Loki",
                       id_doctor = 1,
                       nameands = "Megan Foc",
                       id_user = 3,
                       name = "Trevor",
                       surname = "Foot",
                       date = DateTime.Parse("2022/12/02 12:00:00")
                   });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void RemoveData()
        {
            using var db = new VetOfficeDbContext();
            foreach (var item in db.User)
            {
                db.Remove(item);
            }
            foreach (var item in db.Animal)
            {
                db.Remove(item);
            }
            foreach (var item in db.Doctor)
            {
                db.Remove(item);
            }
            foreach (var item in db.Role)
            {
                db.Remove(item);
            }
            foreach (var item in db.Users_Logins)
            {
                db.Remove(item);
            }
            foreach (var item in db.Presen)
            {
                db.Remove(item);
            }
            foreach (var item in db.Visit)
            {
                db.Remove(item);
            }

            db.SaveChanges();
        }
    }
}