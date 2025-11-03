using GeometryTests.Utility;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.UnitTests
{
    public class Line3dTests
    {
        [Fact]
        public void CreateLineTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(-2.3, LengthUnit.Centimeter);
            var y2 = new Length(-5.4, LengthUnit.Centimeter);
            var z2 = new Length(-6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            ILine3d ln = new Line3d(pt1, pt2);

            // Assert
            TestUtility.TestLengthsAreEqual(x1, ln.Start.X);
            TestUtility.TestLengthsAreEqual(x2, ln.End.X);
            TestUtility.TestLengthsAreEqual(y1, ln.Start.Y);
            TestUtility.TestLengthsAreEqual(y2, ln.End.Y);
            TestUtility.TestLengthsAreEqual(z1, ln.Start.Z);
            TestUtility.TestLengthsAreEqual(z2, ln.End.Z);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(-2.3, LengthUnit.Centimeter);
            var y2 = new Length(-5.4, LengthUnit.Centimeter);
            var z2 = new Length(-6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var ln = new Line3d(pt1, pt2);

            // Assert
            Assert.Equal("3D Line (S:(X:2.3 cm, Y:5.4 cm, Z:6.8 cm) - E:(X:-2.3 cm, Y:-5.4 cm, Z:-6.8 cm)", ln.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void LineSurvivesJsonRoundtripTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(-2.3, LengthUnit.Centimeter);
            var y2 = new Length(-5.4, LengthUnit.Centimeter);
            var z2 = new Length(-6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            ILine3d ln = new Line3d(pt1, pt2);
            string json = ln.ToJson();
            ILine3d lnDeserialized = json.FromJson<Line3d>();

            // Assert
            TestUtility.TestLengthsAreEqual(x1, lnDeserialized.Start.X);
            TestUtility.TestLengthsAreEqual(x2, lnDeserialized.End.X);
            TestUtility.TestLengthsAreEqual(y1, lnDeserialized.Start.Y);
            TestUtility.TestLengthsAreEqual(y2, lnDeserialized.End.Y);
            TestUtility.TestLengthsAreEqual(z1, lnDeserialized.Start.Z);
            TestUtility.TestLengthsAreEqual(z2, lnDeserialized.End.Z);
        }

        [Fact]
        public void LineCastToVectorTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(-2.3, LengthUnit.Centimeter);
            var y2 = new Length(-5.4, LengthUnit.Centimeter);
            var z2 = new Length(-6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var ln = new Line3d(pt1, pt2);
            var vector = (Vector3d)ln;

            // Assert
            TestUtility.TestLengthsAreEqual(x1 - x2, vector.X);
            TestUtility.TestLengthsAreEqual(y1 - y2, vector.Y);
            TestUtility.TestLengthsAreEqual(z1 - z2, vector.Z);
        }

        [Fact]
        public void LineCastToPolylineTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(-2.3, LengthUnit.Centimeter);
            var y2 = new Length(-5.4, LengthUnit.Centimeter);
            var z2 = new Length(-6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt1 = new Point3d(x1, y1, z1);
            IPoint3d pt2 = new Point3d(x2, y2, z2);
            var ln = new Line3d(pt1, pt2);
            var Polyline = (Polyline3d)ln;

            // Assert
            TestUtility.TestLengthsAreEqual(x1, Polyline.Points[0].X);
            TestUtility.TestLengthsAreEqual(x2, Polyline.Points[1].X);
            TestUtility.TestLengthsAreEqual(y1, Polyline.Points[0].Y);
            TestUtility.TestLengthsAreEqual(y2, Polyline.Points[1].Y);
            TestUtility.TestLengthsAreEqual(z1, Polyline.Points[0].Z);
            TestUtility.TestLengthsAreEqual(z2, Polyline.Points[1].Z);
        }
    }
}
