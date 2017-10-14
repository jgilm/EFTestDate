using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;

namespace EFTestDate
{
    class MyTable
    {
        public int Id { get; set; }
        public DateTimeOffset OffsetDate { get; set; }
        public DateTime RegularDate { get; set; }
    }

    class TestContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            const string connectionString = "Server=127.0.0.1;Port=5432;Database=MyTest;User Id=<USER>;Password=<PASSWORD>;";
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<MyTable> MyTable { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var context = new TestContext();

            // Query against the int column
            ExecuteQuery(context.MyTable.Where(t => t.Id == 1), 1);

            // Query against DateTimeOffset column
            ExecuteQuery(context.MyTable.Where(t => t.OffsetDate == new DateTime(2017, 1, 1)), 2);

            // Query against DateTime column
            ExecuteQuery(context.MyTable.Where(t => t.RegularDate == new DateTime(2017, 1, 1)), 3);

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        static void ExecuteQuery(IQueryable<MyTable> query, int testNumber)
        {
            try
            {
                var list = query.ToList();
                Console.WriteLine($"- Test {testNumber} count: {list.Count}\r\n");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Test {testNumber} Exception thrown for statement: \r\n\"{ex.Statement.ToString()}\r\n");
            }
        }
    }

}
