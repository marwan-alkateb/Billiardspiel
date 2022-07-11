using Emgu.CV;

namespace Tracking.Core.Utils
{
    internal static class ImageUtil
    {
        /// <summary>
        /// Inverts the image by applying p' = 255 - p to each pixel.
        /// </summary>
        /// <remarks>
        /// WARNING: Assumes a grayscale image
        /// </remarks>
        /// <param name="mat"></param>
        public static unsafe void InvertImage(Mat mat)
        {
            var ptr = (byte*)mat.DataPointer.ToPointer();
            var width = mat.Width;
            var height = mat.Height;
            var offset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ptr[offset] = (byte)(255 - ptr[offset]);
                    offset++;
                }
            }
        }
    }
}
