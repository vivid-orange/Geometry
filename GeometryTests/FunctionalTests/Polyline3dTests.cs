using UnitsNet.Units;
using VividOrange.Geometry;

namespace GeometryTests.FunctionalTests
{
    public class Polyline3dTests
    {
        [Fact]
        public void GetBarycenterTest()
        {
            // Assemble
            var pt1 = new Point3d(1, 2, 3, LengthUnit.Meter);
            var pt2 = new Point3d(3, 4, 5, LengthUnit.Meter);

            // Act
            var poly = new Polyline3d(new List<IPoint3d>() { pt1, pt2 });
            Point3d center = poly.GetBarycenter();

            // Assert
            Assert.Equal(2, center.X.Value);
            Assert.Equal(3, center.Y.Value);
            Assert.Equal(4, center.Z.Value);
        }

        [Fact]
        public void PlaneLineIntersectionTest()
        {
            // Assemble
            var pt1 = new Point3d(1, 1, 0, LengthUnit.Meter);
            var pt2 = new Point3d(1, 1, 2, LengthUnit.Meter);
            var pln1 = new Point3d(0, 0, 1, LengthUnit.Meter);
            var pln2 = new Point3d(2, 0, 1, LengthUnit.Meter);
            var pln3 = new Point3d(2, 2, 1, LengthUnit.Meter);

            // Act
            var ln = new Polyline3d(new List<IPoint3d> { pt1, pt2 });
            var pln = new Polyline3d(new List<IPoint3d> { pln1, pln2, pln3 });
            Point3d intersect = Polyline3d.PlaneLineIntersection(ln, pln);

            // Assert
            Assert.Equal(1, intersect.X.Value);
            Assert.Equal(1, intersect.Y.Value);
            Assert.Equal(1, intersect.Z.Value);
        }

        [Fact]
        public void IsClosedTest()
        {
            // Assemble
            var pt1 = new Point3d(0, 0, 0, LengthUnit.Meter);
            var pt2 = new Point3d(4, 0, 0, LengthUnit.Meter);
            var pt3 = new Point3d(4, 4, 0, LengthUnit.Meter);
            var pt4 = new Point3d(0, 4, 0, LengthUnit.Meter);
            var pt5 = new Point3d(0, 0, 0, LengthUnit.Meter);

            // Act
            var openPolyline = new Polyline3d(new List<IPoint3d> { pt1, pt2, pt3, pt4 });
            var closedPolyline = new Polyline3d(new List<IPoint3d> { pt1, pt2, pt3, pt4, pt5 });

            // Assert
            Assert.False(openPolyline.IsClosed);
            Assert.True(closedPolyline.IsClosed);
        }

        //[Fact]
        //public void IsInsideTest()
        //{
        //    // Assemble
        //    var pt1 = new Point3d(0, 0, 0, LengthUnit.Meter);
        //    var pt2 = new Point3d(4, 0, 0, LengthUnit.Meter);
        //    var pt3 = new Point3d(4, 4, 0, LengthUnit.Meter);
        //    var pt4 = new Point3d(0, 4, 0, LengthUnit.Meter);
        //    var testInside = new Point3d(2, 2, 0, LengthUnit.Meter);
        //    var testOutside = new Point3d(-2, 2, 0, LengthUnit.Meter);

        //    // Act
        //    var Polyline = new Polyline3d(new List<IPoint3d> { pt1, pt2, pt3, pt4 });

        //    // Assert
        //    Assert.True(Polyline.IsInsidePlane(testInside));
        //    Assert.False(Polyline.IsInsidePlane(testOutside));
        //}
    }
}
