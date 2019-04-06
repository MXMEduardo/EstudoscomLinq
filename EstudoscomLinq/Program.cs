using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstudoscomLinq.Entities;


namespace EstudoscomLinq {
    class Program {

        public static void Print<T>(string Mensagem, IEnumerable<T> collection ) {
            Console.WriteLine();
            Console.WriteLine(Mensagem);
            Console.WriteLine("----------------------------------------------");
            foreach (T obj in collection) {
                Console.WriteLine(obj);
            }
            Console.ReadLine();
        }
        public static void Print<T>(string Mensagem, T Obj) {
            Console.WriteLine();
            Console.WriteLine(Mensagem);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(Obj);
            Console.ReadLine();
        }

        static void Main(string[] args) {

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("TIER = 1 and PRICE < 900 : " , r1);

            var r2 = products.Where(p => p.Category.Name == "Tools").Select(p =>  p.Name );
            Print("NAME OF PRODUCTS FROM TOOLS : ", r2);

            var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, categoruName = p.Category.Name });
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", r3);

            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name).Select(p=> new { p.Category.Tier, p.Name, p.Price });
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", r4);

            var r5 = products.Skip(2).Take(4);
            Print("SKIP 2 AND TAKE 4", r5);

            var r6 = products.FirstOrDefault();
            Print("FIRST OR DEFAULT", new List<string>() {r6.ToString()});

            var r7 = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Print("FIRST OR DEFAULT with NULL", r7);

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Print("SINGLE OR DEFAULT with NULL", r8);

            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Print("SINGLE OR DEFAULT with NULL", r9);

            var r10 = products.Max(p => p.Price);
            Print("MAX PRICE", r10);

            var r11 = products.Min(p => p.Price);
            Print("MIN PRICE", r11);

            var r12 = products.Where(p => p.Category.Tier == 1).Sum(P => P.Price);
            Print("SUM PRICE FOR TIER 1", r12);

            var r13 = products.Where(p => p.Category.Tier == 1).Average(p => p.Price);
            Print("AVERAGE PRICE FOR TIER 1", r13);

            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0).Average();
            Print("AVERAGE PRICE FOR TIER 5 with NULL", r14);

            var r15 = products.GroupBy(p => p.Category);
            foreach(IGrouping<Category, Product> grpCategory in r15) {
                Console.WriteLine("Category : " + grpCategory.Key.Name);
                Console.WriteLine();
                foreach(Product ProductbyCategory in grpCategory) {
                    Console.WriteLine(ProductbyCategory);
                }
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
