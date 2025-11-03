using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;

namespace GeometryTests.UnitTests
{
    public class MeshTests
    {
        [Fact]
        public void CreateMeshTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();
            m.AddVertex(x, y, z, new Coordinate());

            // Assert
            Assert.Single(m.Verticies);
            TestUtility.TestLengthsAreEqual(x, m.Verticies[0].X);
            TestUtility.TestLengthsAreEqual(y, m.Verticies[0].Y);
            TestUtility.TestLengthsAreEqual(z, m.Verticies[0].Z);
        }

        [Fact]
        public void MeshSurvivesJsonRoundtripTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();
            m.AddVertex(x, y, z, new Coordinate());
            string json = m.ToJson();
            IMesh meshDeserialized = json.FromJson<Mesh>();

            // Assert
            Assert.Single(m.Verticies);
            TestUtility.TestLengthsAreEqual(x, meshDeserialized.Verticies[0].X);
            TestUtility.TestLengthsAreEqual(y, meshDeserialized.Verticies[0].Y);
            TestUtility.TestLengthsAreEqual(z, meshDeserialized.Verticies[0].Z);
        }
    }
}
