using Emgu.CV;
using Emgu.CV.Structure;

namespace Tracking.Core.Utils
{
    /// <summary>
    /// Wraps an EmguCV <see cref="Image{TColor, TDepth}"/> (Bgra/byte only) and
    /// provides very fast pixel access to it.
    /// </summary>
    /// <remarks>
    /// EmguCV's native images internally use a P/Invoke call for each pixel. This is very slow and inefficient.
    /// To overcome this performance bottleneck, <see cref="FastImage"/> uses direct unsafe access to the 
    /// pixel data, which is magnitudes faster.
    /// </remarks>
    internal unsafe class FastImage
    {
        private readonly byte* ptr;
        private readonly int pixelStride;
        private readonly int rowStride;

        public int Width { get; }

        public int Height { get; }

        public FastImage(Image<Bgra, byte> image)
        {
            Width = image.Width;
            Height = image.Height;
            pixelStride = image.NumberOfChannels;
            rowStride = Width * pixelStride;
            ptr = (byte*)image.Mat.DataPointer.ToPointer();
        }

        public Bgra this[int x, int y]
        {
            get
            {
                var offset = y * rowStride + x * pixelStride;
                return new Bgra(ptr[offset], ptr[offset + 1], ptr[offset + 2], ptr[offset + 3]);
            }
            set
            {
                var offset = y * rowStride + x * pixelStride;
                ptr[offset + 0] = (byte)value.Blue;
                ptr[offset + 1] = (byte)value.Green;
                ptr[offset + 2] = (byte)value.Red;
                ptr[offset + 3] = (byte)value.Alpha;
            }
        }

    }
}
