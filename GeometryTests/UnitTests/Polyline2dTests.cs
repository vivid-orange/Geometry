using GeometryTests.Utility;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.UnitTests
{
    public class Polyline2dTests
    {
        [Fact]
        public void CreatePolylineTest()
        {
            // Assemble
            var u1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var u2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);

            // Act
            IPoint2d pt1 = new Point2d(u1, y1);
            IPoint2d pt2 = new Point2d(u2, y2);
            var pts = new List<IPoint2d>() { pt1, pt2 };
            IPolyline2d Polyline = new Polyline2d(pts);

            // Assert
            TestUtility.TestLengthsAreEqual(u1, Polyline.Points[0].U);
            TestUtility.TestLengthsAreEqual(y1, Polyline.Points[0].V);
            TestUtility.TestLengthsAreEqual(u2, Polyline.Points[1].U);
            TestUtility.TestLengthsAreEqual(y2, Polyline.Points[1].V);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var u1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var u2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);

            // Act
            IPoint2d pt1 = new Point2d(u1, y1);
            IPoint2d pt2 = new Point2d(u2, y2);
            var pts = new List<IPoint2d>() { pt1, pt2 };
            var Polyline = new Polyline2d(pts);

            // Assert
            Assert.Equal("2D Polyline (2 points;Open)", Polyline.ToString());
        }

        [Fact]
        public void CastPolylineToLineTest()
        {
            // Assemble
            var u1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var u2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);

            // Act
            IPoint2d pt1 = new Point2d(u1, y1);
            IPoint2d pt2 = new Point2d(u2, y2);
            var pts = new List<IPoint2d>() { pt1, pt2 };
            var Polyline = new Polyline2d(pts);
            Line2d ln = (Line2d)Polyline;

            // Assert
            TestUtility.TestLengthsAreEqual(u1, ln.Start.U);
            TestUtility.TestLengthsAreEqual(y1, ln.Start.V);
            TestUtility.TestLengthsAreEqual(u2, ln.End.U);
            TestUtility.TestLengthsAreEqual(y2, ln.End.V);
        }

        [Fact]
        public void PolylineSurvivesJsonRoundtripTest()
        {
            // Assemble
            var u1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var u2 = new Length(-5.0, LengthUnit.Centimeter);
            var y2 = new Length(7.1, LengthUnit.Centimeter);

            // Act
            IPoint2d pt1 = new Point2d(u1, y1);
            IPoint2d pt2 = new Point2d(u2, y2);
            var pts = new List<IPoint2d>() { pt1, pt2 };
            IPolyline2d Polyline = new Polyline2d(pts);
            string json = Polyline.ToJson();
            IPolyline2d poligonDeserialized = json.FromJson<Polyline2d>();

            // Assert
            TestUtility.TestLengthsAreEqual(u1, poligonDeserialized.Points[0].U);
            TestUtility.TestLengthsAreEqual(y1, poligonDeserialized.Points[0].V);
            TestUtility.TestLengthsAreEqual(u2, poligonDeserialized.Points[1].U);
            TestUtility.TestLengthsAreEqual(y2, poligonDeserialized.Points[1].V);
        }

        [Fact]
        public void ThrowsForInvalidInputTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);

            // Assert
            Assert.Throws<ArgumentException>(() => new Polyline2d(new List<IPoint2d>()));

            // Act
            IPoint2d pt = new Point2d(x, y);
            var pts = new List<IPoint2d>() { pt };
            Assert.Throws<ArgumentException>(() => new Polyline2d(pts));
        }
    }
}
