namespace PhoneBookWebApi
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhoneBookContext : DbContext
    {
     
        public PhoneBookContext()
            : base("name=PhoneBookContext")
        {
            Database.SetInitializer<PhoneBookContext>(new MyContextInitializer());
        }



         public virtual DbSet<Person> People { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string  Phone {get;set;}

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public bool Status { get; set; }


    }
}