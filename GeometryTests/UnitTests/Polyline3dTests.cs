using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;

namespace GeometryTests.UnitTests
{
    public class Polyline3dTests
    {
        [Fact]
        public void CreatePolylineTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(10, LengthUnit.Centimeter);
            var x2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);
            var z2 = new Length(-2.5, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var pts = new List<IPoint3d>() { pt1, pt2 };
            IPolyline3d Polyline = new Polyline3d(pts);

            // Assert
            TestUtility.TestLengthsAreEqual(x1, Polyline.Points[0].X);
            TestUtility.TestLengthsAreEqual(y1, Polyline.Points[0].Y);
            TestUtility.TestLengthsAreEqual(z1, Polyline.Points[0].Z);
            TestUtility.TestLengthsAreEqual(x2, Polyline.Points[1].X);
            TestUtility.TestLengthsAreEqual(y2, Polyline.Points[1].Y);
            TestUtility.TestLengthsAreEqual(z2, Polyline.Points[1].Z);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(10, LengthUnit.Centimeter);
            var x2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);
            var z2 = new Length(-2.5, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var pts = new List<IPoint3d>() { pt1, pt2 };
            var Polyline = new Polyline3d(pts);

            // Assert
            Assert.Equal("3D Polyline (2 points;Open)", Polyline.ToString());
        }

        [Fact]
        public void CastPolylineToLineTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(10, LengthUnit.Centimeter);
            var x2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);
            var z2 = new Length(-2.5, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var pts = new List<IPoint3d>() { pt1, pt2 };
            var Polyline = new Polyline3d(pts);
            var ln = (Line3d)Polyline;

            // Assert
            TestUtility.TestLengthsAreEqual(x1, ln.Start.X);
            TestUtility.TestLengthsAreEqual(y1, ln.Start.Y);
            TestUtility.TestLengthsAreEqual(z1, ln.Start.Z);
            TestUtility.TestLengthsAreEqual(x2, ln.End.X);
            TestUtility.TestLengthsAreEqual(y2, ln.End.Y);
            TestUtility.TestLengthsAreEqual(z2, ln.End.Z);
        }

        [Fact]
        public void PolylineSurvivesJsonRoundtripTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(10, LengthUnit.Centimeter);
            var x2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);
            var z2 = new Length(-2.5, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var pts = new List<IPoint3d>() { pt1, pt2 };
            IPolyline3d Polyline = new Polyline3d(pts);
            string json = Polyline.ToJson();
            IPolyline3d poligonDeserialized = json.FromJson<Polyline3d>();

            // Assert
            TestUtility.TestLengthsAreEqual(x1, Polyline.Points[0].X);
            TestUtility.TestLengthsAreEqual(y1, Polyline.Points[0].Y);
            TestUtility.TestLengthsAreEqual(z1, Polyline.Points[0].Z);
            TestUtility.TestLengthsAreEqual(x2, Polyline.Points[1].X);
            TestUtility.TestLengthsAreEqual(y2, Polyline.Points[1].Y);
            TestUtility.TestLengthsAreEqual(z2, Polyline.Points[1].Z);
        }

        [Fact]
        public void ThrowsForInvalidInputTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(10, LengthUnit.Centimeter);

            // Assert
            Assert.Throws<ArgumentException>(() => new Polyline3d(new List<IPoint3d>()));

            // Act
            IPoint3d pt = new Point3d(x, y, z);
            var pts = new List<IPoint3d>() { pt };
            Assert.Throws<ArgumentException>(() => new Polyline3d(pts));
        }
    }
}
