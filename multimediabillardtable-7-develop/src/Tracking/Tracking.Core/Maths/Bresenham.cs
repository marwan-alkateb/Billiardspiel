using System;
using System.Drawing;

namespace Tracking.Core.Maths
{
    /// <summary>
    /// Implements Bresenham's line iteration algorithm.
    /// 
    /// Code source: https://stackoverflow.com/a/11683720/7702748
    /// </summary>
    public static class Bresenham
    {

        /// <summary>
        /// Walks each pixel along the shortest line from
        /// Point A to Point B, calling walkCallback for each one.
        /// If the callback returns false, the loop is stopped.
        /// </summary>
        /// <param name="a">Source</param>
        /// <param name="b">Destination</param>
        /// <param name="walkCallback">Pixel callback</param>
        public static void Walk(Point a, Point b, Func<Point, bool> walkCallback)
        {
            int x = a.X;
            int y = a.Y;
            int x2 = b.X;
            int y2 = b.Y;
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if (!walkCallback(new Point(x, y)))
                    return;
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

    }
}
