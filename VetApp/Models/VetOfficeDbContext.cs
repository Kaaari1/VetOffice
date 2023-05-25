using Microsoft.EntityFrameworkCore;
using System;

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
                       new Users()
                       {
                           id_user = 4,
                           name = "Mariusz",
                           surname = "Alfred",
                           phone_number = "851497562"
                       });
                db.Add(
                       new Users()
                       {
                           id_user = 5,
                           name = "Fredryk",
                           surname = "Mustang",
                           phone_number = "745986123"
                       });
                db.Add(
                       new Users_login()
                       {
                           id_user = 1,
                           email = "admin@gmail.com",
                           password = "Admin!23",
                           id_role = 1
                       });


                db.Add(
                       new Users_login()
                       {
                           id_user = 2,
                           email = "meganfoc@gmail.com",
                           password = "Meggi88",
                           id_role = 3
                       });

                db.Add(
                       new Users_login()
                       {
                           id_user = 3,
                           email = "trevorfoot@gmail.com",
                           password = "Trev987",
                           id_role = 2
                       });
                db.Add(
                       new Users_login()
                       {
                           id_user = 4,
                           email = "mariuszalfred@gmail.com",
                           password = "Alf584",
                           id_role = 3
                       });
                db.Add(
                       new Users_login()
                       {
                           id_user = 5,
                           email = "fredrykm@gmail.com",
                           password = "Mustang1",
                           id_role = 2
                       });

                db.Add(
                       new Doctors()
                       {
                           id_doctor = 1,
                           id_user = 2,
                           work_hours_from = TimeSpan.Parse("08:00"),
                           work_hours_to = TimeSpan.Parse("16:00"),
                           is_active = true
                       });
                db.Add(
                       new Doctors()
                       {
                           id_doctor = 2,
                           id_user = 4,
                           work_hours_from = TimeSpan.Parse("08:00"),
                           work_hours_to = TimeSpan.Parse("16:00"),
                           is_active = true
                       });
                db.Add(
                       new Animals()
                       {
                           id_animal = 1,
                           name_a = "Bambi",
                           petType = "Dog",
                           dateofbirth = DateTime.Parse("2020/12/10"),
                           id_user = 3
                       });
                db.Add(
                      new Animals()
                      {
                          id_animal = 2,
                          name_a = "Bruno",
                          petType = "Cat",
                          dateofbirth = DateTime.Parse("2021/06/06"),
                          id_user = 3
                      });
                db.Add(
                      new Animals()
                      {
                          id_animal = 3,
                          name_a = "Tytus",
                          petType = "Turtle",
                          dateofbirth = DateTime.Parse("2019/08/30"),
                          id_user = 5
                      });
                db.Add(
                   new Visits()
                   {
                       id_visit = 1,
                       id_animal = 1,
                       id_doctor = 1,
                       id_user = 3,
                       date = DateTime.Parse("2022/12/02 12:00:00"),
                       is_active = true,
                   });
                db.Add(
                   new Visits()
                   {
                       id_visit = 2,
                       id_animal = 2,
                       id_doctor = 2,
                       id_user = 3,
                       date = DateTime.Parse("2023/05/29 14:00:00"),
                       is_active = true,
                   });
                db.Add(
                   new Visits()
                   {
                       id_visit = 3,
                       id_animal = 3,
                       id_doctor = 2,
                       id_user = 5,
                       date = DateTime.Parse("2023/05/30 10:00:00"),
                       is_active = true,
                   });
                db.Add(
                   new Presence()
                   {
                       dayoff_id = 1,
                       id_doctor = 1,
                       is_active = true,
                       nworking_days = DateTime.Parse("2023/05/02"),
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