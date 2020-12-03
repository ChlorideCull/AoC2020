using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    class Map
    {
        private Coordinates _mapPosition;
        private char[,] _map; // row-column order
        private int _mapHeight;
        private int _mapWidth;
        
        public Map(List<string> mapLines)
        {
            _mapPosition = new Coordinates {X = 1, Y = 1};
            _mapWidth = mapLines[0].Length;
            _mapHeight = mapLines.Count;
            
            _map = new char[_mapHeight, _mapWidth];
            for (int i = 0; i < _mapHeight; i++)
            {
                for (int j = 0; j < _mapWidth; j++)
                {
                    _map[i, j] = mapLines[i][j];
                }
            }
        }

        public void MoveRelative(int x, int y)
        {
            _mapPosition.X += x;
            _mapPosition.Y += y;
            if (_mapPosition.Y > _mapHeight)
                throw new InvalidOperationException("Moved outside boundaries");
        }

        public void MoveAbsolute(int x, int y)
        {
            _mapPosition.X = x;
            _mapPosition.Y = y;
            if (_mapPosition.Y > _mapHeight || _mapPosition.Y < 1)
                throw new InvalidOperationException("Moved outside boundaries");
        }

        public char GetTile(int x, int y)
        {
            return _map[y - 1, (x - 1) % _mapWidth];
        }

        public char GetTileAtCurrentLocation() => GetTile(_mapPosition.X, _mapPosition.Y);
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * First "real" challenge. Map, tiling over X. For a repeated movement, find how many trees you collide with.
             * Bonus: Multiply the results of the five listed slopes.
             */
            var inputLines = File.ReadAllLines(args[0]).ToList();
            var map = new Map(inputLines);
            var first = TryMove(map, 3, 1);
            Console.WriteLine(first);
            
            var a = TryMove(map, 1, 1);
            var b = TryMove(map, 5, 1);
            var c = TryMove(map, 7, 1);
            var d = TryMove(map, 1, 2);
            Console.WriteLine(first * a * b * c * d);
        }

        static long TryMove(Map map, int x, int y)
        {
            map.MoveAbsolute(1, 1);
            var collisions = 0;
            try
            {
                while (true)
                {
                    map.MoveRelative(x, y);
                    if (map.GetTileAtCurrentLocation() == '#')
                        collisions += 1;
                }
            }
            catch (InvalidOperationException)
            {
                return collisions;
            }
        }
    }
}