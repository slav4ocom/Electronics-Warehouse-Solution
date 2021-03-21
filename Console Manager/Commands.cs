using CommonModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Console_Manager
{
    public static class Commands
    {
        public static void Add()
        {
            var db = Engine.db;

            Console.WriteLine("Add new part: name, part type, price ");
            var arguments = Console.ReadLine()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToArray();

            db.Add(new Article()
            {
                Name = arguments[0],
                PartType = arguments[1],
                Price = decimal.Parse(arguments[2])
            });

            db.SaveChanges();
        }

        public static void List()
        {
            var db = Engine.db;

            db.Articles.ToList().ForEach(a => Console.WriteLine($"{a.Id}. {a.Name} {a.PartType} {a.Price} BGN"));

        }

        public static void Remove()
        {
            var db = Engine.db;

            Console.WriteLine("Enter Id to remove:");
            var id = int.Parse(Console.ReadLine());
            db.Remove(db.Articles.FirstOrDefault(a => a.Id == id));
            db.SaveChanges();

        }

        public static void Edit()
        {
            var db = Engine.db;

            Console.WriteLine("Enter Id to edit:");
            var idToEdit = int.Parse(Console.ReadLine());
            var part = db.Articles.FirstOrDefault(p => p.Id == idToEdit);
            if (part != null)
            {
                Console.WriteLine($"{part.Id}. {part.Name} {part.PartType} {part.Price} BGN");
                Console.WriteLine("Enter new data: name, part type, price ");
                var editArgs = Console.ReadLine()
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim())
                    .ToArray();

                part.Name = editArgs[0];
                part.PartType = editArgs[1];
                part.Price = decimal.Parse(editArgs[2]);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Invalid part.");
            }

        }

        public static void Help()
        {
            var text = File.ReadAllText("../../../Help.txt");

            Console.WriteLine(text);
        }
    }
}
