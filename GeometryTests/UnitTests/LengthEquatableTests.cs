using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry.Extensions;

namespace GeometryTests.UnitTests
{
    public class LengthEquatableTests
    {
        [Fact]
        public void IsEqualWithoutToleranceTest()
        {
            // Assemble
            Length length = new Length(5.1, LengthUnit.Meter);
            Length other = new Length(510, LengthUnit.Centimeter);

            // Act & Assert
            Assert.True(length.IsEqual(other));
        }

        [Fact]
        public void IsNotEqualWithoutToleranceTest()
        {
            // Assemble
            Length length = new Length(5.1, LengthUnit.Meter);
            Length other = new Length(511, LengthUnit.Centimeter);

            // Act & Assert
            Assert.False(length.IsEqual(other));
        }

        [Fact]
        public void IsEqualWithToleranceTest()
        {
            // Assemble
            Length length = new Length(5.1, LengthUnit.Meter);
            Length other = new Length(5.11, LengthUnit.Meter);

            // Act & Assert
            Assert.False(length.IsEqual(other, 0.01));
            Assert.True(length.IsEqual(other, 0.02));
        }

        [Fact]
        public void IsNotEqualWithToleranceTest()
        {
            // Assemble
            Length length = new Length(5.1, LengthUnit.Meter);
            Length other = new Length(5.11, LengthUnit.Meter);

            // Act & Assert
            Assert.False(length.IsEqual(other, 0.001));
        }

        [Fact]
        public void IsEqualListWithoutToleranceTest()
        {
            // Assemble
            Length length1 = new Length(5.1, LengthUnit.Meter);
            Length length2 = new Length(5.2, LengthUnit.Meter);
            Length other1 = new Length(510, LengthUnit.Centimeter);
            Length other2 = new Length(520, LengthUnit.Centimeter);

            // Act
            var lengths = new List<Length>()
            {
                length1,
                length2,
            };
            var others = new List<Length>()
            {
                other1,
                other2,
            };

            // Assert
            Assert.True(lengths.IsEqual(others));
        }

        [Fact]
        public void IsNotEqualListWithoutToleranceTest()
        {
            // Assemble
            Length length1 = new Length(5.1, LengthUnit.Meter);
            Length length2 = new Length(5.2, LengthUnit.Meter);
            Length other1 = new Length(520, LengthUnit.Centimeter);
            Length other2 = new Length(510, LengthUnit.Centimeter);

            // Act
            var lengths = new List<Length>()
            {
                length1,
                length2,
            };
            var others = new List<Length>()
            {
                other1,
                other2,
            };

            // Assert
            Assert.False(lengths.IsEqual(others));
        }

        [Fact]
        public void IsEqualListWithToleranceTest()
        {
            // Assemble
            Length length1 = new Length(5.11, LengthUnit.Meter);
            Length length2 = new Length(5.21, LengthUnit.Meter);
            Length other1 = new Length(510, LengthUnit.Centimeter);
            Length other2 = new Length(520, LengthUnit.Centimeter);

            // Act
            var lengths = new List<Length>()
            {
                length1,
                length2,
            };
            var others = new List<Length>()
            {
                other1,
                other2,
            };

            // Assert
            Assert.True(lengths.IsEqual(others, 0.01));
        }

        [Fact]
        public void IsNotEqualListWithToleranceTest()
        {
            // Assemble
            Length length1 = new Length(5.11, LengthUnit.Meter);
            Length length2 = new Length(5.21, LengthUnit.Meter);
            Length other1 = new Length(510, LengthUnit.Centimeter);
            Length other2 = new Length(520, LengthUnit.Centimeter);

            // Act
            var lengths = new List<Length>()
            {
                length1,
                length2,
            };
            var others = new List<Length>()
            {
                other1,
                other2,
            };

            // Assert
            Assert.False(lengths.IsEqual(others, 0.001));
        }

        [Fact]
        public void IsEqualListUnequalLengthTest()
        {
            // Assemble
            Length length1 = new Length(5.11, LengthUnit.Meter);
            Length length2 = new Length(5.21, LengthUnit.Meter);
            Length other1 = new Length(510, LengthUnit.Centimeter);

            // Act
            var lengths = new List<Length>()
            {
                length1,
                length2,
            };
            var others = new List<Length>()
            {
                other1,
            };

            // Assert
            Assert.False(lengths.IsEqual(others));
        }
    }
}
