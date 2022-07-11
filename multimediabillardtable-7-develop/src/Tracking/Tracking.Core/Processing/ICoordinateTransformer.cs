using System.Drawing;

namespace Tracking.Core.Processing
{
    internal interface ICoordinateTransformer
    {
        /// <summary>
        /// Transforms the point from playfield space to raw color space.
        /// </summary>
        /// <param name="point">Point in playfield space</param>
        /// <returns>Point in raw color space</returns>
        PointF TransformPlayfieldToRaw(PointF point);
    }
}
