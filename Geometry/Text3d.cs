namespace VividOrange.Geometry
{
    public class Text3d : IText3d
    {
        public IPoint3d Position { get; set; }
        public double Height { get; set; }
        public string Text { get; set; }
        public IVector3d Direction { get; set; }
        public IVector3d Up { get; set; } = Vector3d.UnitZ;
        public bool IsDoubleSided { get; set; } = true;
        public IColor Color { get; set; } = new Color();
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Centre;
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Centre;

        private Text3d() { }

        public Text3d(string text, IPoint3d position, IVector3d direction, double height)
        {
            Position = position;
            Direction = direction;
            Text = text;
            Height = height;
        }

        public Text3d(string text, IPoint3d position, IVector3d direction, double height, IVector3d up)
        {
            Position = position;
            Direction = direction;
            Text = text;
            Height = height;
            Up = up;
        }
    }
}
