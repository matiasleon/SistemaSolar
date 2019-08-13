using System;
using API.Commons;

namespace API.Business.Weathers.Calculators
{
    public class GeometricCalculator
    {
       
        public float PoligonArea(params Point[] points)
        {
            var lengthPoints = points.Length;
            Point[] pts = new Point[lengthPoints + 1];
            points.CopyTo(pts, 0);
            pts[lengthPoints] = points[0];

            float area = 0;
            for (int i = 0; i < lengthPoints; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            return Math.Abs(area);
        }

        public Point CalculteCoordinates(int distance, double angularVelocity, double t)
        {
            var point = new Point();
            point.X = Convert.ToInt32(0 + distance * Math.Cos(angularVelocity * t));
            point.Y = Convert.ToInt32(0 + distance * Math.Sin(angularVelocity * t));

            return point;
        }

        public bool ArePlanetsAligned(Point referencePoint, Point p2, Point p3)
        {
            var m1 = (p2.Y - referencePoint.Y) / (p2.X - referencePoint.X);
            var m2 = (p3.Y - referencePoint.Y) / (p3.X - referencePoint.X);

            return m1 == m2;
        }
    }
}
