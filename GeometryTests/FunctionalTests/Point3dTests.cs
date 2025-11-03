using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;

namespace GeometryTests.FunctionalTests
{
    public class Point3dTests
    {
        [Fact]
        public void GetBarycenterTest()
        {
            // Assemble
            var pt1 = new Point3d(1, 2, 3, LengthUnit.Meter);
            var pt2 = new Point3d(3, 4, 5, LengthUnit.Meter);

            // Act
            var list = new List<IPoint3d>() { pt1, pt2 };
            Point3d center = Point3d.GetBarycenter(list);

            // Assert
            Assert.Equal(2, center.X.Value);
            Assert.Equal(3, center.Y.Value);
            Assert.Equal(4, center.Z.Value);
        }

        [Fact]
        public void DistanceTest()
        {
            // Assemble
            var pt1 = new Point3d(0, 0, 0, LengthUnit.Meter);
            var pt2 = new Point3d(4, 3, 0, LengthUnit.Meter);

            // Act
            Length dist = Point3d.Distance(pt1, pt2);

            // Assert
            Assert.Equal(5, dist.Value);
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
            var ln = new List<IPoint3d> { pt1, pt2 };
            var pln = new List<IPoint3d> { pln1, pln2, pln3 };
            Point3d intersect = Point3d.PlaneLineIntersection(ln, pln);

            // Assert
            Assert.Equal(1, intersect.X.Value);
            Assert.Equal(1, intersect.Y.Value);
            Assert.Equal(1, intersect.Z.Value);
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
        //    var list = new List<IPoint3d> { pt1, pt2, pt3, pt4 };

        //    // Assert
        //    Assert.True(Point3d.IsInsidePlane(testInside, list));
        //    Assert.False(Point3d.IsInsidePlane(testOutside, list));
        //}

        //[Fact]
        //public void GetTransformMatrix2dTo3d()
        //{
        //    // Assemble
        //    // Act
        //    // Assert
        //}

        //[Fact]
        //public void GetTransformMatrixPlaneIn3dTo2d()
        //{
        //    // Assemble
        //    // Act
        //    // Assert
        //}

        //[Fact]
        //public void TransformedPoint()
        //{
        //    // Assemble
        //    // Act
        //    // Assert
        //}
    }
}
