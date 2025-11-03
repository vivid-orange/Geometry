using VividOrange.Geometry;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.FunctionalTests
{
    public class Point2dTests
    {
        [Fact]
        public void GetBarycenterTest()
        {
            // Assemble
            var pt1 = new Point2d(1, 2, LengthUnit.Meter);
            var pt2 = new Point2d(3, 4, LengthUnit.Meter);

            // Act
            var list = new List<IPoint2d>() { pt1, pt2 };
            Point2d center = VividOrange.Geometry.Utility.GetCenterLocal(list);

            // Assert
            Assert.Equal(2, center.U.Value);
            Assert.Equal(3, center.V.Value);
        }

        [Fact]
        public void DistanceTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 3, LengthUnit.Meter);

            // Act
            Length dist = Point2d.Distance(pt1, pt2);

            // Assert
            Assert.Equal(5, dist.Value);
        }

        [Fact]
        public void DistancePointToLineTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt = new Point2d(2, 2, LengthUnit.Meter);

            // Act
            Line2d ln = new Line2d(pt1, pt2);
            Length dist = Point2d.DistancePointToLine(ln, pt);

            // Assert
            Assert.Equal(2, dist.Value);
        }

        [Fact]
        public void ProjectPointToLineTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt = new Point2d(2, 2, LengthUnit.Meter);

            // Act
            Line2d ln = new Line2d(pt1, pt2);
            Point2d ptOnLine = Point2d.PointProjOnLine(ln, pt);

            // Assert
            Assert.Equal(2, ptOnLine.U.Value);
            Assert.Equal(0, ptOnLine.V.Value);
        }

        [Fact]
        public void IsCloseToPolylineTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt3 = new Point2d(5, 2, LengthUnit.Meter);
            var pt = new Point2d(2, 2, LengthUnit.Meter);
            Length dist1 = new Length(0.5, LengthUnit.Meter);
            Length dist2 = new Length(2, LengthUnit.Meter);

            // Act
            Polyline2d Polyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3 });
            (bool close, Point2d pt) ptClose1 = Point2d.IsCloseToPolyline(pt, Polyline, dist1);
            (bool close, Point2d pt) ptClose2 = Point2d.IsCloseToPolyline(pt, Polyline, dist2);

            // Assert
            Assert.False(ptClose1.close);
            Assert.Null(ptClose1.pt);
            Assert.True(ptClose2.close);
            Assert.Equal(2, ptClose2.pt.U.Value);
            Assert.Equal(0, ptClose2.pt.V.Value);
        }

        [Fact]
        public void RotatePointsTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt3 = new Point2d(4, 4, LengthUnit.Meter);
            var pt4 = new Point2d(0, 4, LengthUnit.Meter);

            // Act
            var list = new List<IPoint2d> { pt1, pt2, pt3, pt4 };
            List<Point2d> rotated = Point2d.RotatePoints(list, new Angle(90, AngleUnit.Degree));

            // Assert
            Assert.Equal(0, rotated[0].U.Value, 6);
            Assert.Equal(0, rotated[0].V.Value, 6);
            Assert.Equal(0, rotated[1].U.Value, 6);
            Assert.Equal(4, rotated[1].V.Value, 6);
            Assert.Equal(-4, rotated[2].U.Value, 6);
            Assert.Equal(4, rotated[2].V.Value, 6);
            Assert.Equal(-4, rotated[3].U.Value, 6);
            Assert.Equal(0, rotated[3].V.Value, 6);
        }

        [Fact]
        public void RotateZeroPointTest()
        {
            // Assemble
            var pt = new Point2d(0, 0, LengthUnit.Meter);

            // Act
            Point2d rotated = Point2d.RotatePoint(pt, new Angle(90, AngleUnit.Degree));

            // Assert
            Assert.Equal(0, rotated.U.Value, 6);
            Assert.Equal(0, rotated.V.Value, 6);
        }

        [Fact]
        public void RotateZeroTest()
        {
            // Assemble
            var pt = new Point2d(4, 4, LengthUnit.Meter);

            // Act
            Point2d rotated = Point2d.RotatePoint(pt, new Angle(90, AngleUnit.Degree));

            // Assert
            Assert.Equal(-4, rotated.U.Value, 6);
            Assert.Equal(4, rotated.V.Value, 6);
        }

        [Fact]
        public void RotatePointAroundOtherTest()
        {
            // Assemble
            var pt = new Point2d(4, 4, LengthUnit.Meter);
            var center = new Point2d(2, 2, LengthUnit.Meter);

            // Act
            Point2d rotated = Point2d.RotatePoint(pt, new Angle(180, AngleUnit.Degree), center);

            // Assert
            Assert.Equal(0, rotated.U.Value, 6);
            Assert.Equal(0, rotated.V.Value, 6);
        }

        //[Fact]
        //public void GetBoundingBoxTest()
        //{
        //    // Assemble
        //    var pt1 = new Point2d(2, 2, LengthUnit.Meter);
        //    var pt2 = new Point2d(6, 6, LengthUnit.Meter);

        //    // Act
        //    var list = new List<IPoint2d> { pt1, pt2 };
        //    List<Point2d> bbox = Point2d.GetBoundingBox(list);

        //    // Assert
        //    Assert.Equal(6, bbox[0].X.Value, 6);
        //    Assert.Equal(6, bbox[0].Y.Value, 6);
        //    Assert.Equal(2, bbox[1].X.Value, 6);
        //    Assert.Equal(2, bbox[1].Y.Value, 6);
        //    Assert.Equal(2, bbox[2].X.Value, 6);
        //    Assert.Equal(2, bbox[2].Y.Value, 6);
        //    Assert.Equal(6, bbox[3].X.Value, 6);
        //    Assert.Equal(6, bbox[3].Y.Value, 6);
        //}

        [Fact]
        public void IsInsideTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt3 = new Point2d(4, 4, LengthUnit.Meter);
            var pt4 = new Point2d(0, 4, LengthUnit.Meter);
            var testInside = new Point2d(2, 2, LengthUnit.Meter);
            var testOutside = new Point2d(-2, 2, LengthUnit.Meter);

            // Act
            var list = new List<IPoint2d> { pt1, pt2, pt3, pt4 };

            // Assert
            Assert.True(Point2d.IsInside(list, testInside));
            Assert.False(Point2d.IsInside(list, testOutside));
        }

        [Fact]
        public void GetPolylineAreaTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new Point2d(400, 0, LengthUnit.Centimeter);
            var pt3 = new Point2d(4, 4, LengthUnit.Meter);
            var pt4 = new Point2d(0, 4, LengthUnit.Meter);

            // Act
            var list = new List<IPoint2d> { pt1, pt2, pt3, pt4 };
            Area area = Point2d.GetPolylineArea(list);

            // Assert
            Assert.Equal(AreaUnit.SquareMillimeter, area.Unit);
            Assert.Equal(4 * 4, area.SquareMeters);
        }

        //[Fact]
        //public void Contract()
        //{
        //    // Assemble
        //var pt1 = new Point2d(-4, -4, LengthUnit.Meter);
        //var pt2 = new Point2d(-4, 4, LengthUnit.Meter);
        //var pt3 = new Point2d(4, 4, LengthUnit.Meter);
        //var pt4 = new Point2d(4, -4, LengthUnit.Meter);

        //    // Act
        //    var list = new List<IPoint2d> { pt1, pt2, pt3, pt4 };
        //    IList<IPoint2d> contracted = Point2d.Contract(list, 2.0);

        //    // Assert
        //    Assert.Equal(2, contracted[0].X.Value, 6);
        //    Assert.Equal(2, contracted[0].Y.Value, 6);
        //    Assert.Equal(2, contracted[1].X.Value, 6);
        //    Assert.Equal(2, contracted[1].Y.Value, 6);
        //    Assert.Equal(2, contracted[2].X.Value, 6);
        //    Assert.Equal(2, contracted[2].Y.Value, 6);
        //    Assert.Equal(6, contracted[3].X.Value, 6);
        //    Assert.Equal(6, contracted[3].Y.Value, 6);
        //}

        //[Fact]
        //public void Extend()
        //{
        //    // Assemble
        //    var pt1 = new Point2d(-4, -4, LengthUnit.Meter);
        //    var pt2 = new Point2d(-4, 4, LengthUnit.Meter);
        //    var pt3 = new Point2d(4, 4, LengthUnit.Meter);
        //    var pt4 = new Point2d(4, -4, LengthUnit.Meter);

        //    // Act
        //    var list = new List<IPoint2d> { pt1, pt2, pt3, pt4 };
        //    IList<IPoint2d> contracted = Point2d.Extend(list, 10.0);

        //    // Assert
        //    Assert.Equal(2, contracted[0].X.Value, 6);
        //    Assert.Equal(2, contracted[0].Y.Value, 6);
        //    Assert.Equal(2, contracted[1].X.Value, 6);
        //    Assert.Equal(2, contracted[1].Y.Value, 6);
        //    Assert.Equal(2, contracted[2].X.Value, 6);
        //    Assert.Equal(2, contracted[2].Y.Value, 6);
        //    Assert.Equal(6, contracted[3].X.Value, 6);
        //    Assert.Equal(6, contracted[3].Y.Value, 6);
        //}
    }
}
