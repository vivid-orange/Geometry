using System;

namespace VividOrange.Geometry
{
    public class Color : IColor
    {
        public int ColorInt { get; }
        public byte Alpha => (byte)((ColorInt >> (8 * 0)) & 0xff);
        public byte Red => (byte)((ColorInt >> (8 * 1)) & 0xff);
        public byte Green => (byte)((ColorInt >> (8 * 2)) & 0xff);
        public byte Blue => (byte)((ColorInt >> (8 * 3)) & 0xff);

        public Color()
        {
            ColorInt = BitConverter.ToInt32(new byte[] { 255, 0, 0, 0 }, 0);
        }

        public Color(byte alpha, byte red, byte green, byte blue)
        {
            ColorInt = BitConverter.ToInt32(new byte[] { alpha, red, green, blue }, 0);
        }
    }
}
