﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Lab2
{
    public class MapContext
    {

        static Map map;

        public static void AddMapToDatabase(EntityContext context) // Adds map to database
        {
            Console.WriteLine("Enter a map name");
            map.MapName = Console.ReadLine();
            Console.WriteLine("Enter Max amount of moves");
            map.MaxMoves = int.Parse(Console.ReadLine());
            context.Maps.Add(new Map(map.MaxMoves, map.MapName));
            Console.WriteLine($"{map.MapName} added to database");
            context.SaveChanges();
        }


        public static Map IsMapAdded(EntityContext context)  //Checks if map is added before adding a player
        {
            if (context.Maps.Any())
            {
                Console.WriteLine("You must choose a map before adding a player. Enter Map name:");
                Query.ShowMapQuery(context);
                int mapName = int.Parse(Console.ReadLine());
                ChooseMap(context, mapName);
            }
            else if (!context.Maps.Any())
            {
                Console.WriteLine("There is no map added, you must first add a map");
                AddMapToDatabase(context);
                context.SaveChanges();
            }
            return map;
        }

        public static Map ChooseMap(EntityContext context, int mapId) //Chooses map
        {
            if (!Query.DoesMapExist(context, mapId))
                Console.WriteLine("Invalid map");
            else
                map = Query.GetMapById(context, mapId);

            return map;
        }

    }
}
