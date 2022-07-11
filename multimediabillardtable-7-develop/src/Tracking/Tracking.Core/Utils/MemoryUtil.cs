using System;

namespace Tracking.Core.Utils
{
    internal static class MemoryUtil
    {
        /// <summary>
        /// Fills the given pointer with zeroes
        /// </summary>
        /// <remarks>
        /// WARNING: size must be divisble by 8
        /// </remarks>
        /// <param name="ptr">Destination pointer</param>
        /// <param name="size">Number of zeroes to write</param>
        public static unsafe void ZeroMemory(byte* ptr, int size)
        {
            for (int i = 0; i < size; i += 8)
                *(ulong*)(ptr + i) = 0x00;
        }

        /// <summary>
        /// Fills the given pointer with zeroes.
        /// </summary>
        /// <remarks>
        /// WARNING: size must be divisble by 8
        /// </remarks>
        /// <param name="ptr">Destination pointer</param>
        /// <param name="size">Number of zeroes to write</param>
        public static unsafe void ZeroMemory(IntPtr ptr, int size) => ZeroMemory((byte*)ptr.ToPointer(), size);
    }
}
