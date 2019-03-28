using System.Data.Entity;

namespace PhoneBookWebApi
{
     class MyContextInitializer : DropCreateDatabaseIfModelChanges<PhoneBookContext>
    {
        protected override void Seed(PhoneBookContext context)
        {
             context.People.Add(new Person { Name = "Vasyan", NickName = "Vasyanchick", Birthday = new System.DateTime(2001, 12, 10), Phone = "+380987654321", Status = true, Surname = "Vasyanchuck" });

             context.People.Add(new Person { Name = "Vitaliy", NickName = "Vitalya", Birthday = new System.DateTime(1980, 11, 12), Phone = "+380981846365", Status = true, Surname = "Klitschko" });

             context.People.Add(new Person { Name = "Petro", NickName = "Pedro", Birthday = new System.DateTime(1968, 6, 1), Phone = "+380912345678", Status = false, Surname = "Poroshenko" }) ;

             context.SaveChanges();
        }
    }
}