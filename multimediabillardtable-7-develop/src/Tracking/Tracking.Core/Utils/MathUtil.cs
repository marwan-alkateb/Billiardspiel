﻿namespace Tracking.Core.Maths
{
    public static class MathUtil
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
    }
}
