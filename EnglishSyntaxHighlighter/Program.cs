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

                if (wordtype.Contains("pron."))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(word + " ");
                }
                else if (wordtype.Contains("n."))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(word + " ");
                }
                else if (wordtype.Contains("a."))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(word + " ");
                }
                else if (wordtype.Contains("v.") || wordtype.Contains("v. & n."))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(word + " ");
                }
                else if (wordtype.Contains("adv."))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(word + " ");
                }
                else if (wordtype.Contains("prep.") || wordtype.Contains("imp."))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
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
