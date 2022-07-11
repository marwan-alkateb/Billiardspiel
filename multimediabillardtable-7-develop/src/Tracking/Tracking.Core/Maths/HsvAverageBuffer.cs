using Emgu.CV.Structure;
using System;

namespace Tracking.Core.Maths
{
    internal class HsvAverageBuffer
    {
        private double hueXAccum;
        private double hueYAccum;
        private double satAccum;
        private double valAccum;
        private double counter;

        public void Reset()
        {
            hueXAccum = 0;
            hueYAccum = 0;
            satAccum = 0;
            valAccum = 0;
            counter = 0;
        }

        public void Push(Hsv hsv, bool saturationWeighted = false)
        {
            // Since Hue values are angular values, we cannot average them with
            // the classic average. An easy way to average angles, is by converting
            // them into vectors using sine and cosine, and averaging those. This is
            // what we do here:
            var factor = saturationWeighted ? hsv.Satuation / 255.0f : 1.0f;
            hueXAccum += Math.Cos(hsv.Hue / 180 * Math.PI) * factor;
            hueYAccum += Math.Sin(hsv.Hue / 180 * Math.PI) * factor;
            satAccum += hsv.Satuation * factor;
            valAccum += hsv.Value * factor;
            counter += factor;
        }

        public Hsv Average
        {
            get
            {
                if (counter == 0)
                    return default;

                var hueXAverage = hueXAccum / counter;
                var hueYAverage = hueYAccum / counter;
                var hueAverage = Math.Atan2(hueYAverage, hueXAverage) * 180 / Math.PI;
                var satAverage = satAccum / counter;
                var valAverage = valAccum / counter;
                return new Hsv(hueAverage, satAverage, valAverage);
            }
        }

        public int Counter => (int)counter;

    }
}
