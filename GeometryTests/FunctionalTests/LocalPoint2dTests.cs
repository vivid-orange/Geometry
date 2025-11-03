using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;

namespace GeometryTests.FunctionalTests
{
    public class LocalLocalPoint2dTests
    {
        [Fact]
        public void GetPolylineAreaTest()
        {
            // Assemble
            var pt1 = new LocalPoint2d(0, 0, LengthUnit.Millimeter);
            var pt2 = new LocalPoint2d(400, 0, LengthUnit.Centimeter);
            var pt3 = new LocalPoint2d(4, 4, LengthUnit.Meter);
            var pt4 = new LocalPoint2d(0, 4, LengthUnit.Meter);

            // Act
            var list = new List<ILocalPoint2d> { pt1, pt2, pt3, pt4 };
            Area area = LocalPoint2d.GetPolylineArea(list);

            // Assert
            Assert.Equal(AreaUnit.SquareMillimeter, area.Unit);
            Assert.Equal(4 * 4, area.SquareMeters);
        }

        [Fact]
        public void PointsAreEqualTest()
        {
            // Assemble
            var y1 = new Length(2.3, LengthUnit.Centimeter);
            var z1 = new Length(5.4, LengthUnit.Centimeter);
            var y2 = new Length(2.3, LengthUnit.Centimeter);
            var z2 = new Length(5.4, LengthUnit.Centimeter);
            var y3 = new Length(666, LengthUnit.Centimeter);
            var z3 = new Length(777, LengthUnit.Centimeter);

            // Act
            var pt1 = new LocalPoint2d(y1, z1);
            var pt2 = new LocalPoint2d(y2, z2);
            var pt3 = new LocalPoint2d(y3, z3);

            // Assert
            Assert.True(pt1.Equals(pt2));
            Assert.False(pt1.Equals(pt3));
        }
    }
}
