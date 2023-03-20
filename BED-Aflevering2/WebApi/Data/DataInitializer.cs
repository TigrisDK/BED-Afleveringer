using WebApi.Models.Model;
using WebApi.Models.Job;
using WebApi.Models.Expense;

namespace WebApi.Data
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (context.Models.Any())
            {
                return;
            }

            var models = new Model[]
            {
                new Model
                {
                    FirstName = "Kasper",
                    LastName = "Martensen",
                    Email = "Kasper.Martensen@outlook.dk",
                    PhoneNo = "61667230",
                    AddressLine1 = "Some Street 111",
                    AddressLine2 = "",
                    Zip = "8800",
                    City = "Viborg",
                    BirthDay = DateTime.Parse("06-07-1998"),
                    Height = 172,
                    ShoeSize = 42,
                    HairColor = "Dark blond",
                    Comments = "No Comment",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                },

                new Model
                {
                    FirstName = "Alan",
                    LastName = "Khamo",
                    Email = "AlanKhamo@something.com",
                    PhoneNo = "12345678",
                    AddressLine1 = "Some street 1",
                    AddressLine2 = "",
                    Zip = "0000",
                    City = "Aarhus",
                    BirthDay = DateTime.Parse("01-01-1999"),
                    Height = 175,
                    ShoeSize = 42,
                    HairColor = "Dark",
                    Comments = "No Comment",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                },

                new Model
                {
                    FirstName = "some",
                    LastName = "guy",
                    Email = "someguy@someguy.com",
                    PhoneNo = "01234567",
                    AddressLine1 = "that street 1",
                    AddressLine2 = "",
                    Zip = "8000",
                    City = "Landby",
                    BirthDay = DateTime.Parse("02-02-1997"),
                    Height = 170,
                    ShoeSize = 40,
                    HairColor = "Purple",
                    Comments = "Wierd hair color",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                }
            };

            foreach (Model m in models)
            {
                context.Models.Add(m);
            }

            var jobs = new Job[]
            {
                new Job()
                {
                    Customer = "Hugo Boss",
                    StartDate = DateTime.Parse("20-03-2023"),
                    Days = 30,
                    Location = "Aarhus",
                    Comments = "",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                },
                new Job()
                {
                    Customer = "GetJobsNow",
                    StartDate = DateTime.Parse("02-02-2023"),
                    Days = 5,
                    Location = "Copenhagen",
                    Comments = "",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                },
                new Job()
                {
                    Customer = "Mc donalds",
                    StartDate = DateTime.Parse("01-01-2022"),
                    Days = 100,
                    Location = "Tilst",
                    Comments = "",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                },
            };

            foreach (Job j in jobs)
            {
                context.Jobs.Add(j);
            }

            context.SaveChanges();
        }
    }
}
