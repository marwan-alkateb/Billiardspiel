using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tracking.Core.Maths
{
    /// <summary>
    /// Useful extension methods for EmguCV structures
    /// </summary>
    public static class EmguExtensions
    {
        public static PointF PointOnLine(this LineSegment2DF segment, float k)
        {
            var dx = (segment.P2.X - segment.P1.X) * k;
            var dy = (segment.P2.Y - segment.P1.Y) * k;

            return new PointF(segment.P1.X + dx, segment.P1.Y + dy);
        }

        public static VectorOfPoint FindLargestItem(this VectorOfVectorOfPoint vector)
        {
            var largest = vector[0];

            for (var i = 0; i < vector.Size; i++)
            {
                var item = vector[i];
                if (item.Size > largest.Size)
                    largest = item;
            }

            return largest;
        }

        public static float GetArea(this RotatedRect rect)
        {
            return rect.Size.Width * rect.Size.Height;
        }

        public static LineSegment2DF[] GetEdges(this RotatedRect rect)
        {
            var vertices = rect.GetVertices();
            var segments = new LineSegment2DF[4];

            for (var i = 0; i < 4; i++)
            {
                var a = vertices[i];
                var b = vertices[(i + 1) % vertices.Length];
                segments[i] = new LineSegment2DF(a, b);
            }

            return segments;
        }

        public static bool Intersects(this CircleF a, CircleF b)
        {
            var distVec = b.Center.Sub(a.Center);
            var dist = Math.Sqrt(distVec.X * distVec.X + distVec.Y * distVec.Y);
            return dist < a.Radius + b.Radius;
        }

        public static double DistanceTo(this Bgra a, Bgra b)
        {
            var dr = b.Red - a.Red;
            var dg = b.Green - a.Green;
            var db = b.Blue - a.Blue;
            return Math.Sqrt(dr * dr + dg * dg + db * db);
        }

        public static Bgra Average(this IEnumerable<Bgra> enumerable)
        {
            int n = 0;
            double r = 0, g = 0, b = 0, a = 0;
            foreach (var col in enumerable)
            {
                r += col.Red;
                g += col.Green;
                b += col.Blue;
                a += col.Alpha;
                n++;
            }
            return new Bgra(b / n, g / n, r / n, a / n);
        }

        public static Hsv DistanceTo(this Hsv a, Hsv b)
        {
            var absHueDiff = Math.Abs(b.Hue - a.Hue);
            var hueDiff = Math.Min(absHueDiff, 180 - absHueDiff);
            var satDiff = Math.Abs(b.Satuation - a.Satuation);
            var valDiff = Math.Abs(b.Value - a.Value);
            return new Hsv(hueDiff, satDiff, valDiff);
        }

        // Color Conversion Algorithms from:
        // https://stackoverflow.com/a/1626175/7702748
        //  because Microsoft's implementation is buggy

        public static Hsv ToHsv(this Bgra bgra)
        {
            var color = Color.FromArgb(255, (int)bgra.Red, (int)bgra.Green, (int)bgra.Blue);
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            var hue = color.GetHue(); // Range [0..360]
            var saturation = (max == 0) ? 0 : 1d - (1d * min / max); // Range [0..1]
            var value = max / 255d; // Range [0..1]

            // Convert Range to EmguCV (H: 0..180, S: 0..255, V: 0.255)
            return new Hsv(hue / 2.0, saturation * 255.0, value * 255.0);
        }

        public static Color ToArgb(this Hsv hsv)
        {
            // Convert range from EmguCV to System.Drawing representation
            var hue = hsv.Hue * 2.0;
            var saturation = hsv.Satuation / 255.0;
            var value = hsv.Value / 255.0;

            // Convert to ARGB
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static Bgra ToBgra(this Hsv hsv)
        {
            var argb = hsv.ToArgb();
            return new Bgra(argb.B, argb.G, argb.R, argb.A);
        }
    }
}
