using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace EnglishSyntaxHighlighter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type sentence");
            string input = Console.ReadLine();

            string[] words = input.Split(' ');

            string cs = @"Data Source=.\Dictionary.db";

            foreach (var word in words)
            {
                string sql = $"SELECT DISTINCT wordtype FROM entries WHERE word == '{word}'; ";

                string[] wordtypes = new string[] { };

                string wordtype = String.Empty;

                using (var connection = new SQLiteConnection(cs))
                {
                    
                    wordtypes = connection.Query<string>(sql).ToArray<string>();
                    
                }

                if (wordtypes.Count() > 1)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(0, wordtypes.Count());

                    wordtype = wordtypes[randomIndex]; 
                }
                else
                {
                    wordtype = wordtypes.FirstOrDefault();
                }

                
                if (wordtype == "n." || wordtype == "pl.")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(word + " ");
                }
                else if (wordtype == "a.")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(word + " ");
                }
                else if (wordtype == "v." || wordtype == "v. & n.")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(word + " ");
                }
                else if (wordtype == "adv.")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(word + " ");
                }
                else if (wordtype == "prep." || wordtype == "imp.")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(word + " ");
                }
                else if (wordtype == "pron.")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(word + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(word + " ");
                }
            }

            Console.Read();
        }
    }
}
