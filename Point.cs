using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightWatch
{
    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x; this.Y = y;
        }

        public Point(int x, int y, Point calcDistanceFrom)
        {
            this.X = x; this.Y = y;
            this.DistFromGivenPoint = DistanceBetweenTwoPoints(new Point(x, y), calcDistanceFrom);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public double DistFromGivenPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private double DistanceBetweenTwoPoints(Point point1, Point point2)
        {
            var distSquared = Math.Pow((point2.X - point1.X), 2) + Math.Pow((point2.Y - point1.Y), 2);
            var d = Math.Sqrt(distSquared);
            return d;
        }

    }    
}
