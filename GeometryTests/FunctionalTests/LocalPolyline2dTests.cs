using VividOrange.Geometry;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.FunctionalTests
{
    public class LocalPolyline2dTests
    {
        [Fact]
        public void GetBarycenterTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(1, 2, LengthUnit.Meter);
            var pt2 = new LocalPoint2d(3, 4, LengthUnit.Meter);

            // Act
            var poly = new LocalPolyline2d(new List<ILocalPoint2d>() { pt1, pt2 });
            LocalPoint2d center = poly.GetBarycenter();

            // Assert
            Assert.Equal(2, center.Y.Value);
            Assert.Equal(3, center.Z.Value);
        }

        [Fact]
        public void RotatePointsTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(0, 0, LengthUnit.Meter);
            var pt2 = new LocalPoint2d(4, 0, LengthUnit.Meter);
            var pt3 = new LocalPoint2d(4, 4, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(0, 4, LengthUnit.Meter);

            // Act
            var Polyline = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            LocalPolyline2d rotated = Polyline.Rotate(new Angle(90, AngleUnit.Degree));

            // Assert
            Assert.Equal(0, rotated.Points[0].Y.Value, 6);
            Assert.Equal(0, rotated.Points[0].Z.Value, 6);
            Assert.Equal(0, rotated.Points[1].Y.Value, 6);
            Assert.Equal(4, rotated.Points[1].Z.Value, 6);
            Assert.Equal(-4, rotated.Points[2].Y.Value, 6);
            Assert.Equal(4, rotated.Points[2].Z.Value, 6);
            Assert.Equal(-4, rotated.Points[3].Y.Value, 6);
            Assert.Equal(0, rotated.Points[3].Z.Value, 6);
        }

        [Fact]
        public void GetPolylineAreaTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new LocalPoint2d(400, 0, LengthUnit.Centimeter);
            var pt3 = new LocalPoint2d(4, 4, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(0, 4, LengthUnit.Meter);

            // Act
            var Polyline = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            Area area = Polyline.GetArea();

            // Assert
            Assert.Equal(AreaUnit.SquareMillimeter, area.Unit);
            Assert.Equal(4 * 4, area.SquareMeters);
        }

        [Fact]
        public void GetDomainTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(-100, 0, LengthUnit.Millimeter);
            var pt2 = new LocalPoint2d(400, -250, LengthUnit.Centimeter);
            var pt3 = new LocalPoint2d(4, 4, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(0, 5, LengthUnit.Meter);

            // Act
            var Polyline = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            ILocalDomain2d domain = Polyline.Domain();

            // Assert
            Assert.Equal(4, domain.Max.Y.Meters);
            Assert.Equal(5, domain.Max.Z.Meters);
            Assert.Equal(-100, domain.Min.Y.Millimeters);
            Assert.Equal(-250, domain.Min.Z.Centimeters);
        }

        [Fact]
        public void IsClosedTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new LocalPoint2d(400, -250, LengthUnit.Centimeter);
            var pt3 = new LocalPoint2d(4, 4, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(0, 5, LengthUnit.Meter);
            var pt5 = new LocalPoint2d(Length.Zero, Length.Zero);

            // Act
            var openPolyline = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            var closedPolyline = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4, pt5 });

            // Assert
            Assert.False(openPolyline.IsClosed);
            Assert.True(closedPolyline.IsClosed);
        }

        [Fact]
        public void IsClockwiseTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new LocalPoint2d(0, 250, LengthUnit.Centimeter);
            var pt3 = new LocalPoint2d(250, 250, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(250, 0, LengthUnit.Meter);
            var pt5 = new LocalPoint2d(Length.Zero, Length.Zero);

            // Act
            var clockwiseOpen = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            var counterclockwiseOpen = new LocalPolyline2d(new List<ILocalPoint2d> { pt4, pt3, pt2, pt1 });
            var clockwiseClosed = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4, pt5 });
            var counterclockwiseClosed = new LocalPolyline2d(new List<ILocalPoint2d> { pt5, pt4, pt3, pt2, pt1 });

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
            var pt1 = new LocalPoint2d(0, 0, u);
            var pt2 = new LocalPoint2d(0, 250, u);
            var pt3 = new LocalPoint2d(250, 250, u);
            var pt4 = new LocalPoint2d(250, 0, u);
            var pt5 = new LocalPoint2d(Length.Zero, Length.Zero);

            var offset = new Length(50, u);

            var clockwiseOpen = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 });
            var counterclockwiseOpen = new LocalPolyline2d(new List<ILocalPoint2d> { pt4, pt3, pt2, pt1 });
            var clockwiseClosed = new LocalPolyline2d(new List<ILocalPoint2d> { pt1, pt2, pt3, pt4, pt5 });
            var counterclockwiseClosed = new LocalPolyline2d(new List<ILocalPoint2d> { pt5, pt4, pt3, pt2, pt1 });

            // Act
            ILocalPolyline2d clockwiseOpenOffset = clockwiseOpen.Offset(offset);
            ILocalPolyline2d counterclockwiseOpenOffset = counterclockwiseOpen.Offset(offset);
            ILocalPolyline2d clockwiseClosedOffset = clockwiseClosed.Offset(offset);
            ILocalPolyline2d counterclockwiseClosedOffset = counterclockwiseClosed.Offset(offset);

            // Assert
            Assert.False(clockwiseOpenOffset.IsClosed);
            Assert.Equal(4, clockwiseOpenOffset.Points.Count);
            int i = 0;
            Assert.Equal(-50, clockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(0, clockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(-50, clockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(300, clockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(0, clockwiseOpenOffset.Points[i++].Z.As(u), 12);

            Assert.False(counterclockwiseOpenOffset.IsClosed);
            Assert.Equal(4, counterclockwiseOpenOffset.Points.Count);
            i = 0;
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(0, counterclockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(50, counterclockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(200, counterclockwiseOpenOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(50, counterclockwiseOpenOffset.Points[i].Y.As(u), 12);
            Assert.Equal(0, counterclockwiseOpenOffset.Points[i++].Z.As(u), 12);

            Assert.True(clockwiseClosedOffset.IsClosed);
            Assert.Equal(5, clockwiseClosedOffset.Points.Count);
            i = 0;
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(300, clockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(-50, clockwiseClosedOffset.Points[i++].Z.As(u), 12);

            Assert.True(counterclockwiseClosedOffset.IsClosed);
            Assert.Equal(5, counterclockwiseClosedOffset.Points.Count);
            i = 0;
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(200, counterclockwiseClosedOffset.Points[i++].Z.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i].Y.As(u), 12);
            Assert.Equal(50, counterclockwiseClosedOffset.Points[i++].Z.As(u), 12);
        }
    }
}
