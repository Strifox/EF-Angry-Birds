﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DB.Lab2
{
    public class Query
    {
        //TODO: Fixa edit player query
        #region Player Query

        public static void ShowPlayerQuery(EntityContext context)
        {
            var showPlayerQuery = from show in context.Players
                                  select show.Name;

            foreach (var player in showPlayerQuery)
            {
                Console.WriteLine(player);
            }
        }

        public static bool DoesMapExist(EntityContext context, int Id)
        {
            var maps = from map in context.Maps
                          select map;

            foreach (var map in maps)
            {
                if (map.Id == Id)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static bool DoesPlayerExist(EntityContext context, string Name)
        {
            var players = from player in context.Players
                         select player;

            foreach (var player in players)
            {
                if (player.Name == Name)
                    return true;
                else
                    return false;
            }
            return false;
        }

        //public static void ChoosePlayerQuery(EntityContext context)
        //{
        //    var chooseQuery = from choose in context.Players
        //                      where choose.Id == playerId
        //                      select choose;

        //    foreach (var player in chooseQuery)
        //    {
        //        Console.WriteLine(player);
        //    }
        //}
        public static void EditPlayerNameQuery(EntityContext context)
        {
           
            Console.WriteLine("Type your new name");
            var nameQuery = from p in context.Players
                            where p.Name == Console.ReadLine()
                            select p.Name;

            Console.WriteLine($"You updated player name to: {nameQuery}");
        }
        public static void EditPlayerScoreQuery(EntityContext context)
        {
            Console.WriteLine("Type your new score");
            var scoreQuery = from score in context.Scores
                             where score.PlayerScore == Int32.Parse(Console.ReadLine())
                             select score;
            foreach (var score in scoreQuery)
            {
                Console.WriteLine("You updates player score to:");
                Console.WriteLine(score);
            }
        }

        #endregion

        #region Map Query

        public static void ShowMapQuery(EntityContext context)
        {
            var showMapQuery = from show in context.Maps
                               select new
                               {
                                   id = show.Id,
                                   name = show.MapName
                               };

            foreach (var map in showMapQuery)
            {
                Console.WriteLine($"Map Id: {map.id}, Map Name: {map.name}");
            }
        }

        #endregion
    }
}
