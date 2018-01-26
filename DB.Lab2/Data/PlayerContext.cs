﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DB.Lab2
{
    class PlayerContext
    {
        static Map map = new Map();
        static Player player = new Player();

        public static void AddPlayerToDatabase(EntityContext context)
        {
            Console.WriteLine("Type your Name (Case sensitive)");
            player.Name = Console.ReadLine(); // Sets player name in database to this

            if (!Query.DoesPlayerExist(context, player.Name))
            {
                context.Players.Add(new Player(player.Name)); //Adds player to Database
                context.SaveChanges();
                Console.WriteLine("Database added");
            }
            else
            {
                Console.WriteLine("Player already exists..");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadKey();
            }

        }

        public static void AddMovesToPlayer(EntityContext context)
        {
            bool correctlyEntered;
            MapContext.IsMapAdded(context);

            Console.WriteLine("Type your Name (Case sensitive)");
            player.Name = Console.ReadLine(); // Sets player name in database to this

            if (!Query.DoesPlayerExist(context, player.Name))
            {
                do
                {
                    Console.WriteLine($"Max moves: {map.MaxMoves}");
                    Console.WriteLine("Type how many moves you made");
                    player.Moves = int.Parse(Console.ReadLine()); // Sets player moves in database to this
                    if (player.Moves <= map.MaxMoves)
                    {
                        context.Players.Add(new Player(player.Moves)); //Adds player to Database
                        context.SaveChanges();
                        Console.WriteLine("Database added");
                        correctlyEntered = true;
                    }
                    else
                    {
                        Console.WriteLine($"You can't do more steps than max.\n(max moves: {map.MaxMoves} )");
                        Console.WriteLine("\nPress enter to continue..");
                        Console.ReadKey();
                        correctlyEntered = false;
                    }

                } while (!correctlyEntered);
            }
            else
            {
                Console.WriteLine("Player already exists..");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadKey();
            }
        }

        public static void ShowPlayer(EntityContext context)  // Shows all existing players
        {
            Console.Clear();
            Query.ShowPlayerQuery(context);
        }

        public static void EditPlayer(EntityContext context) // A Switch menu to choose wether to edit player name or score
        {
            Console.Clear();
            Console.WriteLine("Press '1' to edit Player Name");
            Console.WriteLine("Press '2' to edit Player Score");
            string menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    ShowPlayer(context);
                    EditPlayerName(context, player.Name);
                    break;
                case "2":
                    Query.UpdateScore(context);
                    break;
            }
            context.SaveChanges();
        }

        //public static int ReturnPlayerId()
        //{
        //    int playerId = int.Parse(Console.ReadLine());
        //    return playerId;
        //}

        public static Player ChoosePlayer(EntityContext context, ref int playerId)
        {
            player = Query.GetPlayerById(context, playerId);
            return player;
        }
        public static void EditPlayerName(EntityContext context, string playerName)
        {
            //Choose player - v
            //Change player
            //Save player - v
            Console.WriteLine("\nChoose Player by id");
            int playerId = int.Parse(Console.ReadLine());
            var player = ChoosePlayer(context, ref playerId);
            Console.WriteLine($"You chose {player.Name}");
            Console.WriteLine("\nType your new name");
            playerName = Console.ReadLine();
            player.Name = playerName;
            Console.WriteLine($"You updated player name to: {playerName}");
            context.SaveChanges();
            Thread.Sleep(1500);
        }

        public static bool IsInputValid(string input)
        {
            string pattern = @"^[0-9]{1,}$";
            var match = Regex.Match(input, pattern);

            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
