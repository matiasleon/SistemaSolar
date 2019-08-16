﻿using System;
using API.Business.Commons;
using API.Commons;

namespace API.Business.Weathers.Calculators
{
    public class GeometricCalculator
    {
        public double PoligonArea(params Point[] points)
        {
            var lengthPoints = points.Length;
            Point[] pts = new Point[lengthPoints + 1];
            points.CopyTo(pts, 0);
            pts[lengthPoints] = points[0];

            double area = 0;
            for (int i = 0; i < lengthPoints; i++)
            {
                area += (pts[i + 1].X - pts[i].X) * (pts[i + 1].Y + pts[i].Y) / 2;
            }

            return Convert.ToInt32(Math.Abs(area));
        }

        public Point CalculteCoordinates(int distance, int angularVelocity, int t)
        {
            var point = new Point
            {
                X = Convert.ToInt32(0 + distance * Math.Cos(angularVelocity * t)),
                Y = Convert.ToInt32(0 + distance * Math.Sin(angularVelocity * t))
            };

            return point;
        }

        public double CalculatePerimeterOfTriangule(Point p1, Point p2, Point p3)
        {
            var p1p2 = CalculateDistanceBetween(p1, p2);
            var p1p3 = CalculateDistanceBetween(p1, p3);
            var p2p3 = CalculateDistanceBetween(p2, p3);
            var perimeter = p1p2 + p1p3 + p2p3;

            return perimeter;
        }

        private double CalculateDistanceBetween(Point p1, Point p2)
        {
            var a = p2.X - p1.X;
            var b = p2.Y - p1.Y;

            return Math.Sqrt(a * a + b * b);
        }
    }
}
