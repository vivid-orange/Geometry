namespace VividOrange.Geometry
{
    public class Brush : IBrush
    {
        public ShadingType Shading { get; set; } = ShadingType.Solid;
        public IColor Color { get; set; }

        public Brush()
        {
            Color = new Color();
        }

        public Brush(byte red, byte green, byte blue)
        {
            Color = new Color(255, red, green, blue);
        }

        public Brush(byte alpha, byte red, byte green, byte blue)
        {
            Color = new Color(alpha, red, green, blue);
        }
    }
}
