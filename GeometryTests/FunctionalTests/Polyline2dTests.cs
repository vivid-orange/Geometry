using VividOrange.Geometry;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.FunctionalTests
{
    public class Polyline2dTests
    {
        [Fact]
        public void GetBarycenterTest()
        {
            // Assemble
            var pt1 = new Point2d(1, 2, LengthUnit.Meter);
            var pt2 = new Point2d(3, 4, LengthUnit.Meter);

            // Act
            var poly = new Polyline2d(new List<IPoint2d>() { pt1, pt2 });
            Point2d center = poly.GetBarycenter();

            // Assert
            Assert.Equal(2, center.U.Value);
            Assert.Equal(3, center.V.Value);
        }


        [Fact]
        public void GetClosestVertexTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Meter);
            var pt2 = new Point2d(4, 0, LengthUnit.Meter);
            var pt = new Point2d(5, 1, LengthUnit.Meter);

            // Act
            var poly = new Polyline2d(new List<IPoint2d>() { pt1, pt2 });
            Point2d ptOnLine = poly.GetClosest(pt);

            // Assert
            Assert.Equal(4, ptOnLine.U.Value);
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
            var Polyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3 });
            (bool close, Point2d pt) ptClose1 = Polyline.IsCloseToPolyline(pt, dist1);
            (bool close, Point2d pt) ptClose2 = Polyline.IsCloseToPolyline(pt, dist2);

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
            var Polyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });
            Polyline2d rotated = Polyline.Rotate(new Angle(90, AngleUnit.Degree));

            // Assert
            Assert.Equal(0, rotated.Points[0].U.Value, 6);
            Assert.Equal(0, rotated.Points[0].V.Value, 6);
            Assert.Equal(0, rotated.Points[1].U.Value, 6);
            Assert.Equal(4, rotated.Points[1].V.Value, 6);
            Assert.Equal(-4, rotated.Points[2].U.Value, 6);
            Assert.Equal(4, rotated.Points[2].V.Value, 6);
            Assert.Equal(-4, rotated.Points[3].U.Value, 6);
            Assert.Equal(0, rotated.Points[3].V.Value, 6);
        }


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
            var Polyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });

            // Assert
            Assert.True(Polyline.IsInside(testInside));
            Assert.False(Polyline.IsInside(testOutside));
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
            var Polyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });
            Area area = Polyline.GetArea();

            // Assert
            Assert.Equal(AreaUnit.SquareMillimeter, area.Unit);
            Assert.Equal(4 * 4, area.SquareMeters);
        }

        [Fact]
        public void IsClosedTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new Point2d(400, 0, LengthUnit.Centimeter);
            var pt3 = new Point2d(4, 4, LengthUnit.Meter);
            var pt4 = new Point2d(0, 4, LengthUnit.Meter);
            var pt5 = new Point2d(0, 0, LengthUnit.Millimeter);

            // Act
            var openPolyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });
            var closedPolyline = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4, pt5 });

            // Assert
            Assert.False(openPolyline.IsClosed);
            Assert.True(closedPolyline.IsClosed);
        }

        [Fact]
        public void IsClockwiseTest()
        {
            // Assemble
            var pt1 = new Point2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new Point2d(0, 250, LengthUnit.Centimeter);
            var pt3 = new Point2d(250, 250, LengthUnit.Meter);
            var pt4 = new Point2d(250, 0, LengthUnit.Meter);
            var pt5 = new Point2d(Length.Zero, Length.Zero);

            // Act
            var clockwiseOpen = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });
            var counterclockwiseOpen = new Polyline2d(new List<IPoint2d> { pt4, pt3, pt2, pt1 });
            var clockwiseClosed = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4, pt5 });
            var counterclockwiseClosed = new Polyline2d(new List<IPoint2d> { pt5, pt4, pt3, pt2, pt1 });

            // Assert
            Assert.False(clockwiseOpen.IsClosed);
            Assert.False(counterclockwiseOpen.IsClosed);
            Assert.True(clockwiseOpen.IsClockwise());
            Assert.False(counterclockwiseOpen.IsClockwise());
            Assert.True(clockwiseClosed.IsClosed);
            Assert.True(counterclockwiseClosed.IsClosed);
            Assert.True(clockwiseClosed.IsClockwise());
            Assert.False(counterclockwiseClosed.IsClockwise());
        }

        [Fact]
        public void OffsetTest()
        {
            // Assemble
            LengthUnit u = LengthUnit.Centimeter;
            var pt1 = new Point2d(0, 0, u);
            var pt2 = new Point2d(0, 250, u);
            var pt3 = new Point2d(250, 250, u);
            var pt4 = new Point2d(250, 0, u);
            var pt5 = new Point2d(Length.Zero, Length.Zero);

            var offset = new Length(50, u);

            var clockwiseOpen = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4 });
            var counterclockwiseOpen = new Polyline2d(new List<IPoint2d> { pt4, pt3, pt2, pt1 });
            var clockwiseClosed = new Polyline2d(new List<IPoint2d> { pt1, pt2, pt3, pt4, pt5 });
            var counterclockwiseClosed = new Polyline2d(new List<IPoint2d> { pt5, pt4, pt3, pt2, pt1 });

            // Act
            IPolyline2d clockwiseOpenOffset = clockwiseOpen.Offset(offset);
            IPolyline2d counterclockwiseOpenOffset = counterclockwiseOpen.Offset(offset);
            IPolyline2d clockwiseClosedOffset = clockwiseClosed.Offset(offset);
            IPolyline2d counterclockwiseClosedOffset = counterclockwiseClosed.Offset(offset);

            // Assert
            Assert.False(clockwiseOpenOffset.IsClosed);
            Assert.Equal(4, clockwiseOpenOffset.Points.Count);
            int i = 0;
            Assert.Equal(-50, clockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(0, clockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(-50, clockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(0, clockwiseOpenOffset.Points[i++].V.As(u), 12);

            Assert.False(counterclockwiseOpenOffset.IsClosed);
            Assert.Equal(4, counterclockwiseOpenOffset.Points.Count);
            i = 0;
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(0, counterclockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(50, counterclockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i++].V.As(u), 12);
            Assert.Equal(50, counterclockwiseOpenOffset.Points[i].U.As(u), 12);
            Assert.Equal(0, counterclockwiseOpenOffset.Points[i++].V.As(u), 12);

            Assert.True(clockwiseClosedOffset.IsClosed);
            Assert.Equal(5, clockwiseClosedOffset.Points.Count);
            i = 0;
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].V.As(u), 12);

            Assert.True(counterclockwiseClosedOffset.IsClosed);
            Assert.Equal(5, counterclockwiseClosedOffset.Points.Count);
            i = 0;
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i++].V.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].U.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].V.As(u), 12);
        }
    }
}
