using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tracking.Core.Maths
{
    /// <summary>
    /// Useful extension methods related to System.Drawing.Point(F)
    /// </summary>
    public static class PointExtensions
    {
        public static PointF Rotate90(this PointF a)
        {
            return new PointF(a.Y, -a.X);
        }

        public static PointF Add(this PointF a, PointF b)
        {
            return new PointF(b.X + a.X, b.Y + a.Y);
        }

        public static PointF Mul(this PointF a, float m)
        {
            a.X *= m;
            a.Y *= m;
            return a;
        }

        public static PointF Sub(this PointF a, PointF b)
        {
            return new PointF(b.X - a.X, b.Y - a.Y);
        }

        public static Point ToInt(this PointF p)
        {
            return new Point((int)p.X, (int)p.Y);
        }

        public static PointF ToFloat(this Point p)
        {
            return new PointF(p.X, p.Y);
        }

        public static Point MoveTowards(this Point a, Point b, int distance)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float dlen = (float)Math.Sqrt(dx * dx + dy * dy);

            dx = dx / dlen * distance;
            dy = dy / dlen * distance;

            return new Point((int)(a.X + dx), (int)(a.Y + dy));
        }

        public static int DistanceTo(this PointF a, PointF b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float dlen = (float)Math.Sqrt(dx * dx + dy * dy);

            return (int)dlen;
        }

        public static PointF Average(this IEnumerable<PointF> enumerable)
        {
            int n = 0;
            float x = 0, y = 0;
            foreach (var p in enumerable)
            {
                x += p.X;
                y += p.Y;
                n++;
            }
            if (n == 0)
                return PointF.Empty;
            else
                return new PointF(x / n, y / n);
        }
    }

}
