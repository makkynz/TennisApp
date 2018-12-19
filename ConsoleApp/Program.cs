using System;
using Core.Services.TennisNzScrapper;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine($"Now: {DateTime.Now}");
            
            var players = new GradingListPage().GetAllPlayers();

            Console.WriteLine($"Player count: {players.Count}");
            
            Console.WriteLine($"Finish: {DateTime.Now}");

            Console.ReadLine();

        }
    }
}
