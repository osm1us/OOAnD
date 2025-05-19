using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class ShapeIdentifierTests
    {
        [Fact]
        public void GetShapeId_ValidShape_ReturnsHashCode()
        {
            var shapeIdentifier = new ShapeIdentifier();
            var shapeName = "square";

            var expectedHashCode = "square".ToLower().GetHashCode();

            var result = shapeIdentifier.GetShapeId(shapeName);

            Assert.Equal(expectedHashCode, result);
        }

        [Fact]
        public void GetShapeId_EmptyString_ReturnsZero()
        {
            var shapeIdentifier = new ShapeIdentifier();

            var result = shapeIdentifier.GetShapeId(string.Empty);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetShapeId_Null_ReturnsZero()
        {
            var shapeIdentifier = new ShapeIdentifier();

            var result = shapeIdentifier.GetShapeId(null);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetShapeId_SameShape_ReturnsSameHashCode()
        {
            var shapeIdentifier = new ShapeIdentifier();
            var shapeName1 = "circle";
            var shapeName2 = "circle";

            var result1 = shapeIdentifier.GetShapeId(shapeName1);
            var result2 = shapeIdentifier.GetShapeId(shapeName2);

            Assert.Equal(result1, result2);
        }

        [Fact]
        public void GetShapeId_DifferentCase_ReturnsSameHashCode()
        {
            var shapeIdentifier = new ShapeIdentifier();
            var shapeName1 = "triangle";
            var shapeName2 = "TRIANGLE";

            var result1 = shapeIdentifier.GetShapeId(shapeName1);
            var result2 = shapeIdentifier.GetShapeId(shapeName2);

            Assert.Equal(result1, result2);
        }
    }
}
