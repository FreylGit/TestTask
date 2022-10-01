using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestTask;
using TestTask.Models;
class Program
{
    static async Task Main(string[] args)
    {
        switch (args[0])
        {
            case "1":
                OneTask();
                break;
            case "2":
                TwoTask(args);
                break;
            case "3":
                ThreeTask();
                break;
            case "4":
                await FourTask();
                break;
            case "5":
                FiveTask();
                break;
            default:
                return;
        }
    }
    public static string getFullName(int g)
    {
        var namesM = new[] { "Алексей", "Егор", "Адрей", "Михаил", "Дмитрий" };
        var namesV = new[] { "Юля", "Марина", "Саша", "Даша", "Полина" };
        var names2M = new[] { "Алексеевич", "Егорович", "Адреевич", "Михайлович", "Дмитриевич" };
        var names2V = new[] { "Игоревна", "Юрьевна", "Андреевна", "Витальевна", "Михайловна" };
        var name3 = new[] { "Иванов", "Петров", "Сидоров" };

        var random = new Random();
        if (g == 0)
        {
            string name1 = namesV[random.Next(0, namesV.Length - 1)];
            string name2 = names2V[random.Next(0, names2V.Length - 1)];
            string name3V = name3[random.Next(0, name3.Length - 1)];
            return name1 + " " + name2 + " " + name3V;
        }
        else
        {
            string name1 = namesM[random.Next(0, namesM.Length - 1)];
            string name2 = names2M[random.Next(0, names2M.Length - 1)];
            string name3M = name3[random.Next(0, name3.Length - 1)];
            return name1 + " " + name2 + " " + name3M;
        }
    }
    public static void OneTask()
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            context.Database.Migrate();
        }

    }
    public static void TwoTask(string[] args)
    {
        var person = new Person()
        {
            FullName = args[1] + " " + args[2] + " " + args[3],
            DateBirth = DateTime.Parse(args[4]),
            Gender = args[5]
        };
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            context.Persons.Add(person);
            context.SaveChanges();
        }
    }
    public static void ThreeTask()
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            var persons = context.Persons.ToList().DistinctBy(p => new { p.FullName, p.DateBirth }).OrderBy(p => p.FullName);

            var now = DateTime.Today;


            foreach (var person in persons)
            {
                int year = 0;
                if (now.Month < person.DateBirth.Month)
                {
                    year = (now.Year - person.DateBirth.Year) + 1;
                }
                else
                {
                    year = now.Year - person.DateBirth.Year;
                }
                Console.WriteLine(person.FullName + " " + person.DateBirth.ToShortDateString() + " " + year);
            }
        }
    }
    public async static Task FourTask()
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {

            for (int i = 0; i < 1000000; i++)
            {
                if (i % 1000 == 0)
                {
                    var person = new Person
                    {
                        FullName = "F" + "Test" + i,
                        Gender = "Мужской",
                        DateBirth = DateTime.Now,

                    };
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                }
                else
                {
                    List<Person> persons = new List<Person>();

                    if (i % 2 == 0)
                    {
                        var person = new Person
                        {
                            FullName = getFullName(i % 2),
                            Gender = "Женский",
                            DateBirth = DateTime.Now,

                        };
                        persons.Add(person);
                    }
                    else
                    {
                        var person = new Person
                        {
                            FullName = getFullName(i % 2),
                            Gender = "Мужской",
                            DateBirth = DateTime.Now,

                        };
                        persons.Add(person);

                    }
                    await context.AddRangeAsync(persons);
                    await context.SaveChangesAsync();
                }

            }
        }
    }
    public static void FiveTask()
    {
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var Fperson = context.Persons.ToList().Where(p => p.FullName[0].Equals('F'));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds + "mls");
        }

    }
}