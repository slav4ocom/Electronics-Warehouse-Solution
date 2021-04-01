﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using Microsoft.EntityFrameworkCore;
using PictureProcessing;

namespace Console_Manager
{
    public static class Engine
    {
        public static StudentDbContext db { get; private set; }
        public static void Run()
        {
            db = new StudentDbContext();
            db.Database.Migrate();

            CultureInfo.CurrentCulture = new CultureInfo("en-EN");
            Console.WriteLine("slav4o.com Electronics Warehouse Manager");

            while (true)
            {
                Console.WriteLine("ready");
                var line = Console.ReadLine().ToLower().Trim();

                if (line == "exit")
                {
                    break;
                }
                else
                {
                    Parse(line);
                }
            }

            db.Dispose();
        }

        public static void Parse(string command)
        {
            switch (command)
            {
                case "cls":
                    Console.Clear();
                    break;

                case "add":
                    Commands.Add();
                    break;

                case "list":
                    Commands.List();
                    break;

                case "remove":
                    Commands.Remove();
                    break;

                case "edit":
                    Commands.Edit();
                    break;

                case "help":
                    Commands.Help();
                    break;

                case "download":
                    Console.WriteLine("Enter URL:");
                    var url = Console.ReadLine();
                    new PictureProcessor().Download(url);
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }

    }
}
